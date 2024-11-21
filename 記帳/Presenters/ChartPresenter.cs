using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using ChartGraph = System.Windows.Forms.DataVisualization.Charting.Chart;
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

        private ChartBuilder chartBuilder = new ChartBuilder();

        private List<Record> records = new List<Record>();

        public ChartPresenter(IChartView view)
        {
            this.view = view;
        }

        public void GetDataRangeRecord(
            DateTime picker1,
            DateTime picker2,
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            // step1
            this.records =
                this.csvService.GetDateRangeRecord(picker1, picker2);
            NotifyChartTypeChanged(optionsForAllCheckBoxes);
        }

        public void ChartTypeChanged(Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            SeriesChartType selectedType = view.GetSealectedChartType();
            Console.WriteLine(selectedType.ToString());

            if (this.records.Count == 0)
            {
                return;
            }
            NotifyChartTypeChanged(optionsForAllCheckBoxes);
        }

        private void NotifyChartTypeChanged(Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            ChartGraph chart = this.chartBuilder
            .SetRecord(this.records)
            .SetOptionsForAllCheckBox(optionsForAllCheckBoxes)
            .GroupData()
            .BindingData(this.view.GetSealectedChartType())
            .Build();

            this.view.DrawingChart(chart);
        }
    }
}