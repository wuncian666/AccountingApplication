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

        private List<GroupAccountingModel> currentGroups;

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

            this.comboBox1.Items.AddRange(new string[] { "長條圖", "圓餅圖", "折線圖" });
            this.comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.chartPresenter.GetDataRangeRecord(
                this.dateTimePicker1.Value,
                this.dateTimePicker2.Value,
                this.optionsForAllCheckBoxes);
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

        void IChartView.DrawingChart(List<GroupAccountingModel> groups)
        {
            // 保存當前數據
            this.currentGroups = groups;

            // 根據目前選擇的圖表類型繪製
            DrawChartByType(this.comboBox1.SelectedItem.ToString(), groups);

            //chart1.Series.Clear();

            // 將每個條件和數據加入圖表的數據系列
            //foreach (var group in groups)
            //{
            //    var series = new Series(group.Conditions)
            //    {
            //        ChartType = SeriesChartType.StackedColumn,
            //        IsValueShownAsLabel = true // 顯示數值標籤
            //    };

            //    // 將數據加入系列
            //    series.Points.Add(group.Sum);

            //    chart1.Series.Add(series);
            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentGroups != null)
            {
                string chartType = this.comboBox1.SelectedItem.ToString();
                DrawChartByType(chartType, currentGroups);
            }
            //string chartType = this.comboBox1.SelectedItem.ToString();
            //this.SetChartType(chartType);
        }

        private void SetChartType(string chartType)
        {
            SeriesChartType selectedType = SeriesChartType.StackedColumn;

            switch (chartType)
            {
                case "圓餅圖":
                    selectedType = SeriesChartType.Pie;

                    break;

                case "折線圖":
                    selectedType = SeriesChartType.Line;
                    break;

                case "長條圖":
                    //selectedType = SeriesChartType.Column;// 一般長條圖
                    selectedType = SeriesChartType.StackedColumn;// 疊加長條圖
                    break;

                default:
                    selectedType = SeriesChartType.StackedColumn;
                    break;
            }

            // 更新所有 Series 的圖表類型
            foreach (var series in chart1.Series)
            {
                series.ChartType = selectedType;
            }

            // 如果選擇圓餅圖，處理點的資料結構（圓餅圖只接受單一系列）
            if (selectedType == SeriesChartType.Pie)
            {
                chart1.Series.Clear();
                var pieSeries = new Series("圓餅圖")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true
                };

                foreach (var group in chart1.Series)
                {
                    pieSeries.Points.AddXY(group.Name, group.Points[0].YValues[0]);
                }

                chart1.Series.Add(pieSeries);
            }
        }

        private void DrawChartByType(string chartType, List<GroupAccountingModel> groups)
        {
            chart1.Series.Clear();

            switch (chartType)
            {
                case "圓餅圖":
                    var pieSeries = new Series("圓餅圖")
                    {
                        ChartType = SeriesChartType.Pie,
                        IsValueShownAsLabel = true
                    };

                    foreach (var group in groups)
                    {
                        pieSeries.Points.AddXY(group.Conditions, group.Sum);
                    }

                    chart1.Series.Add(pieSeries);
                    break;

                case "折線圖":
                    var lineSeries = new Series("折線圖")
                    {
                        ChartType = SeriesChartType.Line,
                        IsValueShownAsLabel = true
                    };

                    foreach (var group in groups)
                    {
                        lineSeries.Points.AddXY(group.Conditions, group.Sum);
                    }

                    chart1.Series.Add(lineSeries);
                    break;

                case "長條圖":
                default:
                    foreach (var group in groups)
                    {
                        var series = new Series(group.Conditions)
                        {
                            ChartType = SeriesChartType.StackedColumn,
                            IsValueShownAsLabel = true
                        };
                        series.Points.Add(group.Sum);
                        chart1.Series.Add(series);
                    }
                    break;
            }
        }
    }
}