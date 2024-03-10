import tensorflow as tf
from keras import layers
from keras.models import Model
import keras.backend as K
from tensorflow.keras.models import load_model
from keras.optimizers import SGD
from keras.applications import MobileNetV2
from keras.callbacks import LearningRateScheduler, ModelCheckpoint

from keras.datasets import fashion_mnist
import numpy as np
from tqdm import tqdm
from sklearn.metrics import euclidean_distances, roc_auc_score

import datetime

def create_mobilenet_v2():
    input = layers.Input((224,224,1),name="inputImage")
    #x = input #layers.UpSampling2D(3)(input)
    model = MobileNetV2(include_top=False, input_tensor=input, weights=None, pooling="avg")
    x = model.layers[-1].output
    x = layers.Dense(128,name="outputArray")(x)
    return Model(input, x)

# OnlineのTriplet選択
def triplet_loss(label, embeddings):
    # バッチ内のユークリッド距離
    x1 = tf.expand_dims(embeddings, axis=0)
    x2 = tf.expand_dims(embeddings, axis=1)
    euclidean = tf.reduce_sum((x1-x2)**2, axis=-1)

    # ラベルが等しいかの行列（labelの次元が128次元になるので[0]だけ取る）
    lb1 = tf.expand_dims(label[:, 0], axis=0)
    lb2 = tf.expand_dims(label[:, 0], axis=1)
    equal_mat = tf.equal(lb1, lb2)

    # positives
    positive_ind = tf.where(equal_mat)
    positive_dists = tf.gather_nd(euclidean, positive_ind)

    # negatives
    negative_ind = tf.where(tf.logical_not(equal_mat))
    negative_dists = tf.gather_nd(euclidean, negative_ind)

    # [P, N]
    positives = tf.expand_dims(positive_dists, axis=1)
    negatives = tf.expand_dims(negative_dists, axis=0)
    triplets = tf.maximum(positives - negatives + 0.2, 0.0) # Margin=0.2
    return tf.reduce_mean(triplets) # sumだと大きなりすぎてinfになるため

def load_data(data_dir, img_height=224, img_width=224, batch_size=16):
    # 訓練データと検証データを読み込む
    train_ds = tf.keras.preprocessing.image_dataset_from_directory(
        data_dir,
        validation_split=0.2,
        color_mode="grayscale",
        subset="training",
        seed=123,
        image_size=(img_height, img_width),
        batch_size=batch_size)

    val_ds = tf.keras.preprocessing.image_dataset_from_directory(
        data_dir,
        validation_split=0.2,
        color_mode="grayscale",
        subset="validation",
        seed=123,
        image_size=(img_height, img_width),
        batch_size=batch_size)

    # データセットから画像とラベルを取得
    X_train, y_train = zip(*[(x.numpy(), y.numpy()) for x, y in train_ds])
    X_test, y_test = zip(*[(x.numpy(), y.numpy()) for x, y in val_ds])

    # numpy配列に変換
    X_train = np.concatenate(X_train)
    y_train = np.concatenate(y_train)
    X_test = np.concatenate(X_test)
    y_test = np.concatenate(y_test)

    # (0)＝正常、(1)＝異常、(2-)＝その他
    # 訓練画像 ＝ 異常以外。
    train_masks = y_train != 1
    X_use_train = (X_train[train_masks]) 
    y_use_train = (y_train[train_masks] == 0).astype(np.float32)

    # アンカー画像 = X_trainの0(正常)
    anchor_masks = y_use_train == 1.0
    X_anchor = X_use_train[anchor_masks] # (OKcount, img_height, img_width, 1)

    # テスト画像 = X_testの0(正常)と1(異常)→これがどのぐらいの精度で分類できるか
    test_masks = np.logical_or(y_test==0, y_test==1)
    X_use_test = X_test[test_masks] # (OKcount, img_height, img_width, 1)
    y_use_test = (y_test[test_masks]==0).astype(np.float32) # (OKcount,)


    return (X_use_train, y_use_train), X_anchor, (X_use_test, y_use_test)

def step_decay(epoch):
    x = 1e-3
    if epoch >= 25: x /= 10.0
    if epoch >= 45: x /= 10.0
    return x


