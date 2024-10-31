using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Contracts;
using 記帳.Models.ModelTypes;
using 記帳.Presenters;

namespace 記帳.Forms
{
    public partial class ChartForm : Form, Contracts.IChartView
    {
        private readonly IChartPresenter chartPresenter = null;

        private readonly List<string> typeCheckBoxOptions = new List<string>();
        private readonly List<string> paymentCheckBoxOptions = new List<string>();
        private readonly List<string> targetCheckBoxOptions = new List<string>();

        public ChartForm()
        {
            InitializeComponent();
            this.chartPresenter = new ChartPresenter(this);

            this.comboBox1.Items.Add("圓餅圖");
            this.comboBox1.Items.Add("折線圖");
            this.comboBox1.Items.Add("長條圖");
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            // 運用今天教的group by程式繪製 圓餅圖
            // 圓餅 折線 長條圖
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // get data
            this.chartPresenter.GetDataRangeRecord(
                dateTimePicker1.Value,
                dateTimePicker2.Value,
                this.typeCheckBoxOptions,
                this.paymentCheckBoxOptions,
                this.targetCheckBoxOptions);
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

        void IChartView.DrawingChart(List<GroupAccountingModel> data)
        {
            chart1.Series.Clear();
            //Series series = chart1.Series.Add("data");
            List<String> conditions = data.Select(x => x.Conditions).ToList();
            List<int> sum = data.Select(x => x.Sum).ToList();

            Series series = new Series();
            //series.ChartType = SeriesChartType.Bar;

            series.Points.DataBindXY(conditions, sum);
            series.Label = "#PERCENT";
            series.IsValueShownAsLabel = true;

            series.Color = Color.Blue;
            series.BorderWidth = 1;

            series["PointWidth"] = "0.6"; // 設定長條寬度

            Legend legend2 = new Legend("條件");
            legend2.Title = "图例";
            legend2.TitleBackColor = Color.Transparent;
            legend2.BackColor = Color.Transparent;
            legend2.TitleForeColor = Color.Blue;
            legend2.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            legend2.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            legend2.ForeColor = Color.Blue;

            chart1.Legends[0].Docking = Docking.Left;
            series.LegendText = legend2.Name;
            //chart1.Size = new Size(800, 500);

            chart1.Series.Add(series);

            //chart1.ChartAreas[0].InnerPlotPosition = new ElementPosition(10, 5, 85 , 85);
            //chart1.Height = 500;
            chart1.ChartAreas[0].Position = new ElementPosition(5, -10, 100, 100);

            chart1.ChartAreas[0].AxisX.IsMarginVisible = true; // 自動調整X軸的間距
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount; // 自動調整刻度間距

            chart1.ChartAreas[0].AxisY.IsMarginVisible = true; // 自動調整Y軸的間距
            chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount; // 自動調整刻度間距

            //chart1.ChartAreas[0].AxisX.Interval = 1;
            //chart1.ChartAreas[0].AxisX.Maximum = conditions.Count;

            //chart1.ChartAreas[0].AxisY.Minimum = 0;
            //chart1.ChartAreas[0].AxisY.Maximum = sum.Max() * 1.2;
            chart1.ChartAreas[0].RecalculateAxesScale();

            chart1.ChartAreas[0].BackColor = Color.LightGray;

            // 自動調整字體大小，避免數字重疊
            chart1.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;
            chart1.ChartAreas[0].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;

            // 圖表填滿表單
            //chart1.Dock = DockStyle.Fill;

            //chart1.Titles.Add("圖表標題");

            // 顯示多條曲線或多種圖表類型
            //Series series2 = new Series("項目2");
            //series2.ChartType = SeriesChartType.StackedBar;
            //series2.Points.DataBindXY(conditions, sum);
            //chart1.Series.Add(series2);
            //chart1.Series[0].Label = "#VAL";
            //長條圖
            //series.ChartType = SeriesChartType.Bar;
            //圓餅圖
            //series.ChartType = SeriesChartType.Pie;
            //折線圖
            //series.ChartType = SeriesChartType.Line;

            //series.Points.AddXY("A", 10);
            //series.Points.AddXY("B", 30);
            //series.Points.AddXY("C", 10);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox1.SelectedItem.ToString())
            {
                case "圓餅圖":
                    chart1.Series[0].ChartType = SeriesChartType.Pie;
                    break;

                case "折線圖":
                    chart1.Series[0].ChartType = SeriesChartType.Line;
                    break;

                case "長條圖":
                    chart1.Series[0].ChartType = SeriesChartType.Bar;
                    break;
            }
        }
    }
}