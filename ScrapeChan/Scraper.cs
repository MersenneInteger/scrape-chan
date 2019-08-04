using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ScrapeChan
{
    public class Scraper
    {
        private List<string> ImageLinks;
        private enum ChanType { _4chan, _lainchan};
        private ChanType chanType;

        public Scraper()
        {
            ImageLinks = new List<string>();
        }

        /// <summary>
        /// give an html webpage, find all matches that meet the criteria for an image file
        /// </summary>
        /// <param name="webPage"></param>
        /// <returns>List<string></returns>
        public List<string> Scrape(string webPage, string url)
        {
            MatchCollection matches;
            GetChanType(url);

            if (chanType == ChanType._4chan)
                matches = Regex.Matches(webPage, @"<a class=\""fileThumb\"" href=\""(.*?)\""");
            else if (chanType == ChanType._lainchan)
                matches = Regex.Matches(webPage, @"<div class=\""file\""><p class=\""fileinfo\"">File: <a href=\""(.*?)\"" target=\""_blank\"">");
            else
                matches = Regex.Matches(webPage, @"^.*\.(jpg|png|jpeg|gif)");

            foreach (Match match in matches)
                ImageLinks.Add(match.Groups[1].Value);

            PrependHTTP();

            return ImageLinks;
        }

        /// <summary>
        /// if 4chan, prepend "http:" to all links if it is not already there
        /// if Lainchan, prepend "http://lainchan.org" to all links
        /// this is due to the html tags only containing the internal link
        /// </summary>
        private void PrependHTTP()
        {
            if(chanType == ChanType._4chan)
            {
                for (var i = 0; i < ImageLinks.Count; i++)
                    if (!ImageLinks[i].Contains("http:"))
                        ImageLinks[i] = "http:" + ImageLinks[i];
            }
            else if(chanType == ChanType._lainchan)
            {
                for (var i = 0; i < ImageLinks.Count; i++)
                    if (!ImageLinks[i].Contains("http:"))
                        ImageLinks[i] = "http://lainchan.org" + ImageLinks[i];
            }
        }

        /// <summary>
        /// determine default path to create new directory in, return path
        /// </summary>
        /// <returns>String</returns>
        public string CreateDirectoryToSavePictures()
        {
            var savePath = string.Empty;

            savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            savePath += @"\chanPics\";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            return savePath;
        }

        /// <summary>
        /// determine whether lainchan or 4chan is being scraped
        /// </summary>
        /// <param name="url">Url of website to be scraped</param>
        private ChanType GetChanType(string url)
        {
            if (url.Contains("4chan"))
                chanType = ChanType._4chan;
            else if (url.Contains("lainchan"))
                chanType = ChanType._lainchan;
            return chanType;
        }
    }
}
