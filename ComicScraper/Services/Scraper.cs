using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
        private bool useCustomNumbering;

        #region CTOR
        public Scraper(string link)
        {
            PrepareDocument(new Uri(link));
        }
        #endregion


        public ResultModel Scrape_Comic(ComicModel model)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Occurrence = DateTime.Now,
                Data = Constants.Error,
                Result = Enums.ResultTypes.Error
            };

            useCustomNumbering = model.Numbering;

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
            var comicNameFromUri = Get_ComicName(model);

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
                modelToReturn.Data = $@"{Get_ComicName(model)} has {listOfImageLinks.Count} items.{Environment.NewLine}{listOfImageLinks[0]}{Environment.NewLine}{listOfImageLinks[listOfImageLinks.Count/2]}{Environment.NewLine}{listOfImageLinks[listOfImageLinks.Count-1]}{Environment.NewLine} . . .";
                modelToReturn.Result = Enums.ResultTypes.Success;
            }
            catch (Exception ex)
            {
                modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
            }

            return modelToReturn;
        }


        #region Utility
        private ResultModel PrepareDocument(Uri fullLink)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Data = Constants.Error,
                Occurrence = DateTime.Now,
                Result = Enums.ResultTypes.Error
            };

            try
            {
                var web = new HtmlWeb();

                documentToScrape = web.Load(fullLink);

                //var select = documentToScrape.DocumentNode.SelectNodes($"//meta[contains(@content, '{scrapeAddress}')]");

                scrapeAddress = fullLink;

                modelToReturn.Result = Enums.ResultTypes.Success;
                modelToReturn.Data = string.Empty;
            }
            catch (Exception ex)
            {
                modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
            }

            return modelToReturn;
        }

        private List<string> Get_Nodes(ComicModel model)
        {
            int pageCount = 1;
            bool continueCheckingForImagesOverMultiplePages = model.QueryString != null && model.QueryString != string.Empty;
            var listOfImageLinks = new List<string>();
            int imagesPerPage = 0;
            bool doNotAddToList;

            do
            {
                doNotAddToList = false;
                pageCount++;

                //get the collection
                var nodes = documentToScrape
                    .DocumentNode
                    .SelectNodes($@"{model.XPath}");

                var scrapedImageLinks = new List<string>();

                var srcTag = model.TagNameInsideImage.Equals(string.Empty) ? Constants.DefaultAttributeToLookForInImage : model.TagNameInsideImage;

                //https://stackoverflow.com/a/17158393
                foreach (var item in nodes.Descendants("img")
                        .Select(img => WebUtility.HtmlDecode(img.GetAttributeValue(srcTag, null)))
                        .Where(s => !String.IsNullOrEmpty(s)))
                {
                    StringBuilder baseUrl = new StringBuilder();

                    var url = string.Empty;

                    if (model.AppendDomain)
                        baseUrl.Append(model.Link);

                    if (!model.ReplaceString.Equals(string.Empty) || model.RemoveDimensions)
                    {
                        if(!model.ReplaceString.Equals(string.Empty))
                            url = item.Replace(model.ReplaceString, model.ReplaceWith);

                        if (model.RemoveDimensions)
                        {
                            if (string.IsNullOrEmpty(url))
                                url = item.RemoveDimensionsFromLink();
                            else
                                url = url.RemoveDimensionsFromLink();
                        }
                    }
                    else
                    {
                        baseUrl.Append(item);
                    }
 
                    scrapedImageLinks.Add(baseUrl.ToString() + url);
                }

                if (imagesPerPage < scrapedImageLinks.Count)
                    imagesPerPage = scrapedImageLinks.Count;

                if (continueCheckingForImagesOverMultiplePages)
                {
                    var resultModel = PrepareDocument(new Uri($@"{scrapeAddress}?{model.QueryString}={pageCount}"));

                    //to check to see if the site has redirects instead of a custom error page for non-existant pages when trying to scrape for images over multiple pages, we will compare image names
                    if (resultModel.Result == Enums.ResultTypes.Error || scrapedImageLinks.Count - imagesPerPage < 0)
                    {
                        //page not found (custom error page)
                        continueCheckingForImagesOverMultiplePages = false;
                    }
                    else if (pageCount > 2 && 
                        (listOfImageLinks[0].Equals(scrapedImageLinks[0]) || 
                        scrapedImageLinks[scrapedImageLinks.Count-1].Equals(listOfImageLinks[((pageCount - 2) * imagesPerPage)-1])))
                    {
                        //image names case 1) very first image with newest page, case 2) newest page with last page
                        continueCheckingForImagesOverMultiplePages = false;
                        doNotAddToList = true;
                    }
                }

                if(!doNotAddToList)
                    listOfImageLinks.AddRange(scrapedImageLinks);
            }
            while (continueCheckingForImagesOverMultiplePages);

            return listOfImageLinks;
        }

        private string Get_ComicName(ComicModel model)
        {
            var comicName = string.Empty;

            if (model.XPathComicName != null && model.XPathComicName != string.Empty)
            {
                var titleNode = documentToScrape
                    .DocumentNode
                    .SelectSingleNode($@"{model.XPathComicName}");

                comicName = titleNode.InnerText;
            }
            else
            {
                var strings = model.ComicLink.Split('/').ToUniqueList();

                //var testUri = new Uri(model.ComicLink);
                int index = strings.Count - 1;

                //.IndexOf("string", StringComparison.OrdinalIgnoreCase) >= 0
                if (Constants.WordsToRemove.Any(w => strings[index].IndexOf(w, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    index--;
                }

                comicName = strings[index].Replace('-', ' ');//testUri.LocalPath;
            }

            return WebUtility.HtmlDecode(comicName);
        }

        /// <summary>
        /// Parallel example https://stackoverflow.com/a/31044246
        /// </summary>
        /// <param name="urls"></param>
        private void Download(List<string> urls)
        {
            Parallel.ForEach(
                urls, new ParallelOptions { MaxDegreeOfParallelism = 10 }, (line, state, index) => DownloadFile(line, index));

            //Parallel.ForEach(
            //    urls,
            //    new ParallelOptions { MaxDegreeOfParallelism = 10 },
            //    DownloadFile);
        }

        private void DownloadFile(string url, long index)
        {
            using (WebClient client = new WebClient())
            {
                if(!useCustomNumbering)
                    client.DownloadFile(url, $@"{folderPathToSaveTo}\{TextHelper.GetImageNameFromLink(url)}");
                else
                    client.DownloadFile(url, $@"{folderPathToSaveTo}\{string.Format("{0:D3}", index)}{TextHelper.GetImageExtensionFromLink(url)}");
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
