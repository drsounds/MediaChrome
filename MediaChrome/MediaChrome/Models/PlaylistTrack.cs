using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    public class PlaylistTrack : Track
    {   
        public Playlist playlist { get; set; }
        public User added_by { get; set; }
        public DateTime added_at { get; set; }
        public Track track { get; set; }
    }
}
