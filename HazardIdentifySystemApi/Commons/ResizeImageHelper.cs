using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Commons
{
    public class ResizeImageHelper
    {

        #region SingelTon
        private static object lockObj = new object();
        private ResizeImageHelper() { }
        private static ResizeImageHelper _instance;
        public static ResizeImageHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new ResizeImageHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// Giam kich thuoc cua hinh anh : neu kich thuoc goc>=5mb-> giam xuong 5mb, neu kich thuoc nho hon 5mb -> thi giu nguyen
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="OutImageSize"></param>
        /// <param name="OutImageByteArr"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public bool ReduceImageSize(string inputPath, out string OutImageSize , out byte[] OutImageByteArr, long targetSize= 5 * 1024 * 1024)
        {
            OutImageSize = "";
            using (Image image = Image.FromFile(inputPath))
            {
                long quality = 65; // Start with high quality
                MemoryStream ms = new MemoryStream();

                do
                {
                    ms.SetLength(0); // Clear the memory stream
                    SaveImageWithQuality(image, ms, quality);
                    quality -= 5; // Reduce quality incrementally
                } while (ms.Length > targetSize && quality > 0);

                if (ms.Length <= targetSize)
                {
                    OutImageByteArr = ms.ToArray();
                    OutImageSize = $"{ms.Length / 1024 / 1024.0:F2} MB";
                    return true;
                  //  File.WriteAllBytes(outputPath, ms.ToArray());
                  //  Console.WriteLine($"Image successfully reduced to {ms.Length / 1024 / 1024.0:F2} MB");
                }
                else
                {
                    OutImageByteArr = null;
                    return false;
                 //   Console.WriteLine("Unable to reduce image to 5MB");
                }
            }
        }

        private void SaveImageWithQuality(Image image, Stream stream, long quality)
        {
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            image.Save(stream, jpegCodec, encoderParams);
        }

        private  ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == mimeType)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}