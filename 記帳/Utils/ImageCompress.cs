using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace 記帳.Utils
{
    class ImageCompress
    {
        public static Bitmap Compress(Image image, int quality, bool isOrigin)
        {
            MemoryStream memoryStream = new MemoryStream();
            var encoder = GetEncoder(ImageFormat.Jpeg);
            var parameters = GetCompressParameters(quality);
            image.Save(memoryStream, encoder, parameters);

            if (isOrigin)
            {
                return new Bitmap(memoryStream);
            }

            Bitmap bitmap = new Bitmap(memoryStream);
            return new Bitmap(bitmap, 50, 50);
        }

        private static EncoderParameters GetCompressParameters(int quality)
        {
            Encoder QualityEncoder = Encoder.Quality;
            EncoderParameters parameters = new EncoderParameters(1);
            EncoderParameter parameter = new EncoderParameter(QualityEncoder, quality);
            parameters.Param[0] = parameter;
            return parameters;
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
