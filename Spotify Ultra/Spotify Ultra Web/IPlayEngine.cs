﻿/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 2010-10-29
 * Time: 15:02
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
using MediaChrome.Views;


namespace MediaChrome
{
    /// <summary>
    /// A IMedia represents the instances of songs,artists and albums
    /// </summary>
    public interface IMedia
    {
        /// <summary>
        /// The link to the artist
        /// </summary>
        string Link { get; set; }

        /// <summary>
        /// The name of the artist
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The engine the album is using
        /// </summary>
        IPlayEngine Engine { get; set; }
    }
	/*public interface IImportEngine 
	{
		bool Ready {get;set;}
		int FilesCompleted {get;set;}
		int TotalFiles {get;set;}
		void Import(SQLiteConnection Conn, String rootDir );
		void ImportEx(SQLiteConnection Conn,String rootDir );
	}*/

    /// <summary>
    /// An artist
    /// </summary>
    public class Artist : IMedia
    {
        /// <summary>
        /// Available albums for the artist
        /// </summary>
        public Album[] Albums { get; set; }

        /// <summary>
        /// The link to the artist
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// The name of the artist
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The engine the album is using
        /// </summary>
        public IPlayEngine Engine { get; set; }
    }

    /// <summary>
    /// Class for album
    /// </summary>
    public class Album : IMedia
    {
        /// <summary>
        /// Songs of the album
        /// </summary>
        public Song[] Songs { get; set; }

        /// <summary>
        /// Album artist
        /// </summary>
        public Artist Artist { get; set; }
        /// <summary>
        /// Name of the album
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The link of the album
        /// </summary>
        public String Link { get; set; }

        /// <summary>
        /// The engine the album is using
        /// </summary>
        public IPlayEngine Engine { get; set; }
    }

	/// <summary>
	/// Description of IPlayEngine.
	/// </summary>
	/// 
	
	
	public class Song :IMedia
    {
        
        public String Name
        {
            get
            {
                return Title;
            }
            set
            {
                Title = value;
            }
        }
        public String Link
        {
            get
            {
                return Path;
            }
            set
            {
                Path = value;
            }
        }
        /// <summary>
        /// Decides whether the song is checked.
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Address to an bitmap file with coverart, either locally or remote
        /// </summary>
        public string CoverArt { get; set; }
        
        

        /// <summary>
        /// The title of the song
        /// </summary>
		public string Title{get;set;}

        /// <summary>
        /// The ID of the song (URI)
        /// </summary>
		public string ID {get;set;}

