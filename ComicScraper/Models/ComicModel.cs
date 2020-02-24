﻿using System;
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
    }
}