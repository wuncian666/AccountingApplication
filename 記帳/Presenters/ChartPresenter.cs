using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳.Contracts;
using 記帳.Models;
using 記帳.Models.ModelTypes;
using 記帳.Repository;
using 記帳.Service;
using 記帳.Utils;

namespace 記帳.Presenters
{
    internal class ChartPresenter : IChartPresenter
    {
        private IChartView view = null;

        private CsvService csvService = new CsvService();
        private DataGroupByService dataGroupByService = new DataGroupByService();

        public ChartPresenter(IChartView view)
        {
            this.view = view;
        }

        public void GetDataRangeRecord(
            DateTime picker1,
            DateTime picker2,
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            List<Record> data =
                this.csvService.GetDateRangeRecord(picker1, picker2);

            // 給圓餅圖
            List<GroupAccountingModel> groupData =
                this.dataGroupByService.GroupToAccountingModel(
                    data,
                    optionsForAllCheckBoxes);

            // 給折線圖

            // 給長條圖

            //var allDates = DateUtils.GetTimeRange(picker1, picker2);
            //Console.WriteLine($"AllDate Count:= {allDates.Count}");

            //List<RecordForStackedColumn> recordForStackedColumn =
            //    this.dataGroupByService.GroupByForStackedColumn(
            //        allDates,
            //        data,
            //        optionsForAllCheckBoxes);

            //Dictionary<string, List<int>> seriesData =
            //    this.dataGroupByService.GenerateSeriesData(
            //        recordForStackedColumn,
            //        allDates);

            this.view.DrawingChart(groupData);
        }
    }
}