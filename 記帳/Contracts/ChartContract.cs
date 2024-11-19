using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳.Models.ModelTypes;

namespace 記帳.Contracts
{
    public interface IChartPresenter
    {
        void GetDataRangeRecord(
            DateTime picker1,
            DateTime picker2,
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes);
    }

    public interface IChartView
    {
        void DrawingChart(List<GroupAccountingModel> groups);
    }
}