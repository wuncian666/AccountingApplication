using System.ComponentModel;

namespace 記帳.Models
{
    internal class Record
    {
        [DisplayName("總計")]
        public string Amount { get; set; }
        [DisplayName("類型")]
        public string Type { get; set; }
        [DisplayName("項目")]
        public string Item { get; set; }
        [DisplayName("付款方式")]
        public string PaymentMethod { get; set; }
        [DisplayName("對象")]
        public string Target { get; set; }
        [DisplayName("圖片1路徑")]
        public string image1 { get; set; }
        [DisplayName("圖片2路徑")]
        public string image2 { get; set; }

        public Record() { }

        public Record(string amount, string type, string item, string paymentMethod, string target, string image1, string image2)
        {
            Amount = amount;
            Type = type;
            Item = item;
            PaymentMethod = paymentMethod;
            Target = target;
            this.image1 = image1;
            this.image2 = image2;
        }
    }
}
