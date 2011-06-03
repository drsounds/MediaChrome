/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 2010-10-31
 * Time: 10:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using MediaChrome;

using WMPLib;
using MediaChrome.Views;
namespace MediaChrome
{
	/// <summary>
	/// Description of Mp3.
	/// </summary>
	public class MP3Player : IPlayEngine
    {
        public List<Song> Import(String query)
        {
            return new List<Song>();
        }
        #region DefaultValues
        public String Address { get; set; }
        public String Company { get; set; }
        public String Copyright { get; set; }
        public bool LoggedIn { get; set; }
        public void Login() { }
        public void Logout() { }
        public bool Purchase(Song song) { return false; }
        public Uri ServiceUri { get; set; }
        public void ShowOptions()      {      }
        public bool DownloadStore { get; set; }
        public Uri CompanyWebSite { get; set; }
        public List<Song> Purchases { get; set; }
        public bool Streaming { get; set; }
        public string Link;
        #endregion
        public System.Drawing.Image Icon { get; set; }
        public MediaChrome.Album[] FindAlbum(string ID)
        {
            return new Album[] { };
        }
        public MediaChrome.Artist[] FindArtist(string ID)
        {
            return new Artist[] { };
        }
      

        public MediaChrome.Album GetAlbum(Artist artist, string ID)
        {
            Album R = new Album();
            SQLiteConnection D = MediaChrome.MainForm.MakeConnection();
            SQLiteDataReader r = new SQLiteCommand("SELECT name,artist,album,path FROM song WHERE album = '" + ID + "' AND artist='"+artist.Name+"' ", D).ExecuteReader();
            
            // Create temporary list of songs

            List<Song> songs = new List<Song>();
            
            // Add all songs from the result
            while (r.Read())
            {
                Song d = new Song();
                d.Album = R;
                d.Artists = new Artist[] { artist };
                d.Name = (string)r["name"];
                d.Path = (string)r["path"];
                d.Link = (string)r["path"];
                d.Engine = this;
                songs.Add(d);

            }
            R.Songs = songs.ToArray();
            return R;
        }
        public MediaChrome.Album GetAlbum(string ID)
        {
            Album R = new Album();
            SQLiteConnection D = MediaChrome.MainForm.MakeConnection();
            SQLiteDataReader r = new SQLiteCommand("SELECT name,artist,album,path FROM song WHERE album = '" + ID + "'", D).ExecuteReader();

            // Create temporary list of songs

            List<Song> songs = new List<Song>();

            // Add all songs from the result
            while (r.Read())
            {
                Song d = new Song();
                d.Album = R;
                d.Artist = (string)r["artist"];
                d.Name = (string)r["name"];
                d.Path = (string)r["path"];
                d.Link = (string)r["path"];
                d.Engine = this;
                songs.Add(d);

            }
            R.Songs = songs.ToArray();
            return R;
        }

