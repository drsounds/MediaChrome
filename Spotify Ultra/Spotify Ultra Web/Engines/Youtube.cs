/*
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
        public MediaChrome.Artist[] FindArtist(string ID)
        {
            throw new NotImplementedException();
        }
        public MediaChrome.Album[] FindAlbum(string ID)
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
        public SpofityRuntime.Form1 Host { get; set; }
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
              _Song.Path="youtube:"+((XmlElement)Item.GetElementsByTagName("link")[3]).GetAttribute("href").Replace("http://gdata.youtube.com/feeds/api/videos/","").Replace("?v=1","");
              //  _Song.Path = "youtube:" + ((XmlElement)Item.GetElementsByTagName("link")[0]).GetAttribute("href"); _Song.Engine = "youtube";
                _Song.Store = "Youtube";
                if (_Song.Title.Contains(_Song2.Title) && _Song.Title.Contains(_Song2.Artist)) 
                    return _Song;
              

            }
            return null;
		}
		public Youtube()
		{
			YouPlayer=new WebBrowser();
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
                _Song.Path="youtube:"+((XmlElement)Item.GetElementsByTagName("link")[3]).GetAttribute("href").Replace("http://gdata.youtube.com/feeds/api/videos/","").Replace("?v=1","");
                //_Song.Path = "youtube:" + ((XmlElement)Item.GetElementsByTagName("link")[0]).GetAttribute("href");
                _Song.Engine = "youtube";
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
            YouPlayer.Navigate("javascript:play()");
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
		
		public void Load(string Content)
		{
			if(YouPlayer==null)
			{
				
	
				YouPlayer.Show();
				
			}
            YouPlayer.Show();
            YouPlayer.Navigate("about:blank");
			YouPlayer.Navigate("http://code.google.com/intl/en-US/apis/youtube/youtube_player_demo.html");
            YouPlayer.Navigate("javascript:loadVideo('" + Content.Replace("youtube:", "") + "',0,'default')");
        
         //   YouPlayer.Navigate(Content.Replace("youtube:",""));
		}
		
		public void ImportEx(System.Data.SQLite.SQLiteConnection Conn, string RootDir)
		{
			throw new NotImplementedException();
		}
		
		public void Import(System.Data.SQLite.SQLiteConnection Conn, string RootDir)
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
		
		public void RemoveFromPlaylist(string playlistID, int pos)
		{
		
		}
		
		public void MoveSongPlaylist(string playlistID, int startLoc, int endLoc)
		{
			
		}
		
		public List<MediaChrome.Views.Playlist> Playlists {
			get {
				return null;
			}
			set {
				
			}
		}
	}
}
