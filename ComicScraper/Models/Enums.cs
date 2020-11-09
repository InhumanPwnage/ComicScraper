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

        public enum FileExtensions 
        {
            [Description("JPEG")]
            jpg = 0,

            [Description("JPEG 2")]
            jpeg = 1,

            [Description("GIF")]
            gif = 2,

            [Description("PNG")]
            png = 3,
        }
    }
}
