using System;
using System.Collections.Concurrent;
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
using static ComicScraper.Models.Enums;

namespace ComicScraper.Services
{
    public class Scraper : IScraper
    {
        private Uri scrapeAddress;
        private HtmlDocument documentToScrape;
        private string folderPathToSaveTo;
        private bool useCustomNumbering;
        private bool galleryHasMultipleMediaTypes;
        //private ConcurrentBag<InvalidFileModel> concurrentListOfInvalidUrls;
        private ConcurrentDictionary<long, string> concurrentListOfUrls;

        #region CTOR
        public Scraper(string linkOrHtml)
        {
            galleryHasMultipleMediaTypes = true;

            if (Uri.TryCreate(linkOrHtml, UriKind.Absolute, out Uri parsedUri))
            {
                PrepareDocumentFromUri(parsedUri);//new Uri(link)

            }
            else
            {
                PrepareDocumentFromHtml(linkOrHtml);
            }
        }
        #endregion

        /// <summary>
        /// High level function to extract & download images
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public (ResultModel, ConcurrentDictionary<long, string>) Scrape_Comic(ComicModel model, bool isProperScrape)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Occurrence = DateTime.Now,
                Data = Constants.Error,
                Result = Enums.ResultTypes.Error
            };

            //useCustomNumbering = model.Numbering;

            ////check if Comics folder exists
            //if (FileHelper.Folder_Exists(Constants.ComicsFolderName).Result == Enums.ResultTypes.Error)
            //{
            //    FileHelper.Create_Folder(Constants.ComicsFolderName);
            //}

            //var siteFolder = $@"{Constants.ComicsFolderName}\{model.Name}";

            ////check if Site folder exists
            //if (FileHelper.Folder_Exists(siteFolder).Result == Enums.ResultTypes.Error)
            //{
            //    FileHelper.Create_Folder(siteFolder);
            //}

            ////Create folder for comic to download to with UNIX to make unique
            //var comicNameFromUri = Get_ComicName(model);

            //foreach (var word in model.ListOfWordsToRemoveFromLink)
            //{
            //    comicNameFromUri = comicNameFromUri.Replace(word, string.Empty);
            //}

            //comicNameFromUri = comicNameFromUri
            //    .Replace("-", " ")
            //    .Replace("/", " ")
            //    .Trim();

            //folderPathToSaveTo = $@"{siteFolder}\{comicNameFromUri} {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            //FileHelper.Create_Folder(folderPathToSaveTo);

            var listOfImageLinks = new ConcurrentDictionary<long, string>();

            try
            {
                listOfImageLinks = Get_Nodes(model, isProperScrape ? -1 : 5);

                modelToReturn.Result = Enums.ResultTypes.Success;
                modelToReturn.Data = isProperScrape ? Constants.Scraped :
                    $@"{Get_ComicName(model)} has {listOfImageLinks.Count} items.{Environment.NewLine}{listOfImageLinks[0]}{Environment.NewLine}{listOfImageLinks[listOfImageLinks.Count / 2]}{Environment.NewLine}{listOfImageLinks[listOfImageLinks.Count - 1]}{Environment.NewLine} . . .";
            }
            catch (Exception ex)
            {
                modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
            }

