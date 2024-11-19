using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳.Models;
using 記帳.Models.ModelTypes;

namespace 記帳.Service
{
    internal class DataGroupByService
    {
        public List<GroupAccountingModel> GroupToAccountingModel(
            List<Record> data,
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            if (this.isAllOptionsEmpty(optionsForAllCheckBoxes))
            {
                return data.Select(g => new GroupAccountingModel
                {
                    Conditions = g.Type + " " + g.PaymentMethod + " " + g.Target,
                    Sum = int.Parse(g.Amount),
                    Count = 1,
                }).ToList();
            }

            List<Record> dataNeedToBeGrouped = this.GetDataNeedToBeGrouped(data, optionsForAllCheckBoxes);
            Console.WriteLine(dataNeedToBeGrouped.Count());

            var dataHasAlreadyBeenGrouped = dataNeedToBeGrouped.GroupBy(h => new
            {
                Type =
                optionsForAllCheckBoxes["typeCheckBoxOptions"].Count > 0 &&
                optionsForAllCheckBoxes["typeCheckBoxOptions"].Contains(h.Type) ? h.Type : null,
                PaymentMethod =
                optionsForAllCheckBoxes["paymentCheckBoxOptions"].Count > 0 &&
                optionsForAllCheckBoxes["paymentCheckBoxOptions"].Contains(h.PaymentMethod) ? h.PaymentMethod : null,
                Target =
                optionsForAllCheckBoxes["targetCheckBoxOptions"].Count > 0 &&
                optionsForAllCheckBoxes["targetCheckBoxOptions"].Contains(h.Target) ? h.Target : null
            });

            var groupAccountingModels = dataHasAlreadyBeenGrouped.Select(g => new GroupAccountingModel
            {
                Conditions = g.Key.Type + " " + g.Key.PaymentMethod + " " + g.Key.Target,
                Sum = (int)g.Sum(a => int.Parse(a.Amount)),
                Count = g.Count(),
            }).ToList();

            return groupAccountingModels;
        }

        public Dictionary<string, List<int>> GenerateSeriesData(List<RecordForStackedColumn> processedData, List<string> allDates)
        {
            var seriesData = processedData
                .GroupBy(record => record.Conditions)
                .ToDictionary(
                group => group.Key,
                group => allDates.Select(date =>
                group.FirstOrDefault(record => record.Date == date)?.Sum ?? 0)
                    .ToList());

            return seriesData;
        }

