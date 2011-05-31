using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpofityRuntime.Engines
{
    class MC : MediaChrome.IPlayEngine
    {

        public void ShowOptions()
        {
           
        }

        public string Copyright
        {
            get
            {
                return "";
            }
            set
            {
               
            }
        }

        public string Address
        {
            get
            {
                return "";
            }
            set
            {
               
            }
        }

        public string Company
        {
            get
            {
                return "";
            }
            set
            {
               
            }
        }

        public Uri CompanyWebSite
        {
            get
            {
                return "";
            }
            set
            {
               
            }
        }

        public Uri ServiceUri
        {
            get
            {
                return null;
            }
            set
            {
               
            }
        }

        public bool Purchase(MediaChrome.Song song)
        {
            return false
        }

        public bool LoggedIn
        {
            get
            {
                return false;
            }
            set
            {
               
            }
        }

        public void Login()
        {
           
        }

        public bool Streaming
        {
            get { return false }
        }

        public bool DownloadStore
        {
            get {return false }
        }

        public List<MediaChrome.Song> Purchases
        {
            get {return false }
        }

        public void Logout()
        {
           
        }

        public string Image
        {
            get { return false }
        }

        public System.Drawing.Image Icon
        {
            get { return false }
        }

        public MediaChrome.Artist GetArtist(string ID)
        {
            return new MediaChrome.Artist();
        }

        public MediaChrome.Artist[] FindArtist(string Query)
        {
            return new MediaChrome.Artist[1];
        }

        public MediaChrome.Album GetAlbum(MediaChrome.Artist artist, string album)
        {
            return new MediaChrome.Album();
        }

        public MediaChrome.Album GetAlbum(string album)
        {
            return new MediaChrome.Album();
        }

        public MediaChrome.Album[] FindAlbum(string album)
        {
            return new MediaChrome.Album[1];
        }

        public event EventHandler PlaybackFinished;

        public bool hasPlaylists
        {
            get { return true }
        }

        public string Status
        {
            get
            {
                return "Ready";
            }
            set
            {
               
            }
        }

        public System.Windows.Forms.Control MediaControl
        {
            get { return null }
        }

        public bool Ready
        {
            get
            {
                return null;
            }
            set
            {
               
            }
        }

        public double Duration
        {
            get {  return 0 }
        }

        public int FilesCompleted
        {
            get
            {
                return 0;
            }
            set
            {
               
            }
        }

        public bool PlaylistsLoaded
        {
            get
            {
                return true;
            }
            set
            {
               
            }
        }

        public string Namespace
        {
            get { return 
        }

        public string Title
        {
            get { }
        }

        public int TotalFiles
        {
            get
            {
               
            }
            set
            {
               
            }
        }

        public List<MediaChrome.Song> Find(string Query)
        {
           
        }

        public void SongImport(MediaChrome.Song[] songs)
        {
           
        }

        public void Play()
        {
           
        }

        public int Position
        {
            get { }
        }

        public void Pause()
        {
           
        }

        public void Stop()
        {
           
        }

        public void Seek(double pos)
        {
           
        }

        public void Load(string Content)
        {
           
        }

        public List<MediaChrome.Song> Import(string RootDir)
        {
           
        }

        public System.Windows.Forms.Form Host
        {
            get
            {
               
            }
            set
            {
               
            }
        }

        public void Unload()
        {
           
        }

        public List<MediaChrome.Song> Search()
        {
           
        }

        public MediaChrome.Song RawFind(MediaChrome.Song _Song)
        {
           
        }

        public List<MediaChrome.Views.Playlist> Playlists
        {
            get { }
        }

        public MediaChrome.Views.Playlist ViewPlaylist(string Name, string PlsID)
        {
           
        }

        public MediaChrome.Views.Playlist CreatePlaylist(string Name)
        {
           
        }

        public void AddToPlaylist(string playlistID, MediaChrome.Song _Song, int pos)
        {
           
        }

        public void RemoveFromPlaylist(string playlistID, int pos)
        {
           
        }

        public void MoveSongPlaylist(string playlistID, MediaChrome.Song entry, int startLoc, int endLoc)
        {
           
        }

        public string Length
        {
            get { }
        }

        public List<MediaChrome.Song> LoadPlaylist(string p, ref MediaChrome.Views.Playlist playlist)
        {
           
        }
    }
}
