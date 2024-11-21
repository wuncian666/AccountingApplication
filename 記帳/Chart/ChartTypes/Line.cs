using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Models.ModelTypes;

namespace 記帳.Chart.ChartTypes
{
    internal class Line : ChartType
    {
        public override List<Series> GetSeries(List<GroupAccountingModel> groups)
        {
            List<Series> series = new List<Series>();
            SeriesChartType chartType = SeriesChartType.Line;
            Series seriesLine = new Series(chartType.ToString())
            {
                ChartType = chartType,
                IsValueShownAsLabel = true
            };

            foreach (var group in groups)
            {
                seriesLine.Points.AddXY(group.Conditions, group.Sum);
            }
            series.Add(seriesLine);

            return series;
        }
    }
}