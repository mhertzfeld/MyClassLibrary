using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace MyClassLibrary.Imaging
{
    public static class ImageConverter
    {
        public static Byte[] ConvertToByteArray(Image image, ImageFormat imageFormat)
        {
            MemoryStream memoryStream = new MemoryStream();

            image.Save(memoryStream, imageFormat);

            Byte[] byteArray = memoryStream.ToArray();

            memoryStream.Close();

            memoryStream.Dispose();

            return byteArray;
        }
    }
}