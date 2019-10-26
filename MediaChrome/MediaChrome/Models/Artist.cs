using MediaChrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{

    /// <summary>
    /// An artist
    /// </summary>
    public class Artist : Model, IMedia
    {
        public Artist() : base()
        {

            albums = new List<Album>();
        }
        /// <summary>
        /// Available albums for the artist
        /// </summary>
        public List<Album> albums { get; set; }
       
        /// <summary>
        /// The engine the album is using
        /// </summary>
        public IPlayEngine Engine { get; set; }
    }
    /// <summary>
    /// Description of IPlayEngine.
    /// </summary>
    /// 
}