        public List<RecordForStackedColumn> GroupByForStackedColumn(
            List<string> allDates,
            List<Record> data,
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            if (this.isAllOptionsEmpty(optionsForAllCheckBoxes))
            {
                return data.Select(g => new RecordForStackedColumn
                {
                    Conditions = g.Type + " " + g.PaymentMethod + " " + g.Target,
                    Date = g.Date,
                    Sum = int.Parse(g.Amount),
                    Count = 1,
                }).ToList();
            }

            List<Record> dataNeedToBeGrouped = this.GetDataNeedToBeGrouped(data, optionsForAllCheckBoxes);
            Console.WriteLine($"FilterData Count: {dataNeedToBeGrouped.Count}");

            var groupedData = dataNeedToBeGrouped
                .GroupBy(record => new
                {
                    Type =
                    optionsForAllCheckBoxes["typeCheckBoxOptions"].Count > 0 &&
                    optionsForAllCheckBoxes["typeCheckBoxOptions"].Contains(record.Type) ? record.Type : null,
                    PaymentMethod =
                    optionsForAllCheckBoxes["paymentCheckBoxOptions"].Count > 0 &&
                    optionsForAllCheckBoxes["paymentCheckBoxOptions"].Contains(record.PaymentMethod) ? record.PaymentMethod : null,
                    Target =
                    optionsForAllCheckBoxes["targetCheckBoxOptions"].Count > 0 &&
                    optionsForAllCheckBoxes["targetCheckBoxOptions"].Contains(record.Target) ? record.Target : null
                })
                .Select(group => new
                {
                    GroupKey = group.Key,
                    Records = group.GroupBy(record => record.Date)
                    .ToDictionary(
                        g => g.Key,
                        g => new
                        {
                            Sum = g.Sum(r => int.TryParse(r.Amount, out var amount) ? amount : 0),
                            Count = g.Count(),
                        })
                })
                .ToList();

            var result = new List<RecordForStackedColumn>();
            foreach (var group in groupedData)
            {
                foreach (var date in allDates)
                {
                    var recordForDate = group.Records.TryGetValue(date, out var record)
                        ? new RecordForStackedColumn
                        {
                            Date = date,
                            Conditions = $"{group.GroupKey.Type} {group.GroupKey.PaymentMethod}",
                            Sum = record.Sum,
                            Count = record.Count
                        }
                        : new RecordForStackedColumn
                        {
                            Date = date,
                            Conditions = $"{group.GroupKey.Type} {group.GroupKey.PaymentMethod}",
                            Sum = 0,
                            Count = 0
                        };

                    result.Add(recordForDate);
                }
            }

            return result;

            //var groupedData = dataNeedToBeGrouped

            //    .GroupBy(record => record.Date)
            //    .Select(group => new RecordForStackedColumn
            //    {
            //        Date = group.Key,
            //        Sum = group.Sum(record => int.TryParse(record.Amount, out var amount) ? amount : 0),
            //        Count = group.Count(),
            //        Conditions = string.Join(" ", group.Select(record => record.Type + " " + record.PaymentMethod + " " + record.Target))
            //    })
            //    .ToDictionary(g => g.Date);

            //var result = allDates.Select(date =>
            //groupedData.TryGetValue(date, out var existingRecord)
            //? existingRecord : new RecordForStackedColumn
            //{
            //    Date = date,
            //    Sum = 0,
            //    Count = 0,
            //    Conditions = "Empty" // 用來標記補足的資料
            //}).ToList();

            //return result;

            //var result = data
            //    .Where(h =>
            //        (typeCheckBoxOptions.Count == 0 || typeCheckBoxOptions.Contains(h.Type)) &&
            //        (paymentCheckBoxOptions.Count == 0 || paymentCheckBoxOptions.Contains(h.PaymentMethod)) &&
            //        (targetCheckBoxOptions.Count == 0 || targetCheckBoxOptions.Contains(h.Target))
            //        );

            //var result2 = result.GroupBy(h => new
            //{
            //    Type = typeCheckBoxOptions.Count() > 0 &&
            //         typeCheckBoxOptions.Contains(h.Type) ? h.Type : null,

            //    PaymentMethod = paymentCheckBoxOptions.Count() > 0 &&
            //         paymentCheckBoxOptions.Contains(h.PaymentMethod) ? h.PaymentMethod : null,

            //    Target = targetCheckBoxOptions.Count() > 0 &&
            //         targetCheckBoxOptions.Contains(h.Target) ? h.Target : null,

            //    h.Date
            //});

            //var groupedData = result2.Select(g => new RecordForStackedColumn
            //{
            //    Conditions = g.Key.Type + " " + g.Key.PaymentMethod + " " + g.Key.Target,
            //    Date = g.Key.Date,
            //    Sum = (int)g.Sum(a => int.Parse(a.Amount)),
            //    Count = g.Count(),
            //}).ToList();

            //return groupedData;
        }

        private bool isAllOptionsEmpty(
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            return optionsForAllCheckBoxes.All(options => options.Value.Count == 0);
        }

        private List<Record> GetDataNeedToBeGrouped(
            List<Record> data,
            Dictionary<string, HashSet<string>> optionsForAllCheckBoxes)
        {
            return data.Where(h =>
                (optionsForAllCheckBoxes["typeCheckBoxOptions"].Count == 0 || optionsForAllCheckBoxes["typeCheckBoxOptions"].Contains(h.Type)) &&
                (optionsForAllCheckBoxes["paymentCheckBoxOptions"].Count == 0 || optionsForAllCheckBoxes["paymentCheckBoxOptions"].Contains(h.PaymentMethod)) &&
                (optionsForAllCheckBoxes["targetCheckBoxOptions"].Count == 0 || optionsForAllCheckBoxes["targetCheckBoxOptions"].Contains(h.Target))
            ).ToList();
        }
    }
}