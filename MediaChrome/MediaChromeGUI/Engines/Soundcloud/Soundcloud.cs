using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MediaChrome
{
    public class Soundcloud : IPlayEngine
    {
        /// <summary>
        /// Get an custom property of the engine
        /// </summary>
        /// <param name="prop">name of property</param>
        public object GetProperty(string prop)
        {
            return null;
        }

        /// <summary>
        /// Set an custom property of the engine
        /// </summary>
        /// <param name="prop">property name</param>
        /// <param name="val">value in object</param>
        public void SetProperty(string prop, object val)
        {
        }
        public Icon SystemIcon
        {
            get
            {
                return Properties.Resources.app_icon;
            }
        }
        public Song GetSongFromISRC(string ISRC)
        {
            throw new NotImplementedException();
        }

        public Artist ArtistFromID(string ID)
        {
            throw new NotImplementedException();
        }

        public Album AlbumFromUPC(string UPC)
        {
            throw new NotImplementedException();
        }

        public List<Song> QueryRadio(string Query)
        {
            throw new NotImplementedException();
        }

        public bool Paused
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Song ConvertSongFromLink(string URI)
        {
            throw new NotImplementedException();
        }

        public Song CurrentSong
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string AudioSignature
        {
            get { throw new NotImplementedException(); }
        }

        public void ShowOptions()
        {
            throw new NotImplementedException();
        }

        public string Copyright
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Address
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Company
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Uri CompanyWebSite
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Uri ServiceUri
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Purchase(Song song)
        {
            throw new NotImplementedException();
        }

        public bool LoggedIn
        {
            get;set;
        }

        public bool Login()
        {
            return false;
        }

        public bool Streaming
        {
            get { throw new NotImplementedException(); }
        }

        public bool DownloadStore
        {
            get { throw new NotImplementedException(); }
        }

        public List<Song> Purchases
        {
            get { throw new NotImplementedException(); }
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public string Image
        {
            get { throw new NotImplementedException(); }
        }

        public System.Drawing.Image Icon
        {
            get { throw new NotImplementedException(); }
        }

        public Artist GetArtist(string ID)
        {
            throw new NotImplementedException();
        }

        public Artist[] FindArtist(string Query)
        {
            throw new NotImplementedException();
        }

        public Album GetAlbum(Artist artist, string album)
        {
            throw new NotImplementedException();
        }

        public Album GetAlbum(string album)
        {
            throw new NotImplementedException();
        }

        public Album[] FindAlbum(string album)
        {
            throw new NotImplementedException();
        }

        public event EventHandler PlaybackFinished;

        public bool hasPlaylists
        {
            get { throw new NotImplementedException(); }
        }

        public string Status
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Windows.Forms.Control MediaControl
        {
            get { throw new NotImplementedException(); }
        }

        public bool Ready
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public double Duration
        {
            get { throw new NotImplementedException(); }
        }

        public int FilesCompleted
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool PlaylistsLoaded
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Namespace
        {
            get { throw new NotImplementedException(); }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public int TotalFiles
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<Song> Find(string Query)
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public int Position
        {
            get { throw new NotImplementedException(); }
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Seek(double pos)
        {
            throw new NotImplementedException();
        }

        public void Load(string Content)
        {
            throw new NotImplementedException();
        }

        public List<Song> Import(string RootDir, ref float progress)
        {
            throw new NotImplementedException();
        }

        public System.Windows.Forms.Form Host
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }

        public List<Song> Search()
        {
            throw new NotImplementedException();
        }

        public Song RawFind(Song _Song)
        {
            throw new NotImplementedException();
        }

        public List<Views.Playlist> Playlists
        {
            get { throw new NotImplementedException(); }
        }

        public Views.Playlist ViewPlaylist(string Name, string PlsID)
        {
            throw new NotImplementedException();
        }

        public Views.Playlist CreatePlaylist(string Name)
        {
            throw new NotImplementedException();
        }

        public void AddToPlaylist(string playlistID, Song _Song, int pos)
        {
            throw new NotImplementedException();
        }

        public void AddToPlaylist(Views.Playlist pls, Song _Song, int pos)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromPlaylist(string playlistID, int pos)
        {
            throw new NotImplementedException();
        }

        public void MoveSongPlaylist(string playlistID, Song entry, int startLoc, int endLoc)
        {
            throw new NotImplementedException();
        }

        public string Length
        {
            get { throw new NotImplementedException(); }
        }

        public List<Song> LoadPlaylist(string p, ref Views.Playlist playlist)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Parameters
        {
            get;
            set;
        }

        public object InvokeCommand(string command, params object[] arguments)
        {
            switch (command)
            {

            }
            return new object();
        }
    }
}
