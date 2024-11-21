using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using ChartGraph = System.Windows.Forms.DataVisualization.Charting.Chart;

using 記帳.Models;
using 記帳.Models.ModelTypes;

namespace 記帳.Contracts
{
    public interface IChartPresenter
    {
        void GetDataRangeRecord(
            DateTime picker1,
            DateTime picker2,
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes);

        void ChartTypeChanged(
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes);
    }

    public interface IChartView
    {
        void DrawingChart(ChartGraph chart);

        SeriesChartType GetSealectedChartType();
    }
}