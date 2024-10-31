using System;
using System.Collections.Generic;
using System.Linq;
using 記帳.Contracts;
using 記帳.Models;
using 記帳.Models.ModelTypes;
using 記帳.Repository;
using 記帳.Service;

namespace 記帳.Presenters
{
    internal class AccountingPresenter : IAccountPresenter
    {
        private IAccountView view = null;

        private CsvService csvService = new CsvService();
        private DataGroupByService dataGroupByService = new DataGroupByService();

        public AccountingPresenter(IAccountView view)
        {
            this.view = view;
        }

        public void GetDataRangeRecord(
            DateTime picker1,
            DateTime picker2,
            List<string> typeCheckBoxOptions,
            List<string> paymentCheckBoxOptions,
            List<string> targetCheckBoxOptions)
        {
            List<Record> data = this.csvService.GetDateRangeRecord(picker1, picker2);

            if (typeCheckBoxOptions.Count == 0 &&
                paymentCheckBoxOptions.Count == 0 &&
                targetCheckBoxOptions.Count == 0)
            {
                this.view.ResetView(
                    data.Select(g => new GroupAccountingModel
                    {
                        Conditions = g.Type + " " + g.PaymentMethod + " " + g.Target,
                        Sum = int.Parse(g.Amount),
                        Count = 1,
                    }).ToList());
                return;
            }

            var result = data
                .Where(h =>
                    (typeCheckBoxOptions.Count == 0 || typeCheckBoxOptions.Contains(h.Type)) &&
                    (paymentCheckBoxOptions.Count == 0 || paymentCheckBoxOptions.Contains(h.PaymentMethod)) &&
                    (targetCheckBoxOptions.Count == 0 || targetCheckBoxOptions.Contains(h.Target))
                    );

            Console.WriteLine(result.Count());

            var result2 = result.GroupBy(h => new
            {
                Type = typeCheckBoxOptions.Count() > 0 &&
                     typeCheckBoxOptions.Contains(h.Type) ? h.Type : null,
                PaymentMethod = paymentCheckBoxOptions.Count() > 0 &&
                     paymentCheckBoxOptions.Contains(h.PaymentMethod) ? h.PaymentMethod : null,
                Target = targetCheckBoxOptions.Count() > 0 &&
                     targetCheckBoxOptions.Contains(h.Target) ? h.Target : null
            });

            var result3 = result2.Select(g => new GroupAccountingModel
            {
                Conditions = g.Key.Type + " " + g.Key.PaymentMethod + " " + g.Key.Target,
                Sum = (int)g.Sum(a => int.Parse(a.Amount)),
                Count = g.Count(),
            }).ToList();

            this.view.ResetView(result3);
        }
    }
}