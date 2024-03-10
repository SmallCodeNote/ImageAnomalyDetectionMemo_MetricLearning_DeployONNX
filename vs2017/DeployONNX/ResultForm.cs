using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeployONNX
{
    public partial class ResultForm : UserControl
    {
        public ResultForm(Bitmap img, string filename, double distance, string format = "g4", double distanceMax = 400.0)
        {
            InitializeComponent();
            pictureBoxUpdate(pictureBox_targetImage, img);
            this.filename = filename;
            this.distanceMax = distanceMax;
            this.distance = distance;

        }

        private string _filename = "";
        private string _format = "g4";
        private double _distance = 0;
        private double _distanceMax = 400;

        public string filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                label_Filename.Text = _filename;

            }
        }

        public string format
        {
            get { return _format; }
            set
            {
                _format = value;
                label_Value.Text = _distance.ToString(_format);
            }
        }

        public double distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                label_Value.Text = _distance.ToString(_format);
                panel_bar.Width = (int)(_distance / _distanceMax * (this.Width - pictureBox_targetImage.Width - 2));

            }
        }

        public double distanceMax
        {
            get { return _distance; }
            set
            {
                _distanceMax = value;
                panel_bar.Width = (int)(distance / _distanceMax * (this.Width - pictureBox_targetImage.Width - 2));
            }
        }

        private void pictureBoxUpdate(PictureBox pictureBox, Bitmap bitmap)
        {
            if (pictureBox.Image != null) pictureBox.Image.Dispose();
            pictureBox.Image = bitmap;
        }
    }
}
