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

        public static IEnumerable<string> GetFiles(string objectContext)
        {
            string _path = Path.Combine(HttpContext.Current.Server.MapPath(ROOT_FOLDER), objectContext);
            if (Directory.Exists(_path))
            {
                return Directory.GetFiles(_path).Select(f => Path.GetFileName(f));
            }
            else
            {
                return new List<string>();
            }
        }

        public static string GetDownloadUrl(string filename, string objectContext)
        {
            return string.Format("{0}/{1}/{2}", VirtualPathUtility.ToAbsolute(ROOT_FOLDER), objectContext, filename);
        }
    }
}