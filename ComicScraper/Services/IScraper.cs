using ComicScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Services
{
    public interface IScraper
    {
        ResultModel Scrape_Comic(ComicModel model);

        ResultModel Test_Scrape_Comic(ComicModel model);
    }
}
