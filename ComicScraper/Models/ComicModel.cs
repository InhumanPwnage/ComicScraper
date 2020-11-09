using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Models
{
    public class ComicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string Link { get; set; }
        public string XPath { get; set; }

        public List<string> ListOfWordsToRemoveFromLink { get; set; }
        public bool AppendDomain { get; set; }
        public bool RemoveDimensions { get; set; }

        public string QueryString { get; set; }
        public bool Numbering { get; set; }
        public string ReplaceString { get; set; }
        public string ReplaceWith { get; set; }

        public string XPathComicName { get; set; }
        public string TagNameInsideImage { get; set; }
        public string TagToLookFor { get; set; }
        public string ReplaceImageExtensionWith { get; set; }
        public string ReplaceTextInImageNames { get; set; }
        public string ReplaceTextInImageNamesWith { get; set; }
        public bool DoubleCheckLinks { get; set; }

        //public bool PromptForHtmlIfLinkFails { get; set; }


        [JsonIgnore]
        public string ComicLink { get; set; }
    }
}
