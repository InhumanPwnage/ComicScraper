using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ComicScraper.Helpers;
using ComicScraper.Models;
using HtmlAgilityPack;

namespace ComicScraper.Services
{
    public class Scraper : IScraper
    {
        private Uri scrapeAddress;
        private HtmlDocument documentToScrape;
        private string folderPathToSaveTo;

        #region CTOR
        public Scraper(string link)
        {
            scrapeAddress = new Uri(link);

            var web = new HtmlWeb();

            documentToScrape = web.Load(scrapeAddress);
        }
        #endregion


        public ResultModel Scrape_Comic(ComicModel model, string comicName)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Occurrence = DateTime.Now,
                Data = Constants.Error,
                Result = Enums.ResultTypes.Error
            };

            //check if Comics folder exists
            if (FileHelper.Folder_Exists(Constants.ComicsFolderName).Result == Enums.ResultTypes.Error)
            {
                FileHelper.Create_Folder(Constants.ComicsFolderName);
            }

            var siteFolder = $@"{Constants.ComicsFolderName}\{model.Name}";

            //check if Site folder exists
            if (FileHelper.Folder_Exists(siteFolder).Result == Enums.ResultTypes.Error)
            {
                FileHelper.Create_Folder(siteFolder);
            }

            //Create folder for comic to download to with UNIX to make unique
            var comicNameFromUri = new Uri(comicName).LocalPath;

            foreach (var word in model.ListOfWordsToRemoveFromLink)
            {
                comicNameFromUri = comicNameFromUri.Replace(word, string.Empty);
            }

            comicNameFromUri = comicNameFromUri
                .Replace("-", " ")
                .Replace("/", " ")
                .Trim();

            folderPathToSaveTo = $@"{siteFolder}\{comicNameFromUri} {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            FileHelper.Create_Folder(folderPathToSaveTo);

            try
            {
                var listOfImageLinks = Get_Nodes(model);

                Download(listOfImageLinks);

                modelToReturn.Result = Enums.ResultTypes.Success;
                modelToReturn.Data = Constants.Scraped;
            }
            catch (Exception ex)
            {
                modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
            }

            return modelToReturn;
        }

        public ResultModel Test_Scrape_Comic(ComicModel model)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Occurrence = DateTime.Now,
                Data = Constants.Error,
                Result = Enums.ResultTypes.Error
            };

            try
            {
                var listOfImageLinks = Get_Nodes(model);

                //return the list of images
                modelToReturn.Data = $@"Found {listOfImageLinks.Count} items.{Environment.NewLine}{string.Join("\n", listOfImageLinks.Take(10))}";
                modelToReturn.Result = Enums.ResultTypes.Success;
            }
            catch (Exception ex)
            {
                modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
            }

            return modelToReturn;
        }


        #region Utility
        private List<string> Get_Nodes(ComicModel model)
        {
            //get the collection
            var nodes = documentToScrape
                .DocumentNode
                .SelectNodes($@"{model.XPath}");

            //extract list of images
            var listOfImageLinks = nodes.Descendants("img")
                    .Select(img => img.GetAttributeValue("src", null))
                    .Where(s => !String.IsNullOrEmpty(s))
                    .ToList();

            if (model.AppendDomain || model.RemoveDimensions)
            {
                var newList = new List<string>();

                foreach (var item in listOfImageLinks)
                {
                    StringBuilder sb = new StringBuilder();

                    if (model.AppendDomain)
                        sb.Append(model.Link);

                    if (model.RemoveDimensions)
                        sb.Append(item.RemoveDimensionsFromLink());
                    else
                        sb.Append(item);

                    newList.Add(sb.ToString());
                }

                listOfImageLinks = newList;
            }

            return listOfImageLinks;
        }

        private void Download(List<string> urls)
        {
            Parallel.ForEach(
                urls,
                new ParallelOptions { MaxDegreeOfParallelism = 10 },
                DownloadFile);
        }

        private void DownloadFile(string url)
        {
            Uri uri = new Uri(url);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, $@"{folderPathToSaveTo}\{Path.GetFileName(uri.LocalPath.Split('/').Last())}");
                // OR 
                //client.DownloadFileAsync(url, $@"{folderPathToSaveTo}\{Path.GetFileName(uri.LocalPath.Split('/').Last())}");
            }

            //using (var httpClient = new HttpClient())
            //{
            //    var imageBytes = httpClient.get(uri);
            //    File.WriteAllBytes($@"{folderPathToSaveTo}\{Path.GetFileName(uri.LocalPath.Split('/').Last())}", imageBytes);
            //}

            //using (var sr = new StreamReader(HttpWebRequest.Create(url).GetResponse().GetResponseStream()))
            //{
            //    Uri uri = new Uri(url);

            //    using (var sw = new StreamWriter($@"{folderPathToSaveTo}\{Path.GetFileName(uri.LocalPath.Split('/').Last())}"))
            //    {
            //        sw.Write(sr.ReadToEnd());
            //    }
            //}
        }
        #endregion
    }
}
