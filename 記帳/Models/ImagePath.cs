using System;

namespace 記帳.Models
{
    internal class ImagePath
    {
        public string Origin { get; set; }
        public string Compress { get; set; }

        public ImagePath(string path)
        {
            string jpg = ".jpg";
            string guid = Guid.NewGuid().ToString();
            Origin = path + "\\" + guid + "_origin" + jpg;
            Compress = path + "\\" + guid + "_compress" + jpg;
        }
    }
}
