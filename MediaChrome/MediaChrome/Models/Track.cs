using MediaChrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    public class Track : Artistic, IMedia
    {
        public static String findVersion(String text)
        {
            bool inVersion = false;
            String ver = "";
            foreach (Char d in text)
            {
                if (inVersion)
                    ver += d;
                if (d == ')')
                {
                    inVersion = false;
                    continue;
                }
                if (d == '(')
                {
                    inVersion = true;
                    continue;
                }
            }
            return ver;
        }
        public static String findCommit(String text)
        {
            bool inVersion = false;
            String ver = "";
            foreach (Char d in text)
            {

                if (d == ']')
                {
                    inVersion = false;
                    continue;
                }
                if (d == '[')
                {
                    inVersion = true;
                    continue;
                }
                if (inVersion)
                    ver += d;
            }
            return ver;
        }
        public static Track GetSongFromURI(String D)
        {
            Track P = new Track();



            P.Version = findVersion(D);
            P.Contributing = findCommit(D);
            D = D.Replace("song://", "http://");
            Uri Url = new System.Uri(D.Replace("(" + P.Version + ")", "").Replace("[" + P.Contributing + "]", "").Replace("{", "").Replace("}", ""));


            P.artists = new List<Artist>() { new Artist() { name = Url.Segments[0].Replace("/", "").Replace("%20", " ") } };//Url.Host.Replace("_"," ");
            P.name = Url.Segments[1].Replace("/", "").Replace("%20", " ");



            P.AlbumName = Url.Segments[2].Replace("/", "").Replace("%20", " ");
            try
            {
                P.ProposedEngine = UriHelper.Querystrings(Url)["service"];
            }
            catch
            {

            }
            P.uri = D;
            try
            {
                P.id = UriHelper.Querystrings(Url)["id"];
            }
            catch
            {

            }
            return P;
        }

        /// <summary>
        /// Decides whether the song is checked.
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Address to an bitmap file with coverart, either locally or remote
        /// </summary>
        public string CoverArt { get; set; }


        public string Artist
        {
            get
            {
                if (artists != null)
                    return artists[0].name;
                return "";
            }
            set
            {
                if (artists == null)
                    artists = new List<Artist>();
                artists.Add(new Artist() { name = value });
            }
        }

        /// <summary>
        /// Gets and sets the artist's uri of the track
        /// </summary>
        public string ArtistUrl { get; set; }

        /// <summary>
        /// Gets and sets the service URI for the album of the song
        /// </summary>
        public string AlbumUrl { get; set; }

        /// <summary>
        /// The album of the song.
        /// </summary>
		public Album album { get; set; }

        /// <summary>
        /// Gets the album name or sets it, making a default album instance
        /// </summary>
        public string AlbumName
        {
            get
            {
                if (album != null)
                    return album.name;
                return "";
            }
            set
            {
                if (album == null)
                    album = new Album();
                album.name = value;

            }
        }

        /// <summary>
        /// The MediaChrome' uri of the song
        /// </summary>
		public string uri { get; set; }

        /// <summary>
        /// Gets and sets the engine used for the song
        /// </summary>
        public IPlayEngine Engine
        {
            get;
            set;
        }

        /// <summary>
        /// The store of the song
        /// </summary>
		public string Store { get; set; }

        /// <summary>
        /// The version of the song
        /// </summary>
		public string Version { get; set; }

        /// <summary>
        /// Popularity of the song at the store
        /// </summary>
        public float popularity { get; set; }

        /// <summary>
        /// Gets or sets the contributing musician of the song
        /// </summary>
		public string Contributing { get; set; }

        /// <summary>
        /// Gets or sets the featured artist on the song
        /// </summary>
		public string Feature { get; set; }

        /// <summary>
        /// Gets and sets the desired Media Engine for this particular instance
        /// </summary>
		public string ProposedEngine { get; set; }

        /// <summary>
        /// Gets and sets the composer of the song
        /// </summary>
		public string Composer { get; set; }



        public bool Subsong = false;
        public bool opned = false;
        public Track ParentSong = null;

        /// <summary>
        /// Sub-songs. Not used
        /// </summary>
        private List<Track> subSongs;
        public List<Track> SubSongs
        {
            get
            {
                if (subSongs == null)
                {
                    List<Track> Songs = new List<Track>();
                    /*      SQLiteConnection Conn = MainForm.MakeConnection();
                        Conn.Open();
                        String SQL = "SELECT * FROM song WHERE artist LIKE '%" + Artist + "%' AND name LIKE '%" + Title + "%'";
                        SQLiteCommand D = new SQLiteCommand(SQL, Conn);
                        SQLiteDataReader SQLDR = D.ExecuteReader();
                        while (SQLDR.Read())
                        {
                            Song _Song = new Song();
                            _Song.Title = (string)SQLDR["name"];
                            _Song.Artist = (string)SQLDR["artist"];
                            _Song.ParentSong = this;
                            _Song.Album = (string)SQLDR["album"];
                            _Song.Path = (string)SQLDR["path"];
                            Songs.Add(_Song);
                        }
                        subSongs = Songs;*/
                    Songs = new List<Track>();
                    return Songs;
                }
                else
                {
                    return subSongs;
                }
            }
        }

    }

}
