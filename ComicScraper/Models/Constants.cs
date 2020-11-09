using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Models
{
    public static class Constants
    {
        public static string FileName = "comics.json";
        public static string ComicsFolderName = "Comics";

        public static string DefaultAttributeToLookForInImage = "src";
        public static string DefaultTagToLookFor = "img";

        public static string NoComicSite = "No matching comic site to delete.";
        public static string AddedComicSite = "Successfully added new Comic site to scrape from.";
        public static string DeletedComicSite = "Successfully deleted existing Comic site.";
        public static string NewSettingsFile = "Created new Settings file and Successfully added new Comic site.";
        public static string Error = "An error occurred. Here's the exception;";
        public static string Scraped = "Successfully scraped comic.";

        public static List<string> WordsToRemove = new List<string>() { "issue" };
    }
}
