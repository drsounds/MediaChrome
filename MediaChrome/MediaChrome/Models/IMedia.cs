using MediaChrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    /// <summary>
    /// A IMedia represents the instances of songs,artists and albums
    /// </summary>
    public interface IMedia
    {
        /// <summary>
        /// The link to the artist
        /// </summary>
        string url { get; set; }
        string id { get; set; }

        /// <summary>
        /// The name of the artist
        /// </summary>
        string name { get; set; }
        /// <summary>
        /// The engine the album is using
        /// </summary>
        IPlayEngine Engine { get; set; }
    }
}
