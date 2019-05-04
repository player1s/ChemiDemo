using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemiDemo.Web.Utility
{
    public class Downloader
    {
        public static object Download(string uri, string name)
        {

            Console.WriteLine("been here ------------------ {0}", uri);
            object o = null;
            using (var client = new System.Net.WebClient())
            {
                try
                {
                    client.DownloadFile(uri, name + ".pdf");
                }
                
                catch(System.Net.WebException e)
                {

                }
            }
            return o;
        }
    }
}
