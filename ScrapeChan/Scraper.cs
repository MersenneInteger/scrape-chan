using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrapeChan
{
    public class Scraper
    {
        private List<string> ImageLinks;

        public Scraper()
        {
            ImageLinks = new List<string>();
        }

        public List<string> Scrape(string webPage)
        {
            MatchCollection matches = Regex.Matches(webPage, @"<a class=\""fileThumb\"" href=\""(.*?)\""");
     
            foreach(Match match in matches)
                ImageLinks.Add(match.Groups[1].Value);
            return ImageLinks;
        }

        public void PrependHTTP()
        {
            for(var i = 0; i < ImageLinks.Count; i++)
                ImageLinks[i] = "http:" + ImageLinks[i];

            //foreach (var image in ImageLinks)
            //    Console.WriteLine(image);
        }
    }
}
