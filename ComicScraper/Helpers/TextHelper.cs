using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComicScraper.Helpers
{
    public static class TextHelper
    {
        public static string NameFromLink(this string link)
        {
            Uri uriAddress = new Uri(link);

            //if (Uri.TryCreate(link, UriKind.Absolute, out uriAddress))
            //{

            //}

            return uriAddress.Authority.Replace("www.", string.Empty);
        }

        public static string WebsiteNameFromLink(this string link)
        {
            return Regex.Match(link, @"http(s?)://([\w]+\.){1}([\w]+\.?)+").Value;
        }

        public static string RemoveDimensionsFromLink(this string link)
        {
            return Regex.Replace(link, @"-([0-9])\w+x([0-9])\w+", string.Empty);
        }

        public static string GetImageNumberFromLink(this string linkToImage)
        {
            return Regex.Match(linkToImage, @"\d+\.\w+").Value;
        }

        public static string GetImageExtensionFromLink(this string linkToImage)
        {
            return Regex.Match(linkToImage, @"\.(jpg|jpeg|gif|png|bmp|tiff|tga|svg)").Value;
        }

        public static string GetImageNameFromLink(this string linkToImage)
        {
            return
            //Path.GetFileName(new Uri(linkToImage).LocalPath.Split('/').Last());
            //Regex.Match(linkToImage, @"([0-9a-zA-Z\._-]+.(png|PNG|gif|GIF|jp[e]?g|JP[E]?G))").Value;
            Regex.Match(linkToImage, @"([0-9a-zA-Z\._-]+.(png|PNG|gif|GIF|jp[e]?g|JP[E]?G))").Value;
        }

        public static bool StringIsWebsite(this string website)
        {
            return Regex.Match(website, @"^(https?:\/\/)?([\w\d-_]+)\.([\w\d-_\.]+)\/?\??([^#\n\r]*)?#?([^\n\r]*)").Captures.Count > 0;
        }

        public static string RemoveIllegalFileNameChars(string input, string replacement = "")
        {
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(input, replacement);
        }

        /// <summary>
        /// https://stackoverflow.com/a/5259991
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="newExtension"></param>
        /// <returns></returns>
        public static string ChangeFileExtension(this string fileName, string newExtension) { 
            return Path.ChangeExtension(fileName, $".{newExtension}");
        }

        /// <summary>
        /// https://stackoverflow.com/questions/8374742/regex-last-occurrence
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="toReplace"></param>
        /// <param name="toReplaceWith"></param>
        /// <returns></returns>
        public static string ReplaceLastInstance(this string imageName, string toReplace, string toReplaceWith) {
            var expression = $@"(?:.(?!{toReplace}))+$";

            return Regex.Replace(imageName, expression, toReplaceWith);
        }

        public static string GetUrlFromText(this string text) 
        {
            return Regex.Match(text, @"((\w+:\/\/)[-a-zA-Z0-9:@;?&=\/%\+\.\*!'\(\),\$_\{\}\^~\[\]`#|]+)").Value;
        }
    }
}
