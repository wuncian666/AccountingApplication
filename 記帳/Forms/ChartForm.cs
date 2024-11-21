using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ChartGraph = System.Windows.Forms.DataVisualization.Charting.Chart;
using 記帳.Contracts;
using 記帳.Models.ModelTypes;
using 記帳.Presenters;
using 記帳.Utils;

namespace 記帳.Forms
{
    public partial class ChartForm : Form, Contracts.IChartView
    {
        private readonly IChartPresenter chartPresenter = null;

        private readonly HashSet<string> typeCheckBoxOptions = new HashSet<string>();
        private readonly HashSet<string> paymentCheckBoxOptions = new HashSet<string>();
        private readonly HashSet<string> targetCheckBoxOptions = new HashSet<string>();

        private readonly Dictionary<string, HashSet<string>> optionsForAllCheckBoxes;

        public ChartForm()
        {
            InitializeComponent();
            this.chartPresenter = new ChartPresenter(this);

            this.dateTimePicker2.Value = DateTime.Now.AddDays(30);

            this.optionsForAllCheckBoxes = new Dictionary<string, HashSet<string>>()
            {
                { "typeCheckBoxOptions", this.typeCheckBoxOptions },
                { "paymentCheckBoxOptions", this.paymentCheckBoxOptions },
                { "targetCheckBoxOptions", this.targetCheckBoxOptions }
            };

            this.comboBox1.Items.AddRange(new string[] {
                SeriesChartType.Line.ToString(),
                SeriesChartType.Pie.ToString(),
                SeriesChartType.StackedColumn.ToString() });
            this.comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.chartPresenter.GetDataRangeRecord(
                this.dateTimePicker1.Value,
                this.dateTimePicker2.Value,
                this.optionsForAllCheckBoxes);
        }

        private void OptionsProcess(CheckBox checkBox, HashSet<string> options)
        {
            string option = checkBox.Text;
            if (checkBox.Checked)
            {
                options.Add(option);
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
                        foreach (var option in options)
                        {
                            this.typeCheckBoxOptions.Add(option);
                        }
                        break;

                    case "所有付款方式":
                        foreach (var option in options)
                        {
                            this.paymentCheckBoxOptions.Add(option);
                        }

                        break;

                    case "所有對象":
                        foreach (var option in options)
                        {
                            this.targetCheckBoxOptions.Add(option);
                        }
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

        public void DrawingChart(ChartGraph chart)
        {
            this.flowLayoutPanel1.Controls.Clear();
            this.flowLayoutPanel1.Controls.Add(chart);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chartPresenter.ChartTypeChanged(this.optionsForAllCheckBoxes);
        }

        public SeriesChartType GetSealectedChartType()
        {
            return (SeriesChartType)Enum.Parse(typeof(SeriesChartType), this.comboBox1.SelectedItem.ToString());
        }

        private void OptionsClick(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string option = checkBox.Tag.ToString();

            switch (option)
            {
                case "type":
                    this.OptionsProcess(checkBox, this.typeCheckBoxOptions);
                    break;

                case "payment":
                    this.OptionsProcess(checkBox, this.paymentCheckBoxOptions);
                    break;

                case "target":
                    this.OptionsProcess(checkBox, this.targetCheckBoxOptions);
                    break;
            }
        }
    }
}