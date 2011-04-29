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

    
	/*public interface IImportEngine 
	{
		bool Ready {get;set;}
		int FilesCompleted {get;set;}
		int TotalFiles {get;set;}
		void Import(SQLiteConnection Conn, String rootDir );
		void ImportEx(SQLiteConnection Conn,String rootDir );
	}*/

    public class Artist
    {
        public string Url { get; set; }
        public string Name { get; set; }
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
		public string Artist{get;set;}
        
        public string ArtistUrl { get; set; }
        public string AlbumUrl { get; set; }
		public string Album {get;set;}
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
        event EventHandler PlaybackFinished;
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
		void Play();
        int Position { get; }
		void Pause();
		void Stop();
		void Seek(double pos);
		void Load(String Content);
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
