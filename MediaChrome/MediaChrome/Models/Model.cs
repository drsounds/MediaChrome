using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    public abstract class Model
    {
        public string id { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public Model()
        {
            images = new List<Picture>();
        }
        public List<Picture> images { get; set; }
    }
}
