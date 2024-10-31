using System;
using System.Collections.Generic;
using 記帳.Models;

namespace 記帳.Contracts
{
    public interface IAddRecordPresenter
    {
        void AddRecord(String path, List<Record> records);
    }

    public interface IAddRecordView
    {
        void OnAddRecord();
    }
}
