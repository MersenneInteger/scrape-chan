using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ScrapeChan
{
    class Program
    {
        static void Main(string[] args)
        {

            Scraper scraper = new Scraper();
            string url, webPage;
            string savePath = string.Empty;
            int i = 0;

            savePath = CreateDirectoryToSavePictures();

            Console.WriteLine("Enter 4chan url: ");
            url = Console.ReadLine() ?? string.Empty;

            if (url.Contains("https"))
                url.Replace("https", "http");

            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Headers.Add("user-agent", "Mozilla/5.0");
                    webPage = client.DownloadString(url);

                    List<string> ImageLinks = scraper.Scrape(webPage);
                    scraper.PrependHTTP();
                    Console.WriteLine("prepending done");
                    foreach (var image in ImageLinks)
                    {
                        client.DownloadFile(new Uri(image), $"{savePath}\\image{++i}.png");
                        Console.Write("*");
                    }

                    Console.WriteLine("\ndone...");
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
