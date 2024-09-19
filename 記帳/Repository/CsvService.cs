using CSVHelper;
using System;
using System.Collections.Generic;
using System.IO;
using 記帳.Models;

namespace 記帳.Repository
{
    internal class CsvService
    {
        public void Write(String currentDayPath, List<Record> records)
        {
            CSV.Write(currentDayPath + "\\record.csv", records, true, false);
        }

        public List<Record> GetDateRangeRecord(DateTime picker1, DateTime picker2)
        {
            List<Record> data = new List<Record>();
            var range = picker2 - picker1;
            int days = range.Days;

            var temp = picker1;
            for (int i = 0; i <= days; i++)
            {
                string dayPath = "D:\\files\\" + temp.ToString("yyyy_MM_dd");
                if (Directory.Exists(dayPath))
                {
                    data.AddRange(CSV.Read<Record>(dayPath + "\\record.csv", false));
                }
                temp = temp.AddDays(1);
            }
            return data;
        }
    }
}
