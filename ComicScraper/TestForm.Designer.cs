namespace ComicScraper
{
    partial class TestForm
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
            this.txtLinkToComic = new System.Windows.Forms.TextBox();
            this.lblLinkToComic = new System.Windows.Forms.Label();
            this.lblHtmlToScrape = new System.Windows.Forms.Label();
            this.lblOr = new System.Windows.Forms.Label();
            this.rchHtmlToScrape = new System.Windows.Forms.RichTextBox();
            this.btnTestScrape = new System.Windows.Forms.Button();
            this.scraperBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.btnCancelScraping = new System.Windows.Forms.Button();
            this.prgScrapingCompletionBar = new System.Windows.Forms.ProgressBar();
            this.lblScrapingProgress = new System.Windows.Forms.Label();
            this.btnExitBackToMain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLinkToComic
            // 
            this.txtLinkToComic.Location = new System.Drawing.Point(89, 6);
            this.txtLinkToComic.Name = "txtLinkToComic";
            this.txtLinkToComic.Size = new System.Drawing.Size(363, 20);
            this.txtLinkToComic.TabIndex = 0;
            // 
            // lblLinkToComic
            // 
            this.lblLinkToComic.AutoSize = true;
            this.lblLinkToComic.Location = new System.Drawing.Point(12, 9);
            this.lblLinkToComic.Name = "lblLinkToComic";
            this.lblLinkToComic.Size = new System.Drawing.Size(71, 13);
            this.lblLinkToComic.TabIndex = 1;
            this.lblLinkToComic.Text = "Link to Comic";
            // 
            // lblHtmlToScrape
            // 
            this.lblHtmlToScrape.AutoSize = true;
            this.lblHtmlToScrape.Location = new System.Drawing.Point(12, 68);
            this.lblHtmlToScrape.Name = "lblHtmlToScrape";
            this.lblHtmlToScrape.Size = new System.Drawing.Size(88, 13);
            this.lblHtmlToScrape.TabIndex = 2;
            this.lblHtmlToScrape.Text = "HTML To scrape";
            // 
            // lblOr
            // 
            this.lblOr.AutoSize = true;
            this.lblOr.Location = new System.Drawing.Point(12, 40);
            this.lblOr.Name = "lblOr";
            this.lblOr.Size = new System.Drawing.Size(16, 13);
            this.lblOr.TabIndex = 3;
            this.lblOr.Text = "or";
            // 
            // rchHtmlToScrape
            // 
            this.rchHtmlToScrape.Location = new System.Drawing.Point(15, 84);
            this.rchHtmlToScrape.Name = "rchHtmlToScrape";
            this.rchHtmlToScrape.Size = new System.Drawing.Size(437, 166);
            this.rchHtmlToScrape.TabIndex = 4;
            this.rchHtmlToScrape.Text = "";
            // 
            // btnTestScrape
            // 
            this.btnTestScrape.Location = new System.Drawing.Point(377, 286);
            this.btnTestScrape.Name = "btnTestScrape";
            this.btnTestScrape.Size = new System.Drawing.Size(75, 23);
            this.btnTestScrape.TabIndex = 5;
            this.btnTestScrape.Text = "Scrape";
            this.btnTestScrape.UseVisualStyleBackColor = true;
            this.btnTestScrape.Click += new System.EventHandler(this.btnTestScrape_Click);
            // 
            // scraperBackgroundWorker
            // 
            this.scraperBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.scraperBackgroundWorker_DoWork);
            this.scraperBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.scraperBackgroundWorker_ProgressChanged);
            this.scraperBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.scraperBackgroundWorker_RunWorkerCompleted);
            // 
            // btnCancelScraping
            // 
            this.btnCancelScraping.Location = new System.Drawing.Point(296, 286);
            this.btnCancelScraping.Name = "btnCancelScraping";
            this.btnCancelScraping.Size = new System.Drawing.Size(75, 23);
            this.btnCancelScraping.TabIndex = 6;
            this.btnCancelScraping.Text = "Stop";
            this.btnCancelScraping.UseVisualStyleBackColor = true;
            this.btnCancelScraping.Click += new System.EventHandler(this.btnCancelScraping_Click);
            // 
            // prgScrapingCompletionBar
            // 
            this.prgScrapingCompletionBar.Location = new System.Drawing.Point(15, 256);
            this.prgScrapingCompletionBar.Name = "prgScrapingCompletionBar";
            this.prgScrapingCompletionBar.Size = new System.Drawing.Size(437, 23);
            this.prgScrapingCompletionBar.TabIndex = 7;
            // 
            // lblScrapingProgress
            // 
            this.lblScrapingProgress.AutoSize = true;
            this.lblScrapingProgress.Location = new System.Drawing.Point(12, 291);
            this.lblScrapingProgress.Name = "lblScrapingProgress";
            this.lblScrapingProgress.Size = new System.Drawing.Size(0, 13);
            this.lblScrapingProgress.TabIndex = 8;
            // 
            // btnExitBackToMain
            // 
            this.btnExitBackToMain.Location = new System.Drawing.Point(215, 286);
            this.btnExitBackToMain.Name = "btnExitBackToMain";
            this.btnExitBackToMain.Size = new System.Drawing.Size(75, 23);
            this.btnExitBackToMain.TabIndex = 9;
            this.btnExitBackToMain.Text = "Back";
            this.btnExitBackToMain.UseVisualStyleBackColor = true;
            this.btnExitBackToMain.Click += new System.EventHandler(this.btnExitBackToMain_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 321);
            this.Controls.Add(this.btnExitBackToMain);
            this.Controls.Add(this.lblScrapingProgress);
            this.Controls.Add(this.prgScrapingCompletionBar);
            this.Controls.Add(this.btnCancelScraping);
            this.Controls.Add(this.btnTestScrape);
            this.Controls.Add(this.rchHtmlToScrape);
            this.Controls.Add(this.lblOr);
            this.Controls.Add(this.lblHtmlToScrape);
            this.Controls.Add(this.lblLinkToComic);
            this.Controls.Add(this.txtLinkToComic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scrape Test";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLinkToComic;
        private System.Windows.Forms.Label lblLinkToComic;
        private System.Windows.Forms.Label lblHtmlToScrape;
        private System.Windows.Forms.Label lblOr;
        private System.Windows.Forms.RichTextBox rchHtmlToScrape;
        private System.Windows.Forms.Button btnTestScrape;
        private System.ComponentModel.BackgroundWorker scraperBackgroundWorker;
        private System.Windows.Forms.Button btnCancelScraping;
        private System.Windows.Forms.ProgressBar prgScrapingCompletionBar;
        private System.Windows.Forms.Label lblScrapingProgress;
        private System.Windows.Forms.Button btnExitBackToMain;
    }
}