def train_generator(X, y_label, batch_size):
    while True:
        indices = np.random.permutation(X.shape[0])
        for i in range(len(indices)//batch_size):
            current_indices = indices[i*batch_size:(i+1)*batch_size]
            X_batch = X[current_indices] / 255.0
            y_batch = np.zeros((batch_size, 128), np.float32)
            y_batch[:,0] = y_label[current_indices]
            yield X_batch, y_batch

def train():
    (X_train, y_train), X_anchor, (X_test, y_test) = load_data(R"R:\testImg_Line224")
    print(X_train.shape)
    model = create_mobilenet_v2()
    model.compile(SGD(1e-3, 0.9), triplet_loss)

    log_dir = "logs/fit/" + datetime.datetime.now().strftime("%Y%m%d-%H%M%S")
    tensorboard_callback = tf.keras.callbacks.TensorBoard(log_dir=log_dir, histogram_freq=1)




    # 訓練
    scheduler = LearningRateScheduler(step_decay)
    checkpoint = ModelCheckpoint("model224.hdf5", monitor="loss", save_best_only=True, save_weights_only=True)
    batch_size = 128 #128
    epoch_count = 50 #50
    model.fit_generator(train_generator(X_train, y_train, batch_size), steps_per_epoch=X_train.shape[0]//batch_size,
                        callbacks=[checkpoint, scheduler], max_queue_size=1, epochs=epoch_count)

    # ベストのモデルを読み込む
    model.load_weights("model224.hdf5")

    model.save(R'R:\model224',overwrite=True)

    # embeddingを取る
    anchor_embeddings = model.predict(X_anchor/255.0, verbose=1) # 正常だけ
    test_embeddings = model.predict(X_test/255.0, verbose=1) # 正常+異常
    # 距離行列
    dist_matrix = np.zeros((test_embeddings.shape[0], anchor_embeddings.shape[0]), np.float32)
    for i in range(dist_matrix.shape[0]):
        dist_matrix[i,:] = euclidean_distances(test_embeddings[i,:].reshape(1,-1),
                                               anchor_embeddings)[0]
    # 各アンカーに対して最小距離を取る
    min_dist = np.min(dist_matrix, axis=-1)

    # AUCスコア
    # y_testは正常かどうかのフラグ、距離は正常のほうが距離が小さいので、y_testは異常のほうを1とする
    auc = roc_auc_score(1.0-y_test, min_dist)
    print(auc)
    # 0.906577

    # 結果の表示
    plot_result(X_test, y_test, min_dist)

import matplotlib.pyplot as plt

def plot_result(test_images, y_test, dists):
    # スニーカーに似ていないブーツ
    far_boots_ind = np.argsort(dists)[::-1]
    i = 1
    for ind in far_boots_ind:
        if y_test[ind] == 0.0: continue # OKを除外
        ax = plt.subplot(4,4,i)
        ax.imshow(test_images[ind,:,:,0])
        ax.axis("off")
        ax.set_title(f"score={dists[ind]:.04}")
        i += 1
        if i == 9: break
    # NGに酷似
    close_boots_ind = np.argsort(dists)
    for ind in close_boots_ind:
        if y_test[ind] == 0.0: continue # OKを除外
        ax = plt.subplot(4,4,i)
        ax.imshow(test_images[ind,:,:,0])
        ax.axis("off")
        ax.set_title(f"score={dists[ind]:.04}")
        i += 1
        if i == 17: break
    plt.show()

import time

def speed_test(n_anchors):
    model = create_mobilenet_v2()
    model.load_weights("model224.hdf5")
    #model = Model('model')

    (_, _), X_anchor, (X_test, y_test) = load_data()
    X_anchor = X_anchor[:n_anchors]
    anchor_embeddding = model.predict(X_anchor/255.0)

    pred_dist = []
    start_time = time.time()
    for i in range(X_test.shape[0]):
        embedding = model.predict(X_test[i:i+1]/255.0)
        dists = euclidean_distances(embedding, anchor_embeddding)[0]
        pred_dist.append(np.min(dists))
    elapsed = time.time() - start_time

    roc = roc_auc_score(1.0-y_test, np.array(pred_dist))
    print(f"n_anchors={n_anchors}, elapsed={elapsed}, roc={roc}")

def speed_test_with_batch(n_anchors):
    #model = create_mobilenet_v2()
    #model.load_weights("model224.hdf5")
    model = Model('model224')

    (_, _), X_anchor, (X_test, y_test) = load_data()
    X_anchor = X_anchor[:n_anchors]
    anchor_embeddding = model.predict(X_anchor/255.0)

    pred_dist = np.zeros(X_test.shape[0], np.float32)
    start_time = time.time()
    for i in range(X_test.shape[0]//20):
        embedding = model.predict(X_test[i*20:(i+1)*20]/255.0, batch_size=20)
        dists = euclidean_distances(embedding, anchor_embeddding)
        pred_dist[i*20:(i+1)*20] = np.min(dists, axis=-1)
    elapsed = time.time() - start_time

    roc = roc_auc_score(1.0-y_test, pred_dist)
    print(f"n_anchors={n_anchors}, elapsed={elapsed}, roc={roc}, batch20")

if __name__ == "__main__":
    train()
    #for i in [6000, 3000, 1000, 500, 100, 50]:
    #    speed_test_with_batch(i)
