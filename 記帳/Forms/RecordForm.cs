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
    public partial class RecordForm : Form, Contracts.IRecordView
    {
        IRecordPresenter presenter = null;

        List<Record> records = null;

        public RecordForm()
        {
            InitializeComponent();
            this.presenter = new RecordPresenter(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DebounceClick(() =>
                this.presenter.GetDataRangeRecord(dateTimePicker1.Value, dateTimePicker2.Value),
                1000);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Record record = records[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 7:
                    this.presenter.OnClickImage(record.Image1);
                    break;
                case 8:
                    this.presenter.OnClickImage(record.Image2);
                    break;
                case 9:
                    // delete row
                    this.records.Remove(records[e.RowIndex]);
                    string path = PathUtils.GetPathFromImagePath(record.Image1);
                    this.presenter.DeleteRecord(path, this.records);
                    break;
            }
        }

        public void ResetView(List<Record> records)
        {
            this.records = records;
            dataGridView1.Columns.Clear();
            GC.Collect();
            dataGridView1.DataSource = records;

            if (dataGridView1.Columns.Count == 0) return;
            int[] columns = { 5, 6, 7, 8 };
            dataGridView1.GroupHide(columns);
            dataGridView1.AddImageColumn(7, "圖片1");
            dataGridView1.AddImageColumn(8, "圖片2");
            dataGridView1.AddTrashcan(9);
            dataGridView1.SetRowImage(7, 8);
            dataGridView1.SetComboBox();
        }

        // 修改按下 Enter 觸發
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine($"{e.ColumnIndex}, {e.RowIndex}");
            // 當下點的 cell
            string newValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

            Record record = records[e.RowIndex];
            string path = PathUtils.GetPathFromImagePath(record.Image1);

            switch (e.ColumnIndex)
            {
                case 0:
                    record.Amount = newValue;
                    this.presenter.EditRecord(path, this.records);
                    break;
                case 1:
                    // 從類型選甚麼 map 出項目種類 items
                    string[] items = ModelTypes.typesMap[newValue];
                    // 抓項目的欄位(2)
                    DataGridViewComboBoxCell item = (DataGridViewComboBoxCell)dataGridView1[2, e.RowIndex];
                    item.DataSource = items; // change combo box items
                    item.Value = items[0];
                    record.Type = newValue;
                    record.Item = dataGridView1[2, e.RowIndex].Value.ToString();
                    this.presenter.EditRecord(path, this.records);
                    break;
                case 2:
                    record.Item = newValue;
                    this.presenter.EditRecord(path, this.records);
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
