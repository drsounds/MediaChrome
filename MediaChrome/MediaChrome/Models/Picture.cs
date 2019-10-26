using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    public class Picture : Resource
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
