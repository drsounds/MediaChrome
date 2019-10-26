using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{

    public static class UriHelper
    {
        public static Dictionary<String, String> Querystrings(Uri d)
        {
            Dictionary<String, String> QueryList = new Dictionary<string, string>();

            String[] Queries = d.Query.Split('&');
            if (d.Query == "")
                return new Dictionary<string, string>();
            foreach (String query in Queries)
            {
                try
                {
                    string[] pair = query.Split('=');
                    QueryList.Add(pair[0].Replace("?", ""), pair[1]);
                }
                catch
                {
                }
            }
            return QueryList;
        }
    }
}
