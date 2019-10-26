using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    public class Result<T> where T : Model
    {
        public string href { get; set; }
        public List<T> items { get; set; }
        public Result()
        {
            items = new List<T>();
        }
        
    }
}
