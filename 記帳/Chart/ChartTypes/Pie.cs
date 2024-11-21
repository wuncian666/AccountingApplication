using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Models.ModelTypes;

namespace 記帳.Chart.ChartTypes
{
    internal class Pie : ChartType
    {
        public override List<Series> GetSeries(List<GroupAccountingModel> groups)
        {
            List<Series> series = new List<Series>();
            SeriesChartType chartType = SeriesChartType.Pie;
            Series seriesPie = new Series(chartType.ToString())
            {
                ChartType = chartType,
                IsValueShownAsLabel = true
            };

            foreach (var group in groups)
            {
                seriesPie.Points.AddXY(group.Conditions, group.Sum);
            }
            series.Add(seriesPie);

            return series;
        }
    }
}