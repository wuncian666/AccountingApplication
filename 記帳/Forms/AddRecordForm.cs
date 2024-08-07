using CSVHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using 記帳.Extensions;
using 記帳.Models;
using 記帳.Models.ModelTypes;

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
            pictureBox1.Load("C:\\Users\\wuncian\\Downloads\\photo.png");
            pictureBox2.Load("C:\\Users\\wuncian\\Downloads\\photo.png");
        }

        private void ComboBoxOptions()
        {
            comboBoxType.FillingComboBoxItem(ModelTypes.types);
            comboBoxPaymentMethod.FillingComboBoxItem(ModelTypes.paymentMethods);
            comboBoxTarget.FillingComboBoxItem(ModelTypes.targets);
            comboBoxType.SelectedIndex = 0;
            comboBoxPaymentMethod.SelectedIndex = 0;
            comboBoxTarget.SelectedIndex = 0;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            comboBoxItem.Items.Clear();
            comboBoxItem.AddItem(index);
            comboBoxItem.SelectedIndex = 0;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string path = "D:\\files";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string guid1 = Guid.NewGuid().ToString();
            string image1Path = path + "\\" + guid1 + "image.jpg";
            pictureBox1.Image.Save(image1Path);
            string guid2 = Guid.NewGuid().ToString();
            string image2Path = path + "\\" + guid2 + "image.jpg";
            pictureBox2.Image.Save(image2Path);


            Record record = new Record(textBoxAmount.Text,
                comboBoxType.Text,
                comboBoxItem.Text,
                comboBoxPaymentMethod.Text,
                comboBoxTarget.Text,
                image1Path,
                image2Path);
            List<Record> records = new List<Record>();
            records.Add(record);
            CSV.Write("D:\\files\\record.csv", records, true, false);


        }

        private void image_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "圖片檔案|*.jpg ; *.png";
            //openFileDialog.ShowDialog();

            PictureBox pictureBox = (PictureBox)sender;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Load(openFileDialog.FileName);
            }
        }

        private void AddRecordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
