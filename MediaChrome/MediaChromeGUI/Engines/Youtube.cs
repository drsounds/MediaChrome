﻿/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 2010-10-30
 * Time: 11:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using MediaChrome;
using MediaChrome.Views;

namespace MediaChrome
{
	/// <summary>
	/// Description of Youtube.
	/// </summary>
	public class Youtube : MediaChrome.IPlayEngine
    {
        public System.Drawing.Icon SystemIcon
        {
            get
            {
                return Properties.Resources.app_icon;
            }
        }
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
        /// <summary>
        /// Returns an song by the specified ISRC
        /// </summary>
        /// <param name="ISRC"></param>
        /// <returns></returns>
        public Song GetSongFromISRC(string ISRC) { return new Song(); }
        /// <summary>
        /// Returns an artist by the specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Artist ArtistFromID(string ID) { return new Artist(); }

        /// <summary>
        /// Returns an album by the specified UPC
        /// </summary>
        /// <param name="UPC"></param>
        /// <returns></returns>
        public Album AlbumFromUPC(string UPC) { return new Album(); }

        public List<Song> QueryRadio(String Query)
        {
            return new List<Song>();
        }
        public bool Paused { get; set; }
        public Song ConvertSongFromLink(String URI)
        {
            throw new NotImplementedException();
        }
        public Song CurrentSong { get; set; }
        public String AudioSignature
        {
            get
            {
                return "youtube:";
            }
        }
        public List<Song> Import(String query,ref float progress)
        {
            return new List<Song>();
        }
        #region DefaultValues
        public String Address { get; set; }
        public String Company { get; set; }
        public String Copyright { get; set; }
        public bool LoggedIn { get; set; }
        public bool Login() { return false; }
        public void Logout() { }
        public bool Purchase(Song song) { return false; }
        public Uri ServiceUri { get; set; }
        public void ShowOptions() { }
        public bool DownloadStore { get; set; }
        public Uri CompanyWebSite { get; set; }
        public List<Song> Purchases { get; set; }
        public bool Streaming { get; set; }
        public string Link;
        #endregion
        public System.Drawing.Image Icon
        {
            get
            {
                return MediaChrome.Properties.Resources.YouTube_logo;
            }
        }
        public MediaChrome.Artist[] FindArtist(string ID)
        {
            throw new NotImplementedException();
        }
        public MediaChrome.Album[] FindAlbum(string ID)
        {
            throw new NotImplementedException();
        }
        public MediaChrome.Album GetAlbum(Artist artist,string ID)
        {
            throw new NotImplementedException();
        }
        public MediaChrome.Album GetAlbum(string ID)
        {
            throw new NotImplementedException();
        }
        public MediaChrome.Artist GetArtist(string ID)
        {
            throw new NotImplementedException();
        }

        public bool PlaylistsLoaded { get; set; }
        public event EventHandler PlaybackFinished;
        public Form Host { get; set; }
        private int position;
        public int Position
        {
            get
            {
                return position;
            }
        }

        Timer _PlayTimer;
        public List<Song> LoadPlaylist(String ID,ref MediaChrome.Views.Playlist playlist)
        {
            return new List<Song>();
        }
      
        public Playlist CreatePlaylist(String Name)
        {
            throw new NotImplementedException();
        }
        public String Status
        { get; set; }
        public void SongImport(Song[] query)
        {
        }
        public String Image
        {
            get
            {
                return "youtube.png";
            }
        }
		public String Title
		{
			get
			{
				return "You Tube";
			}
		}
		public String Namespace
		{
			get
			{
				return "youtube";
			}
		}
        private double duration = 100;
        public double Duration
        {
            get

            { return duration; }
        }

		WebBrowser YouPlayer;
		public Song RawFind(MediaChrome.Song _Song2)
		{
            WebClient CN = new WebClient();

            List<Song> songs = new List<Song>();
                XmlDocument D = new XmlDocument();

            D.Load("http://gdata.youtube.com/feeds/api/videos?q=" + (_Song2.Title+" "+_Song2.Artist).Replace(" ", "+") + "&v=1");
            var Items = D.GetElementsByTagName("entry");
            foreach (XmlElement Item in Items)
            {
                MediaChrome.Song _Song = new MediaChrome.Song();
                String Name = Item.GetElementsByTagName("title")[0].InnerText;
                _Song.Title = Name;
                _Song.Artist = "Youtube";
                if (Name.Contains("-"))
                {
                    String[] markup = Name.Split('-');
                    _Song.Title = markup[1].Trim(' ');
                    _Song.Artist = markup[0].Trim(' ');

                }
                // http://www.youtube.com/apiplayer?enablejsapi=1&version=3
           //   _Song.Path="youtube:"+((XmlElement)Item.GetElementsByTagName("link")[3]).GetAttribute("href").Replace("http://gdata.youtube.com/feeds/api/videos/","").Replace("?v=1","");
                _Song.Path = "youtube:" + ((XmlElement)Item.GetElementsByTagName("link")[0]).GetAttribute("href"); _Song.Engine = this;
                _Song.Store = "Youtube";
                if (_Song.Title.Contains(_Song2.Title) && _Song.Title.Contains(_Song2.Artist)) 
                    return _Song;
              

            }
            return null;
		}
		public Youtube()
		{
			YouPlayer=new WebBrowser();

            if (r == null)
            {
                r = new Form();
                r.Controls.Add(YouPlayer);
                YouPlayer.Dock = DockStyle.Fill;
                r.Show();
            }
            YouPlayer.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(YouPlayer_DocumentCompleted);
		}

