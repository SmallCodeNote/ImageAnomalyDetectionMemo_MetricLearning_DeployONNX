namespace DeployONNX
{
    partial class ResultForm
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_targetImage = new System.Windows.Forms.PictureBox();
            this.panel_bar = new System.Windows.Forms.Panel();
            this.label_Filename = new System.Windows.Forms.Label();
            this.label_Value = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_targetImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_targetImage
            // 
            this.pictureBox_targetImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_targetImage.Name = "pictureBox_targetImage";
            this.pictureBox_targetImage.Size = new System.Drawing.Size(48, 48);
            this.pictureBox_targetImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_targetImage.TabIndex = 0;
            this.pictureBox_targetImage.TabStop = false;
            // 
            // panel_bar
            // 
            this.panel_bar.BackColor = System.Drawing.Color.SteelBlue;
            this.panel_bar.Location = new System.Drawing.Point(50, 31);
            this.panel_bar.Name = "panel_bar";
            this.panel_bar.Size = new System.Drawing.Size(400, 16);
            this.panel_bar.TabIndex = 1;
            // 
            // label_Filename
            // 
            this.label_Filename.AutoSize = true;
            this.label_Filename.Location = new System.Drawing.Point(53, 5);
            this.label_Filename.Name = "label_Filename";
            this.label_Filename.Size = new System.Drawing.Size(11, 12);
            this.label_Filename.TabIndex = 2;
            this.label_Filename.Text = "...";
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Location = new System.Drawing.Point(52, 17);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(11, 12);
            this.label_Value.TabIndex = 2;
            this.label_Value.Text = "...";
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Value);
            this.Controls.Add(this.label_Filename);
            this.Controls.Add(this.panel_bar);
            this.Controls.Add(this.pictureBox_targetImage);
            this.Name = "ResultForm";
            this.Size = new System.Drawing.Size(450, 50);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_targetImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_targetImage;
        private System.Windows.Forms.Panel panel_bar;
        private System.Windows.Forms.Label label_Filename;
        private System.Windows.Forms.Label label_Value;
    }
}
