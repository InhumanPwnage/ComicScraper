using ComicScraper.Helpers;
using ComicScraper.Models;
using ComicScraper.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ComicScraper
{
    public partial class TestForm : Form
    {
        private ComicModel _comicModel;
        private Scraper _scraper;
        private bool _isProperScrape = false;

        private bool _useNumbering = false;
        private ConcurrentDictionary<long, string> concurrentListOfUrls;
        private string folderPathToSaveTo;
        private decimal numberOfImagesToDownload;
        private decimal scrapingProgress;
        private decimal unitOfScrapingProgress;

        /// <summary>
        /// Supposedly helps to disable the Close button at the top-right 
        /// https://stackoverflow.com/questions/7301825/how-to-hide-only-the-close-x-button
        /// </summary>
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        #region CTOR
        public TestForm()
        {
            InitializeComponent();

            btnCancelScraping.Enabled = false;
        }

        public TestForm(ComicModel comicModel, bool isProperScrape)
        {
            //txtLinkToComic.Text = string.Empty;

            _comicModel = comicModel;
            _isProperScrape = isProperScrape;
            _useNumbering = comicModel.Numbering;

            InitializeComponent();
            btnCancelScraping.Enabled = false;

            //InitializeBackgroundWorker();

            scraperBackgroundWorker.WorkerReportsProgress = true;
            scraperBackgroundWorker.WorkerSupportsCancellation = true;
        }

        private void InitializeBackgroundWorker()
        {
            scraperBackgroundWorker.DoWork += new DoWorkEventHandler(scraperBackgroundWorker_DoWork);
            scraperBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(scraperBackgroundWorker_RunWorkerCompleted);
            scraperBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(scraperBackgroundWorker_ProgressChanged);
        }
        #endregion

        /// <summary>
        /// Lots of options can be found in the Scraper class
        /// For a blocking/sequential option, there's this one using a queue
        /// https://stackoverflow.com/questions/6992553/how-do-i-async-download-multiple-files-using-webclient-but-one-at-a-time?rq=1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestScrape_Click(object sender, EventArgs e)
        {
            if (scraperBackgroundWorker.IsBusy != true)
            {
                //so you cannot double click
                btnTestScrape.Enabled = false;
                btnCancelScraping.Enabled = true;

                concurrentListOfUrls = new ConcurrentDictionary<long, string>();

                //check if Comics folder exists
                if (FileHelper.Folder_Exists(Constants.ComicsFolderName).Result == Enums.ResultTypes.Error)
                {
                    FileHelper.Create_Folder(Constants.ComicsFolderName);
                }

                var siteFolder = $@"{Constants.ComicsFolderName}\{_comicModel.Name}";

                //check if Site folder exists
                if (FileHelper.Folder_Exists(siteFolder).Result == Enums.ResultTypes.Error)
                {
                    FileHelper.Create_Folder(siteFolder);
                }

                _comicModel.ComicLink = txtLinkToComic.Text;

                _scraper = new Scraper(
                    string.IsNullOrEmpty(_comicModel.ComicLink) ?
                    rchHtmlToScrape.Text :
                    _comicModel.ComicLink
                    );

                //Create folder for comic to download to with UNIX to make unique
                var comicNameFromUri = _scraper.Get_ComicName(_comicModel);

                foreach (var word in _comicModel.ListOfWordsToRemoveFromLink)
                {
                    comicNameFromUri = comicNameFromUri.Replace(word, string.Empty);
                }

                comicNameFromUri = comicNameFromUri
                    .Replace("-", " ")
                    .Replace("/", " ")
                    .Trim();

                folderPathToSaveTo = $@"{siteFolder}\{comicNameFromUri} {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
                FileHelper.Create_Folder(folderPathToSaveTo);

                // Start the asynchronous operation.
                scraperBackgroundWorker.RunWorkerAsync();
            }

            //_comicModel.ComicLink = txtLinkToComic.Text;

            //_scraper = new Scraper(
            //    string.IsNullOrEmpty(_comicModel.ComicLink) ? 
            //    rchHtmlToScrape.Text : 
            //    _comicModel.ComicLink
            //    );

            //ResultModel resultModel = null;

            //if (_isProperScrape)
            //{
            //    resultModel = _scraper.Scrape_Comic(_comicModel);
            //}
            //else
            //{
            //    resultModel = _scraper.Test_Scrape_Comic(_comicModel);
            //}

            //var result = _scraper.Test_Scrape_Comic(_comicModel);

            //MessageBox.Show(resultModel.Data, resultModel.Result.ToName(), MessageBoxButtons.OK, FormsHelper.SelectIcon(resultModel.Result));
        }

        private void btnCancelScraping_Click(object sender, EventArgs e)
        {
            if (scraperBackgroundWorker.WorkerSupportsCancellation == true)
            {
                //can scrape again
                btnTestScrape.Enabled = true;
                btnCancelScraping.Enabled = false;

                lblScrapingProgress.Text = string.Empty;

                // Cancel the asynchronous operation.
                scraperBackgroundWorker.CancelAsync();
            }
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker?view=netcore-3.1
        /// as suggested from
        /// https://www.codeproject.com/Questions/5268310/How-do-I-download-multiple-file-with-downloadfilea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scraperBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            _comicModel.ComicLink = txtLinkToComic.Text;

            var resultModel = _scraper.Scrape_Comic(_comicModel, _isProperScrape);

            concurrentListOfUrls = resultModel.Item2;
            numberOfImagesToDownload = concurrentListOfUrls.Count;

            //find out how much a unit of work is
            unitOfScrapingProgress = (1 / concurrentListOfUrls.Count) * 100;
            scrapingProgress = 0;

            do
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    //Parallel_Download();

                    Parallel.ForEach(
                        concurrentListOfUrls, new ParallelOptions { MaxDegreeOfParallelism = 10 }, (kvp, state, index) => //DownloadFile(kvp)
                        {
                            try
                            {
                                using (WebClient client = new WebClient())
                                {
                                    if (!_useNumbering)
                                        client.DownloadFile(kvp.Value, $@"{folderPathToSaveTo}\{TextHelper.GetImageNameFromLink(kvp.Value)}");
                                    else
                                        client.DownloadFile(kvp.Value, $@"{folderPathToSaveTo}\{string.Format("{0:D3}", kvp.Key)}{TextHelper.GetImageExtensionFromLink(kvp.Value)}");
                                    // OR 
                                    //client.DownloadFileAsync(url, $@"{folderPathToSaveTo}\{Path.GetFileName(uri.LocalPath.Split('/').Last())}");
                                    concurrentListOfUrls.TryRemove(kvp.Key, out string value);

                                    scrapingProgress = ((numberOfImagesToDownload - concurrentListOfUrls.Count) / numberOfImagesToDownload) * 100;
                                    //Console.WriteLine($@"({numberOfImagesToDownload} - {concurrentListOfUrls.Count}) / {numberOfImagesToDownload} = {scrapingProgress}");
                                    
                                    //scrapingProgress += unitOfScrapingProgress;
                                    worker.ReportProgress((int)(scrapingProgress > 100 ? 100 : scrapingProgress));
                                }
                            }
                            catch (Exception)
                            {
                                var extension = TextHelper.GetImageExtensionFromLink(kvp.Value).Replace(".", "");

                                //transform data
                                Enum.TryParse(extension, out Enums.FileExtensions matchingEnum);

                                var newLink = TextHelper.ChangeFileExtension(kvp.Value, matchingEnum.Next().ToString());

                                concurrentListOfUrls.TryUpdate(kvp.Key, newLink, kvp.Value);
                            }
                        }
                    );
                }
            }
            while (concurrentListOfUrls.Count > 0);

            e.Result = resultModel.Item1;
        }

        private void scraperBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgScrapingCompletionBar.Value = e.ProgressPercentage;

            lblScrapingProgress.Text = (e.ProgressPercentage.ToString() + "%");
        }

        private void scraperBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Successfully halted scraping at " + lblScrapingProgress.Text, "Cancelled Scraping", MessageBoxButtons.OK, FormsHelper.SelectIcon(Enums.ResultTypes.NoAction));

                lblScrapingProgress.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                MessageBox.Show("An error occurred while scraping, stopping at " + lblScrapingProgress.Text + Environment.NewLine + e.Error.Message, "Error while Scraping", MessageBoxButtons.OK, FormsHelper.SelectIcon(Enums.ResultTypes.Error));

                lblScrapingProgress.Text = "Error";
            }
            else
            {
                ResultModel resultModel = (ResultModel)e.Result;

                MessageBox.Show(resultModel.Data, resultModel.Result.ToName(), MessageBoxButtons.OK, FormsHelper.SelectIcon(resultModel.Result));

                lblScrapingProgress.Text = "Done!";
            }

            btnTestScrape.Enabled = true;
            btnCancelScraping.Enabled = false;
        }

        /// <summary>
        /// Could have been chagned to async as such
        /// https://stackoverflow.com/a/30535102
        /// </summary>
        private void Parallel_Download()
        {
            Parallel.ForEach(
                concurrentListOfUrls, new ParallelOptions { MaxDegreeOfParallelism = 10 }, (kvp, state, index) => DownloadFile(kvp));
        }

        private void DownloadFile(KeyValuePair<long, string> imageLink) //string url, long index
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    if (!_useNumbering)
                        client.DownloadFile(imageLink.Value, $@"{folderPathToSaveTo}\{TextHelper.GetImageNameFromLink(imageLink.Value)}");
                    else
                        client.DownloadFile(imageLink.Value, $@"{folderPathToSaveTo}\{string.Format("{0:D3}", imageLink.Key)}{TextHelper.GetImageExtensionFromLink(imageLink.Value)}");
                    // OR 
                    //client.DownloadFileAsync(url, $@"{folderPathToSaveTo}\{Path.GetFileName(uri.LocalPath.Split('/').Last())}");
                    concurrentListOfUrls.TryRemove(imageLink.Key, out string value);
                }
            }
            catch (Exception)
            {
                //transform data
                Enum.TryParse(TextHelper.GetImageExtensionFromLink(imageLink.Value), out Enums.FileExtensions matchingEnum);

                concurrentListOfUrls[imageLink.Key] = TextHelper.ChangeFileExtension(imageLink.Value, (matchingEnum++).ToString());
            }
        }

        private void btnExitBackToMain_Click(object sender, EventArgs e)
        {
            if (scraperBackgroundWorker.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                scraperBackgroundWorker.CancelAsync();
            }

            btnCancelScraping.Enabled = false;
            btnExitBackToMain.Enabled = false;

            lblScrapingProgress.Text = "Stopping & Exiting please wait ...";

            this.Close();
        }
    }
}
