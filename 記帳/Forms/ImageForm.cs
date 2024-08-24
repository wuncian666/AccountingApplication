using System;
using System.Windows.Forms;

namespace 記帳.Forms
{
    public partial class ImageForm : Form
    {
        public ImageForm(string imagePath)
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Load(imagePath);

        }

        private void ImageForm_Load(object sender, EventArgs e)
        {

        }

        private void ImageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();  // 釋放原有的圖片資源
                pictureBox1.Image = null;
            }
        }
    }
}
