using System;
using System.Collections.Generic;
using 記帳.Contracts;
using 記帳.Models;
using 記帳.Repository;

namespace 記帳.Presenters
{
    internal class AddRecordPresenter : Contracts.IAddRecordPresenter
    {
        IAddRecordView view = null;

        CsvService csvService = new CsvService();

        public AddRecordPresenter(IAddRecordView view)
        {
            this.view = view;
        }

        public void AddRecord(String path, List<Record> records)
        {
            csvService.Write(path, records);
            this.view.OnAddRecord();
        }
    }
}
