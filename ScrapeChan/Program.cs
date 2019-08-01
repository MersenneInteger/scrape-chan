using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeChan
{
    class Program
    {
        static void Main(string[] args)
        {
            //<a class="fileThumb" href=""
            Scraper scraper = new Scraper();
            string url = "https://is2.4chan.org/wg/1562245792289.png";
            string savePath = string.Empty;

            savePath = CreateDirectoryToSavePictures();

            if (url.Contains("https"))
                url.Replace("https", "http");

            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(new Uri(url), $"{savePath}\\image35.png");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// determine default path to create new directory in, return path
        /// </summary>
        /// <returns>String</returns>
        private static string CreateDirectoryToSavePictures()
        {
            var savePath = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                savePath += @"\chanPics\";
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                //
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //
            }
            return savePath;
        }
    }
}
