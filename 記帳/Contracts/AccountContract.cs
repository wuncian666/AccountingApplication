using System;
using System.Collections.Generic;
using 記帳.Models.ModelTypes;

namespace 記帳.Contracts
{
    internal interface IAccountPresenter
    {
        void GetDataRangeRecord(
            DateTime picker1,
            DateTime picker2,
            List<string> typeCheckBoxOptions,
            List<string> paymentCheckBoxOptions,
            List<string> targetCheckBoxOptions);
    }

    internal interface IAccountView
    {
        void ResetView(List<GroupAccountingModel> records);
    }
}