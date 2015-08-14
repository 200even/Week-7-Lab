using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Week7_Lab.Models
{
    public class Pin
    {
        public int PinId { get; set; }

        public PinterestUser WhoPinned { get; set; }

        public byte[] Image { get; set; }
        public string ImageLink { get; set; }

        [MaxLength(1000)]
        public string Note { get; set; }

        public static byte[] ScaleImage(byte[] source, int maxWidth, int maxHeight)
        {
            var image = System.Drawing.Image.FromStream(new MemoryStream(source));

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            Bitmap bmp = new Bitmap(newImage);

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

    }
}