using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Models.ModelTypes;

namespace 記帳.Chart.ChartTypes
{
    internal abstract class ChartType
    {
        public abstract List<Series> GetSeries(
            List<GroupAccountingModel> groups);
    }
}