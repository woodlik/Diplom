using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GoSport.Client.Infrastructure
{
    public static class ImageHelper
    {
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public static ICollection<string> SanitizeImageUrls(string[] urls)
        {
            for (int i = 0; i < urls.Count(); i++)
            {
                var startPoint = urls[i].IndexOf("Content");
                urls[i] = urls[i].Substring(startPoint - 1);
            }

            return urls;
        }

        public static string SaveImage(string path,HttpPostedFileBase image, string sportCenterName, int picturerNumber)
        {
            var imageUrl = path + sportCenterName + picturerNumber + ".jpg";
            using (FileStream output = System.IO.File.OpenWrite(imageUrl))
            {
                CopyStream(image.InputStream, output);
            }

            return imageUrl;
        }
    }
}