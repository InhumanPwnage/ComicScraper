namespace ComicScraper
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
            this.tabAddSite = new System.Windows.Forms.TabPage();
            this.lblPathToCollection = new System.Windows.Forms.Label();
            this.lblSiteLink = new System.Windows.Forms.Label();
            this.lblFromSite = new System.Windows.Forms.Label();
            this.cmbSites = new System.Windows.Forms.ComboBox();
            this.lblComicLink = new System.Windows.Forms.Label();
            this.txtLinkToComic = new System.Windows.Forms.TextBox();
            this.txtSiteLink = new System.Windows.Forms.TextBox();
            this.txtXpath = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtTestComicLink = new System.Windows.Forms.TextBox();
            this.lblTestComic = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnScrape = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWordsToRemove = new System.Windows.Forms.TextBox();
            this.chkAppendDomain = new System.Windows.Forms.CheckBox();
            this.chkRemoveDimensions = new System.Windows.Forms.CheckBox();
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
            this.tabOptions.Size = new System.Drawing.Size(408, 207);
            this.tabOptions.TabIndex = 0;
            // 
            // tabScrape
            // 
            this.tabScrape.Controls.Add(this.btnScrape);
            this.tabScrape.Controls.Add(this.btnDelete);
            this.tabScrape.Controls.Add(this.txtLinkToComic);
            this.tabScrape.Controls.Add(this.lblComicLink);
            this.tabScrape.Controls.Add(this.cmbSites);
            this.tabScrape.Controls.Add(this.lblFromSite);
            this.tabScrape.Location = new System.Drawing.Point(4, 22);
            this.tabScrape.Name = "tabScrape";
            this.tabScrape.Padding = new System.Windows.Forms.Padding(3);
            this.tabScrape.Size = new System.Drawing.Size(400, 181);
            this.tabScrape.TabIndex = 0;
            this.tabScrape.Text = "Scrape";
            this.tabScrape.UseVisualStyleBackColor = true;
            // 
            // tabAddSite
            // 
            this.tabAddSite.Controls.Add(this.chkRemoveDimensions);
            this.tabAddSite.Controls.Add(this.chkAppendDomain);
            this.tabAddSite.Controls.Add(this.txtWordsToRemove);
            this.tabAddSite.Controls.Add(this.label1);
            this.tabAddSite.Controls.Add(this.lblTestComic);
            this.tabAddSite.Controls.Add(this.txtTestComicLink);
            this.tabAddSite.Controls.Add(this.btnTest);
            this.tabAddSite.Controls.Add(this.btnAdd);
            this.tabAddSite.Controls.Add(this.txtXpath);
            this.tabAddSite.Controls.Add(this.txtSiteLink);
            this.tabAddSite.Controls.Add(this.lblSiteLink);
            this.tabAddSite.Controls.Add(this.lblPathToCollection);
            this.tabAddSite.Location = new System.Drawing.Point(4, 22);
            this.tabAddSite.Name = "tabAddSite";
            this.tabAddSite.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddSite.Size = new System.Drawing.Size(400, 181);
            this.tabAddSite.TabIndex = 1;
            this.tabAddSite.Text = "Add Site";
            this.tabAddSite.UseVisualStyleBackColor = true;
            // 
            // lblPathToCollection
            // 
            this.lblPathToCollection.AutoSize = true;
            this.lblPathToCollection.Location = new System.Drawing.Point(6, 42);
            this.lblPathToCollection.Name = "lblPathToCollection";
            this.lblPathToCollection.Size = new System.Drawing.Size(101, 13);
            this.lblPathToCollection.TabIndex = 22;
            this.lblPathToCollection.Text = "XPath To Collection";
            // 
            // lblSiteLink
            // 
            this.lblSiteLink.AutoSize = true;
            this.lblSiteLink.Location = new System.Drawing.Point(6, 3);
            this.lblSiteLink.Name = "lblSiteLink";
            this.lblSiteLink.Size = new System.Drawing.Size(48, 13);
            this.lblSiteLink.TabIndex = 23;
            this.lblSiteLink.Text = "Site Link";
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
            // cmbSites
            // 
            this.cmbSites.FormattingEnabled = true;
            this.cmbSites.Location = new System.Drawing.Point(83, 14);
            this.cmbSites.Name = "cmbSites";
            this.cmbSites.Size = new System.Drawing.Size(157, 21);
            this.cmbSites.TabIndex = 10;
            // 
            // lblComicLink
            // 
            this.lblComicLink.AutoSize = true;
            this.lblComicLink.Location = new System.Drawing.Point(6, 43);
            this.lblComicLink.Name = "lblComicLink";
            this.lblComicLink.Size = new System.Drawing.Size(71, 13);
            this.lblComicLink.TabIndex = 2;
            this.lblComicLink.Text = "Link to Comic";
            // 
            // txtLinkToComic
            // 
            this.txtLinkToComic.Location = new System.Drawing.Point(9, 59);
            this.txtLinkToComic.Name = "txtLinkToComic";
            this.txtLinkToComic.Size = new System.Drawing.Size(345, 20);
            this.txtLinkToComic.TabIndex = 11;
            // 
            // txtSiteLink
            // 
            this.txtSiteLink.Location = new System.Drawing.Point(9, 19);
            this.txtSiteLink.Name = "txtSiteLink";
            this.txtSiteLink.Size = new System.Drawing.Size(383, 20);
            this.txtSiteLink.TabIndex = 4;
            // 
            // txtXpath
            // 
            this.txtXpath.Location = new System.Drawing.Point(9, 58);
            this.txtXpath.Name = "txtXpath";
            this.txtXpath.Size = new System.Drawing.Size(383, 20);
            this.txtXpath.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(317, 152);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(317, 123);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtTestComicLink
            // 
            this.txtTestComicLink.Location = new System.Drawing.Point(9, 97);
            this.txtTestComicLink.Name = "txtTestComicLink";
            this.txtTestComicLink.Size = new System.Drawing.Size(383, 20);
            this.txtTestComicLink.TabIndex = 6;
            // 
            // lblTestComic
            // 
            this.lblTestComic.AutoSize = true;
            this.lblTestComic.Location = new System.Drawing.Point(6, 81);
            this.lblTestComic.Name = "lblTestComic";
            this.lblTestComic.Size = new System.Drawing.Size(60, 13);
            this.lblTestComic.TabIndex = 20;
            this.lblTestComic.Text = "Test Comic";
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
            // btnScrape
            // 
            this.btnScrape.Location = new System.Drawing.Point(276, 85);
            this.btnScrape.Name = "btnScrape";
            this.btnScrape.Size = new System.Drawing.Size(75, 23);
            this.btnScrape.TabIndex = 13;
            this.btnScrape.Text = "Scrape";
            this.btnScrape.UseVisualStyleBackColor = true;
            this.btnScrape.Click += new System.EventHandler(this.btnScrape_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "CSV Words to Remove";
            // 
            // txtWordsToRemove
            // 
            this.txtWordsToRemove.Location = new System.Drawing.Point(129, 125);
            this.txtWordsToRemove.Name = "txtWordsToRemove";
            this.txtWordsToRemove.Size = new System.Drawing.Size(182, 20);
            this.txtWordsToRemove.TabIndex = 7;
            // 
            // chkAppendDomain
            // 
            this.chkAppendDomain.AutoSize = true;
            this.chkAppendDomain.Location = new System.Drawing.Point(9, 156);
            this.chkAppendDomain.Name = "chkAppendDomain";
            this.chkAppendDomain.Size = new System.Drawing.Size(102, 17);
            this.chkAppendDomain.TabIndex = 8;
            this.chkAppendDomain.Text = "Append Domain";
            this.chkAppendDomain.UseVisualStyleBackColor = true;
            // 
            // chkRemoveDimensions
            // 
            this.chkRemoveDimensions.AutoSize = true;
            this.chkRemoveDimensions.Location = new System.Drawing.Point(129, 156);
            this.chkRemoveDimensions.Name = "chkRemoveDimensions";
            this.chkRemoveDimensions.Size = new System.Drawing.Size(169, 17);
            this.chkRemoveDimensions.TabIndex = 9;
            this.chkRemoveDimensions.Text = "Remove Dimensions from Link";
            this.chkRemoveDimensions.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 209);
            this.Controls.Add(this.tabOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
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
        private System.Windows.Forms.TextBox txtLinkToComic;
        private System.Windows.Forms.Label lblComicLink;
        private System.Windows.Forms.ComboBox cmbSites;
        private System.Windows.Forms.Label lblFromSite;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtXpath;
        private System.Windows.Forms.TextBox txtSiteLink;
        private System.Windows.Forms.Label lblTestComic;
        private System.Windows.Forms.TextBox txtTestComicLink;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnScrape;
        private System.Windows.Forms.CheckBox chkRemoveDimensions;
        private System.Windows.Forms.CheckBox chkAppendDomain;
        private System.Windows.Forms.TextBox txtWordsToRemove;
        private System.Windows.Forms.Label label1;
    }
}

