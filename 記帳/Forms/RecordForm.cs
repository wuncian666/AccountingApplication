using CSVHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using 記帳.Models;
using 記帳.Utils;

namespace 記帳.Forms
{
    public partial class RecordForm : Form
    {
        public RecordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.DebounceClick(() => ResetView(), 1000);
            //debounce.Click(() =>
            //{
            //    this.Invoke((Action)(() =>
            //    {
            //        ResetView();
            //    }));
            //}, 1000);
        }

        private void ResetView()
        {
            dataGridView1.Columns.Clear();
            GC.Collect();

            List<Record> data = this.GetDateRangeRecord();
            dataGridView1.DataSource = data;

            // Hide the path columns.
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;

            this.AddNewDataGridViewImageColumn(7, "圖片1");
            this.AddNewDataGridViewImageColumn(8, "圖片2");

            // Add the images to each row.
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                Bitmap bmp1 = new Bitmap(data[row].Image1);
                ((DataGridViewImageCell)dataGridView1.Rows[row].Cells[7]).Value = bmp1;

                Bitmap bmp2 = new Bitmap(data[row].Image2);
                ((DataGridViewImageCell)dataGridView1.Rows[row].Cells[8]).Value = bmp2;
            }
        }

        private List<Record> GetDateRangeRecord()
        {
            List<Record> data = new List<Record>();
            var range = dateTimePicker2.Value - dateTimePicker1.Value;
            int days = range.Days;

            var temp = dateTimePicker1.Value;
            for (int i = 0; i <= days; i++)
            {
                string dayPath = "D:\\files\\" + temp.ToString("yyyy_MM_dd");
                if (Directory.Exists(dayPath))
                {
                    data.AddRange(CSV.Read<Record>(dayPath + "\\record.csv", false));
                }
                temp = temp.AddDays(1);
            }
            return data;
        }

        private void AddNewDataGridViewImageColumn(int index, string headerText)
        {
            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn
            {
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };

            dataGridView1.Columns.Insert(index, iconColumn);
            dataGridView1.Columns[index].HeaderText = headerText;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(e.RowIndex + "," + e.ColumnIndex);

            List<Record> data = this.GetDateRangeRecord();

            string imagePath = "";
            if (e.ColumnIndex == 7)
            {
                imagePath = data[e.RowIndex].Image1;
            }
            else if (e.ColumnIndex == 8)
            {
                imagePath = data[e.RowIndex].Image2;
            }

            ImageForm imageForm = new ImageForm(imagePath);
            imageForm.ShowDialog();
        }
    }
}
