using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ScrapeChan
{
    public class Scraper
    {
        private List<string> ImageLinks;

        public Scraper()
        {
            ImageLinks = new List<string>();
        }

        /// <summary>
        /// give an html webpage, find all matches that meet the criteria for an image file
        /// </summary>
        /// <param name="webPage"></param>
        /// <returns>List<string></returns>
        public List<string> Scrape(string webPage)
        {
            MatchCollection matches = Regex.Matches(webPage, @"<a class=\""fileThumb\"" href=\""(.*?)\""");
            //MatchCollection matches = Regex.Matches(webPage, @"<a href=\""(.*?)\"" target=\""_blank\""><img class=\""post-image\""");

            foreach (Match match in matches)
                ImageLinks.Add(match.Groups[1].Value);
            return ImageLinks;
        }

        /// <summary>
        /// prepend "http:" to all links if it is not already there
        /// </summary>
        public void PrependHTTP()
        {

            for(var i = 0; i < ImageLinks.Count; i++)
                if(!ImageLinks[i].Contains("http:"))
                    ImageLinks[i] = "http:" + ImageLinks[i];
        }

        /// <summary>
        /// determine default path to create new directory in, return path
        /// </summary>
        /// <returns>String</returns>
        public string CreateDirectoryToSavePictures()
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
