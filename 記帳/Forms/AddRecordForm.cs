using CSVHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using 記帳.Extensions;
using 記帳.Models;
using 記帳.Models.ModelTypes;
using 記帳.Utils;

namespace 記帳.Forms
{
    public partial class AddRecordForm : Form
    {
        public AddRecordForm()
        {
            InitializeComponent();
            ComboBoxOptions();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            string uploadImage = "D:\\files\\images\\upload_image.png";
            pictureBox1.Load(uploadImage);
            pictureBox2.Load(uploadImage);
        }

        private void ComboBoxOptions()
        {
            comboBoxType.FillingComboBoxItem(ModelTypes.types);
            comboBoxType.SelectedIndex = 0;

            comboBoxPaymentMethod.FillingComboBoxItem(ModelTypes.paymentMethods);
            comboBoxPaymentMethod.SelectedIndex = 0;

            comboBoxTarget.FillingComboBoxItem(ModelTypes.targets);
            comboBoxTarget.SelectedIndex = 0;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            comboBoxItem.Items.Clear();
            int index = ((ComboBox)sender).SelectedIndex;
            comboBoxItem.AddItem(index);
            comboBoxItem.SelectedIndex = 0;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string path = "D:\\files";
            this.CheckPathExists(path);
            string currentDayPath = path + "\\" + dateTimePicker1.Value.ToString("yyyy_MM_dd");
            this.CheckPathExists(currentDayPath);

            ImagePath imagePath1 = new ImagePath(currentDayPath);
            this.SaveImage(pictureBox1.Image, imagePath1.Origin, imagePath1.Compress);
            ImagePath imagePath2 = new ImagePath(currentDayPath);
            this.SaveImage(pictureBox2.Image, imagePath2.Origin, imagePath2.Compress);

            List<Record> records = new List<Record>
            {
                new Record(textBoxAmount.Text,
                comboBoxType.Text,
                comboBoxItem.Text,
                comboBoxPaymentMethod.Text,
                comboBoxTarget.Text,
                imagePath1.Origin,
                imagePath1.Compress,
                imagePath2.Origin,
                imagePath2.Compress)
            };

            CSV.Write(currentDayPath + "\\record.csv", records, true, false);
        }

        private void CheckPathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void SaveImage(Image image, string originPath, string compressPath)
        {
            image.Save(originPath);

            var compress = ImageCompress.CompressImage(1);
            Bitmap result = ImageCompress.Compress(image, compress);
            result.Save(compressPath);
        }

        private void image_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "圖片檔案|*.jpg;*.png;*.jpeg"
            };

            PictureBox pictureBox = (PictureBox)sender;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Load(openFileDialog.FileName);
            }
        }
    }
}
