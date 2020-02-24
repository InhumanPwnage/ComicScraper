using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Models
{
    public class ResultModel
    {
        public Enums.ResultTypes Result { get; set; }

        public dynamic Data { get; set; }

        public DateTime Occurrence { get; set; }
    }
}
