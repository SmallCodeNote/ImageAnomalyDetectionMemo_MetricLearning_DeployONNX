namespace DeployONNX
{
    partial class Form1
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_ONNX = new System.Windows.Forms.TabPage();
            this.chart_Prediction = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_LoadOnnxFileInfo = new System.Windows.Forms.TextBox();
            this.textBox_LoadOnnxFileInfo_Size = new System.Windows.Forms.TextBox();
            this.button_ONNX_Prediction28List = new System.Windows.Forms.Button();
            this.button_ONNX_Prediction224List = new System.Windows.Forms.Button();
            this.button_ONNX_Prediction28 = new System.Windows.Forms.Button();
            this.button_ONNX_Prediction224 = new System.Windows.Forms.Button();
            this.button_LoadOnnxFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_LoadOnnx28Filename = new System.Windows.Forms.TextBox();
            this.textBox_LoadOnnx224Filename = new System.Windows.Forms.TextBox();
            this.tabPage_CreateTestImage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_CreateTestImage_Size = new System.Windows.Forms.TextBox();
            this.button_CreateTestImage_Run = new System.Windows.Forms.Button();
            this.textBox_CreateTestImage_imgCount = new System.Windows.Forms.TextBox();
            this.textBox_CreateTestImage_TargetDir = new System.Windows.Forms.TextBox();
            this.tabPage_CreateFileList = new System.Windows.Forms.TabPage();
            this.textBox_CreateFileList = new System.Windows.Forms.TextBox();
            this.textBox_CreateFileListPath_TargetDir = new System.Windows.Forms.TextBox();
            this.button_CreateFileList = new System.Windows.Forms.Button();
            this.chart_PredictionDistance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1.SuspendLayout();
            this.tabPage_ONNX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Prediction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage_CreateTestImage.SuspendLayout();
            this.tabPage_CreateFileList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_PredictionDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_ONNX);
            this.tabControl1.Controls.Add(this.tabPage_CreateTestImage);
            this.tabControl1.Controls.Add(this.tabPage_CreateFileList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1172, 658);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_ONNX
            // 
            this.tabPage_ONNX.Controls.Add(this.chart_PredictionDistance);
            this.tabPage_ONNX.Controls.Add(this.chart_Prediction);
            this.tabPage_ONNX.Controls.Add(this.pictureBox2);
            this.tabPage_ONNX.Controls.Add(this.pictureBox1);
            this.tabPage_ONNX.Controls.Add(this.textBox_LoadOnnxFileInfo);
            this.tabPage_ONNX.Controls.Add(this.textBox_LoadOnnxFileInfo_Size);
            this.tabPage_ONNX.Controls.Add(this.button_ONNX_Prediction28List);
            this.tabPage_ONNX.Controls.Add(this.button_ONNX_Prediction224List);
            this.tabPage_ONNX.Controls.Add(this.button_ONNX_Prediction28);
            this.tabPage_ONNX.Controls.Add(this.button_ONNX_Prediction224);
            this.tabPage_ONNX.Controls.Add(this.button_LoadOnnxFile);
            this.tabPage_ONNX.Controls.Add(this.label1);
            this.tabPage_ONNX.Controls.Add(this.textBox_LoadOnnx28Filename);
            this.tabPage_ONNX.Controls.Add(this.textBox_LoadOnnx224Filename);
            this.tabPage_ONNX.Location = new System.Drawing.Point(4, 22);
            this.tabPage_ONNX.Name = "tabPage_ONNX";
            this.tabPage_ONNX.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_ONNX.Size = new System.Drawing.Size(1164, 632);
            this.tabPage_ONNX.TabIndex = 0;
            this.tabPage_ONNX.Text = "ONNX";
            this.tabPage_ONNX.UseVisualStyleBackColor = true;
            // 
            // chart_Prediction
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_Prediction.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_Prediction.Legends.Add(legend2);
            this.chart_Prediction.Location = new System.Drawing.Point(13, 366);
            this.chart_Prediction.Name = "chart_Prediction";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_Prediction.Series.Add(series2);
            this.chart_Prediction.Size = new System.Drawing.Size(1143, 243);
            this.chart_Prediction.TabIndex = 6;
            this.chart_Prediction.Text = "chart1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(497, 120);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(224, 224);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(257, 120);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(224, 224);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // textBox_LoadOnnxFileInfo
            // 
            this.textBox_LoadOnnxFileInfo.Location = new System.Drawing.Point(13, 157);
            this.textBox_LoadOnnxFileInfo.Multiline = true;
            this.textBox_LoadOnnxFileInfo.Name = "textBox_LoadOnnxFileInfo";
            this.textBox_LoadOnnxFileInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_LoadOnnxFileInfo.Size = new System.Drawing.Size(213, 187);
            this.textBox_LoadOnnxFileInfo.TabIndex = 4;
            this.textBox_LoadOnnxFileInfo.WordWrap = false;
            // 
            // textBox_LoadOnnxFileInfo_Size
            // 
            this.textBox_LoadOnnxFileInfo_Size.Location = new System.Drawing.Point(126, 130);
            this.textBox_LoadOnnxFileInfo_Size.Name = "textBox_LoadOnnxFileInfo_Size";
            this.textBox_LoadOnnxFileInfo_Size.Size = new System.Drawing.Size(100, 19);
            this.textBox_LoadOnnxFileInfo_Size.TabIndex = 3;
            // 
            // button_ONNX_Prediction28List
            // 
            this.button_ONNX_Prediction28List.Location = new System.Drawing.Point(637, 53);
            this.button_ONNX_Prediction28List.Name = "button_ONNX_Prediction28List";
            this.button_ONNX_Prediction28List.Size = new System.Drawing.Size(113, 23);
            this.button_ONNX_Prediction28List.TabIndex = 2;
            this.button_ONNX_Prediction28List.Text = "Prediction28List";
            this.button_ONNX_Prediction28List.UseVisualStyleBackColor = true;
            // 
            // button_ONNX_Prediction224List
            // 
            this.button_ONNX_Prediction224List.Location = new System.Drawing.Point(637, 24);
            this.button_ONNX_Prediction224List.Name = "button_ONNX_Prediction224List";
            this.button_ONNX_Prediction224List.Size = new System.Drawing.Size(113, 23);
            this.button_ONNX_Prediction224List.TabIndex = 2;
            this.button_ONNX_Prediction224List.Text = "Prediction224List";
            this.button_ONNX_Prediction224List.UseVisualStyleBackColor = true;
            this.button_ONNX_Prediction224List.Click += new System.EventHandler(this.button_ONNX_Prediction224List_Click);
            // 
            // button_ONNX_Prediction28
            // 
            this.button_ONNX_Prediction28.Location = new System.Drawing.Point(518, 53);
            this.button_ONNX_Prediction28.Name = "button_ONNX_Prediction28";
            this.button_ONNX_Prediction28.Size = new System.Drawing.Size(113, 23);
            this.button_ONNX_Prediction28.TabIndex = 2;
            this.button_ONNX_Prediction28.Text = "Prediction28";
            this.button_ONNX_Prediction28.UseVisualStyleBackColor = true;
            this.button_ONNX_Prediction28.Click += new System.EventHandler(this.button_ONNX_Prediction28_Click);
            // 
            // button_ONNX_Prediction224
            // 
            this.button_ONNX_Prediction224.Location = new System.Drawing.Point(518, 24);
            this.button_ONNX_Prediction224.Name = "button_ONNX_Prediction224";
            this.button_ONNX_Prediction224.Size = new System.Drawing.Size(113, 23);
            this.button_ONNX_Prediction224.TabIndex = 2;
            this.button_ONNX_Prediction224.Text = "Prediction224";
            this.button_ONNX_Prediction224.UseVisualStyleBackColor = true;
            this.button_ONNX_Prediction224.Click += new System.EventHandler(this.button_ONNX_Prediction2_Click);
            // 
            // button_LoadOnnxFile
            // 
            this.button_LoadOnnxFile.Location = new System.Drawing.Point(23, 128);
            this.button_LoadOnnxFile.Name = "button_LoadOnnxFile";
            this.button_LoadOnnxFile.Size = new System.Drawing.Size(75, 23);
            this.button_LoadOnnxFile.TabIndex = 2;
            this.button_LoadOnnxFile.Text = "Load";
            this.button_LoadOnnxFile.UseVisualStyleBackColor = true;
            this.button_LoadOnnxFile.Click += new System.EventHandler(this.button_LoadOnnxFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "LoadOnnxFilename";
            // 
            // textBox_LoadOnnx28Filename
            // 
            this.textBox_LoadOnnx28Filename.Location = new System.Drawing.Point(8, 55);
            this.textBox_LoadOnnx28Filename.Name = "textBox_LoadOnnx28Filename";
            this.textBox_LoadOnnx28Filename.Size = new System.Drawing.Size(504, 19);
            this.textBox_LoadOnnx28Filename.TabIndex = 0;
            // 
            // textBox_LoadOnnx224Filename
            // 
            this.textBox_LoadOnnx224Filename.Location = new System.Drawing.Point(8, 26);
            this.textBox_LoadOnnx224Filename.Name = "textBox_LoadOnnx224Filename";
            this.textBox_LoadOnnx224Filename.Size = new System.Drawing.Size(504, 19);
            this.textBox_LoadOnnx224Filename.TabIndex = 0;
            // 
            // tabPage_CreateTestImage
            // 
            this.tabPage_CreateTestImage.Controls.Add(this.label4);
            this.tabPage_CreateTestImage.Controls.Add(this.label3);
            this.tabPage_CreateTestImage.Controls.Add(this.label2);
            this.tabPage_CreateTestImage.Controls.Add(this.textBox_CreateTestImage_Size);
            this.tabPage_CreateTestImage.Controls.Add(this.button_CreateTestImage_Run);
            this.tabPage_CreateTestImage.Controls.Add(this.textBox_CreateTestImage_imgCount);
            this.tabPage_CreateTestImage.Controls.Add(this.textBox_CreateTestImage_TargetDir);
            this.tabPage_CreateTestImage.Location = new System.Drawing.Point(4, 22);
            this.tabPage_CreateTestImage.Name = "tabPage_CreateTestImage";
            this.tabPage_CreateTestImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_CreateTestImage.Size = new System.Drawing.Size(1164, 632);
            this.tabPage_CreateTestImage.TabIndex = 1;
            this.tabPage_CreateTestImage.Text = "CreateTestImage";
            this.tabPage_CreateTestImage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(202, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "imgCout / class";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "TargetDir";
            // 
            // textBox_CreateTestImage_Size
            // 
            this.textBox_CreateTestImage_Size.Location = new System.Drawing.Point(98, 29);
            this.textBox_CreateTestImage_Size.Name = "textBox_CreateTestImage_Size";
            this.textBox_CreateTestImage_Size.Size = new System.Drawing.Size(100, 19);
            this.textBox_CreateTestImage_Size.TabIndex = 2;
            this.textBox_CreateTestImage_Size.Text = "224,224";
            // 
            // button_CreateTestImage_Run
            // 
            this.button_CreateTestImage_Run.Location = new System.Drawing.Point(6, 14);
            this.button_CreateTestImage_Run.Name = "button_CreateTestImage_Run";
            this.button_CreateTestImage_Run.Size = new System.Drawing.Size(75, 36);
            this.button_CreateTestImage_Run.TabIndex = 1;
            this.button_CreateTestImage_Run.Text = "Run";
            this.button_CreateTestImage_Run.UseVisualStyleBackColor = true;
            this.button_CreateTestImage_Run.Click += new System.EventHandler(this.button_CreateTestImage_Run_Click);
            // 
            // textBox_CreateTestImage_imgCount
            // 
            this.textBox_CreateTestImage_imgCount.Location = new System.Drawing.Point(204, 29);
            this.textBox_CreateTestImage_imgCount.Name = "textBox_CreateTestImage_imgCount";
            this.textBox_CreateTestImage_imgCount.Size = new System.Drawing.Size(64, 19);
            this.textBox_CreateTestImage_imgCount.TabIndex = 0;
            // 
            // textBox_CreateTestImage_TargetDir
            // 
            this.textBox_CreateTestImage_TargetDir.Location = new System.Drawing.Point(6, 72);
            this.textBox_CreateTestImage_TargetDir.Name = "textBox_CreateTestImage_TargetDir";
            this.textBox_CreateTestImage_TargetDir.Size = new System.Drawing.Size(262, 19);
            this.textBox_CreateTestImage_TargetDir.TabIndex = 0;
            // 
            // tabPage_CreateFileList
            // 
            this.tabPage_CreateFileList.Controls.Add(this.textBox_CreateFileList);
            this.tabPage_CreateFileList.Controls.Add(this.textBox_CreateFileListPath_TargetDir);
            this.tabPage_CreateFileList.Controls.Add(this.button_CreateFileList);
            this.tabPage_CreateFileList.Location = new System.Drawing.Point(4, 22);
            this.tabPage_CreateFileList.Name = "tabPage_CreateFileList";
            this.tabPage_CreateFileList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_CreateFileList.Size = new System.Drawing.Size(1164, 632);
            this.tabPage_CreateFileList.TabIndex = 2;
            this.tabPage_CreateFileList.Text = "CreateFileList";
            this.tabPage_CreateFileList.UseVisualStyleBackColor = true;
            // 
            // textBox_CreateFileList
            // 
            this.textBox_CreateFileList.Location = new System.Drawing.Point(6, 130);
            this.textBox_CreateFileList.Name = "textBox_CreateFileList";
            this.textBox_CreateFileList.Size = new System.Drawing.Size(509, 19);
            this.textBox_CreateFileList.TabIndex = 1;
            // 
            // textBox_CreateFileListPath_TargetDir
            // 
            this.textBox_CreateFileListPath_TargetDir.Location = new System.Drawing.Point(6, 74);
            this.textBox_CreateFileListPath_TargetDir.Name = "textBox_CreateFileListPath_TargetDir";
            this.textBox_CreateFileListPath_TargetDir.Size = new System.Drawing.Size(509, 19);
            this.textBox_CreateFileListPath_TargetDir.TabIndex = 1;
            // 
            // button_CreateFileList
            // 
            this.button_CreateFileList.Location = new System.Drawing.Point(6, 6);
            this.button_CreateFileList.Name = "button_CreateFileList";
            this.button_CreateFileList.Size = new System.Drawing.Size(121, 32);
            this.button_CreateFileList.TabIndex = 0;
            this.button_CreateFileList.Text = "CreateFileList";
            this.button_CreateFileList.UseVisualStyleBackColor = true;
            this.button_CreateFileList.Click += new System.EventHandler(this.button_CreateFileList_Click);
            // 
            // chart_PredictionDistance
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_PredictionDistance.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_PredictionDistance.Legends.Add(legend1);
            this.chart_PredictionDistance.Location = new System.Drawing.Point(769, 26);
            this.chart_PredictionDistance.Name = "chart_PredictionDistance";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_PredictionDistance.Series.Add(series1);
            this.chart_PredictionDistance.Size = new System.Drawing.Size(387, 300);
            this.chart_PredictionDistance.TabIndex = 7;
            this.chart_PredictionDistance.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 658);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "DeployONNX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_ONNX.ResumeLayout(false);
            this.tabPage_ONNX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Prediction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage_CreateTestImage.ResumeLayout(false);
            this.tabPage_CreateTestImage.PerformLayout();
            this.tabPage_CreateFileList.ResumeLayout(false);
            this.tabPage_CreateFileList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_PredictionDistance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_ONNX;
        private System.Windows.Forms.TabPage tabPage_CreateTestImage;
        private System.Windows.Forms.Button button_CreateTestImage_Run;
        private System.Windows.Forms.TextBox textBox_CreateTestImage_TargetDir;
        private System.Windows.Forms.TextBox textBox_CreateTestImage_Size;
        private System.Windows.Forms.Button button_LoadOnnxFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_LoadOnnx224Filename;
        private System.Windows.Forms.TextBox textBox_LoadOnnxFileInfo;
        private System.Windows.Forms.TextBox textBox_LoadOnnxFileInfo_Size;
        private System.Windows.Forms.Button button_ONNX_Prediction224;
        private System.Windows.Forms.TabPage tabPage_CreateFileList;
        private System.Windows.Forms.Button button_CreateFileList;
        private System.Windows.Forms.TextBox textBox_CreateFileListPath_TargetDir;
        private System.Windows.Forms.TextBox textBox_CreateFileList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_CreateTestImage_imgCount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Prediction;
        private System.Windows.Forms.Button button_ONNX_Prediction28;
        private System.Windows.Forms.TextBox textBox_LoadOnnx28Filename;
        private System.Windows.Forms.Button button_ONNX_Prediction28List;
        private System.Windows.Forms.Button button_ONNX_Prediction224List;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_PredictionDistance;
    }
}

