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
using System.Windows.Forms;

using System.IO;

using MediaChrome.Views;


namespace MediaChrome
{
    /// <summary>
    /// Login states
    /// </summary>
    public enum LoginResult
    {
        Pass, Fail, Cancelled
    }
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
    /// An track is an instance of playable song
    /// </summary>
    /// 

	public class Song :IMedia
    {
        public enum SongType
        {
            Local, Streaming, Cached
        }

        /// <summary>
        /// The type of Song.
        /// </summary>
        public SongType Type { get; set; }

        
        public static class UriHelper
        {
            public static Dictionary<String, String> Querystrings(Uri d)
            {
                Dictionary<String, String> QueryList = new Dictionary<string, string>();

                String[] Queries = d.Query.Split('&');
                if (d.Query == "")
                    return new Dictionary<string, string>();
                foreach (String query in Queries)
                {
                    try
                    {
                        string[] pair = query.Split('=');
                        QueryList.Add(pair[0].Replace("?", ""), pair[1]);
                    }
                    catch
                    {
                    }
                }
                return QueryList;
            }
        }
        public static String findVersion(String text)
        {
            bool inVersion = false;
            String ver = "";
            foreach (Char d in text)
            {
                if (inVersion)
                    ver += d;
                if (d == ')')
                {
                    inVersion = false;
                    continue;
                }
                if (d == '(')
                {
                    inVersion = true;
                    continue;
                }
            }
            return ver;
        }
        public static String findCommit(String text)
        {
            bool inVersion = false;
            String ver = "";
            foreach (Char d in text)
            {

                if (d == ']')
                {
                    inVersion = false;
                    continue;
                }
                if (d == '[')
                {
                    inVersion = true;
                    continue;
                }
                if (inVersion)
                    ver += d;
            }
            return ver;
        }

       
        /// <summary>
        /// Gets an song from the specific uri. Also checks about 
        /// matching song from the specific list of engines.
        /// </summary>
        /// <param name="D">The URI to match to</param>
        /// <param name="Engines">List of IPlayEngine to search in</param>
        /// <remarks>This function may lock the current thread, as it may search on remote sources. This should
        /// be run in an separate thread.</remarks>
        /// <returns>An song instance</returns>
        public static Song GetSongFromURI(String D, List<IPlayEngine> Engines)
        {
            /***
           * Check if the URI can be managed by an extension!
           * */
            foreach (IPlayEngine engine in Engines)
            {
                if (D.StartsWith(engine.AudioSignature))
                {
                   
                    Song d = engine.ConvertSongFromLink(D);
                    return d;
                }
            }
            
            // Otherwise do as usual
            return GetSongFromURI(D);

        }
       
        /// <summary>
        /// Convert an mediachrome URI to an Song instance
        /// </summary>
        /// <param name="D">The input URI</param>
        /// <returns>A song instance</returns>
        public static Song GetSongFromURI(String D)
        {
          
       
            Song P = new Song();

            D = D.Replace("music://", "http://music.resource/");

            P.Version = findVersion(D);
            P.Contributing = findCommit(D);
             Uri Url = new System.Uri(D.Replace("(" + P.Version + ")", "").Replace("[" + P.Contributing + "]", "").Replace("{", "").Replace("}", ""));

             P.ArtistName = Url.Segments[1].Replace("%20", " ").Replace("/","");
             P.Title = Url.Segments[3].Replace("%20", " ").Replace("/", "");



             P.AlbumName = Url.Segments[2].Replace("%20", " ").Replace("/", "");
             P.Link = D;
            try
            {
                P.ProposedEngine = UriHelper.Querystrings(Url)["service"];
            }
            catch
            {

            }
          
            try
            {
                P.ID = UriHelper.Querystrings(Url)["id"];
            }
            catch
            {

            }
            return P;
        }
       
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
        public string ArtistName
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
        /// The System .ICO of the engine
        /// </summary>
        System.Drawing.Icon SystemIcon { get;  }

        /// <summary>
        /// Get an custom property of the engine
        /// </summary>
        /// <param name="prop">name of property</param>
        object GetProperty(string prop);

        /// <summary>
        /// Set an custom property of the engine
        /// </summary>
        /// <param name="prop">property name</param>
        /// <param name="val">value in object</param>
        void SetProperty(string prop, object val);

        /// <summary>
        /// Invokes an command to the engine
        /// </summary>
        /// <param name="command">the name of the command</param>
        /// <param name="arguments">The arguments of the command</param>
        /// <returns>An object from the command</returns>
        object InvokeCommand(string command, params object[] arguments);
        /// <summary>
        /// Returns an Song by the ISRC playable in the specified engine.
        /// </summary>
        /// <param name="ISRC"></param>
        /// <returns></returns>
        Song GetSongFromISRC(String ISRC);
        /// <summary>
        /// Returns an artist from the specified ID, called from the separate thread
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Artist ArtistFromID(string ID);
        /// <summary>
        /// Returns an album from UPC. Should be called in an separated thread.
        /// </summary>
        /// <param name="UPC"></param>
        /// <returns></returns>
        Album AlbumFromUPC(string UPC);
        /// <summary>
        /// Query a radio stream.
        /// </summary>
        /// <param name="Query">Query specific for the service</param>
        /// <returns></returns>
        List<Song> QueryRadio(String Query);
        /// <summary>
        /// Gets or sets if the player has paused the current media
        /// </summary>
        bool Paused { get; set; }
        /// <summary>
        /// Convert an URI to an instance of Song Object. Used in conjunction with AudioSignature
        /// </summary>
        /// <param name="URI">The resource URI</param>
        /// <returns>A Song with properties beloning to the resource behind the URI</returns>
        Song ConvertSongFromLink(String URI);
        /// <summary>
        /// Current song
        /// </summary>
        Song CurrentSong
        {
            get;
            set;
        }
        /// <summary>
        /// Returns the base for the song urls (eg. spotify:track:*)
        /// </summary>
        string AudioSignature { get;  }
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
        /// </summary>           s
        bool LoggedIn { get; set; }

