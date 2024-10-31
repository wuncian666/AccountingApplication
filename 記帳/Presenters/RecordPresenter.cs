using System;
using System.Collections.Generic;
using 記帳.Contracts;
using 記帳.Forms;
using 記帳.Models;
using 記帳.Repository;

namespace 記帳.Presenters
{
    public class RecordPresenter : IRecordPresenter
    {
        IRecordView view = null;

        CsvService csvService = new CsvService();

        public RecordPresenter(IRecordView view)
        {
            this.view = view;
        }

        public void GetDataRangeRecord(DateTime picker1, DateTime picker2)
        {
            List<Record> data = this.csvService.GetDateRangeRecord(picker1, picker2);
            this.view.ResetView(data);
        }

        public void EditRecord(string path, List<Record> records)
        {
            Console.WriteLine("edit");
            // clean csv
            this.csvService.Clear(path);
            this.csvService.Write(path, records);
        }

        public void DeleteRecord(string path, List<Record> records)
        {
            this.csvService.Clear(path);
            this.csvService.Write(path, records);
            this.view.ResetView(records);
        }

        public void OnClickImage(string path)
        {
            if (path == "" || path == null) return;

            ImageForm imageForm = new ImageForm(path);
            imageForm.ShowDialog();
        }
    }
}
