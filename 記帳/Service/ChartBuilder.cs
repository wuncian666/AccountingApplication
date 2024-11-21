using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartGraph = System.Windows.Forms.DataVisualization.Charting.Chart;
using 記帳.Chart;
using 記帳.Chart.ChartTypes;
using 記帳.Models;
using 記帳.Models.ModelTypes;
using System.Windows.Forms.DataVisualization.Charting;

namespace 記帳.Service
{
    internal class ChartBuilder
    {
        private List<Series> series = new List<Series>();

        private DataGroupByService dataGroupByService = new DataGroupByService();

        private List<Record> data = null;
        private Dictionary<string, HashSet<string>> optionsForAllCheckBoxes = null;

        private List<GroupAccountingModel> groups = null;

        public ChartBuilder()
        {
        }

        public ChartBuilder SetRecord(List<Record> records)
        {
            this.data = records;

            return this;
        }

        public ChartBuilder SetOptionsForAllCheckBox(Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            this.optionsForAllCheckBoxes = optionsForAllCheckBoxes;

            return this;
        }

        public ChartBuilder GroupData()
        {
            //根據日期內容取得csv內的資料 OK
            //將csv內的資料進行group OK
            //選擇圖表類型
            //根據圖表類型進行資料重組
            //創建Series填充資料與設計
            //透過builder產生Series並傳入View進行繪製

            this.groups =
                this.dataGroupByService.GroupToAccountingModel(
                    this.data,
                    this.optionsForAllCheckBoxes);

            return this;
        }

        public ChartBuilder BindingData(SeriesChartType chartType)
        {
            this.series.Clear();
            ChartType type = SeriesFactory.GetChartType(chartType);
            this.series = type.GetSeries(this.groups);

            return this;
        }

        public ChartGraph Build()
        {
            if (this.series == null)
            {
                throw new Exception("Series is not set");
            }

            var chart = new ChartGraph();
            chart.Width = 500; // 設定寬度
            chart.Height = 300; // 設定高度
            chart.ChartAreas.Add(new ChartArea("ChartArea1")); // 必須添加 ChartArea

            foreach (var serie in this.series)
            {
                serie.ChartArea = "ChartArea1";
                chart.Series.Add(serie);
            }
            return chart;
        }
    }
}