        /// <summary>
        /// Gets an artist for the work
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public MediaChrome.Artist GetArtist(string ID)
        {
            SQLiteConnection  D = MediaChrome.MainForm.MakeConnection();

            Artist artist = new Artist();
            artist.Name = ID;
            artist.Link = ID;
            /***
             * Associate albums and songs to the artist
             * */
            SQLiteDataReader SQLDR = new SQLiteCommand("SELECT DISTINCT album FROM song WHERE artist='" + ID + "'", D).ExecuteReader();

            List<Album> albums = new List<Album>(); // Create an temporary list of albums
            while (SQLDR.Read())
            {
                Album r = GetAlbum(artist,(string)SQLDR["album"]);
                albums.Add(r);
            }
            Artist d = new Artist();
            d.Name = ID;
            d.Albums = albums.ToArray();
            return d;
            

        }
        public bool PlaylistsLoaded { get; set; }
        public event EventHandler PlaybackFinished;
        public Form Host { get; set; }
        Timer _Timer;
        private int position;
        public int Position 
        {
            get
            {
                try
                {
                    return (int)((player.controls.currentPosition / player.controls.currentItem.duration) * 100);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public Playlist CreatePlaylist( String Name)
        {
            if (Name == "" || Name == null)
                return null;
            String Path = MediaChrome.MainForm.DownloadDir + "\\" + Name + ".pls";
            if (!File.Exists(Path))
            {
                FileStream d = File.Create(Path);
                d.Close();
               Playlist Plst = new Playlist(this,Name+".pls","mp3:playlist:"+Name,this.Host);
               Plst.CanModify = true;
               return Plst;
            }
            else
            {
                Playlist D = ViewPlaylist(Name,"mp3:playlist:"+Name);
                D.CanModify = true;
                return D;
            }
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
                return "mp3.png";
            }
        }
		public String Title
		{
			get
			{
				return "Local Files";
			}
		}
		public String Namespace
		{
			get
			{
				return "mp3";
			}
		}
		public Song RawFind(Song D)
		{
            SQLiteConnection Conn = MediaChrome.MainForm.MakeConnection();
           
			SQLiteCommand Command = new SQLiteCommand("SELECT * FROM song WHERE name LIKE '%"+D.Title+"%' AND artist LIKE '%"+D.Artist+"%' AND  ( engine = 'mp3' OR store ='mp3')",Conn);
			SQLiteDataReader Reader = Command.ExecuteReader();
			if(Reader.HasRows)
			{

                Song result = MediaChrome.MainForm.GetSongFromQuery(Reader);
                return result;
     //("mp3:"+Reader.GetString(0));
				
				
			}
			else
			{
				return null;
			}
            return null;
            Conn.Close();
		}
	
		public Control MediaControl
		{
			get
			{
				return new Panel();
			}
			
			
		}
        

		public List<Song> Find(String Query)
		{
			List<Song> Songs = new List<Song>();
            SQLiteConnection Conn = MediaChrome.MainForm.MakeConnection();

			SQLiteCommand Command = new SQLiteCommand("SELECT name,artist,album,path,store,engine FROM song WHERE name LIKE '%"+Query+"%' OR artist LIKE '%"+Query+"%' OR album LIKE '%"+Query+"%' AND engine='mp3'",Conn);
			SQLiteDataReader  DR = Command.ExecuteReader();
			while(DR.Read())
			{
				
				Song D = new Song();
				try{
				D.Title = DR.GetString(0);
				D.Artist = DR.GetString(1);
				D.AlbumName = DR.GetString(2);
				D.Path=DR.GetString(3);
				D.Store=DR.GetString(4);
				D.Engine=this;
				}catch{}
				Songs.Add(D);
			}
			return Songs;
			
		}
		
		public bool Ready {get;set;}
		public int FilesCompleted {get;set;}
		public int TotalFiles {get;set;}
		private WMPLib.WindowsMediaPlayerClass player;
		
		public void ImportEx(SQLiteConnection Conn,string RootDir){
			
			DirectoryInfo DI = new DirectoryInfo(RootDir);
				
			FileInfo[] D = DI.GetFiles("*.mp3");
			foreach(FileInfo X in D){
				TotalFiles++;
			}
			DirectoryInfo[] Directories = DI.GetDirectories();
			foreach(DirectoryInfo R in Directories){
				ImportEx(Conn,R.FullName);
			}
		}
		public void ImportData(SQLiteConnection Conn,string RootDir){
			DirectoryInfo DI = new DirectoryInfo(RootDir);
				
			FileInfo[] D = DI.GetFiles("*.mp3");
			foreach(FileInfo X in D){
				
					id3.MP3 Dw = new id3.MP3(X.DirectoryName,X.Name);
					id3.FileCommands.readMP3Tag(ref Dw);
                    SQLiteConnection CP = MediaChrome.MainForm.MakeConnection();
				
					
					String P = "INSERT INTO song(name,artist,album,path,no,composer,store,engine) VALUES (\""+Dw.id3Title.Replace("\0","").Replace("\"","'")+"\" ,\""+Dw.id3Artist.Replace("\0","").Replace("\"","'")+"\",\""+Dw.id3Album.Replace("\0","").Replace("\"","'")+"\",\"mp3:"+X.FullName+"\","+Dw.id3TrackNumber+",\"Other\",\"Local Files\",\"mp3\")";
					SQLiteCommand C = new SQLiteCommand(P,CP);
					C.ExecuteNonQuery();
					FilesCompleted++;
					CP.Close();
			
				
			}
			DirectoryInfo[] Directories = DI.GetDirectories();
			foreach(DirectoryInfo R in Directories){
				ImportData(Conn,R.FullName);
			}
		}
		public void Import(object Conn,string RootDir){
			Ready=false;
			ImportData((SQLiteConnection)Conn,RootDir);
			Ready=true;
			
		}
		
		public List<Song> Search()
		{
			return new List<Song>();
		}
		public MP3Player()
		{
			Ready=true;
			player = new WMPLib.WindowsMediaPlayerClass();
            player.PositionChange+=new _WMPOCXEvents_PositionChangeEventHandler(player_PositionChange);
            _Timer = new Timer();
            _Timer.Tick += new EventHandler(_Timer_Tick);
			
		}

        void _Timer_Tick(object sender, EventArgs e)
        {
                
        }

       
			

		

		void player_OpenStateChange(int NewState)
		{
			
		}

		void player_PositionChange(double oldPosition, double newPosition)
		{
            position = (int)newPosition;
            if(newPosition >= player.currentMedia.duration-3)
			{
				player.stop();
                /**
                 * Raise general playback finished event
                 * so mediachrome can request next song
                 * */
                if (PlaybackFinished != null)
                    PlaybackFinished(this, new EventArgs());
               
			}

		}
		public void Play()
		{

			player.play();
            Status = "Playing... '" + player.currentItem.getItemInfo("title")+"'";
		}
		public void Pause()
		{
			player.pause();
		}
		public void Stop()
		{
			player.stop();
		}
		public void Seek()
		{
			
		}
		public void Load(String URL)
		{
			player.URL = ("file:///"+URL.ToString());
            Status = "Connecting to Media";
		}
		public double Duration 
		{
			get { return player.currentItem.duration; }
		}
		public String Length
		{
			get { return player.currentItem.durationString; }
		}

        private Dictionary<string ,List<Song>> playlists;
        /// <summary>
        /// Cache of loaded playlists
        /// </summary>
        public Dictionary<string, List<Song>> Lists
        {
            get
            {
                if (playlists == null)
                    playlists = new Dictionary<string, List<Song>>();
                return playlists;
            }
            set
            {
                if(playlists == null)
                    playlists = new Dictionary<string, List<Song>>();
            }
        }
		
		public void Seek(double pos)
		{
            player.controls.currentPosition = pos;
		}
		
		public void Unload()
		{
		
		}
		
		public bool hasPlaylists {
			get {
				return true;
			}
		}
       
        public List<Song> LoadPlaylist(String PlsID,ref  Playlist playlist)
        {

            List<Song> _Songs = new List<Song>();
            playlist.CanModify = true;
            SQLiteConnection Conn = MediaChrome.MainForm.MakeConnection();
            try
            {
                using (StreamReader SR = new StreamReader(MediaChrome.MainForm.DownloadDir + "\\" + PlsID.Replace(".pls","") + ".pls"))
                {
                    String D = "";
                    while ((D = SR.ReadLine()) != null)
                    {
                        if (D.StartsWith("music:"))
                        {
                            /* Uri Url = new System.Uri(D);
                             Song P = new Song();
                             P.Artist = Url.Segments[1].Replace("/", "").Replace("%20", " ");//Url.Host.Replace("_"," ");

                             P.Album = Url.Segments[3].Replace("/", "").Replace("%20", " ");
                             P.Title = Url.Segments[2].Replace("/", "").Replace("%20", " ");

                             P.Path = D;
                        

                             _Songs.Add(P);*/
                            Song P = Song.GetSongFromURI(D);

                            _Songs.Add(P);

                            // TODO: TO be implemented later

                        }
                        else
                        {
                            SQLiteCommand Command = new SQLiteCommand("SELECT * FROM " + " WHERE path='" + D + "'", Conn);
                            SQLiteDataReader SQLDR = Command.ExecuteReader();
                            try
                            {
                                SQLDR.Read();
                                Song _Song = Song.GetSongFromURI(D);
                                 
                                _Songs.Add(_Song);
                            }
                            catch
                            {

                            }
                        }

                    }
                }
            }
            catch
            {
            }
            // Add the list to the playlist cache
            playlist.Songs = _Songs;
            Conn.Close();
            return _Songs;
        }
		public Playlist ViewPlaylist(string Name,string PlsID)
		{
            try
            {
                /**
                 * Create an new playlist, load it if 
                 * no previous buffer exist
                 *
                if (Lists.ContainsKey(PlsID))
                    return new Playlist(this,Name, PlsID, this.Host);
                Playlist d = new Playlist(this,Name,PlsID,this.Host);
              
                 */
                Playlist d = new Playlist(this,Name,PlsID,Host);
                LoadPlaylist(Name, ref d);
                
                return d;
            }
            catch
            {
                return null;
            }
		}
        public void AddToPlaylist(Playlist pls, Song _Song, int pos)
        {
            try
            {
                String Path = MediaChrome.MainForm.GetURIFromSong(_Song);
                string playlistFile = MediaChrome.MainForm.DownloadDir + "\\" + pls.Title + ".pls";
                if (File.Exists(playlistFile))
                {
                    List<String> Strings = new List<string>();
                    using (StreamReader SR = new StreamReader(playlistFile))
                    {
                        String Line = "";
                        while ((Line = SR.ReadLine()) != null)
                        {
                            Strings.Add(Line.Replace(" ", "%20"));
                        }
                        SR.Close();
                    }
                    Strings.Insert(pos, Path.Replace(" ", "%20"));
                    using (StreamWriter SW = new StreamWriter(playlistFile))
                    {
                        foreach (String d in Strings)
                        {
                            SW.WriteLine(d);
                        }
                        SW.Close();

                    }
                }
            }
            catch
            {
            }

        }
		public void AddToPlaylist(string playlistID, Song _Song, int pos)
		{
            try
            {
                String Path = MediaChrome.MainForm.GetURIFromSong(_Song);
                if (File.Exists(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                {
                    List<String> Strings = new List<string>();
                    using (StreamReader SR = new StreamReader(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                    {
                        String Line = "";
                        while ((Line = SR.ReadLine()) != null)
                        {
                            Strings.Add(Line.Replace(" ","%20"));
                        }
                        SR.Close();
                    }
                    Strings.Insert(pos, Path.Replace(" ","%20"));
                    using (StreamWriter SW = new StreamWriter(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                    {
                        foreach (String d in Strings)
                        {
                            SW.WriteLine(d);
                        }
                        SW.Close();

                    }
                }
            }
            catch
            {
            }
           
		}

        public void RemoveFromPlaylist(string playlistID, int pos)
        {
            try
            {
                if (File.Exists(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                {
                    List<String> Strings = new List<string>();
                    using (StreamReader SR = new StreamReader(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                    {
                        String Line = "";
                        int fpos = 0;
                        while ((Line = SR.ReadLine()) != null)
                        {
                            if (pos != fpos)
                                Strings.Add(Line);
                            fpos++;
                        }
                        
                        SR.Close();
                    }
                    using (StreamWriter SW = new StreamWriter(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                    {
                        foreach (String d in Strings)
                        {
                            SW.WriteLine(d);
                        }

                    }
                }
            }
            catch
            {
            }
        }
		
		public void MoveSongPlaylist(string playlistID,Song _Song, int startLoc, int endLoc)
		{
            try
            {
			    if (File.Exists(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                {
                    List<String> Strings = new List<string>();
                    String Track = "";
                    using (StreamReader SR = new StreamReader(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                    {
                        String Line = "";
                        int pos = 0;
                        while ((Line = SR.ReadLine()) != null)
                        {
                            if (pos != startLoc)
                            {
                                
                                Strings.Add(Line);
                                continue;
                            }
                            else
                            {
                                Track = Line;
                            }
                            if( pos == endLoc)
                            {
                                Strings.Insert(pos,Track);
                                continue;
                            }
                            pos++;
                        }
                        SR.Close();
                    }
                    using (StreamWriter SW = new StreamWriter(MediaChrome.MainForm.DownloadDir + "\\" + playlistID))
                    {
                        foreach (String d in Strings)
                        {
                            SW.WriteLine(d);
                        }

                    }
                }
            }
            catch
            {
            }
		}
		
		public List<MediaChrome.Views.Playlist> Playlists 
        {
			get
            {
                List<Views.Playlist> Playlists = new List<MediaChrome.Views.Playlist>();
                    
                try
                {

                   DirectoryInfo D = new DirectoryInfo(MediaChrome.MainForm.DownloadDir);
                    FileInfo[] playlists = D.GetFiles("*.pls");
                    foreach (FileInfo __Playlist in playlists)
                    {
                        try
                        {
                            /**
                             * Create an playlist
                             * */
                            Views.Playlist _Playlist = this.CreatePlaylist(__Playlist.Name.Replace(".pls", ""));
                            _Playlist.Engine = this;
                            //_Playlist.ID = "mp3:playlist:" + __Playlist.Name.Replace(".pls", "");
                            Playlists.Add(_Playlist);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
				return Playlists;
            
			}
			
		}
	}

}
