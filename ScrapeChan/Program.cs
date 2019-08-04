using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Text;

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

            savePath = scraper.CreateDirectoryToSavePictures();

            Console.WriteLine("Enter chan url: ");
            url = Console.ReadLine() ?? string.Empty;

            if (url.Contains("https"))
                url.Replace("https", "http");

            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Headers.Add("user-agent", "Mozilla/5.0");
                    webPage = client.DownloadString(url);

                    List<string> ImageLinks = scraper.Scrape(webPage, url);

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
    }
}
