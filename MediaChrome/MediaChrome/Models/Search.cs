using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    public class SearchResult : Resource
    {
        public SearchResult()
        {
            artists = new Result<Artist>();
            tracks = new Result<Track>();
            users = new Result<User>();
            playlists = new Result<Playlist>();
            albums = new List<Album>();
        }

        public Result<Artist> artists { get; set; }
        public Result<Track> tracks { get; set; }
        public Result<User> users { get; set; }
        public Result<Playlist> playlists { get; set; }
        public List<Album> albums { get; private set; }
    }
}
