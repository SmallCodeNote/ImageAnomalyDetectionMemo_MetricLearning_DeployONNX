using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

using OpenCvSharp;
using WinFormStringCnvClass;


namespace DeployONNX
{
    public partial class Form1 : Form
    {
        string thisExeDirPath;
        public Form1()
        {
            InitializeComponent();
            thisExeDirPath = Path.GetDirectoryName(Application.ExecutablePath);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TEXT|*.txt";
            if (false && ofd.ShowDialog() == DialogResult.OK)
            {
                WinFormStringCnv.setControlFromString(this, File.ReadAllText(ofd.FileName));
            }
            else
            {
                string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");
                if (File.Exists(paramFilename))
                {
                    WinFormStringCnv.setControlFromString(this, File.ReadAllText(paramFilename));
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string FormContents = WinFormStringCnv.ToString(this);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TEXT|*.txt";

            if (false && sfd.ShowDialog() == DialogResult.OK)
            {

                File.WriteAllText(sfd.FileName, FormContents);
            }
            else
            {
                string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");
                File.WriteAllText(paramFilename, FormContents);
            }

        }

        private void button_CreateTestImage_Run_Click(object sender, EventArgs e)
        {
            string topDirectoryPath = textBox_CreateTestImage_TargetDir.Text;
            if (!Directory.Exists(topDirectoryPath)) Directory.CreateDirectory(topDirectoryPath);
            if (!Directory.Exists(topDirectoryPath)) return;

            string imageSizeString = textBox_CreateTestImage_Size.Text;
            float defSize = 20.0f;

            int imgCount = int.Parse(textBox_CreateTestImage_imgCount.Text);

            string ext = ".png";

            createTestImage_Line(Path.Combine(topDirectoryPath, "00", "OK" + ext), imageSizeString, imgCount, 0);
            createTestImage_Line(Path.Combine(topDirectoryPath, "01", "NG1" + ext), imageSizeString, imgCount / 3, 1);
            createTestImage_Line(Path.Combine(topDirectoryPath, "01", "NG2" + ext), imageSizeString, imgCount / 3, 2);
            createTestImage_Line(Path.Combine(topDirectoryPath, "01", "NG3" + ext), imageSizeString, imgCount / 3, 3);
            createTestImage_NoizeDot(Path.Combine(topDirectoryPath, "02", "BK" + ext), imageSizeString, imgCount, 16);

        }

        private void createTestImage_NoizeDot(string filename, string imageSize, int imgCount, int dotCount)
        {
            string topDirectoryPath = Path.GetDirectoryName(filename);
            if (!Directory.Exists(topDirectoryPath)) Directory.CreateDirectory(topDirectoryPath);

            string[] sizeArray = imageSize.Trim(',').Split(',');

            int imgWidth = int.Parse(sizeArray[0]);
            int imgHeight = int.Parse(sizeArray[1]);

            Random rnd = new Random();

            for (int imgIndex = 0; imgIndex < imgCount; imgIndex++)
            {
                // 画像の生成
                using (Mat img = new Mat(224, 224, MatType.CV_8UC1, Scalar.Black))
                {
                    for (int i = 0; i < dotCount; i++)
                    {
                        double cx = (float)(imgWidth / 2.0 + (rnd.NextDouble() - 0.5) * imgWidth / 2.0);
                        double cy = (float)(imgHeight / 2.0 + (rnd.NextDouble() - 0.5) * imgHeight / 2.0);
                        OpenCvSharp.Point p0 = new OpenCvSharp.Point(cx, cy);

                        double b = rnd.NextDouble() * 128.0 + 127.0;
                        Scalar scalar = new Scalar(b, b, b);

                        int dotDiameter = (int)((rnd.NextDouble()) * imgWidth / 8.0 + imgWidth / 8.0);

                        img.Circle(p0, dotDiameter / 2, scalar, -1);

                    }

                    // 画像の保存
                    string saveFilename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + imgIndex.ToString("0000") + Path.GetExtension(filename));
                    img.SaveImage(saveFilename);

                }

            }

        }

        private void createTestImage_Line(string filename, string imageSize, int imgCount, int dotCount)
        {
            string topDirectoryPath = Path.GetDirectoryName(filename);
            if (!Directory.Exists(topDirectoryPath)) Directory.CreateDirectory(topDirectoryPath);

            string[] sizeArray = imageSize.Trim(',').Split(',');

            int imgWidth = int.Parse(sizeArray[0]);
            int imgHeight = int.Parse(sizeArray[1]);

            Random rnd = new Random();

            for (int imgIndex = 0; imgIndex < imgCount; imgIndex++)
            {
                // 画像の生成
                using (Mat img = new Mat(224, 224, MatType.CV_8UC1, Scalar.Black))
                {
                    double cx = (float)(imgWidth / 2.0 + (rnd.NextDouble() - 0.5) * imgWidth / 4.0);
                    double cy = (float)(imgHeight / 2.0 + (rnd.NextDouble() - 0.5) * imgHeight / 4.0);
                    OpenCvSharp.Point p0 = new OpenCvSharp.Point(cx, cy);

                    double lineLength = (float)(imgWidth * (0.25 * rnd.NextDouble() + 0.5));
                    double angle = rnd.NextDouble() * 180.0;

                    int thickness = 3;

                    // 白線の描画
                    OpenCvSharp.Point p1 = new OpenCvSharp.Point(p0.X - lineLength * 0.5 * Math.Cos(angle * Math.PI / 180), p0.Y - lineLength * 0.5 * Math.Sin(angle * Math.PI / 180));
                    OpenCvSharp.Point p2 = new OpenCvSharp.Point(p1.X + lineLength * Math.Cos(angle * Math.PI / 180), p1.Y + lineLength * Math.Sin(angle * Math.PI / 180));
                    img.Line(p1, p2, Scalar.White, thickness);

                    if (dotCount > 0)
                    {

                        // 黒い塗りつぶされた円の描画
                        double dx = (p2.X - p1.X) / (double)(dotCount + 1.0);
                        double dy = (p2.Y - p1.Y) / (double)(dotCount + 1.0);
                        for (int i = 1; i <= dotCount; i++)
                        {
                            int dotDiameter = (int)(10 * rnd.NextDouble());
                            if (dotDiameter < thickness * 4) dotDiameter = thickness * 4;

                            OpenCvSharp.Point center = new OpenCvSharp.Point(p1.X + (double)(i) * dx, p1.Y + (double)(i) * dy);
                            //center = new OpenCvSharp.Point(p0.X, p0.Y);

                            img.Circle(center, dotDiameter / 2, Scalar.Black, -1);
                        }
                    }


                    // 画像の保存
                    string saveFilename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + imgIndex.ToString("0000") + Path.GetExtension(filename));
                    img.SaveImage(saveFilename);

                }

            }

        }

