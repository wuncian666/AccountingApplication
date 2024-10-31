using System.Windows.Forms;
using 記帳.Models.ModelTypes;

namespace 記帳.Extensions
{
    internal static class ComboBoxExtension
    {

        public static void FillingComboBoxItem(this ComboBox comboBox, string[] items)
        {
            foreach (var item in items)
            {
                comboBox.Items.Add(item);
            }
        }

        public static void AddItem(this ComboBox comboBox, int index)
        {
            string[] items = ModelTypes.GetItems(index);
            foreach (var item in items)
            {
                comboBox.Items.Add(item);
            }
        }

        public static void ComboBoxOptions(this ComboBox comboBox, string[] options)
        {
            comboBox.FillingComboBoxItem(options);
            comboBox.SelectedIndex = 0;
        }
    }
}
