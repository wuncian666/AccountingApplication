using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 記帳.Contracts;
using 記帳.Models.ModelTypes;
using 記帳.Presenters;
using 記帳.Utils;

namespace 記帳.Forms
{
    public partial class AccountForm : Form, IAccountView
    {
        private readonly IAccountPresenter presenter = null;

        private readonly List<string> typeCheckBoxOptions = new List<string>();
        private readonly List<string> paymentCheckBoxOptions = new List<string>();
        private readonly List<string> targetCheckBoxOptions = new List<string>();

        public AccountForm()
        {
            InitializeComponent();
            this.presenter = new AccountingPresenter(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DebounceClick(() =>
                this.presenter.GetDataRangeRecord(
                    dateTimePicker1.Value,
                    dateTimePicker2.Value,
                    this.typeCheckBoxOptions,
                    this.paymentCheckBoxOptions,
                    this.targetCheckBoxOptions),
                1000);
        }

        void IAccountView.ResetView(List<GroupAccountingModel> records)
        {
            dataGridView1.Columns.Clear();
            GC.Collect();
            dataGridView1.DataSource = records;
        }

        private void Type_Checked_Changed(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            this.OptionsProcess(checkBox, this.typeCheckBoxOptions);
        }

        private void Payment_Checked_Change(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            this.OptionsProcess(checkBox, this.paymentCheckBoxOptions);
        }

        private void Target_Checked_Change(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            this.OptionsProcess(checkBox, this.targetCheckBoxOptions);
        }

        private void OptionsProcess(CheckBox checkBox, List<string> options)
        {
            string option = checkBox.Text;
            if (checkBox.Checked)
            {
                if (!options.Contains(option))
                {
                    options.Add(option);
                }
            }
            else
            {
                options.Remove(option);
            }
        }

        private void All_Options_Checked_Changed(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                string[] options = ModelTypes.allOptions[checkBox.Text];
                switch (checkBox.Text)
                {
                    case "所有類別":
                        this.typeCheckBoxOptions.AddRange(options);
                        break;

                    case "所有付款方式":
                        this.paymentCheckBoxOptions.AddRange(options);
                        break;

                    case "所有對象":
                        this.targetCheckBoxOptions.AddRange(options);
                        break;
                }
            }
            else
            {
                switch (checkBox.Text)
                {
                    case "所有類別":
                        this.typeCheckBoxOptions.Clear();
                        break;

                    case "所有付款方式":
                        this.paymentCheckBoxOptions.Clear();
                        break;

                    case "所有對象":
                        this.targetCheckBoxOptions.Clear();
                        break;
                }
            }
        }
    }
}