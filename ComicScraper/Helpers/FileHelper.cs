using ComicScraper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicScraper.Helpers
{
    public static class FileHelper
    {
        private static readonly string settingsPath = $@"{AppDomain.CurrentDomain.BaseDirectory}{Constants.FileName}";

        /// <summary>
        /// Adds a new Comic to the Dropdown list of available Comic sites to scrape from.
        /// </summary>
        /// <param name="model">The Comic to add.</param>
        /// <param name="updatedListOfComics">Updated list of comics.</param>
        /// <returns>Result Model.</returns>
        public static ResultModel Add_ComicSite(ComicModel model)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Result = Enums.ResultTypes.Error,
                Data = Constants.Error,
                Occurrence = DateTime.Now
            };

            ComicSettingsModel settingsModel = null;

            if (SettingsFile_Exists() && Read_SettingsFile(out settingsModel).Result == Enums.ResultTypes.Success)
            {
                int highest = 0;
                foreach (var comic in settingsModel.Comics)
                {
                    if (comic.Id > highest)
                        highest = comic.Id;
                }

                model.Id = highest + 1;
                settingsModel.Comics.Add(model);

                modelToReturn.Result = Enums.ResultTypes.Success;
                modelToReturn.Data = Constants.AddedComicSite;
            }
            else
            {
                model.Id = 1;

                //create a new settings model and add the new site
                settingsModel = new ComicSettingsModel()
                {
                    LastUpdated = DateTime.Now,
                    Comics = new List<ComicModel>() { model }
                };

                modelToReturn.Result = Enums.ResultTypes.SettingsNotFound; 
                modelToReturn.Data = Constants.NewSettingsFile;
            }

            //this will create or overwrite the settings file
            Create_SettingsFile(settingsModel);

            return modelToReturn;
        }

        /// <summary>
        /// Removes an existing Comic site from the list.
        /// </summary>
        /// <param name="id">The Comic's ID to find and remove.</param>
        /// <param name="updatedListOfComics">An updated list of Comic sites.</param>
        /// <returns>Result Model.</returns>
        public static ResultModel Delete_ComicSite(int id)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Result = Enums.ResultTypes.Error,
                Data = Constants.Error,
                Occurrence = DateTime.Now
            };

            if (id <= 0)
            {
                modelToReturn.Result = Enums.ResultTypes.NoAction;
                modelToReturn.Data = Constants.NoComicSite;
            }
            else
            {
                try
                {
                    var resultRead = Read_SettingsFile(out ComicSettingsModel settingsFile);

                    if (settingsFile != null && resultRead.Result == Enums.ResultTypes.Success)
                    {
                        settingsFile.Comics = settingsFile.Comics.Where(note => note.Id != id).ToList();

                        Create_SettingsFile(settingsFile);

                        modelToReturn.Result = Enums.ResultTypes.SuccessDelete;
                        modelToReturn.Data = Constants.DeletedComicSite;
                    }
                }
                catch (Exception ex)
                {
                    modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
                }
                
            }

            return modelToReturn;
        }

        public static ResultModel Read_SettingsFile(out ComicSettingsModel settingsModel)
        {
            var modelToReturn = Folder_Exists(settingsPath);
            settingsModel = null;

            try
            {
                if (modelToReturn.Result == Enums.ResultTypes.Success)
                {
                    using (StreamReader r = new StreamReader(settingsPath))
                    {
                        settingsModel = JsonConvert.DeserializeObject<ComicSettingsModel>(r.ReadToEnd()) ?? null;
                    }
                }
            }
            catch (Exception ex)
            {
                modelToReturn.Data = $"{ex.Message}\n{ex.StackTrace}";
                modelToReturn.Result = Enums.ResultTypes.Error;
            }

            return modelToReturn;
        }

        private static bool SettingsFile_Exists()
        {
            return File.Exists(settingsPath);
        }

        private static void Create_SettingsFile(ComicSettingsModel settingsModel)
        {
            File.WriteAllText(
                settingsPath, 
                JsonConvert.SerializeObject(settingsModel)
                );
        }

        /// <summary>
        /// Checks if a folder exists within the project. In order to check if deeper levels exist, provide them in the folderName parameter separated by backslashes.
        /// Example: Comics\NXT-Comics
        /// </summary>
        /// <param name="folderName">The Name of the folder, inside the project folder, to check.</param>
        /// <returns>Result model.</returns>
        public static ResultModel Folder_Exists(string folderName)
        {
            ResultModel modelToReturn = new ResultModel()
            {
                Occurrence = DateTime.Now,
                Result = Enums.ResultTypes.Error
            };

            //$@"{AppDomain.CurrentDomain.BaseDirectory}\{folderName}"

            if (Directory.Exists(folderName))
            {
                modelToReturn.Result = Enums.ResultTypes.Overwrite;
            }

            return modelToReturn;
        }

        public static void Create_Folder(string pathWithFolderName)
        {
            Directory.CreateDirectory(pathWithFolderName);
        }

        public static void Delete_Folder(string pathToFolder)
        {
            Directory.Delete(pathToFolder, true);
        }
    }
}
