using ComicScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComicScraper.Helpers
{
    public static class FormsHelper
    {

        /// <summary>
        /// Pick the appropriate Message Box Icon and message to display according to Result type.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static MessageBoxIcon SelectIcon(Enums.ResultTypes result)
        {
            MessageBoxIcon iconToUse = MessageBoxIcon.None;

            switch (result)
            {
                case Enums.ResultTypes.Error:
                    {
                        iconToUse = MessageBoxIcon.Error;
                        break;
                    }
                case Enums.ResultTypes.Success:
                    {
                        iconToUse = MessageBoxIcon.Information;
                        break;
                    }
                case Enums.ResultTypes.NoAction:
                    {
                        iconToUse = MessageBoxIcon.Question;
                        break;
                    }
                case Enums.ResultTypes.Overwrite:
                    {
                        iconToUse = MessageBoxIcon.Warning;
                        break;
                    }
                case Enums.ResultTypes.SettingsNotFound:
                    {
                        iconToUse = MessageBoxIcon.Exclamation;
                        break;
                    }
                case Enums.ResultTypes.SuccessDelete:
                    {
                        iconToUse = MessageBoxIcon.Information;
                        break;
                    }
            }

            return iconToUse;
        }
    }
}