            return (modelToReturn, listOfImageLinks);
        }

        //public (ResultModel, List<string>) Test_Scrape_Comic(ComicModel model)
        //{
        //    ResultModel modelToReturn = new ResultModel()
        //    {
        //        Occurrence = DateTime.Now,
        //        Data = Constants.Error,
        //        Result = Enums.ResultTypes.Error
        //    };

        //    List<string> listOfImageLinks = new List<string>();

        //    try
        //    {
        //        listOfImageLinks = Get_Nodes(model, 5);

        //        //return the list of images
        //        modelToReturn.Data = $@"{Get_ComicName(model)} has {listOfImageLinks.Count} items.{Environment.NewLine}{listOfImageLinks[0]}{Environment.NewLine}{listOfImageLinks[listOfImageLinks.Count/2]}{Environment.NewLine}{listOfImageLinks[listOfImageLinks.Count-1]}{Environment.NewLine} . . .";
        //        modelToReturn.Result = Enums.ResultTypes.Success;
        //    }
        //    catch (Exception ex)
        //    {
        //        modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
        //    }

        //    return (modelToReturn, listOfImageLinks);
        //}


        #region Utility

        /// <summary>
        /// Don't use this, it will time-out on the 3rd call. The host will probably think this is a DOS attack since it happens in a sequence. only way I see this can work is if we re-use connections.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string ValidateUrl(string url) 
        {
            if (galleryHasMultipleMediaTypes)
            {
                Thread.Sleep(5000);
                bool validLink = false;
                int extensionIndex = 0;

                do
                {
                    var request = HttpWebRequest.Create(new Uri(url));//(HttpWebRequest)
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();//(HttpWebResponse)
                                                                                      //var type = response.ContentType;
                    if (response.StatusCode == HttpStatusCode.OK || extensionIndex == 3)
                    {
                        validLink = true;
                    }
                    else
                    {
                        //iterate the next possible file extension
                        extensionIndex++;

                        url = TextHelper.ChangeFileExtension(url, ((FileExtensions)extensionIndex).ToString());
                    }
                }
                while (!validLink);
            }

            return url;
        }

        private ResultModel PrepareDocumentFromUri(Uri fullLink)
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

        /// <summary>
        /// https://stackoverflow.com/questions/9458938/loading-from-string-instead-of-document-url
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private ResultModel PrepareDocumentFromHtml(string html)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Data = Constants.Error,
                Occurrence = DateTime.Now,
                Result = Enums.ResultTypes.Error
            };

            try
            {
                //var web = new HtmlWeb();
                documentToScrape = new HtmlDocument();

                documentToScrape.LoadHtml(html);
                //documentToScrape.LoadHtml(html);
                //web.Load(fullLink);

                //var select = documentToScrape.DocumentNode.SelectNodes($"//meta[contains(@content, '{scrapeAddress}')]");

                scrapeAddress = null;
                    //fullLink;

                modelToReturn.Result = Enums.ResultTypes.Success;
                modelToReturn.Data = string.Empty;
            }
            catch (Exception ex)
            {
                modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
            }

            return modelToReturn;
        }

        /// <summary>
        /// Method to extract links from scraped HTML
        /// </summary>
        /// <param name="model">The site profile</param>
        /// <param name="limit">The number of items to scrape. Pass -1 if to return all.</param>
        /// <returns></returns>
        private ConcurrentDictionary<long, string> Get_Nodes(ComicModel model, int limit)
        {
            int pageCount = 1;
            bool continueCheckingForImagesOverMultiplePages = model.QueryString != null && model.QueryString != string.Empty;
            //var listOfImageLinks = new List<string>();
            int imagesPerPage = 0;
            bool doNotAddToList;
            ConcurrentDictionary<long, string> keyValuePairs = new ConcurrentDictionary<long, string>();

            do
            {
                doNotAddToList = false;
                pageCount++;

                //get the collection
                var nodes = documentToScrape
                    .DocumentNode
                    .SelectNodes($@"{model.XPath}");

                var scrapedImageLinks = new Dictionary<long, string>();

                var srcAttribute = model.TagNameInsideImage.Equals(string.Empty) ? Constants.DefaultAttributeToLookForInImage : model.TagNameInsideImage;
                var srcTag = (bool)(model.TagToLookFor?.Equals(string.Empty)) ? Constants.DefaultTagToLookFor : model.TagToLookFor;

                //var imgs = nodes.Descendants(srcTag);
                //var decodedImgs = imgs.Select(img => WebUtility.HtmlDecode(img.GetAttributeValue(srcAttribute, null)));
                int index = 0;

                //links to process are found here
                var linksToProcess = nodes.Descendants(srcTag)
                        .Select(img => WebUtility.HtmlDecode(img.GetAttributeValue(srcAttribute, null)))
                        .Where(s => !String.IsNullOrEmpty(s))
                        .Take(limit > 0 ? limit : int.MaxValue)
                        .ToList();

                //some websites break their own code, but we can still attempt to grab URLs from text via Regex
                if (model.DoubleCheckLinks) 
                {
                    foreach (var item in nodes.Descendants("a"))
                    {
                        var link = TextHelper.GetUrlFromText(item.OuterHtml);

                        if (!linksToProcess.Contains(link))
                            linksToProcess.Add(link);
                    }
                }


                //https://stackoverflow.com/a/17158393
                foreach (var item in linksToProcess)
                {
                    index++;
                    StringBuilder baseUrl = new StringBuilder();
                    StringBuilder finalUrl = new StringBuilder();

                    var url = string.Empty;

                    if (model.AppendDomain)
                        baseUrl.Append(model.Link);

                    if (!model.ReplaceString.Equals(string.Empty) || model.RemoveDimensions || !model.ReplaceTextInImageNames.Equals(string.Empty))
                    {
                        if(!model.ReplaceString.Equals(string.Empty))
                            url = item.Replace(model.ReplaceString, model.ReplaceWith);

                        //will remove last instance of string provided
                        if (!model.ReplaceTextInImageNames.Equals(string.Empty))
                        {
                            //first grab the file extension
                            var ext = TextHelper.GetImageExtensionFromLink(item);

                            //merge the replaced text with the extension
                            url = TextHelper.ReplaceLastInstance(item, model.ReplaceTextInImageNames, model.ReplaceTextInImageNamesWith) + ext;
                        }

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

                    if (!string.IsNullOrEmpty(model.ReplaceImageExtensionWith))
                        finalUrl.Append(TextHelper.ChangeFileExtension(baseUrl.ToString() + url, model.ReplaceImageExtensionWith));
                    else
                        finalUrl.Append(baseUrl.ToString() + url);

                    //concurrentListOfUrls.TryAdd(index, finalUrl.ToString());
                    scrapedImageLinks.Add(index, finalUrl.ToString());
                }

                if (imagesPerPage < scrapedImageLinks.Count)
                    imagesPerPage = scrapedImageLinks.Count;

                if (continueCheckingForImagesOverMultiplePages && scrapeAddress != null)
                {
                    var resultModel = PrepareDocumentFromUri(new Uri($@"{scrapeAddress}?{model.QueryString}={pageCount}"));

                    //to check to see if the site has redirects instead of a custom error page for non-existant pages when trying to scrape for images over multiple pages, we will compare image names
                    if (resultModel.Result == Enums.ResultTypes.Error || scrapedImageLinks.Count - imagesPerPage < 0)
                    {
                        //page not found (custom error page)
                        continueCheckingForImagesOverMultiplePages = false;
                    }
                    else if (pageCount > 2 && 
                        (keyValuePairs.ElementAt(0).Equals(scrapedImageLinks.ElementAt(0)) ||
                        scrapedImageLinks.ElementAt(scrapedImageLinks.Count-1).Equals(keyValuePairs.ElementAt(((pageCount - 2) * imagesPerPage)-1))))
                    {
                        //image names case 1) very first image with newest page, case 2) newest page with last page
                        continueCheckingForImagesOverMultiplePages = false;
                        doNotAddToList = true;
                    }
                }

                if (!doNotAddToList) 
                {
                    foreach (var imageLink in scrapedImageLinks) 
                    {
                        //keyValuePairs.AddRange(scrapedImageLinks.Select(x => x.Value));
                        keyValuePairs.TryAdd(imageLink.Key, imageLink.Value);
                    }
                }
                    
            }
            while (continueCheckingForImagesOverMultiplePages);

            return keyValuePairs;
        }

        public string Get_ComicName(ComicModel model)
        {
            var comicName = string.Empty;

            if (model.XPathComicName != null && model.XPathComicName != string.Empty)
            {
                var titleNode = documentToScrape
                    .DocumentNode
                    .SelectSingleNode($@"{model.XPathComicName}");
                
                comicName = TextHelper.RemoveIllegalFileNameChars(titleNode.InnerText)
                    .Replace("Issue", "")
                    .Trim();
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
        /// https://stackoverflow.com/a/13638087
        /// </summary>
        /// <param name="urls"></param>
        private void ParallelDoWhile_Download(List<string> urls)
        {
            do 
            {
                Parallel.ForEach(
                    concurrentListOfUrls, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (kvp, state, index) =>
                        {
                            try 
                            {
                                using (WebClient client = new WebClient())
                                {
                                    if (!useCustomNumbering)
                                        await Task.Run(() => client.DownloadFileAsync(new Uri(kvp.Value), $@"{folderPathToSaveTo}\{TextHelper.GetImageNameFromLink(kvp.Value)}"));
                                    else
                                        await Task.Run(() => client.DownloadFileAsync(new Uri(kvp.Value), $@"{folderPathToSaveTo}\{string.Format("{0:D3}", kvp.Key)}{TextHelper.GetImageExtensionFromLink(kvp.Value)}"));

                                    //remove the item from the ConcurrentDictionary if we get this far
                                    concurrentListOfUrls.TryRemove(kvp.Key, out string value);
                                }
                            } 
                            catch (Exception) 
                            {
                                //inside the exception we will transform the file extension in the URL
                                Enum.TryParse(TextHelper.GetImageExtensionFromLink(kvp.Value), out Enums.FileExtensions matchingEnum);

                                concurrentListOfUrls[kvp.Key] = TextHelper.ChangeFileExtension(kvp.Value, (matchingEnum++).ToString());
                            }
                        }
                    );
            }
            while (concurrentListOfUrls.Count > 0);
        }

        private async Task Task_DownloadFileAsync(InvalidFileModel fileModel)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    if (!useCustomNumbering)
                        await webClient.DownloadFileTaskAsync(new Uri(fileModel.Url), $@"{folderPathToSaveTo}\{TextHelper.GetImageNameFromLink(fileModel.Url)}");
                    else
                        await webClient.DownloadFileTaskAsync(new Uri(fileModel.Url), $@"{folderPathToSaveTo}\{string.Format("{0:D3}", fileModel.Index)}{TextHelper.GetImageExtensionFromLink(fileModel.Url)}");

                    //remove model from bag
                    //concurrentListOfInvalidUrls.FirstOrDefault(x => x.Index == fileModel.Index).
                    concurrentListOfUrls.TryRemove(fileModel.Index, out string value);
                }
            }
            catch (Exception)
            {
                //transform data
                Enum.TryParse(TextHelper.GetImageExtensionFromLink(fileModel.Url), out Enums.FileExtensions matchingEnum);

                concurrentListOfUrls[fileModel.Index] = TextHelper.ChangeFileExtension(fileModel.Url, (matchingEnum++).ToString());
            }
        }

        /// <summary>
        /// https://stackoverflow.com/a/16514441
        /// </summary>
        /// <param name="fileModels"></param>
        /// <returns></returns>
        private async Task Task_DownloadMultipleFilesAsync(List<InvalidFileModel> fileModels)
        {
            do 
            {
                await Task.WhenAll(fileModels.Select(fileModel => Task_DownloadFileAsync(fileModel)));
            } 
            while (concurrentListOfUrls.Count > 0);
        }

        /// <summary>
        /// Based on Microsoft example https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentbag-1?view=netcore-3.1
        /// </summary>
        /// <param name="urls"></param>
        //private void Task_DownloadFiles(List<string> urls)
        //{
        //    List<Task> bagAddTasks = new List<Task>();

        //    //foreach(var url in urls)
        //    for(int i = 0; i < urls.Count; i++)
        //    {
        //        InvalidFileModel invalidFileModel = new InvalidFileModel() 
        //        { 
        //            Url = urls[i],
        //            Index = i
        //        }; 

        //        bagAddTasks.Add(Task.Run(() => concurrentListOfInvalidUrls.Add(invalidFileModel)));
        //    }

        //    // Wait for all tasks to complete
        //    Task.WaitAll(bagAddTasks.ToArray());
        //}


        /// <summary>
        /// Parallel example https://stackoverflow.com/a/31044246
        /// </summary>
        /// <param name="urls"></param>
        private void Parallel_Download(List<string> urls)
        {
            Parallel.ForEach(
                urls, new ParallelOptions { MaxDegreeOfParallelism = 10 }, (line, state, index) => DownloadFile(line, index));
        }

        /// <summary>
        /// https://stackoverflow.com/questions/3826370/how-can-i-validate-a-url-in-c-sharp-to-avoid-404-errors
        /// </summary>
        /// <param name="url"></param>
        /// <param name="index"></param>
        private void DownloadFile(string url, long index)
        {
            try 
            {
                using (WebClient client = new WebClient())
                {
                    if (!useCustomNumbering)
                        client.DownloadFile(url, $@"{folderPathToSaveTo}\{TextHelper.GetImageNameFromLink(url)}");
                    else
                        client.DownloadFile(url, $@"{folderPathToSaveTo}\{string.Format("{0:D3}", index)}{TextHelper.GetImageExtensionFromLink(url)}");
                    // OR 
                    //client.DownloadFileAsync(url, $@"{folderPathToSaveTo}\{Path.GetFileName(uri.LocalPath.Split('/').Last())}");
                    concurrentListOfUrls.TryRemove(index, out string value);
                }
            } 
            catch (Exception) 
            {
                //InvalidFileModel invalidFileModel = new InvalidFileModel() 
                //{ 
                //    Url = url,
                //    Index = index
                //};

                //concurrentListOfInvalidUrls.Add(invalidFileModel);
                //transform data
                Enum.TryParse(TextHelper.GetImageExtensionFromLink(url), out Enums.FileExtensions matchingEnum);

                concurrentListOfUrls[index] = TextHelper.ChangeFileExtension(url, (matchingEnum++).ToString());
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
