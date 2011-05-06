/*
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
	
	
	public class Song{
        public bool Checked { get; set; }
        public string CoverArt { get; set; }
		public string Title{get;set;}
		public string ID {get;set;}
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
        public string ArtistUrl { get; set; }
        public string AlbumUrl { get; set; }
		public Album Album {get;set;}
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
		public string Path {get;set;}
		public string Engine {get;set;}
		public string Store {get;set;}
		public string Version {get;set;}
        public float Popularity { get; set; }
		public string Contributing {get;set;}
		public string Feature {get;set;}
		public string ProposedEngine {get;set;}
		public string Composer {get;set;}
        public bool Subsong=false;
        public bool opned = false;
        public Song ParentSong = null;
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
	
	public interface IPlayEngine
	{
        string Image
        {
            get;
       
        }

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
		string Namespace {get;}
		string Title {get;}
		int TotalFiles {get;set;}
		List<Song> Find(String Query);
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

		SpofityRuntime.Form1 Host{get;set;}
		void Unload();
		List<Song> Search();
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