        /// <summary>
        /// Show the log in creditals. Log in is handled by each service
        /// </summary>
        /// <returns>If the log in was sucessfull</returns>
        LoginResult Login();


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
        /// <param name="ID">The ID or name of the artist to retrieve, depending on the service</param>
        /// <returns>An artist enclosed in an instance of MediaChrome's Artist class</returns>
        Artist GetArtist(string ID);

        /// <summary>
        /// Find an artist
        /// </summary>
        /// <param name="Query">The search query.</param>
        /// <returns>Return an array of the founded artist at the engine</returns>
        Artist[] FindArtist(string Query);

        /// <summary>
        /// Get an album by artist
        /// </summary>
        /// <param name="artist">The artist for the query</param>
        /// <param name="album">The name or ID of the album to locate, depending on the service</param>
        /// <returns>An instance of the album enclosed into an Album class instance</returns>
        /// <remarks>Used in conjunction with views frameworks</remarks>
        Album GetAlbum(Artist artist, string album);

        /// <summary>
        /// Get an album
        /// </summary>
        /// <param name="album">The name or ID of the album to locate, depending on the service</param>
        /// <returns>An instance of the album enclosed into an Album class instance</returns>
        /// <remarks>Used in conjunction with views frameworks</remarks>
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
        /// <param name="pos">The new position of the media</param>
		void Seek(double pos);

        /// <summary>
        /// Occurs when loading an song
        /// </summary>
        /// <param name="Content">The data uri associated with the song</param>
		void Load(String Content);
        /// <summary>
        /// Import music to the local database
        /// </summary>
        /// <param name="Conn">SQLiteConnection instance for accessing the internal database</param>
        /// <param name="RootDir">The file directory for the local files to recurse on</param>
        /// <param name="progress">An reference to an float for indicating an progress between 0.00f - 1.00f (0 = 0%, 1 = 100%)</param>
		List<Song> Import(string RootDir,ref float progress);
        
        /// <summary>
        /// Host form. Used by the runtime
        /// </summary>
        System.Windows.Forms.Form Host{get;set;}

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
        /// Used to do an fast extraction of a song instance from an song query 
        /// </summary>
        /// <param name="_Song">instance of song class without a real connection
        /// to an existing service instance</param>
     
        /// <returns></returns>
        Song RawFind(Song _Song);
		//string RawFind(Song _Song);
		
        /// <summary>
        /// Returns a list of playlists from the current service
        /// </summary>
        List<MediaChrome.Views.Playlist> Playlists {get;}
		/// <summary>
		/// Playlist-related functionality
		/// </summary>
        /// <param name="PlsID">Should only consist of the unique id for the playlist, not from the engine</param>
        /// <returns>A instance of the playlist class with content from this source</returns>
		Playlist ViewPlaylist(string Name,String PlsID);
        /// <summary>
        /// Creates an new playlist on the source.
        /// </summary>
        /// <param name="Name">The desired name of the playlist</param>
        /// <returns>An instance of an playlist class representing the new playlist.</returns>
        Playlist CreatePlaylist(String Name);

        /// <summary>
        /// Add an song to an playlist
        /// </summary>
        /// <param name="playlistID">The ID of the target playlist on the source</param>
        /// <param name="_Song">The instance of the song</param>
        /// <param name="pos">The desired position on the playlist to add on. -1 indicates the last position</param>
		void AddToPlaylist(string playlistID,MediaChrome.Song _Song,int pos);

        /// <summary>
        /// Add an song to an playlist
        /// </summary>
        /// <param name="playlistID">The ID of the target playlist on the source</param>
        /// <param name="_Song">The instance of the song</param>
        /// <param name="pos">The desired position on the playlist to add on. -1 indicates the last position</param>
        void AddToPlaylist(Playlist pls, MediaChrome.Song _Song, int pos);

        /// <summary>
        /// Remove a song at the specified index on the playlist.
        /// </summary>
        /// <param name="playlistID">The ID of the playlist, specific for the service</param>
        /// <param name="pos">The index of the song which should be removed</param>
		void RemoveFromPlaylist(string playlistID,int pos);
        /// <summary>
        /// Moves an entry through the playlist, the collection between the starting and end index.
        /// </summary>
        /// <param name="playlistID">The ID of the playlist</param>
        /// <param name="entry">The Song to move</param>
        /// <param name="startLoc">start index of the chunk to move</param>
        /// <param name="endLoc">end segment of cnhunk</param>
		void MoveSongPlaylist(string playlistID,Song entry,int startLoc,int endLoc);
		
	//	bool canSync {get;}
	//	void LoadSynchronized();
	//	bool OfflineService {get;}

        /// <summary>
        /// Returns the duration of the current song (in seconds)
        /// </summary>
		string Length { get; }

        /// <summary>
        /// Load an playlist and return the list. Obsolote.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="playlist"></param>
        /// <returns></returns>
        List<Song> LoadPlaylist(string p,ref  Playlist playlist);
    }

  


	
}
