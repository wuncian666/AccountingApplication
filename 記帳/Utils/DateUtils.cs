using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳.Utils
{
    internal static class DateUtils
    {
        public static List<string> GetTimeRange(DateTime dateTime1, DateTime dateTime2)
        {
            return Enumerable.Range(0, (dateTime2 - dateTime1).Days + 1)
                .Select(offset => dateTime1.AddDays(offset).ToString("yyyy-MM-dd"))
                .ToList();
        }
    }
}