        /// <summary>
        /// An array of artists, first is primary
        /// </summary>
		public Artist[] Artists{get;set;}
        public string Artist
        {
            get
            {
                if (Artists != null)
                    return Artists[0].Name;
                return "";
            }
            set
            {
                if (Artists == null)
                    Artists = new Artist[1];
                Artists[0] = new Artist();
                Artists[0].Name = value;
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
		public Album Album {get;set;}

        /// <summary>
        /// Gets the album name or sets it, making a default album instance
        /// </summary>
        public string AlbumName
        {
            get
            {
                if (Album != null)
                    return Album.Name;
                return "";
            }
            set
            {
                if (Album == null)
                    Album = new Album();
                Album.Name = value;
                
            }
        }

        /// <summary>
        /// The MediaChrome' uri of the song
        /// </summary>
		public string Path {get;set;}
        
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
		public string Store {get;set;}

        /// <summary>
        /// The version of the song
        /// </summary>
		public string Version {get;set;}

        /// <summary>
        /// Popularity of the song at the store
        /// </summary>
        public float Popularity { get; set; }

        /// <summary>
        /// Gets or sets the contributing musician of the song
        /// </summary>
		public string Contributing {get;set;}

        /// <summary>
        /// Gets or sets the featured artist on the song
        /// </summary>
		public string Feature {get;set;}

        /// <summary>
        /// Gets and sets the desired Media Engine for this particular instance
        /// </summary>
		public string ProposedEngine {get;set;}

        /// <summary>
        /// Gets and sets the composer of the song
        /// </summary>
		public string Composer {get;set;}

       
      
        public bool Subsong=false;
        public bool opned = false;
        public Song ParentSong = null;

        /// <summary>
        /// Sub-songs. Not used
        /// </summary>
        private List<Song> subSongs;
        public List<Song> SubSongs
        {
            get
            {
                if (subSongs == null)
                {
                  List<Song> Songs = new List<Song>();
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
                    Songs = new List<Song>();
                    return   Songs;
                }
                else
                {
                    return subSongs;
                }
            }
        }

	}

	/// <summary>
	/// A media service plug in for media chrome. Each music service who want to be integrated
    /// with the mediachrome framework must implement this class.
	/// </summary>
	public interface IPlayEngine
	{
        

        /// <summary>
        /// Shows options dialogue.
        /// </summary>
        void ShowOptions();

        /// <summary>
        /// The copyright of the service
        /// </summary>
        String Copyright { get; set; }

        /// <summary>
        /// Physical adress to the company
        /// </summary>
        String Address { get; set; }
        /// <summary>
        /// The company providing the service
        /// </summary>
        String Company { get; set; }

        /// <summary>
        /// The url to the company's website
        /// </summary>
        Uri CompanyWebSite { get; set; }

        /// <summary>
        /// The web resource of the service
        /// </summary>
        Uri ServiceUri { get; set; }

        /// <summary>
        /// Handles purchases of an song
        /// </summary>
        /// <param name="song">A song</param>
        /// <remarks>Not implemented</remarks>
        /// <returns>Whether the purchase was accepted or rejected by the merchant</returns>
        bool Purchase(Song song);

        /// <summary>
        /// Gets and sets whether the user is logged in to the service
        /// </summary>
        bool LoggedIn { get; set; }

        /// <summary>
        /// Show the log in creditals. Log in is handled by each service
        /// </summary>
        void Login();


        /// <summary>
        /// Defines whether the music is streamed from an internet source
        /// </summary>
        /// <remarks>Not implemented</remarks>
        bool Streaming { get; }

        /// <summary>
        /// Defines if purchases is available at the service
        /// </summary>
        /// <remarks>Not implemented</remarks>
        /// 
        bool DownloadStore { get; }

        /// <summary>
        /// Returns a list of songs ready for purchase
        /// </summary>
        List<Song> Purchases { get; }

        /// <summary>
        /// Method for logging out the user from the service
        /// </summary>
        void Logout();
        /// <summary>
        /// Image representing the service
        /// </summary>
        string Image
        {
            get;
       
        }
        /// <summary>
        /// The icon for the Engine
        /// </summary>
        System.Drawing.Image Icon { get; }
        /// <summary>
        /// Get an artist by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Artist GetArtist(string ID);

        /// <summary>
        /// Find an artist
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        Artist[] FindArtist(string Query);

        /// <summary>
        /// Get an album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        Album GetAlbum(string album);

        /// <summary>
        /// Find an album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        Album[] FindAlbum(string album);
        event EventHandler PlaybackFinished;

        /// <summary>
        /// Gets whether the engine can handle playlists
        /// </summary>
		bool hasPlaylists {get;}
        /// <summary>
        /// The text status of the engine
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// Visible control for the specific engine
        /// </summary>
		Control MediaControl {get;}
        /// <summary>
        /// Denotes if the playback is ready
        /// </summary>
		bool Ready {get;set;}
        /// <summary>
        /// Duration of the current song
        /// </summary>
        double Duration { get; }
        /// <summary>
        /// The amount of files complete in an import
        /// </summary>
		int FilesCompleted {get;set;}
        /// <summary>
        /// Gets and sets if the playlists has been loaded
        /// </summary>
        bool PlaylistsLoaded { get; set; }

        /// <summary>
        /// Namespace of the IPlayEngine. Used for as an arbitrary
        /// identifier for the engine. Must not contain any whitespaces
        /// and not be modified at any point at the application cycle.
        /// </summary>
		string Namespace {get;}

        /// <summary>
        /// Title of the engine. Must not be modified upon runtime
        /// </summary>
		string Title {get;}

        /// <summary>
        /// Total local files
        /// </summary>
		int TotalFiles {get;set;}

        /// <summary>
        /// Query for songs on the service cloud
        /// </summary>
        /// <param name="Query">The textual query</param>
        /// <returns></returns>
		List<Song> Find(String Query);

        /// <summary>
        /// Import songs into the local library according to the specifications.
        /// </summary>
        /// <param name="songs"></param>
        void SongImport(Song[] songs);

        /// <summary>
        /// Starts playing the current song
        /// </summary>
		void Play();

        /// <summary>
        /// The position of the song
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Occurs when pausing
        /// </summary>
		void Pause();

        /// <summary>
        /// Occurs when stopping
        /// </summary>
		void Stop();

        /// <summary>
        /// Occurs when seeking
        /// </summary>
        /// <param name="pos"></param>
		void Seek(double pos);

        /// <summary>
        /// Occurs when loading an song
        /// </summary>
        /// <param name="Content"></param>
		void Load(String Content);
        /// <summary>
        /// Import music to the local database
        /// </summary>
        /// <param name="Conn"></param>
        /// <param name="RootDir"></param>
		void Import(SQLiteConnection Conn,string RootDir);
        
        /// <summary>
        /// Host form. Used by the runtime
        /// </summary>
        SpofityRuntime.Form1 Host{get;set;}

        /// <summary>
        /// Unload the engine
        /// </summary>
		void Unload();

        /// <summary>
        /// Raw search songs from the service
        /// </summary>
        /// <returns>An list of songs</returns>
		List<Song> Search();

        /// <summary>
        /// Unknown
        /// </summary>
        /// <param name="_Song"></param>
        /// <returns></returns>
        Song RawFind(Song _Song);
		//string RawFind(Song _Song);
		List<MediaChrome.Views.Playlist> Playlists {get;}
		/// <summary>
		/// Playlist-related functionality
		/// </summary>
		Playlist ViewPlaylist(string Name,String PlsID);
        Playlist CreatePlaylist(String Name);
		void AddToPlaylist(string playlistID,MediaChrome.Song _Song,int pos);
		void RemoveFromPlaylist(string playlistID,int pos);
		void MoveSongPlaylist(string playlistID,int startLoc,int endLoc);
		
	//	bool canSync {get;}
	//	void LoadSynchronized();
	//	bool OfflineService {get;}
		string Length { get; }


        List<Song> LoadPlaylist(string p,ref  Playlist playlist);
    }
	
}
