using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace 記帳.Utils
{
    class ImageCompress
    {
        public static EncoderParameters CompressImage(int quality)
        {
            Encoder QualityEncoder = Encoder.Quality;
            EncoderParameters parameters = new EncoderParameters(1);
            EncoderParameter parameter = new EncoderParameter(QualityEncoder, quality);
            parameters.Param[0] = parameter;
            return parameters;
        }

        public static Bitmap Compress(Image image, EncoderParameters parameters)
        {
            ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, encoder, parameters);
            Bitmap bitmap = new Bitmap(memoryStream);
            return new Bitmap(bitmap, 50, 50);
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