        void YouPlayer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                YouPlayer.Navigate("javascript:play()");
            }
            catch { }
        }
		public Control MediaControl
		{
			get
			{
				return YouPlayer;
			}
		
		}
		public bool Ready {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public int FilesCompleted {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public int TotalFiles {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
        private string length = "100";
		public string Length {
			get {
                return length;
			}
		}
		
		public System.Collections.Generic.List<MediaChrome.Song> Find(string Query)
		{
			WebClient CN = new WebClient();
			
			List<Song> songs = new List<Song>();
			XmlDocument D = new XmlDocument();
			
			D.Load("http://gdata.youtube.com/feeds/api/videos?q="+Query.Replace(" ","+")+"&v=1");
			var Items =  D.GetElementsByTagName("entry");
			foreach(XmlElement Item in Items)
			{
				MediaChrome.Song _Song = new MediaChrome.Song();
				String Name = Item.GetElementsByTagName("title")[0].InnerText;
				_Song.Title=Name;
				_Song.Artist="Youtube";
				if(Name.Contains("-"))
				{
					String[] markup = Name.Split('-');
					_Song.Title = markup[1].Trim(' ');
					_Song.Artist = markup[0].Trim(' ');
					
				}
				try{
                    //_Song.Path="youtube:"+((XmlElement)Item.GetElementsByTagName("link")[3]).GetAttribute("href").Replace("http://gdata.youtube.com/feeds/api/videos/","").Replace("?v=1","");
                _Song.Path = "youtube:" + ((XmlElement)Item.GetElementsByTagName("link")[0]).GetAttribute("href");
                _Song.Engine = this;
				_Song.Store="Youtube";
				songs.Add(_Song);
				}
				catch
				{
				
				}
					
			}
			return songs;

			
		}
		
		public void Play()
		{
            if (_PlayTimer != null)
                _PlayTimer.Stop();
            _PlayTimer = new Timer();
            _PlayTimer.Tick += new EventHandler(_PlayTimer_Tick);
            
        }

        void _PlayTimer_Tick(object sender, EventArgs e)
        {
            position += 10;
            
        }
		
		public void Pause()
		{
            YouPlayer.Navigate("javascript:pause()");
            _PlayTimer.Stop();
		}
		
		public void Stop()
		{
            YouPlayer.Navigate("javascript:stop()");
		}
		
		public void Seek(double pos)
		{
           
            YouPlayer.Navigate("javascript:seekTo(" + ((float)(((float)pos)/100.0f)).ToString().Replace(",",".") + "*parseInt(document.getElementById('duration').innerHTML))");
            position = (int)pos;
        }
        Form r;
        [STAThread]
		public void Load(string Content)
		{
            if (YouPlayer == null)
			{
				
	
				YouPlayer.Show();
				
			}
            YouPlayer.Navigate(Content.Replace("youtube:", ""));
       
			//YouPlayer.Navigate("http://code.google.com/intl/en-US/apis/youtube/youtube_player_demo.html");
      
            //      YouPlayer.Navigate("javascript:loadVideo('" + Content.Replace("youtube:", "") + "',0,'default')");
            
         
            //   YouPlayer.Navigate(Content.Replace("youtube:",""));
		}
		
		public void ImportEx(object Conn, string RootDir)
		{
			throw new NotImplementedException();
		}
		
		public void Import(object Conn, string RootDir)
		{
			throw new NotImplementedException();
		}
		
		public void Unload()
		{
			throw new NotImplementedException();
		}
		
		public System.Collections.Generic.List<MediaChrome.Song> Search()
		{
			return new List<MediaChrome.Song>();
		}
		
	
		
		public bool hasPlaylists {
			get {
				throw new NotImplementedException();
			}
		}
		
		public Playlist ViewPlaylist(string Name,string PlsID)
		{
			throw new NotImplementedException();
		}
		
		public void AddToPlaylist(string playlistID, Song _Song, int pos)
		{
			
		}
		public void AddToPlaylist(Playlist plst, Song _Song, int pos)
		{
			
		}
		public void RemoveFromPlaylist(string playlistID, int pos)
		{
		
		}
		
		public void MoveSongPlaylist(string playlistID,Song _Song, int startLoc, int endLoc)
		{
			
		}
		
		public List<MediaChrome.Views.Playlist> Playlists {
			get {
				return null;
			}
			set {
				
			}
		}

        public Dictionary<string, object> Parameters
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

        public object InvokeCommand(string command, params object[] arguments)
        {
            throw new NotImplementedException();
        }
    }
}
