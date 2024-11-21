using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Chart.ChartTypes;

namespace 記帳.Chart
{
    internal class SeriesFactory
    {
        public static ChartType GetChartType(SeriesChartType chartType)
        {
            ChartType result = null;
            switch (chartType)
            {
                case SeriesChartType.Pie:
                    result = new Pie();
                    break;

                case SeriesChartType.Line:
                    result = new Line();
                    break;

                case SeriesChartType.StackedColumn:
                    result = new StackedColumn();
                    break;
            }

            return result;
        }
    }
}