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

            return uriAddress.Authority;
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
    }
}
