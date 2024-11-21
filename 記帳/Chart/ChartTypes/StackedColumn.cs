using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Models.ModelTypes;

namespace 記帳.Chart.ChartTypes
{
    internal class StackedColumn : ChartType
    {
        public override List<Series> GetSeries(List<GroupAccountingModel> groups)
        {
            List<Series> result = new List<Series>();
            var categories = groups.Select(x => x.Conditions).Distinct();
            foreach (var category in categories)
            {
                var series = new Series(category)
                {
                    ChartType = SeriesChartType.StackedColumn,
                    IsValueShownAsLabel = true
                };

                foreach (var group in groups.Where(g => g.Conditions == category))
                {
                    series.Points.AddXY(group.Conditions, group.Sum);
                }

                series.Points.Add(series.Points[0]);

                result.Add(series);
            }

            return result;
        }
    }
}