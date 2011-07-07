﻿/*
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
using MediaChromeGUI;

using WMPLib;
using MediaChrome.Views;
using MediaChrome;
namespace MediaChromeGUI
{
	/// <summary>
	/// Description of Mp3.
	/// </summary>
	public class MP3Player : IPlayEngine
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
        public System.Drawing.Icon SystemIcon
        {
            get
            {
                return Properties.Resources.app_icon;
            }
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
            // TODO: Read IDV3 Tags
            id3.MP3 Dw = new id3.MP3(URI.Replace("mp3:", ""), "");
            id3.FileCommands.readMP3Tag(ref Dw);


            // return a new song
            Song d = new Song();
            d.Title = Dw.id3Title.Replace("\0","");
            d.ArtistName = Dw.id3Artist.Replace("\0", "");
            d.AlbumName = Dw.id3Album.Replace("\0","");
            d.Path = String.Format("http://music/{0}/{1}/{2}", d.ArtistName, d.AlbumName, d.Title);
            return d;

           
        }
        public Song CurrentSong { get; set; }
        public String AudioSignature
        {
            get
            {
                return "mp3:";
            }
        }
        public void enumerateFiles(DirectoryInfo DI, ref float count)
        {
            foreach (FileInfo ct in DI.GetFiles("*.mp3"))
                count++;
            foreach (DirectoryInfo dir in DI.GetDirectories())
                enumerateFiles(dir, ref count);
        }
        private void ImportEx(DirectoryInfo DI, ref float progress,ref List<Song> songs,ref float fileCount)
        {
            FileInfo[] D = DI.GetFiles("*.mp3");
            int i = 0;
            foreach (FileInfo X in D)
            {

                id3.MP3 Dw = new id3.MP3(X.DirectoryName, X.Name);
                id3.FileCommands.readMP3Tag(ref Dw);
                SQLiteConnection CP = MediaChromeGUI.MainForm.MakeConnection();
                Song song = new Song();
                song.Name = Dw.id3Title.Replace("\"","'").Replace("\0","");
                song.ArtistName = Dw.id3Artist.Replace("\"", "'").Replace("\0", "");
                song.AlbumName = Dw.id3Album.Replace("\"", "'").Replace("\0", "");
                song.Path = "mp3:" + X.FullName.Replace("\"", "'").Replace("\0", "");
                song.Engine = this;

                songs.Add(song);

                // set progress to the files
                progress = i / fileCount;
              
                
            }
            foreach (DirectoryInfo dir in DI.GetDirectories())
            {
                ImportEx(dir, ref progress, ref songs,ref fileCount);
            }
          
        }
        public List<Song> Import(String query, ref float progress)
        {
            List<Song> songs = new List<Song>();
            DirectoryInfo DI = new DirectoryInfo(query);
            float fileCount = 0;
            enumerateFiles(DI, ref fileCount);
            ImportEx(DI, ref progress,ref songs,ref fileCount);
            return songs;
           
        }
        #region DefaultValues
        public String Address { get; set; }
        public String Company { get; set; }
        public String Copyright { get; set; }
        public bool LoggedIn { get; set; }
        public LoginResult Login() {

            return LoginResult.Pass;
        
        }
        public void Logout() { }
        public bool Purchase(Song song) { return false; }
        public Uri ServiceUri { get; set; }
        public void ShowOptions()      
        {   
        
        }
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
                return Properties.Resources.icon1;
            }
            set
            {
            }
        }
        public MediaChrome.Album[] FindAlbum(string ID)
        {
           

             SQLiteConnection D = MainForm.MakeConnection();
             SQLiteDataReader r = new SQLiteCommand("SELECT album FROM song WHERE artist LIKE '%" + ID + "%'", D).ExecuteReader();
             List<Album> albums = new List<Album>();
             while (r.Read())
             {
                 Album c = GetAlbum((string)r["album"]);
                 albums.Add(c);
             }
             return albums.ToArray();
        }
        public MediaChrome.Artist[] FindArtist(string ID)
        {
            SQLiteConnection D = MainForm.MakeConnection();
            SQLiteDataReader r = new SQLiteCommand("SELECT artist FROM song WHERE artist LIKE '%" + ID + "%'", D).ExecuteReader();
            List<Artist> artists = new List<Artist>();
            while (r.Read())
            {
                Artist c = GetArtist((string)r["artist"]);
                artists.Add(c);
            }
            return artists.ToArray();
        }
      

        public MediaChrome.Album GetAlbum(Artist artist, string ID)
        {
            Album R = new Album();
            SQLiteConnection D = MediaChromeGUI.MainForm.MakeConnection();
            SQLiteDataReader r = new SQLiteCommand("SELECT name,artist,album,path FROM song WHERE album = '" + ID + "' AND artist='"+artist.Name+"' ", D).ExecuteReader();
            
            // Create temporary list of songs

            List<Song> songs = new List<Song>();
            
            // Add all songs from the result
            while (r.Read())
            {
                Song d = GetSongFromReader(r);
                d.Album = R;
                d.Artists = new Artist[] { artist };
                songs.Add(d);
            }
            R.Songs = songs;
            return R;
        }
        /// <summary>
        /// Converts an table row to an song
        /// </summary>
        /// <param name="Reader"></param>
        /// <returns></returns>
        public Song GetSongFromReader(SQLiteDataReader reader)
        {
            Song d = new Song();
            
            d.Name = (string)reader["name"];
            d.Path = (string)reader["path"];
            d.Link = (string)reader["path"];
            d.ArtistName = (string)reader["artist"];
            d.AlbumName = (string)reader["album"];
            d.Engine = this;
            return d;
        }
        public MediaChrome.Album GetAlbum(string ID)
        {
            
           String name = ID.Replace("mp3:album:", ""); // name of the album
            Album R = new Album();
            
            SQLiteConnection D = MediaChromeGUI.MainForm.MakeConnection();
            SQLiteDataReader r = new SQLiteCommand("SELECT name,artist,album,path FROM song WHERE album = '" + name + "' AND name <> ''", D).ExecuteReader();
            R.Link = "mp3:album:" + ID;
            R.Name = name;
            // Create temporary list of songs

            List<Song> songs = new List<Song>();

            // Add all songs from the result
            while (r.Read())
            {
                Song d = new Song();
                d.Album = R;
                d.ArtistName = (string)r["artist"];
                d.Title = (string)r["name"];
                d.Name = (string)r["name"];
                d.Path = (string)r["path"];
                d.Link = (string)r["path"];
                // d.CoverArt = (string)r["cover_art"];
                d.Engine = this;
                songs.Add(d);

            }
            R.Songs = songs;
            return R;
        }

        /// <summary>
        /// Gets an artist for the work
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public MediaChrome.Artist GetArtist(string ID)
        {
            SQLiteConnection  D = MediaChromeGUI.MainForm.MakeConnection();

            Artist artist = new Artist();
            artist.Name = ID;
            if (ID == null)
                artist.Name = "";
            artist.Link = "mp3:artist:"+ID;
            /***
             * Associate albums and songs to the artist
             * */
            SQLiteDataReader SQLDR = new SQLiteCommand("SELECT DISTINCT artist FROM song WHERE artist='" + ID + "'", D).ExecuteReader();

            List<Album> albums = new List<Album>(); // Create an temporary list of albums
            while (SQLDR.Read())
            {
                Album r = GetAlbum(artist,(string)SQLDR["artist"]);
                albums.Add(r);
                
            }
            Artist d = new Artist();
            d.Name = ID;
            d.Albums = albums;
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
                    return (int)(player.controls.currentPosition);
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
            String Path = MediaChromeGUI.MainForm.DownloadDir + "\\" + Name + ".pls";
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
            SQLiteConnection Conn = MediaChromeGUI.MainForm.MakeConnection();

            SQLiteCommand Command = new SQLiteCommand("SELECT * FROM song WHERE name LIKE '%" + D.Title.Replace("'", "") + "%' AND artist LIKE '%" + D.ArtistName.Replace("'", "") + "%' AND  ( engine = 'mp3' OR store ='mp3')", Conn);
			SQLiteDataReader Reader = Command.ExecuteReader();
			if(Reader.HasRows)
			{

                Song result = MediaChromeGUI.MainForm.GetSongFromQuery(Reader);
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
            SQLiteConnection Conn = MediaChromeGUI.MainForm.MakeConnection();

			SQLiteCommand Command = new SQLiteCommand("SELECT name,artist,album,path,store,engine FROM song WHERE name LIKE '%"+Query+"%' OR artist LIKE '%"+Query+"%' OR album LIKE '%"+Query+"%' AND engine='mp3'",Conn);
			SQLiteDataReader  DR = Command.ExecuteReader();
			while(DR.Read())
			{
				
				Song D = new Song();
				try{
				D.Title = DR.GetString(0);
                D.Artists = new Artist[] { GetArtist((string)DR["artist"]) };
                D.Album = GetAlbum((string)DR["album"]);
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
			
		}
		public void ImportData(SQLiteConnection Conn,string RootDir){
			
		}
		public void Import(object Conn,string RootDir){
			
			
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
            this.Paused = false;
			player.play();
            Status = "Playing... '" + player.currentItem.getItemInfo("title")+"'";
            // Add play to the database
            SQLiteConnection Conn = MainForm.MakeConnection();
            SQLiteCommand Command = new SQLiteCommand("INSERT INTO playback(song_ID) VALUES ( (SELECT id FROM song WHERE path='" + this.CurrentSong.Path + "') )", Conn);
            Command.ExecuteNonQuery();
		}
		public void Pause()
		{
			player.pause();
            this.Paused = true;
		}
		public void Stop()
		{
			player.stop();
            this.Paused = false;
		}
		public void Seek()
		{
			
		}
        
		public void Load(String URL)
		{
			player.URL = ("file:///"+URL.Replace("mp3:","").ToString());

            CurrentSong = new Song();
            CurrentSong.ArtistName = player.currentMedia.getItemInfo("artist");
            CurrentSong.AlbumName = player.currentMedia.getItemInfo("album");
            CurrentSong.Title = player.currentMedia.getItemInfo("title");
            CurrentSong.Name = player.currentMedia.getItemInfo("title");
            CurrentSong.Path = URL;
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
            SQLiteConnection Conn = MediaChromeGUI.MainForm.MakeConnection();
            try
            {
                using (StreamReader SR = new StreamReader(MediaChromeGUI.MainForm.DownloadDir + "\\" + PlsID.Replace(".pls","") + ".pls"))
                {
                    String D = "";
                    while ((D = SR.ReadLine()) != null)
                    {
                        if (D.StartsWith("music:")||D.StartsWith("http://music/"))
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
             //   LoadPlaylist(Name, ref d);
                
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
                String Path = MediaChromeGUI.MainForm.GetURIFromSong(_Song);
                string playlistFile = MediaChromeGUI.MainForm.DownloadDir + "\\" + pls.Title + ".pls";
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
                String Path = _Song.Path;
                string name =playlistID.Replace("mp3:playlist:","")+".pls";
                if (File.Exists(MediaChromeGUI.MainForm.DownloadDir + "\\" +name ))
                {
                    List<String> Strings = new List<string>();
                    using (StreamReader SR = new StreamReader(MediaChromeGUI.MainForm.DownloadDir + "\\" + name))
                    {
                        String Line = "";
                        while ((Line = SR.ReadLine()) != null)
                        {
                            Strings.Add(Line.Replace(" ","%20"));
                        }
                        SR.Close();
                    }
                    Strings.Insert(pos, "http://music/"+_Song.ArtistName+"/"+(_Song.AlbumName != "" ? "%20" : "")+"/"+_Song.Name);
                    using (StreamWriter SW = new StreamWriter(MediaChromeGUI.MainForm.DownloadDir + "\\" + name))
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
                if (File.Exists(MediaChromeGUI.MainForm.DownloadDir + "\\" + playlistID))
                {
                    List<String> Strings = new List<string>();
                    using (StreamReader SR = new StreamReader(MediaChromeGUI.MainForm.DownloadDir + "\\" + playlistID))
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
                    using (StreamWriter SW = new StreamWriter(MediaChromeGUI.MainForm.DownloadDir + "\\" + playlistID))
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
			    if (File.Exists(MediaChromeGUI.MainForm.DownloadDir + "\\" + playlistID))
                {
                    List<String> Strings = new List<string>();
                    String Track = "";
                    using (StreamReader SR = new StreamReader(MediaChromeGUI.MainForm.DownloadDir + "\\" + playlistID))
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
                    using (StreamWriter SW = new StreamWriter(MediaChromeGUI.MainForm.DownloadDir + "\\" + playlistID))
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
                List<MediaChrome.Views.Playlist> Playlists = new List<MediaChrome.Views.Playlist>();
                    
                try
                {

                   DirectoryInfo D = new DirectoryInfo(MediaChromeGUI.MainForm.DownloadDir);
                    FileInfo[] playlists = D.GetFiles("*.pls");
                    foreach (FileInfo __Playlist in playlists)
                    {
                        try
                        {
                            /**
                             * Create an playlist
                             * */
                            MediaChrome.Views.Playlist _Playlist = this.CreatePlaylist(__Playlist.Name.Replace(".pls", ""));
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
        /// <summary>
        /// The MP3 plugin has some user defined methods:
        /// </summary>
        /// <param name="command"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public object InvokeCommand(string command, params object[] arguments)
        {
            switch (command)
            {
                case "getToplist":
                    {
                        List<Song> Result = new List<Song>();
                        // Open the connection
                        SQLiteConnection Conn = MainForm.MakeConnection();
                       
                        SQLiteCommand Command = new SQLiteCommand("SELECT * FROM song, playback WHERE song.id=playback.song_id ORDER BY (SELECT count(*) FROM song,playback WHERE song.id=playback.song_id) DESC LIMIT 10", Conn);
                        
                        // Convert the data into list of song
                        SQLiteDataReader DR = Command.ExecuteReader();
                        while (DR.Read())
                        {
                            Song d = GetSongFromReader(DR);
                            Result.Add(d);
                        }
                        Conn.Close();
                        return Result;
                    }
                    break;
            }
            return false;

        }
    }

}
