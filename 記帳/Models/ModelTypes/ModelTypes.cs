using System.Collections.Generic;

namespace 記帳.Models.ModelTypes
{
    internal class ModelTypes
    {
        public static string[] types =
         {
                "飲食", "日常用品", "交通", "水電瓦斯", "電話網路", "娛樂", "醫療", "其他"
            };

        public static string[] paymentMethods =
        {
                "現金", "信用卡", "金融卡"
            };

        public static string[] targets =
        {
                "自己", "家人", "朋友", "其他"
            };

        private static string[] dietItems =
           {
                "早餐", "午餐", "晚餐", "宵夜", "飲料", "水果", "零食", "其他"
            };

        private static string[] dailyItems =
        {
                "洗衣粉", "洗髮精", "沐浴乳", "牙膏", "牙刷", "衛生紙", "其他"
            };

        private static string[] trafficItems =
        {
                "公車", "捷運", "計程車", "其他"
            };

        private static string[] utilitiesItems =
        {
                "水費", "電費", "瓦斯費", "其他"
            };

        private static string[] phoneItems =
        {
                "手機費", "市話費", "網路費", "其他"
            };

        private static string[] entertainmentItems =
        {
                "電影", "遊戲", "音樂", "書籍", "其他"
            };

        private static string[] medicalItems =
        {
                "看診", "藥品", "其他"
            };

        private static string[] otherItems =
        {
                "其他"
            };

        public static Dictionary<string, string[]> allOptions = new Dictionary<string, string[]>()
        {
            {"所有類別", types },
            {"所有付款方式", paymentMethods },
            {"所有對象", targets }
        };

        public static Dictionary<string, string[]> typesMap = new Dictionary<string, string[]>
        {
            {"飲食", dietItems},
            {"日常用品", dailyItems},
            {"交通", trafficItems},
            {"水電瓦斯", utilitiesItems},
            {"電話網路", phoneItems},
            {"娛樂", entertainmentItems},
            {"醫療", medicalItems},
            {"其他", otherItems}
        };

        private static Dictionary<int, string[]> items = new Dictionary<int, string[]>
            {
                { 0, dietItems },
                { 1, dailyItems },
                { 2, trafficItems },
                { 3, utilitiesItems },
                { 4, phoneItems },
                { 5, entertainmentItems },
                { 6, medicalItems },
                { 7, otherItems }
            };

        public static string[] GetItems(int index)
        {
            return items[index];
        }
    }
}