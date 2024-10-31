using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using 記帳.Models;
using 記帳.Models.ModelTypes;

namespace 記帳.Extensions
{
    internal static class DataGridViewExtension
    {
        public static void GroupHide(this DataGridView dataGridView, int[] items)
        {
            if (dataGridView.Columns.Count == 0) return;
            foreach (var item in items)
            {
                dataGridView.Columns[item].Visible = false;
            }
        }

        public static void AddImageColumn(this DataGridView dataGridView, int index, string headerText)
        {
            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn
            {
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            dataGridView.Columns.Insert(index, iconColumn);
            dataGridView.Columns[index].HeaderText = headerText;
        }

        public static void AddTrashcan(this DataGridView dataGridView, int index)
        {
            string imagePath = "D:\\files\\images\\icon_trashcan.png";
            Bitmap bitmap = new Bitmap(imagePath);
            DataGridViewImageColumn iconColumn = new DataGridViewImageColumn
            {
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Image = bitmap
            };
            dataGridView.Columns.Insert(index, iconColumn);
            dataGridView.Columns[index].HeaderText = "刪除";
        }

        public static void SetRowImage(this DataGridView dataGridView, int index1, int index2)
        {
            List<Record> records = (List<Record>)dataGridView.DataSource;
            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                Bitmap bmp1 = new Bitmap(records[row].Image1);
                ((DataGridViewImageCell)dataGridView.Rows[row].Cells[index1]).Value = bmp1;

                Bitmap bmp2 = new Bitmap(records[row].Image2);
                ((DataGridViewImageCell)dataGridView.Rows[row].Cells[index2]).Value = bmp2;
            }
        }

        public static void SetComboBox(this DataGridView dataGridView)
        {
            DataGridViewComboBoxColumn comboBoxColumnType = GetComboBoxColumn("類型", "Type", ModelTypes.types);
            dataGridView.ResetComboBoxItem(1, comboBoxColumnType);

            string[] items = { };
            DataGridViewComboBoxColumn comboBoxColumnItem = GetComboBoxColumn("項目", "Item", items);
            dataGridView.ResetComboBoxItem(2, comboBoxColumnItem);

            DataGridViewComboBoxColumn comboBoxColumnPaymentMethod = GetComboBoxColumn("付款方式", "PaymentMethod", ModelTypes.paymentMethods);
            dataGridView.ResetComboBoxItem(3, comboBoxColumnPaymentMethod);

            // 根據類型顯示項目
            List<Record> records = (List<Record>)dataGridView.DataSource;
            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                // 找前一格對應的 item
                items = ModelTypes.typesMap[records[row].Type];
                // cell 那格
                DataGridViewComboBoxCell item = (DataGridViewComboBoxCell)dataGridView.Rows[row].Cells[2];
                // combo box 的列表
                item.DataSource = items;
                // 要顯示的值
                item.Value = records[row].Item;
            }
        }

        private static DataGridViewComboBoxColumn GetComboBoxColumn(string header, string name, string[] source)
        {
            DataGridViewComboBoxColumn comboBoxColumnType = new DataGridViewComboBoxColumn
            {
                HeaderText = header,
                Name = name,
                DataSource = source,
                DataPropertyName = name,
            };

            return comboBoxColumnType;
        }

        private static void ResetComboBoxItem(this DataGridView dataGridView, int index, DataGridViewComboBoxColumn comboBox)
        {
            dataGridView.Columns.RemoveAt(index);
            dataGridView.Columns.Insert(index, comboBox);
        }
    }
}
