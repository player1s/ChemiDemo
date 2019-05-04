using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemiDemo.Web.Utility
{
    public class Downloader
    {
        public static bool Download(string uri, string name)
        {
            bool isDownloaded = false;

            Console.WriteLine("been here ------------------ {0}", uri);
            object o = null;

            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(uri, name + ".pdf");
                    isDownloaded = true;
                }
            }

            catch (System.Net.WebException e)
            {

            }
            return isDownloaded;
        }
           
        }
    }

