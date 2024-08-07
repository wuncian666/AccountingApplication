using CSVHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using 記帳.Models;

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
            dataGridView1.Columns.Clear();
            GC.Collect();
            string path = "D:\\files\\record.csv";

            List<Record> data = CSV.Read<Record>(path, false);
            dataGridView1.DataSource = data;

            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;

            DataGridViewImageColumn iconColumn1 = new DataGridViewImageColumn
            {
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dataGridView1.Columns.Insert(7, iconColumn1);
            dataGridView1.Columns[7].HeaderText = "圖片1";

            DataGridViewImageColumn iconColumn2 = new DataGridViewImageColumn
            {
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dataGridView1.Columns.Insert(8, iconColumn2);
            dataGridView1.Columns[8].HeaderText = "圖片2";

            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                Bitmap bmp1 = new Bitmap(data[row].image1);
                ((DataGridViewImageCell)dataGridView1.Rows[row].Cells[7]).Value = bmp1;
                Bitmap bmp2 = new Bitmap(data[row].image2);
                ((DataGridViewImageCell)dataGridView1.Rows[row].Cells[8]).Value = bmp2;
            }
        }
    }
}
