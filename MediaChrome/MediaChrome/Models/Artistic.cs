using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    public class Artistic : Model
    {

        /// <summary>
        /// Album artist
        /// </summary>
        public List<Artist> artists { get; set; }

    }
}