        private void createTestImage_Dot(string filename, string imageSize, float dotDiameter, int imgCount, Brush brush)
        {
            string topDirectoryPath = Path.GetDirectoryName(filename);
            if (!Directory.Exists(topDirectoryPath)) Directory.CreateDirectory(topDirectoryPath);

            string[] sizeArray = imageSize.Trim(',').Split(',');

            int imgWidth = int.Parse(sizeArray[0]);
            int imgHeight = int.Parse(sizeArray[1]);

            Random rnd = new Random();

            for (int imgIndex = 0; imgIndex < imgCount; imgIndex++)
            {
                using (Bitmap img = new Bitmap(imgWidth, imgHeight, PixelFormat.Format24bppRgb))
                {

                    float cx = (float)(imgWidth / 2.0 + (rnd.NextDouble() - 0.5) * imgWidth / 2.0);
                    float cy = (float)(imgHeight / 2.0 + (rnd.NextDouble() - 0.5) * imgHeight / 2.0);
                    float d = (float)(dotDiameter + dotDiameter * 0.5 * (rnd.NextDouble() * 2.0 - 1.0));

                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.FillRectangle(Brushes.White, 0, 0, imgWidth, imgHeight);
                        g.FillEllipse(brush, cx - d / 2.0f, cy - d / 2.0f, d / 2.0f, d / 2.0f);

                    }

                    string saveFilename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + imgIndex.ToString("0000") + Path.GetExtension(filename));
                    img.Save(saveFilename, ImageFormat.Jpeg);

                }

            }

        }

        private void createTestImage_Flagment(string filename, string imageSize, float dotDiameter, int imgCount, Brush brush)
        {
            string topDirectoryPath = Path.GetDirectoryName(filename);
            if (!Directory.Exists(topDirectoryPath)) Directory.CreateDirectory(topDirectoryPath);

            string[] sizeArray = imageSize.Trim(',').Split(',');

            int imgWidth = int.Parse(sizeArray[0]);
            int imgHeight = int.Parse(sizeArray[1]);

            Random rnd = new Random();

            for (int imgIndex = 0; imgIndex < imgCount; imgIndex++)
            {
                using (Bitmap img = new Bitmap(imgWidth, imgHeight, PixelFormat.Format24bppRgb))
                {

                    float cx = (float)(imgWidth / 2.0 + (rnd.NextDouble() - 0.5) * imgWidth / 2.0);
                    float cy = (float)(imgHeight / 2.0 + (rnd.NextDouble() - 0.5) * imgHeight / 2.0);

                    List<System.Drawing.Point> points = new List<System.Drawing.Point>();

                    for (int corIndex = 0; corIndex < 3; corIndex++)
                    {
                        float dx = (float)(dotDiameter + dotDiameter * 0.5 * (rnd.NextDouble() * 2.0 - 1.0));
                        float dy = (float)(dotDiameter + dotDiameter * 0.5 * (rnd.NextDouble() * 2.0 - 1.0));
                        points.Add(new System.Drawing.Point((int)(cx - dx / 2.0f), (int)(cy - dy / 2.0f)));
                    }

                    double areaCount = CalculateTriangleArea(points);
                    if (areaCount <= dotDiameter * 1.5f)
                    {
                        imgIndex--;
                    }
                    else
                    {
                        using (Graphics g = Graphics.FromImage(img))
                        {
                            g.FillRectangle(Brushes.White, 0, 0, imgWidth, imgHeight);
                            g.FillPolygon(brush, points.ToArray());

                        }

                        string saveFilename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + imgIndex.ToString("0000") + Path.GetExtension(filename));
                        img.Save(saveFilename);

                    }

                }

            }

        }

        double CalculateTriangleArea(List<System.Drawing.Point> points)
        {
            if (points.Count != 3)
            {
                throw new ArgumentException("Exactly 3 points are required to calculate the area of a triangle.");
            }

            double area = Math.Abs((points[0].X * (points[1].Y - points[2].Y) +
                                    points[1].X * (points[2].Y - points[0].Y) +
                                    points[2].X * (points[0].Y - points[1].Y)) / 2.0);
            return area;
        }

        private void button_LoadOnnxFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ONNX|*.onnx";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            string modelPath = ofd.FileName;
            textBox_LoadOnnx224Filename.Text = modelPath;

            List<string> Lines = new List<string>();

            using (var session = new InferenceSession(modelPath))
            {
                Lines.Add($"ProducerName: {session.ModelMetadata.ProducerName}");
                Lines.Add($"Version: {session.ModelMetadata.Version.ToString()}");
                Lines.Add($"Description: {session.ModelMetadata.Description}");

                Lines.Add("//Input//");
                foreach (var input in session.InputMetadata)
                {
                    Lines.Add($" Key: {input.Key}, \r\n Value.ElementType: {input.Value.ElementType},\r\n Value.Dimensions: {string.Join(",", input.Value.Dimensions)}");
                    int[] Dimensions = input.Value.Dimensions.ToArray();

                    textBox_LoadOnnxFileInfo_Size.Text = Dimensions[2].ToString() + "," + Dimensions[3].ToString();
                }

                Lines.Add("//Output//");
                foreach (var output in session.OutputMetadata)
                {
                    Lines.Add($" Key: {output.Key},\r\n Value.ElementType: {output.Value.ElementType},\r\n Value.Dimensions: {string.Join(",", output.Value.Dimensions)}");
                }

            };

            textBox_LoadOnnxFileInfo.Text = string.Join("\r\n", Lines.ToArray());

        }

        private void button_ONNX_Prediction224_Click(object sender, EventArgs e)
        {
            string modelPath = textBox_LoadOnnx224Filename.Text;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG|*.png";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            string imagePath = ofd.FileName;

            try
            {
                using (var session = new InferenceSession(modelPath))
                {
                    Console.WriteLine("モデルの読み込みに成功しました。");
                    float[] imageData = LoadImageData(imagePath);
                    Memory<float> memory = new Memory<float>(imageData);

                    ReadOnlySpan<int> dimensions = new int[] { 1, 3, 224, 224 };

                    // DenseTensorを作成します。
                    DenseTensor<float> inputTensor = new DenseTensor<float>(memory, dimensions);

                    var inputs = new NamedOnnxValue[] { NamedOnnxValue.CreateFromTensor("data_0", inputTensor) };

                    // 推論の実行
                    using (var results = session.Run(inputs))
                    {
                        // 出力データの取得
                        var outputData = results.FirstOrDefault(item => item.Name == "prob_1").AsTensor<float>().ToArray();
                        Console.WriteLine($"出力データ: {string.Join(", ", outputData)}");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"推論の実行中にエラーが発生しました: {ex.Message}");
            }
        }


        public float[] LoadImageData(string imagePath)
        {
            // 画像を読み込みます。
            Bitmap bitmap = new Bitmap(imagePath);

            // 画像を224x224にリサイズします。
            Bitmap resizedBitmap = new Bitmap(bitmap, new System.Drawing.Size(224, 224));

            // RGBチャンネル用の配列を作成します。
            float[] imageData = new float[3 * 224 * 224];

            // Bitmapをロックしてピクセルデータにアクセスします。
            BitmapData bitmapData = resizedBitmap.LockBits(new Rectangle(0, 0, resizedBitmap.Width, resizedBitmap.Height), ImageLockMode.ReadOnly, resizedBitmap.PixelFormat);

            // ピクセルデータをbyte配列にコピーします。
            byte[] pixelData = new byte[bitmapData.Stride * bitmapData.Height];
            System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, pixelData, 0, pixelData.Length);

            // Bitmapのロックを解除します。
            resizedBitmap.UnlockBits(bitmapData);

            // ピクセルデータをfloat配列に変換します。
            for (int i = 0; i < pixelData.Length; i += 3)
            {
                // BitmapデータはBGR形式なので、RGB形式に変換します。
                imageData[i / 3] = pixelData[i + 2] / 255.0f; // R
                imageData[i / 3 + 1] = pixelData[i + 1] / 255.0f; // G
                imageData[i / 3 + 2] = pixelData[i] / 255.0f; // B
            }

            return imageData;
        }

        private void setPictureBox(PictureBox p, Bitmap image)
        {
            if (p.Image != null) p.Image.Dispose();

            p.Image = image;

        }


        private void button_CreateFileList_Click(object sender, EventArgs e)
        {

            string targetDir = textBox_CreateFileListPath_TargetDir.Text;

            if (Directory.Exists(targetDir))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV|*.csv";
                sfd.FileName = textBox_CreateFileList.Text;
                if (sfd.ShowDialog() != DialogResult.OK) return;


                string[] FileList = Directory.GetFiles(targetDir, "*.png", SearchOption.AllDirectories);
                List<string> Lines = new List<string>();

                foreach (var filename in FileList)
                {
                    string className = Path.GetFileName(Path.GetDirectoryName(filename));
                    Lines.Add(filename + "," + className);

                }

                File.WriteAllText(sfd.FileName, string.Join("\r\n", Lines.ToArray()));

                textBox_CreateFileList.Text = sfd.FileName;
            }
        }

        private void button_ONNX_Prediction2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG|*.png";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            setPictureBox(pictureBox1, new Bitmap(ofd.FileName));

            var mlContext = new MLContext();

            List<InputData224> inputDatas = new List<InputData224>();
            inputDatas.Add(new InputData224(ofd.FileName));

            var data = mlContext.Data.LoadFromEnumerable(inputDatas);

            var pipeline = mlContext.Transforms.ApplyOnnxModel(textBox_LoadOnnx224Filename.Text);

            var transformedData = pipeline.Fit(data).Transform(data);

            var predictions = mlContext.Data.CreateEnumerable<ImageNetPrediction>(transformedData, reuseRowObject: false).ToList();


            // チャートエリアの追加
            chart_Prediction.ChartAreas.Clear();
            chart_Prediction.ChartAreas.Add(new ChartArea("Area1"));

            // シリーズの追加
            Series series = new Series("Series1");
            series.ChartType = SeriesChartType.Column; // 折れ線グラフから棒グラフに変更
            chart_Prediction.Series.Clear();
            chart_Prediction.Series.Add(series);

            // データの追加
            for (int i = 0; i < predictions[0].Features.Length; i++)
            {
                series.Points.AddY(predictions[0].Features[i]);
            }

            chart_Prediction.Update();

        }
        private void button_ONNX_Prediction28_Click(object sender, EventArgs e)
        {
        }

        int ONNX_Prediction28Count = 0;
        private void button_ONNX_Prediction_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG|*.png";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() != DialogResult.OK) return;

            setPictureBox(pictureBox1, new Bitmap(ofd.FileName));

            var mlContext = new MLContext();

            string onnxFilename = "";

            //List<IInputData> inputDatas = new List<IInputData>();
            IDataView data;

            if (sender == button_ONNX_Prediction224)
            {
                onnxFilename = textBox_LoadOnnx224Filename.Text.Replace("\"", "");

                List<InputData224> inputDatas = new List<InputData224>();

                foreach (var filename in ofd.FileNames)
                {
                    inputDatas.Add(new InputData224(filename));
                }
                data = mlContext.Data.LoadFromEnumerable(inputDatas);

            }
            else if (sender == button_ONNX_Prediction28)
            {
                onnxFilename = textBox_LoadOnnx28Filename.Text.Replace("\"", "");

                List<InputData28> inputDatas = new List<InputData28>();
                foreach (var filename in ofd.FileNames)
                {
                    inputDatas.Add(new InputData28(filename));
                }
                data = mlContext.Data.LoadFromEnumerable(inputDatas);

            }
            else
            {
                return;
            }

            var pipeline = mlContext.Transforms.ApplyOnnxModel(onnxFilename);
            var transformedData = pipeline.Fit(data).Transform(data);
            var predictions = mlContext.Data.CreateEnumerable<ImageNetPrediction>(transformedData, reuseRowObject: false).ToList();

            // チャートエリアの追加
            chart_Prediction.ChartAreas.Clear();
            chart_Prediction.ChartAreas.Add(new ChartArea("Area1"));

            chart_Prediction.Series.Clear();

            List<double> distanceList = new List<double>();

            foreach (var prediction in predictions)
            {
                // シリーズの追加
                Series series = new Series("Series" + ONNX_Prediction28Count.ToString());
                series.ChartType = SeriesChartType.Column; // 折れ線グラフから棒グラフに変更
                chart_Prediction.Series.Add(series);

                // データの追加
                for (int i = 0; i < prediction.Features.Length; i++)
                {
                    series.Points.AddY(prediction.Features[i]);
                }

                distanceList.Add(distance(prediction.Features, predictions[0].Features));

                ONNX_Prediction28Count++;
            }

            chart_Prediction.Update();


            {

                // チャートエリアの追加
                chart_PredictionDistance.ChartAreas.Clear();
                chart_PredictionDistance.ChartAreas.Add(new ChartArea("Area1"));

                chart_PredictionDistance.Series.Clear();

                // シリーズの追加
                Series series = new Series("Series");
                series.ChartType = SeriesChartType.Column; // 折れ線グラフから棒グラフに変更
                chart_PredictionDistance.Series.Add(series);


                // データの追加
                for (int i = 0; i < distanceList.Count; i++)
                {
                    series.Points.AddY(distanceList[i]);
                }

            }


            chart_PredictionDistance.Update();

        }


        private double distance(float[] a, float[] b)
        {
            double r = 0;
            for (int i = 0; i < a.Length; i++)
            {
                r += Math.Pow(a[i] - b[i], 2);
            }

            return Math.Sqrt(r);
        }

        private float[] getAverageFloatArray(List<float[]> listFloatArray, int num, int startIndex = 0)
        {
            int a = startIndex;
            int b = a + num;
            int length = listFloatArray[0].Length;
            return Enumerable.Range(0, length).Select(j => listFloatArray.Skip(a).Take(b - a + 1).Average(arr => arr[j])).ToArray();
        }



        private void button_ONNX_Prediction224List_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string onnxFilename = "";

            if (sender == button_ONNX_Prediction224)
            {
                onnxFilename = textBox_LoadOnnx224Filename.Text;
            }
            else if (sender == button_ONNX_Prediction28)
            {
                onnxFilename = textBox_LoadOnnx28Filename.Text;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG|*.png";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var mlContext = new MLContext();

            List<InputData28> inputDatas = new List<InputData28>();
            inputDatas.Add(new InputData28(ofd.FileName));

            var data = mlContext.Data.LoadFromEnumerable(inputDatas);

            var pipeline = mlContext.Transforms.ApplyOnnxModel(textBox_LoadOnnx224Filename.Text);

            var transformedData = pipeline.Fit(data).Transform(data);

            var predictions = mlContext.Data.CreateEnumerable<ImageNetPrediction>(transformedData, reuseRowObject: false).ToList();

            // チャートエリアの追加
            chart_Prediction.ChartAreas.Clear();
            chart_Prediction.ChartAreas.Add(new ChartArea("Area1"));

            // シリーズの追加
            Series series = new Series("Series" + ONNX_Prediction28Count.ToString());
            series.ChartType = SeriesChartType.Column; // 折れ線グラフから棒グラフに変更
            chart_Prediction.Series.Clear();
            chart_Prediction.Series.Add(series);

            // データの追加
            for (int i = 0; i < predictions[0].Features.Length; i++)
            {
                series.Points.AddY(predictions[0].Features[i]);
            }

            chart_Prediction.Update();

            ONNX_Prediction28Count++;

        }

        private void button_Prediction_Run_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG|*.png";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var mlContext = new MLContext();

            string onnxFilename = "";
            IDataView data;

            onnxFilename = textBox_Prediction_OnnxFilePath.Text.Replace("\"", "");

            List<InputData224> inputDatas = new List<InputData224>();
            List<string> inputFilePathList = new List<string>();


            foreach (var filename in Directory.GetFiles(textBox_Prediction_AnchorDataDirectoryPath.Text.Replace("\"", ""), "*.png", SearchOption.AllDirectories))
            {
                inputDatas.Add(new InputData224(filename));
                inputFilePathList.Add(filename);
            }

            int anchorCount = inputFilePathList.Count;

            foreach (var filename in ofd.FileNames)
            {
                inputDatas.Add(new InputData224(filename));
                inputFilePathList.Add(filename);
            }

            data = mlContext.Data.LoadFromEnumerable(inputDatas);

            var pipeline = mlContext.Transforms.ApplyOnnxModel(onnxFilename);
            var transformedData = pipeline.Fit(data).Transform(data);
            var predictions = mlContext.Data.CreateEnumerable<ImageNetPrediction>(transformedData, reuseRowObject: false).ToList();


            List<float[]> anchorFeatureList = new List<float[]>();
            float[] anchorFeatureAverage = new float[128];
            List<double> distanceList = new List<double>();

            panel_Prediction_Result.Controls.Clear();
            panel_Prediction_Result.Height = 0;

            for (int fileIndex = 0; fileIndex < inputFilePathList.Count; fileIndex++)
            {
                var prediction = predictions[fileIndex];
                var inputFilePath = inputFilePathList[fileIndex];

                if (anchorFeatureList.Count < anchorCount)
                {
                    anchorFeatureList.Add(prediction.Features);
                }
                else
                {
                    double distanceValue = distance(anchorFeatureAverage, prediction.Features);
                    distanceList.Add(distanceValue);
                    var formIns = new ResultForm(new Bitmap(inputFilePath), Path.GetFileNameWithoutExtension(inputFilePath), distanceValue);
                    formIns.Top = panel_Prediction_Result.Height;

                    panel_Prediction_Result.Controls.Add(formIns);
                    panel_Prediction_Result.Height += formIns.Height;


                }

                if (anchorFeatureList.Count == anchorCount && panel_Prediction_Result.Controls.Count==0)
                {
                    anchorFeatureAverage = getAverageFloatArray(anchorFeatureList, anchorCount);
                }

            }

            double distanceValueMax = distanceList.Max()*1.2;



            foreach (var item in panel_Prediction_Result.Controls)
            {
                if (item is ResultForm)
                {
                    ((ResultForm)item).distanceMax = distanceValueMax;
                }
            }

            panel_Prediction_ResultFrame.Update();

        }

    }

    public class ImageNetPrediction
    {

        [VectorType(1, 128)]
        [ColumnName("outputArray")]
        public float[] Features { get; set; }


    }

    public interface IInputData
    {
        [ColumnName("inputimage")]
        float[] Features { get; set; }

        //float[] ConvertImage(Bitmap image);
    }

    public class InputData224 : IInputData
    {
        private int imageSize = 224;

        [VectorType(1, 224, 224, 1)]
        [ColumnName("inputimage")]
        public float[] Features { get; set; }

        public InputData224(string imagePath)
        {
            var bitmap = new Bitmap(Image.FromFile(imagePath));
            Features = ConvertImage(bitmap);
        }

        private float[] ConvertImage(Bitmap image)
        {
            var resized = new Bitmap(image, new System.Drawing.Size(imageSize, imageSize));
            var data = new float[1 * imageSize * imageSize];
            for (int y = 0; y < resized.Height; y++)
            {
                for (int x = 0; x < resized.Width; x++)
                {
                    var color = resized.GetPixel(x, y);
                    data[y * resized.Width + x] = ((float)(color.G));
                    //data[y * resized.Width + x + imageSize * imageSize] = color.G;
                    //data[y * resized.Width + x + 2 * imageSize * imageSize] = color.B;
                }
            }
            return data;
        }

    }


    public class InputData28 : IInputData
    {
        private int imageSize = 28;

        [VectorType(1, 28, 28, 1)]
        [ColumnName("inputimage")]
        public float[] Features { get; set; }

        public InputData28(string imagePath)
        {
            var bitmap = new Bitmap(Image.FromFile(imagePath));
            Features = ConvertImage(bitmap);
        }

        private float[] ConvertImage(Bitmap image)
        {
            var resized = new Bitmap(image, new System.Drawing.Size(imageSize, imageSize));
            var data = new float[1 * imageSize * imageSize];
            for (int y = 0; y < resized.Height; y++)
            {
                for (int x = 0; x < resized.Width; x++)
                {
                    var color = resized.GetPixel(x, y);
                    data[y * resized.Width + x] = ((float)(color.G));

                }
            }
            return data;
        }

    }



}
