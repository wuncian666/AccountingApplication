using System;
using System.Collections.Generic;
using 記帳.Models;

namespace 記帳.Contracts
{
    public interface IRecordPresenter
    {
        void GetDataRangeRecord(DateTime picker1, DateTime picker2);

        void EditRecord(string path, List<Record> records);

        void DeleteRecord(string path, List<Record> records);

        void OnClickImage(string path);
    }

    public interface IRecordView
    {
        void ResetView(List<Record> records);
    }
}
