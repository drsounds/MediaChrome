using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using MediaChrome;
namespace SpofityRuntime
{
    public class Aze : IPlayEngine 
    {

        public void ShowOptions()
        {
            /* throw new NotImplementedException(); */
        }

        public string Copyright
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public string Address
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public string Company
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public Uri CompanyWebSite
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public Uri ServiceUri
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public bool Purchase(Song song)
        {
            /* throw new NotImplementedException(); */
        }

        public bool LoggedIn
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public void Login()
        {
            /* throw new NotImplementedException(); */
        }

        public bool Streaming
        {
            get { return true }
        }

        public bool DownloadStore
        {
            get { return true }
        }

        public List<Song> Purchases
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public void Logout()
        {
            /* throw new NotImplementedException(); */
        }

        public string Image
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public System.Drawing.Image Icon
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public Artist GetArtist(string ID)
        {
            /* throw new NotImplementedException(); */
        }

        public Artist[] FindArtist(string Query)
        {
            /* throw new NotImplementedException(); */
        }

        public Album GetAlbum(string album)
        {
            /* throw new NotImplementedException(); */
        }

        public Album[] FindAlbum(string album)
        {
            /* throw new NotImplementedException(); */
        }

        public event EventHandler PlaybackFinished;

        public bool hasPlaylists
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public string Status
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public System.Windows.Forms.Control MediaControl
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public bool Ready
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public double Duration
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public int FilesCompleted
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public bool PlaylistsLoaded
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public string Namespace
        {
            get { return ""; }
        }

        public string Title
        {
            get { return ""; }
        }

        public int TotalFiles
        {
            get;
            set;
        }

        public List<Song> Find(string Query)
        {
            /* throw new NotImplementedException(); */
        }

        public void SongImport(Song[] songs)
        {
            /* throw new NotImplementedException(); */
        }

        public void Play()
        {
            /* throw new NotImplementedException(); */
        }

        public int Position
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public void Pause()
        {
            /* throw new NotImplementedException(); */
        }

        public void Stop()
        {
            /* throw new NotImplementedException(); */
        }

        public void Seek(double pos)
        {
            /* throw new NotImplementedException(); */
        }

        public void Load(string Content)
        {
            /* throw new NotImplementedException(); */
        }

        public void Import(System.Data.SQLite.SQLiteConnection Conn, string RootDir)
        {
            /* throw new NotImplementedException(); */
        }

        public System.Windows.Forms.Form Host
        {
            get
            {
                /* throw new NotImplementedException(); */
            }
            set
            {
                /* throw new NotImplementedException(); */
            }
        }

        public void Unload()
        {
            /* throw new NotImplementedException(); */
        }

        public List<Song> Search()
        {
            /* throw new NotImplementedException(); */
        }

        public Song RawFind(Song _Song)
        {
            /* throw new NotImplementedException(); */
        }

        public List<MediaChrome.Views.Playlist> Playlists
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public MediaChrome.Views.Playlist ViewPlaylist(string Name, string PlsID)
        {
            /* throw new NotImplementedException(); */
        }

        public MediaChrome.Views.Playlist CreatePlaylist(string Name)
        {
            /* throw new NotImplementedException(); */
        }

        public void AddToPlaylist(string playlistID, Song _Song, int pos)
        {
            /* throw new NotImplementedException(); */
        }

        public void RemoveFromPlaylist(string playlistID, int pos)
        {
            /* throw new NotImplementedException(); */
        }

        public void MoveSongPlaylist(string playlistID, Song entry, int startLoc, int endLoc)
        {
            /* throw new NotImplementedException(); */
        }

        public string Length
        {
            get { /* /* throw new NotImplementedException(); */ }
        }

        public List<Song> LoadPlaylist(string p, ref MediaChrome.Views.Playlist playlist)
        {
            /* throw new NotImplementedException(); */
        }
    }
}
