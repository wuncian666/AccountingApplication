using System;
using System.Windows.Forms;
using 記帳.Forms;
using 記帳.Models.Enums;

namespace 記帳.Components
{
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

        private void ChangeForm(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Enum.TryParse(button.Tag.ToString(), out FormType type);
            Form form = SingletonForm.GetForm(type);
            form.Show();
        }
    }
}
