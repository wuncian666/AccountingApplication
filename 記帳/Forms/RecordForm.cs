using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using 記帳.Models;
using 記帳.Repository;
using 記帳.Utils;

namespace 記帳.Forms
{
    public partial class RecordForm : Form
    {
        CsvService csvService = new CsvService();

        public RecordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DebounceClick(() => ResetView(), 1000);
        }

        private void ResetView()
        {
            dataGridView1.Columns.Clear();
            GC.Collect();

            List<Record> data = this.csvService.GetDateRangeRecord(dateTimePicker1.Value, dateTimePicker2.Value);
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

            List<Record> data = this.csvService.GetDateRangeRecord(dateTimePicker1.Value, dateTimePicker2.Value);

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
