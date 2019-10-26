using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    

    /// <summary>
    /// Class for album
    /// </summary>
    public class Album : Artistic, IMedia
    {
        public Album() : base()
        {
            tracks = new List<Track>();
        }
        /// <summary>
        /// Songs of the album
        /// </summary>
        public List<Track> tracks { get; set; }
        
        /// <summary>
        /// The engine the album is using
        /// </summary>
        public IPlayEngine Engine { get; set; }
    }

}
