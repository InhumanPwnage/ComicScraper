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
        private Scraper _scraper;

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            ComicModel model = Create_ComicModelFromForm();

            _scraper = new Scraper(txtTestComicLink.Text);

            var result = _scraper.Test_Scrape_Comic(model);

            MessageBox.Show(result.Data, result.Result.ToName(), MessageBoxButtons.OK, SelectIcon(result.Result));
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

            MessageBox.Show(result.Data, result.Result.ToName(), MessageBoxButtons.OK, SelectIcon(result.Result));
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

            MessageBox.Show(result.Data, result.Result.ToName(), MessageBoxButtons.OK, SelectIcon(result.Result));
        }

        private void btnScrape_Click(object sender, EventArgs e)
        {
            if (cmbSites.SelectedItem is ComicModel && TextHelper.StringIsWebsite(txtLinkToComic.Text))
            {
                var item = (ComicModel)cmbSites.SelectedItem;

                item.ComicLink = txtLinkToComic.Text;

                _scraper = new Scraper(item.ComicLink);

                var result = _scraper.Scrape_Comic(item);

                MessageBox.Show(result.Data, result.Result.ToName(), MessageBoxButtons.OK, SelectIcon(result.Result));
            }
            else
            {
                MessageBox.Show(Constants.NoComicSite, Enums.ResultTypes.NoAction.ToName(), MessageBoxButtons.OK, SelectIcon(Enums.ResultTypes.NoAction));
            }
        }

        /// <summary>
        /// Pick the appropriate Message Box Icon and message to display according to Result type.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private MessageBoxIcon SelectIcon(Enums.ResultTypes result)
        {
            MessageBoxIcon iconToUse = MessageBoxIcon.None;

            switch (result)
            {
                case Enums.ResultTypes.Error:
                    {
                        iconToUse = MessageBoxIcon.Error;
                        break;
                    }
                case Enums.ResultTypes.Success:
                    {
                        iconToUse = MessageBoxIcon.Information;
                        break;
                    }
                case Enums.ResultTypes.NoAction:
                    {
                        iconToUse = MessageBoxIcon.Question;
                        break;
                    }
                case Enums.ResultTypes.Overwrite:
                    {
                        iconToUse = MessageBoxIcon.Warning;
                        break;
                    }
                case Enums.ResultTypes.SettingsNotFound:
                    {
                        iconToUse = MessageBoxIcon.Exclamation;
                        break;
                    }
                case Enums.ResultTypes.SuccessDelete:
                    {
                        iconToUse = MessageBoxIcon.Information;
                        break;
                    }
            }

            return iconToUse;
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
                ComicLink = txtTestComicLink.Text
            };
        }
    }
}
