using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Models
{
    public class Enums
    {
        public enum ResultTypes
        {
            [Description("Error")]
            Error = 0,

            [Description("Success")]
            Success = 1,

            [Description("Success")]
            SettingsNotFound = 2,

            [Description("No action performed")]
            NoAction = 3,

            [Description("Folder exists")]
            Overwrite = 4,

            [Description("Success")]
            SuccessDelete = 6,
        }
    }
}
