using System.Windows.Forms;

namespace 記帳.Extensions
{
    internal static class DataGridViewComboBoxColumnExtension
    {
        public static void SetComboBox(this DataGridViewComboBoxColumn column, string header, string name, string[] source, int position)
        {
            column.HeaderText = header;
            column.Name = name;
            column.DataSource = source;
            column.DataPropertyName = name;
        }
    }
}
