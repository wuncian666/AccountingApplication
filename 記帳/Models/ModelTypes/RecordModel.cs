namespace 記帳.Models.ModelTypes
{
    internal class RecordModel
    {
        public string Type { get; set; }
        public string PaymentMethod { get; set; }
        public string Target { get; set; }
        public int Sum { get; set; }
        public int Count { get; set; }
    }

    public class GroupAccountingModel
    {
        public string Conditions { get; set; }
        public int Sum { get; set; }
        public int Count { get; set; }
    }

    public class RecordForStackedColumn
    {
        public string Date { get; set; }
        public string Conditions { get; set; }
        public int Sum { get; set; }
        public int Count { get; set; }
    }
}