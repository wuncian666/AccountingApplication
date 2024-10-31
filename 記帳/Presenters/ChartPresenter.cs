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
            List<string> typeCheckBoxOptions,
            List<string> paymentCheckBoxOptions,
            List<string> targetCheckBoxOptions)
        {
            List<Record> data =
                this.csvService.GetDateRangeRecord(picker1, picker2);
            List<GroupAccountingModel> groupData =
                this.dataGroupByService.GroupBy(
                    data,
                    typeCheckBoxOptions,
                    paymentCheckBoxOptions,
                    targetCheckBoxOptions);

            this.view.DrawingChart(groupData);
        }
    }
}