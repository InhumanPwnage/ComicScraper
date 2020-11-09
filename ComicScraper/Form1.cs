using ComicScraper.Helpers;
using ComicScraper.Models;
using ComicScraper.Services;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComicScraper
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSiteLink.Text) && !string.IsNullOrEmpty(txtXpath.Text))
            {
                ComicModel model = Create_ComicModelFromForm();

                load_TestForm(model, false);
            }
            else
            {
                MessageBox.Show("Please enter a valid Site URL and XPath to the gallery of images on the page", "Insufficient Data", MessageBoxButtons.OK, FormsHelper.SelectIcon(Enums.ResultTypes.NoAction));
            }
                
        }

        /// <summary>
        /// https://stackoverflow.com/questions/3965043/how-to-open-a-new-form-from-another-form
        /// </summary>
        /// <param name="comicModel"></param>
        private void load_TestForm(ComicModel comicModel, bool isProperScrape) 
        {
            TestForm testForm = new TestForm(comicModel, isProperScrape);

            testForm.ShowDialog();

            //testForm.Show();
            //this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ComicModel model = Create_ComicModelFromForm();

            var result = FileHelper.Add_ComicSite(model);

            if (result.Result == Enums.ResultTypes.Success || result.Result == Enums.ResultTypes.SettingsNotFound)
            {
                var resultReadUpdates = FileHelper.Read_SettingsFile(out ComicSettingsModel settingsFile);

                if (settingsFile != null)
                {
                    Refresh_Sites(settingsFile.Comics);
                }
                else
                {
                    result.Data = Constants.Error;
                } 
            }

            MessageBox.Show(result.Data, result.Result.ToName(), MessageBoxButtons.OK, FormsHelper.SelectIcon(result.Result));
        }

        /// <summary>
        /// Updates the Comics dropdown field.
        /// </summary>
        /// <param name="listOfComics"></param>
        private void Refresh_Sites(List<ComicModel> listOfComics)
        {
            if (cmbSites.DataSource == null)
                cmbSites.Items.Clear();
            else
                cmbSites.DataSource = null;

            cmbSites.DisplayMember = "Name";
            cmbSites.ValueMember = "Id";

            cmbSites.DataSource = listOfComics;

            CheckIfSelectedItemIsValid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var resultModel = FileHelper.Read_SettingsFile(out ComicSettingsModel settingsFile);

            if (settingsFile != null)
                Refresh_Sites(settingsFile.Comics);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(cmbSites.SelectedValue);

            var result = FileHelper.Delete_ComicSite(id);

            var resultReadUpdates = FileHelper.Read_SettingsFile(out ComicSettingsModel settingsFile);

            if (settingsFile != null)
                Refresh_Sites(settingsFile.Comics);
            else
                result.Data = Constants.Error;

            MessageBox.Show(result.Data, result.Result.ToName(), MessageBoxButtons.OK, FormsHelper.SelectIcon(result.Result));
        }

        private void btnScrape_Click(object sender, EventArgs e)
        {
            if (cmbSites.SelectedItem is ComicModel)
            {
                var item = (ComicModel)cmbSites.SelectedItem;

                load_TestForm(item, true);

                //item.ComicLink = txtLinkToComic.Text;

                //_scraper = new Scraper(item.ComicLink);

                //var result = _scraper.Scrape_Comic(item);

                //MessageBox.Show(result.Data, result.Result.ToName(), MessageBoxButtons.OK, FormsHelper.SelectIcon(result.Result));
            }
            else
            {
                MessageBox.Show(Constants.NoComicSite, Enums.ResultTypes.NoAction.ToName(), MessageBoxButtons.OK, FormsHelper.SelectIcon(Enums.ResultTypes.NoAction));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void cmbSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckIfSelectedItemIsValid();
        }

        private void CheckIfSelectedItemIsValid()
        {
            if (!(cmbSites.SelectedItem is ComicModel))
            {
                btnScrape.Enabled = false;
            }
            else
            {
                btnScrape.Enabled = true;
            }
        }

        /// <summary>
        /// Grabs Form data and formats it into an object for re-use.
        /// </summary>
        /// <returns></returns>
        private ComicModel Create_ComicModelFromForm()
        {
            return new ComicModel()
            {
                Name = TextHelper.NameFromLink(txtSiteLink.Text),
                DateAdded = DateTime.Now,
                Link = txtSiteLink.Text,
                XPath = txtXpath.Text,
                AppendDomain = chkAppendDomain.Checked,
                RemoveDimensions = chkRemoveDimensions.Checked,
                ListOfWordsToRemoveFromLink = txtWordsToRemove.Text.Split(',').ToUniqueList(),

                QueryString = txtMultiplePagesQueryString.Text.Trim(),
                Numbering = chkNumbering.Checked,
                ReplaceString = txtReplace.Text,
                ReplaceWith = txtWith.Text,

                XPathComicName = txtXpathComicTitle.Text,
                TagNameInsideImage = txtTagInsideImageAttribute.Text,
                TagToLookFor = txtTagToLookFor.Text,
                //ComicLink = txtLinkToComic.Text,//txtTestComicLink.Text,
                //PromptForHtmlIfLinkFails = chkPromptForHtmlIfLinkFails.Checked,
                DoubleCheckLinks = chkDoubleCheckLinks.Checked,
                ReplaceImageExtensionWith = txtImageExtension.Text,

                ReplaceTextInImageNames = txtReplaceTextInImages.Text,
                ReplaceTextInImageNamesWith = txtTextToUseForImageNameReplace.Text
            };
        }

        private void lblTagInsideImage_Click(object sender, EventArgs e)
        {

        }
    }
}
