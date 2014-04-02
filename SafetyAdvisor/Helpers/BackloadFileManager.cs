using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Helpers
{
    public static class BackloadFileManager
    {
        // we should read this from the config file using backload itself...no time for that now
        private static string ROOT_FOLDER = "~/files";

        public static IEnumerable<string> GetFiles(int objectId)
        {
            string _path = GetFolderForObject(objectId);
            if (Directory.Exists(_path))
            {
                return Directory.GetFiles(_path).Select(f => Path.GetFileName(f));
            }
            else
            {
                return new List<string>();
            }
        }

        public static string GetDownloadUrl(string filename, int objectId)
        {
            return string.Format("{0}/{1}/{2}", VirtualPathUtility.ToAbsolute(ROOT_FOLDER), objectId.ToString(), filename);
        }

        public static void DeleteAllFiles(int objectId)
        {
            string _folderPath = GetFolderForObject(objectId);
            if (Directory.Exists(_folderPath))
            {
                Directory.Delete(_folderPath, true);
            }
        }

        private static string GetFolderForObject(int objectId)
        { 
            string _path = Path.Combine(HttpContext.Current.Server.MapPath(ROOT_FOLDER), objectId.ToString());
            return _path;
        }
    }
}