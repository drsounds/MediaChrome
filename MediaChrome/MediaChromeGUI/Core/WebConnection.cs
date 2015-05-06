using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MediaChrome
{
    static class WebConnection
    {
      
        /// <summary>
        /// Opens an web connection
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static WebClient OpenWebConnection(Uri Url)
        {
            return new WebClient();
        }
    }
}
