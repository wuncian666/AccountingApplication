using System.ComponentModel;

namespace 記帳.Models
{
    public class Record
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
        public string Image1 { get; set; }

        [DisplayName("圖片1壓縮路徑")]
        public string CompressImage1 { get; set; }

        [DisplayName("圖片2路徑")]
        public string Image2 { get; set; }

        [DisplayName("圖片2壓縮路徑")]
        public string CompressImage2 { get; set; }

        [DisplayName("日期")]
        public string Date { get; set; }

        public Record()
        { }

        public Record(
            string date,
            string amount,
            string type,
            string item,
            string paymentMethod,
            string target,
            string image1,
            string compressImage1,
            string image2,
            string compressImage2
            )
        {
            this.Date = date;
            this.Amount = amount;
            this.Type = type;
            this.Item = item;
            this.PaymentMethod = paymentMethod;
            this.Target = target;
            this.Image1 = image1;
            this.CompressImage1 = compressImage1;
            this.Image2 = image2;
            this.CompressImage2 = compressImage2;
        }
    }
}