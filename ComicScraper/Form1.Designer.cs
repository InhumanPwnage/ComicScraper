﻿namespace ComicScraper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.tabScrape = new System.Windows.Forms.TabPage();
            this.btnScrape = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbSites = new System.Windows.Forms.ComboBox();
            this.lblFromSite = new System.Windows.Forms.Label();
            this.tabAddSite = new System.Windows.Forms.TabPage();
            this.lblImageExtension = new System.Windows.Forms.Label();
            this.txtImageExtension = new System.Windows.Forms.TextBox();
            this.txtTagToLookFor = new System.Windows.Forms.TextBox();
            this.lblTagToLookFor = new System.Windows.Forms.Label();
            this.txtTagInsideImageAttribute = new System.Windows.Forms.TextBox();
            this.lblTagInsideImage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtXpathComicTitle = new System.Windows.Forms.TextBox();
            this.chkNumbering = new System.Windows.Forms.CheckBox();
            this.txtWith = new System.Windows.Forms.TextBox();
            this.lblReplaceWith = new System.Windows.Forms.Label();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.lblDownloadReplace = new System.Windows.Forms.Label();
            this.txtMultiplePagesQueryString = new System.Windows.Forms.TextBox();
            this.lblMultiplePagesQueryString = new System.Windows.Forms.Label();
            this.chkRemoveDimensions = new System.Windows.Forms.CheckBox();
            this.chkAppendDomain = new System.Windows.Forms.CheckBox();
            this.txtWordsToRemove = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtXpath = new System.Windows.Forms.TextBox();
            this.txtSiteLink = new System.Windows.Forms.TextBox();
            this.lblSiteLink = new System.Windows.Forms.Label();
            this.lblPathToCollection = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReplaceTextInImages = new System.Windows.Forms.TextBox();
            this.lblReplaceImageNameWith = new System.Windows.Forms.Label();
            this.txtTextToUseForImageNameReplace = new System.Windows.Forms.TextBox();
            this.chkDoubleCheckLinks = new System.Windows.Forms.CheckBox();
            this.tabOptions.SuspendLayout();
            this.tabScrape.SuspendLayout();
            this.tabAddSite.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.tabScrape);
            this.tabOptions.Controls.Add(this.tabAddSite);
            this.tabOptions.Location = new System.Drawing.Point(1, 1);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(459, 438);
            this.tabOptions.TabIndex = 0;
            // 
            // tabScrape
            // 
            this.tabScrape.Controls.Add(this.btnScrape);
            this.tabScrape.Controls.Add(this.btnDelete);
            this.tabScrape.Controls.Add(this.cmbSites);
            this.tabScrape.Controls.Add(this.lblFromSite);
            this.tabScrape.Location = new System.Drawing.Point(4, 22);
            this.tabScrape.Name = "tabScrape";
            this.tabScrape.Padding = new System.Windows.Forms.Padding(3);
            this.tabScrape.Size = new System.Drawing.Size(451, 412);
            this.tabScrape.TabIndex = 0;
            this.tabScrape.Text = "Scrape";
            this.tabScrape.UseVisualStyleBackColor = true;
            // 
            // btnScrape
            // 
            this.btnScrape.Location = new System.Drawing.Point(276, 68);
            this.btnScrape.Name = "btnScrape";
            this.btnScrape.Size = new System.Drawing.Size(75, 23);
            this.btnScrape.TabIndex = 13;
            this.btnScrape.Text = "Scrape";
            this.btnScrape.UseVisualStyleBackColor = true;
            this.btnScrape.Click += new System.EventHandler(this.btnScrape_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(276, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Delete Site";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbSites
            // 
            this.cmbSites.FormattingEnabled = true;
            this.cmbSites.Location = new System.Drawing.Point(83, 14);
            this.cmbSites.Name = "cmbSites";
            this.cmbSites.Size = new System.Drawing.Size(187, 21);
            this.cmbSites.TabIndex = 10;
            this.cmbSites.SelectedIndexChanged += new System.EventHandler(this.cmbSites_SelectedIndexChanged);
            // 
            // lblFromSite
            // 
            this.lblFromSite.AutoSize = true;
            this.lblFromSite.Location = new System.Drawing.Point(26, 17);
            this.lblFromSite.Name = "lblFromSite";
            this.lblFromSite.Size = new System.Drawing.Size(51, 13);
            this.lblFromSite.TabIndex = 0;
            this.lblFromSite.Text = "From Site";
            // 
            // tabAddSite
            // 
            this.tabAddSite.Controls.Add(this.chkDoubleCheckLinks);
            this.tabAddSite.Controls.Add(this.txtTextToUseForImageNameReplace);
            this.tabAddSite.Controls.Add(this.lblReplaceImageNameWith);
            this.tabAddSite.Controls.Add(this.txtReplaceTextInImages);
            this.tabAddSite.Controls.Add(this.label3);
            this.tabAddSite.Controls.Add(this.lblImageExtension);
            this.tabAddSite.Controls.Add(this.txtImageExtension);
            this.tabAddSite.Controls.Add(this.txtTagToLookFor);
            this.tabAddSite.Controls.Add(this.lblTagToLookFor);
            this.tabAddSite.Controls.Add(this.txtTagInsideImageAttribute);
            this.tabAddSite.Controls.Add(this.lblTagInsideImage);
            this.tabAddSite.Controls.Add(this.label2);
            this.tabAddSite.Controls.Add(this.txtXpathComicTitle);
            this.tabAddSite.Controls.Add(this.chkNumbering);
            this.tabAddSite.Controls.Add(this.txtWith);
            this.tabAddSite.Controls.Add(this.lblReplaceWith);
            this.tabAddSite.Controls.Add(this.txtReplace);
            this.tabAddSite.Controls.Add(this.lblDownloadReplace);
            this.tabAddSite.Controls.Add(this.txtMultiplePagesQueryString);
            this.tabAddSite.Controls.Add(this.lblMultiplePagesQueryString);
            this.tabAddSite.Controls.Add(this.chkRemoveDimensions);
            this.tabAddSite.Controls.Add(this.chkAppendDomain);
            this.tabAddSite.Controls.Add(this.txtWordsToRemove);
            this.tabAddSite.Controls.Add(this.label1);
            this.tabAddSite.Controls.Add(this.btnTest);
            this.tabAddSite.Controls.Add(this.btnAdd);
            this.tabAddSite.Controls.Add(this.txtXpath);
            this.tabAddSite.Controls.Add(this.txtSiteLink);
            this.tabAddSite.Controls.Add(this.lblSiteLink);
            this.tabAddSite.Controls.Add(this.lblPathToCollection);
            this.tabAddSite.Location = new System.Drawing.Point(4, 22);
            this.tabAddSite.Name = "tabAddSite";
            this.tabAddSite.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddSite.Size = new System.Drawing.Size(451, 412);
            this.tabAddSite.TabIndex = 1;
            this.tabAddSite.Text = "Add Site";
            this.tabAddSite.UseVisualStyleBackColor = true;
            // 
            // lblImageExtension
            // 
            this.lblImageExtension.AutoSize = true;
            this.lblImageExtension.Location = new System.Drawing.Point(252, 258);
            this.lblImageExtension.Name = "lblImageExtension";
            this.lblImageExtension.Size = new System.Drawing.Size(85, 13);
            this.lblImageExtension.TabIndex = 36;
            this.lblImageExtension.Text = "Image Extension";
            // 
            // txtImageExtension
            // 
            this.txtImageExtension.Location = new System.Drawing.Point(343, 255);
            this.txtImageExtension.Name = "txtImageExtension";
            this.txtImageExtension.Size = new System.Drawing.Size(87, 20);
            this.txtImageExtension.TabIndex = 35;
            // 
            // txtTagToLookFor
            // 
            this.txtTagToLookFor.Location = new System.Drawing.Point(163, 255);
            this.txtTagToLookFor.Name = "txtTagToLookFor";
            this.txtTagToLookFor.Size = new System.Drawing.Size(83, 20);
            this.txtTagToLookFor.TabIndex = 34;
            // 
            // lblTagToLookFor
            // 
            this.lblTagToLookFor.AutoSize = true;
            this.lblTagToLookFor.Location = new System.Drawing.Point(84, 258);
            this.lblTagToLookFor.Name = "lblTagToLookFor";
            this.lblTagToLookFor.Size = new System.Drawing.Size(76, 13);
            this.lblTagToLookFor.TabIndex = 33;
            this.lblTagToLookFor.Text = "Tag to look for";
            // 
            // txtTagInsideImageAttribute
            // 
            this.txtTagInsideImageAttribute.Location = new System.Drawing.Point(163, 229);
            this.txtTagInsideImageAttribute.Name = "txtTagInsideImageAttribute";
            this.txtTagInsideImageAttribute.Size = new System.Drawing.Size(83, 20);
            this.txtTagInsideImageAttribute.TabIndex = 32;
            // 
            // lblTagInsideImage
            // 
            this.lblTagInsideImage.AutoSize = true;
            this.lblTagInsideImage.Location = new System.Drawing.Point(33, 232);
            this.lblTagInsideImage.Name = "lblTagInsideImage";
            this.lblTagInsideImage.Size = new System.Drawing.Size(125, 13);
            this.lblTagInsideImage.TabIndex = 31;
            this.lblTagInsideImage.Text = "Attribute inside image tag";
            this.lblTagInsideImage.Click += new System.EventHandler(this.lblTagInsideImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "XPath to Comic Title";
            // 
            // txtXpathComicTitle
            // 
            this.txtXpathComicTitle.Location = new System.Drawing.Point(9, 97);
            this.txtXpathComicTitle.Name = "txtXpathComicTitle";
            this.txtXpathComicTitle.Size = new System.Drawing.Size(421, 20);
            this.txtXpathComicTitle.TabIndex = 29;
            // 
            // chkNumbering
            // 
            this.chkNumbering.AutoSize = true;
            this.chkNumbering.Location = new System.Drawing.Point(255, 231);
            this.chkNumbering.Name = "chkNumbering";
            this.chkNumbering.Size = new System.Drawing.Size(94, 17);
            this.chkNumbering.TabIndex = 9;
            this.chkNumbering.Text = "Do Numbering";
            this.chkNumbering.UseVisualStyleBackColor = true;
            // 
            // txtWith
            // 
            this.txtWith.Location = new System.Drawing.Point(287, 203);
            this.txtWith.Name = "txtWith";
            this.txtWith.Size = new System.Drawing.Size(143, 20);
            this.txtWith.TabIndex = 13;
            // 
            // lblReplaceWith
            // 
            this.lblReplaceWith.AutoSize = true;
            this.lblReplaceWith.Location = new System.Drawing.Point(252, 206);
            this.lblReplaceWith.Name = "lblReplaceWith";
            this.lblReplaceWith.Size = new System.Drawing.Size(29, 13);
            this.lblReplaceWith.TabIndex = 28;
            this.lblReplaceWith.Text = "With";
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(163, 203);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(83, 20);
            this.txtReplace.TabIndex = 12;
            // 
            // lblDownloadReplace
            // 
            this.lblDownloadReplace.AutoSize = true;
            this.lblDownloadReplace.Location = new System.Drawing.Point(6, 206);
            this.lblDownloadReplace.Name = "lblDownloadReplace";
            this.lblDownloadReplace.Size = new System.Drawing.Size(154, 13);
            this.lblDownloadReplace.TabIndex = 26;
            this.lblDownloadReplace.Text = "To Download Images, Replace";
            // 
            // txtMultiplePagesQueryString
            // 
            this.txtMultiplePagesQueryString.Location = new System.Drawing.Point(163, 177);
            this.txtMultiplePagesQueryString.Name = "txtMultiplePagesQueryString";
            this.txtMultiplePagesQueryString.Size = new System.Drawing.Size(83, 20);
            this.txtMultiplePagesQueryString.TabIndex = 10;
            // 
            // lblMultiplePagesQueryString
            // 
            this.lblMultiplePagesQueryString.AutoSize = true;
            this.lblMultiplePagesQueryString.Location = new System.Drawing.Point(23, 180);
            this.lblMultiplePagesQueryString.Name = "lblMultiplePagesQueryString";
            this.lblMultiplePagesQueryString.Size = new System.Drawing.Size(131, 13);
            this.lblMultiplePagesQueryString.TabIndex = 24;
            this.lblMultiplePagesQueryString.Text = "QueryString for Skip/Take";
            // 
            // chkRemoveDimensions
            // 
            this.chkRemoveDimensions.AutoSize = true;
            this.chkRemoveDimensions.Location = new System.Drawing.Point(255, 179);
            this.chkRemoveDimensions.Name = "chkRemoveDimensions";
            this.chkRemoveDimensions.Size = new System.Drawing.Size(169, 17);
            this.chkRemoveDimensions.TabIndex = 11;
            this.chkRemoveDimensions.Text = "Remove Dimensions from Link";
            this.chkRemoveDimensions.UseVisualStyleBackColor = true;
            // 
            // chkAppendDomain
            // 
            this.chkAppendDomain.AutoSize = true;
            this.chkAppendDomain.Location = new System.Drawing.Point(322, 21);
            this.chkAppendDomain.Name = "chkAppendDomain";
            this.chkAppendDomain.Size = new System.Drawing.Size(102, 17);
            this.chkAppendDomain.TabIndex = 5;
            this.chkAppendDomain.Text = "Append Domain";
            this.chkAppendDomain.UseVisualStyleBackColor = true;
            // 
            // txtWordsToRemove
            // 
            this.txtWordsToRemove.Location = new System.Drawing.Point(163, 151);
            this.txtWordsToRemove.Name = "txtWordsToRemove";
            this.txtWordsToRemove.Size = new System.Drawing.Size(83, 20);
            this.txtWordsToRemove.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "CSV Words to Remove from url";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(274, 281);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 14;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(355, 281);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtXpath
            // 
            this.txtXpath.Location = new System.Drawing.Point(9, 58);
            this.txtXpath.Name = "txtXpath";
            this.txtXpath.Size = new System.Drawing.Size(421, 20);
            this.txtXpath.TabIndex = 6;
            // 
            // txtSiteLink
            // 
            this.txtSiteLink.Location = new System.Drawing.Point(9, 19);
            this.txtSiteLink.Name = "txtSiteLink";
            this.txtSiteLink.Size = new System.Drawing.Size(307, 20);
            this.txtSiteLink.TabIndex = 4;
            // 
            // lblSiteLink
            // 
            this.lblSiteLink.AutoSize = true;
            this.lblSiteLink.Location = new System.Drawing.Point(6, 3);
            this.lblSiteLink.Name = "lblSiteLink";
            this.lblSiteLink.Size = new System.Drawing.Size(55, 13);
            this.lblSiteLink.TabIndex = 23;
            this.lblSiteLink.Text = "Site Link *";
            // 
            // lblPathToCollection
            // 
            this.lblPathToCollection.AutoSize = true;
            this.lblPathToCollection.Location = new System.Drawing.Point(6, 42);
            this.lblPathToCollection.Name = "lblPathToCollection";
            this.lblPathToCollection.Size = new System.Drawing.Size(180, 13);
            this.lblPathToCollection.TabIndex = 22;
            this.lblPathToCollection.Text = "XPath To Collection/Grid of images *";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Replace text in image names";
            // 
            // txtReplaceTextInImages
            // 
            this.txtReplaceTextInImages.Location = new System.Drawing.Point(163, 125);
            this.txtReplaceTextInImages.Name = "txtReplaceTextInImages";
            this.txtReplaceTextInImages.Size = new System.Drawing.Size(83, 20);
            this.txtReplaceTextInImages.TabIndex = 38;
            // 
            // lblReplaceImageNameWith
            // 
            this.lblReplaceImageNameWith.AutoSize = true;
            this.lblReplaceImageNameWith.Location = new System.Drawing.Point(252, 128);
            this.lblReplaceImageNameWith.Name = "lblReplaceImageNameWith";
            this.lblReplaceImageNameWith.Size = new System.Drawing.Size(29, 13);
            this.lblReplaceImageNameWith.TabIndex = 39;
            this.lblReplaceImageNameWith.Text = "With";
            // 
            // txtTextToUseForImageNameReplace
            // 
            this.txtTextToUseForImageNameReplace.Location = new System.Drawing.Point(287, 125);
            this.txtTextToUseForImageNameReplace.Name = "txtTextToUseForImageNameReplace";
            this.txtTextToUseForImageNameReplace.Size = new System.Drawing.Size(143, 20);
            this.txtTextToUseForImageNameReplace.TabIndex = 40;
            // 
            // chkDoubleCheckLinks
            // 
            this.chkDoubleCheckLinks.AutoSize = true;
            this.chkDoubleCheckLinks.Location = new System.Drawing.Point(255, 153);
            this.chkDoubleCheckLinks.Name = "chkDoubleCheckLinks";
            this.chkDoubleCheckLinks.Size = new System.Drawing.Size(118, 17);
            this.chkDoubleCheckLinks.TabIndex = 41;
            this.chkDoubleCheckLinks.Text = "Double Check links";
            this.chkDoubleCheckLinks.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 441);
            this.Controls.Add(this.tabOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comic Scraper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabOptions.ResumeLayout(false);
            this.tabScrape.ResumeLayout(false);
            this.tabScrape.PerformLayout();
            this.tabAddSite.ResumeLayout(false);
            this.tabAddSite.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tabScrape;
        private System.Windows.Forms.TabPage tabAddSite;
        private System.Windows.Forms.Label lblSiteLink;
        private System.Windows.Forms.Label lblPathToCollection;
        private System.Windows.Forms.ComboBox cmbSites;
        private System.Windows.Forms.Label lblFromSite;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtXpath;
        private System.Windows.Forms.TextBox txtSiteLink;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnScrape;
        private System.Windows.Forms.CheckBox chkRemoveDimensions;
        private System.Windows.Forms.CheckBox chkAppendDomain;
        private System.Windows.Forms.TextBox txtWordsToRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkNumbering;
        private System.Windows.Forms.TextBox txtWith;
        private System.Windows.Forms.Label lblReplaceWith;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label lblDownloadReplace;
        private System.Windows.Forms.TextBox txtMultiplePagesQueryString;
        private System.Windows.Forms.Label lblMultiplePagesQueryString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtXpathComicTitle;
        private System.Windows.Forms.TextBox txtTagInsideImageAttribute;
        private System.Windows.Forms.Label lblTagInsideImage;
        private System.Windows.Forms.TextBox txtTagToLookFor;
        private System.Windows.Forms.Label lblTagToLookFor;
        private System.Windows.Forms.Label lblImageExtension;
        private System.Windows.Forms.TextBox txtImageExtension;
        private System.Windows.Forms.TextBox txtTextToUseForImageNameReplace;
        private System.Windows.Forms.Label lblReplaceImageNameWith;
        private System.Windows.Forms.TextBox txtReplaceTextInImages;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkDoubleCheckLinks;
    }
}

