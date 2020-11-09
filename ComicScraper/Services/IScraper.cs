using ComicScraper.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Services
{
    public interface IScraper
    {
        (ResultModel, ConcurrentDictionary<long, string>) Scrape_Comic(ComicModel model, bool isProperScrape);

        //(ResultModel, List<string>) Test_Scrape_Comic(ComicModel model);
    }
}
