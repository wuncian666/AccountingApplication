using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 記帳.Contracts;
using 記帳.Extensions;
using 記帳.Models;
using 記帳.Models.ModelTypes;
using 記帳.Presenters;
using 記帳.Utils;

namespace 記帳.Forms
{
    public partial class AddRecordForm : Form, Contracts.IAddRecordView
    {
        IAddRecordPresenter presenter = null;

        private string uploadImage = "D:\\files\\images\\upload_image.png";
        public AddRecordForm()
        {
            InitializeComponent();
            presenter = new AddRecordPresenter(this);
            //ComboBoxOptions();
            comboBoxType.ComboBoxOptions(ModelTypes.types);
            comboBoxPaymentMethod.ComboBoxOptions(ModelTypes.paymentMethods);
            comboBoxTarget.ComboBoxOptions(ModelTypes.targets);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox1.Load(uploadImage);
            pictureBox2.Load(uploadImage);
        }

        public void OnAddRecord()
        {
            this.ResetView();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxItem.Items.Clear();
            int index = ((ComboBox)sender).SelectedIndex;
            comboBoxItem.AddItem(index);
            comboBoxItem.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DebounceClick(() => this.Process(), 1000);
        }

        private void Process()
        {
            string path = "D:\\files";
            PathUtils.CheckPathExists(path);
            string currentDayPath = path + "\\" + dateTimePicker1.Value.ToString("yyyy_MM_dd");
            PathUtils.CheckPathExists(currentDayPath);

            // Compress to a percentage of the oriainal image.
            int quality = 1;
            ImagePath imagePath1 = new ImagePath(currentDayPath);
            ImageCompress.Compress(pictureBox1.Image, quality, true).Save(imagePath1.Origin);
            ImageCompress.Compress(pictureBox1.Image, quality, false).Save(imagePath1.Compress);

            ImagePath imagePath2 = new ImagePath(currentDayPath);
            ImageCompress.Compress(pictureBox2.Image, quality, true).Save(imagePath2.Origin);
            ImageCompress.Compress(pictureBox2.Image, quality, false).Save(imagePath2.Compress);

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

            this.presenter.AddRecord(currentDayPath, records);
        }

        private void ResetView()
        {
            if (pictureBox1.Image != null || pictureBox2.Image != null)
            {
                pictureBox1.Image.Dispose();  // 釋放原有的圖片資源
                pictureBox1.Image = null;
                pictureBox2.Image.Dispose();  // 釋放原有的圖片資源
                pictureBox2.Image = null;
            }
            GC.Collect();
            pictureBox1.Load(this.uploadImage);
            pictureBox2.Load(this.uploadImage);
            textBoxAmount.Text = "";
            comboBoxType.SelectedIndex = 0;
            comboBoxItem.SelectedIndex = 0;
            comboBoxPaymentMethod.SelectedIndex = 0;
            comboBoxTarget.SelectedIndex = 0;
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
