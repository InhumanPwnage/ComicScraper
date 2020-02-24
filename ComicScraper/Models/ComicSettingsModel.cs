using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Models
{
    public class ComicSettingsModel
    {
        public DateTime LastUpdated { get; set; }

        public List<ComicModel> Comics { get; set; }
    }
}
