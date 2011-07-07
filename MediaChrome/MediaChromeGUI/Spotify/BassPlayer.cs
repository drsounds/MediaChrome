using System;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using MediaChrome;
using MediaChrome.Views;
using Spotify;
using Un4seen.Bass;


namespace MCRuntime
{
	public class spotify : MediaChrome.IPlayEngine
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
        public MediaChrome.Song GetSongFromISRC(string ISRC) { return new Song(); }
        /// <summary>
        /// Returns an artist by the specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public MediaChrome.Artist ArtistFromID(string ID) { return new MediaChrome.Artist(); }

        /// <summary>
        /// Returns an album by the specified UPC
        /// </summary>
        /// <param name="UPC"></param>
        /// <returns></returns>
        public MediaChrome.Album AlbumFromUPC(string UPC) { return new MediaChrome.Album(); }

        public List<Song> Import(string d, ref float progress)
        {
            return new List<Song>();
        }
        public List<Song> QueryRadio(String Query)
        {
            List<Song> Output = new List<Song>();
            // Define a radio query by following:
            // artist:{radio}
            try
            {
             Spotify.Search result =  SpotifySession.SearchSync(Query, new Random().Next(0, 20), new Random().Next(0, 20), new Random().Next(0, 20), new Random().Next(0, 20), new Random().Next(0, 20), new Random().Next(0, 20), new TimeSpan(500));
            
                foreach(Track d in result.Tracks)
                {
                    Song r = TrackToSong(d);
                    Output.Add(r);
                }

            }
            catch
            {
                
            }
            return Output;
        }

        public Song ConvertSongFromLink(String URI)
        {
            Track track = Track.CreateFromLink(Link.Create("spotify:track:" + URI.Replace("spotify:track:", "")));
            
            // wait until the track is ready
            while (track == null) { }
            while (track.Error == sp_error.IS_LOADING) { }
            Song d = new Song();
            d.ArtistName = track.Artists[0].Name;
            d.AlbumName = track.Album.Name;
            d.Title = track.Name;
            return d;

        }
        public Song CurrentSong { get; set; }
        public String AudioSignature
        {
            get
            {
                return "spotify:track:";
            }
        }
        public List<Song> Import(String query)
        {
            return new List<Song>();
        }
        #region DefaultValues
        public String Address { get; set; }
        public String Company { get; set; }
        public String Copyright { get; set; }
        public bool LoggedIn { get; set; }
        public LoginResult Login() {
           

            Login sD = new Login(this);
            if (sD.ShowDialog() == DialogResult.OK)
            {
                SpotifySession.LogInSync(sD.User, sD.Pass, new TimeSpan(4000));
                return LoginResult.Fail;
        
            }
            SpotifySession.OnConnectionError += new SessionEventHandler(SpotifySession_OnConnectionError);
            SpotifySession.OnMusicDelivery += new MusicDeliveryEventHandler(SpotifySession_OnMusicDelivery);

            SpotifySession.OnEndOfTrack += new SessionEventHandler(SpotifySession_OnEndOfTrack);
            SpotifySession.OnPlaylistContainerLoaded += new SessionEventHandler(SpotifySession_OnPlaylistContainerLoaded);
            SpotifySession.OnAlbumBrowseComplete += new AlbumBrowseEventHandler(SpotifySession_OnAlbumBrowseComplete);
            SpotifySession.OnImageLoaded += new ImageEventHandler(SpotifySession_OnImageLoaded);
            SpotifySession.PlaylistContainer.OnContainerLoaded += new PlaylistContainerEventHandler(PlaylistContainer_OnContainerLoaded);
            while (!playlistLoaded)
            {
                Thread.Sleep(100);
            }
            return LoginResult.Pass;
          
        }
        public void Logout() {

            SpotifySession.LogOut();
        }
        public bool Purchase(Song song) { return false; }
        public Uri ServiceUri { get; set; }
        public void ShowOptions() { }
        public bool DownloadStore
        {
            get
            {
                return false;
            }
        }
        public Uri CompanyWebSite { get; set; }
        public List<Song> Purchases { get; set; }
        public bool Streaming { get { return true; } }
       
        #endregion
        public System.Drawing.Image Icon
        {
            get
            {
                return null;
            }
        }
        /// <summary>
        /// Dictionary containing references to loaded tracks, albums and object indexed by their uris
        /// </summary>
        private Dictionary<String, IMedia> artists;
        
        /// <summary>
        /// Finds an artist
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public MediaChrome.Artist[] FindArtist(string ID)
        {
            throw new NotImplementedException();
        }
        public MediaChrome.Album[] FindAlbum(string ID)
        {
            throw new  NotImplementedException();
        }
        public MediaChrome.Artist GetArtist(string ID)
        {
            /**
             * If the artist is already loaded get if from the list
             * */
            if (artists.ContainsKey(ID))
                return (MediaChrome.Artist)artists[ID];


            // Initate objects
            Spotify.ArtistBrowse Browser = SpotifySession.BrowseArtistSync(Spotify.Artist.CreateFromLink(Link.Create("spotify:artist:"+ID)), new TimeSpan(1050000));
            while (Browser == null) { }
            
            MediaChrome.Artist artist = new MediaChrome.Artist();
            artists.Add(ID,artist);
            artist.Link = ID;
            artist.Engine = this;

            // Add albums to the artist instance
            artist.Albums = new List<MediaChrome.Album>();
            for (var i = 0; i < artist.Albums.Count; i++)
            {

                artist.Albums[i] = GetAlbum(Browser.Albums[i].LinkString);
                Thread.Sleep(100);
            }
            return artist;
        }

        /// <summary>
        /// Gets an album. The ID
        /// </summary>
        /// <param name="Name">The URI to the album</param>
        /// <returns></returns>
        public MediaChrome.Album GetAlbum(MediaChrome.Artist artist, string ID)
        {
            if (artists.ContainsKey(ID))
                return (MediaChrome.Album)artists[ID];
            // Initiate objects
            Spotify.AlbumBrowse Browser = SpotifySession.BrowseAlbumSync(Spotify.Album.CreateFromLink(Link.Create("spotify:album"+ID)), new TimeSpan(1050000));
            while (Browser == null) { }

            MediaChrome.Album result = new MediaChrome.Album();
            artists.Add(ID, result);
            result.Name = Browser.Album.Name;


            result.Artist = artist;
            result.Engine = this;
            result.Link = Browser.Album.LinkString;


            // Create tracklist
            result.Songs = new List<Song>();

            for (var i = 0; i < Browser.Tracks.Length; i++)
            {
                Track currentTrack = Browser.Tracks[i];

                Song d = TrackToSong(currentTrack);
                result.Songs[i] = d;

            }

            result.Link = ID;
            return result;

        }

        /// <summary>
        /// Gets an album. The ID
        /// </summary>
        /// <param name="Name">The URI to the album</param>
        /// <returns></returns>
        public MediaChrome.Album GetAlbum(string ID)
        {
            if (artists.ContainsKey(ID))
                return (MediaChrome.Album)artists[ID];
            // Initiate objects
            Spotify.AlbumBrowse Browser = SpotifySession.BrowseAlbumSync(Spotify.Album.CreateFromLink(Link.Create("spotify:album:"+ID)), new TimeSpan(1050000));
            while (Browser == null) { }
            
            MediaChrome.Album result = new MediaChrome.Album();
            artists.Add( ID,result);
            result.Name = Browser.Album.Name;

    
            result.Artist = GetArtist(Browser.Artist.LinkString);
            result.Engine = this;
            result.Link = Browser.Album.LinkString;


            // Create tracklist
            result.Songs = new List<Song>();

            for(var i=0; i < Browser.Tracks.Length;i++)
            {
                Track currentTrack = Browser.Tracks[i];

                Song d = TrackToSong(currentTrack);
                result.Songs[i] = d;

            }

            result.Link = ID;
            return result;
            
        }
        public bool PlaylistsLoaded { get; set; }
        public event EventHandler PlaybackFinished;
        public Form Host { get; set; }
        private double duration;
        public double Duration
        {
            get

            { return duration; }
        }
        private int position;
        public int Position
        {
            get
            {
                return position;
            }
        }

        public List<Song> LoadPlaylist(String ID, ref MediaChrome.Views.Playlist playlist)
        {
            return new List<Song>();
        }
        public MediaChrome.Views.Playlist CreatePlaylist( String Name)
        {
            Spotify.Playlist R = SpotifySession.PlaylistContainer.AddNewPlaylist(Name);
            MediaChrome.Views.Playlist plst = new MediaChrome.Views.Playlist(this, Name, R.LinkString, Host);
            return plst;
        }
        public String Status
        { get; set; }
        public String Image
        {
            get
            {
                return "spotify.png";
            }
        }
        bool playlistLoaded = false;
        public List<MediaChrome.Views.Playlist> playlists;
        public void SongImport(Song[] query)
        {
        }
      
		public String Title
		{
			get
			{
				return "Spotify";
			}
		}
		public String Namespace
		{
			get
			{
				return "spotify";
			}
		}
        public Song RawFind(Song _Song)
        {
            // TODO: Add handler later
            Spotify.Search result = SpotifySession.SearchSync("\""+_Song.Title + "\" artist:\"" + _Song.ArtistName + "\"", 0, 4, 0, 4, 0, 4, new TimeSpan(4000000));
            DateTime s = DateTime.Now;
            // wait until the result has been generated
            while (result == null) {
                if (s.CompareTo(DateTime.Now) > 10)
                    break;
            }
         

            // if song was found deliver the first match
            foreach(Track i in result.Tracks)
            {
                if (i.Name.Contains(_Song.Title) && i.Artists[0].Name.Contains(_Song.ArtistName))
                {
                    // Convert the track to an mc instance
                    Song c = new Song();
                    Track _track = result.Tracks[0];
                    c.Name = c.Name;
                    c.ArtistName = _track.Artists[0].Name;
                    c.AlbumName = _track.Album.Name;
                    c.Path = _track.LinkString;
                    c.Link = _track.LinkString;
                    c.Engine = this;
                    c.ProposedEngine = "spotify";
                    return c;
                }
            }

            // Otherwise return null
            return null;
        }
		private Spotify.SpotifyView view;
		public Control MediaControl
		{
			get
			{
				return view;
			}
			
		}
        
        /// <summary>
        /// Convert an spotify artist to mediachroem artist
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        public MediaChrome.Artist CreateArtist(Spotify.Artist artist)
        {
            return GetArtist(artist.LinkString);
       
        }

    

        /// <summary>
        /// Convert an spotify track to MediaChromeSong
        /// </summary>
        /// <param name="Df"></param>
        /// <returns></returns>
        public Song TrackToSong(Track  Df)
        {

            Song A = new MediaChrome.Song();
            A.Title = Df.Name;
            A.ArtistName = Df.Artists[0].Name;
            A.Album = new MediaChrome.Album();
            A.AlbumName = Df.Album.Name;
       
            A.Path = Df.LinkString;
            A.Store = "Spotify";
            A.Popularity = 0.5f;
            A.Engine = this;
            return A;
        }
        public static MediaChrome.Song ConvertSpotifyTrackToMCSong(string uri)
        {
            // Get song from Spotify

            MediaChrome.Song Song = new MediaChrome.Song();
            Spotify.Track song = Spotify.Track.CreateFromLink(Link.Create(uri));

            // Wait until the song has been loaded
            while (song == null) { }
            while (song.Error == sp_error.IS_LOADING) { }

            // Set song attributes
            Song.Title = song.Name;
            Song.Path = song.LinkString;
            Song.ArtistName = song.Artists[0].Name;
            Song.AlbumName = song.Album.Name;
            return Song;

        }
		public bool Ready {get;set;}
		public int FilesCompleted {get;set;}
		public int TotalFiles {get;set;}
		public List<Song> Find(String Query)
		{
			List<Song> Songs = new List<Song>();
			Search D = SpotifySession.SearchSync(Query,0,100,0,100,0,100,new TimeSpan(4200000));
            try
            {

                foreach (Track Df in D.Tracks)
                {
                    Songs.Add(TrackToSong(Df));

                }
            }
            catch
            {

            }
			return Songs;
		}
		public List<Song> Import(object Connx,String Query)
		{
            return new List<Song>();
        }
	
		
		public String Length {get;set;}
		public void Seek(double a){
            this.SpotifySession.PlayerSeek((int)a);
        
        }
		public void Unload(){}

		public void Pause()
		{
			SpotifySession.PlayerPlay(false);
		}
       /// <summary>
        /// The main entry point for the application.
        /// </summary>
		public  Session SpotifySession ;  
		public  BassPlayer player;
		public  AutoResetEvent playbackDone = new AutoResetEvent(false);
		private  AutoResetEvent loggedOut = new AutoResetEvent(false);
		public  Track currentTrack = null;
		public  bool Sucess=false;
        public static object Mutex = new object();
		public System.Drawing.Image DownloadCover(string url)
        {
            return null;
#if (nobug)
           // if the adress starts with spotify, download the cover image
            if (url.StartsWith("spotify:"))
            {
                try
                {
                    /**
                     * Check if image is already downloaded, if so do not try to download an new one.
                     * All images is stored as their uri but with : replaced to _
                     * */
                    string ImageFilePath = "C:\\temp\\" + url.Replace(":", "_") + ".jpeg"; ;
                    if (!File.Exists(ImageFilePath))
                    {
                        Spotify.Album ct = null;
                        Link d = null;
                        lock (Mutex)
                        {

                            d = Link.Create(e.Adress);
                            ct = Spotify.Album.CreateFromLink(d);
                            Thread.Sleep(100);
                            do
                            {
                                Thread.Sleep(1000);
                            } while (!ct.IsLoaded);

                            /**
                             * Store the covers in an temporary folder. Create temporary directy if not exist
                             * */
                            if (!Directory.Exists("C:\\temp"))
                            {
                                Directory.CreateDirectory("C:\\temp");
                            }


                            // set the path for the image

                            // Download the image if it don't exists in the cache, otherwise the system can load it directly

                            // Download the bitmap
                            System.Drawing.Image cf =Session.LoadImageSync(ct.CoverId, new TimeSpan(1, 0, 0));

                            using (StreamWriter SW = new StreamWriter(ImageFilePath))
                            {
                                cf.Save(SW.BaseStream, ImageFormat.Jpeg);
                                SW.Close();
                            }


                            e.Bitmap = cf;
                            return;
                        }
                    }
                    else
                    {
                        Image cf = Bitmap.FromFile(ImageFilePath);
                        return = cf;
                        
                    }

                }
                catch
                {
                }

            }
#endif
        }
		public List<MediaChrome.Song> Search()
		{
			return new List<MediaChrome.Song>();
		}

		public spotify()
		{
           
			// Creat artists dictionary
            artists = new Dictionary<string, MediaChrome.IMedia>();
            Spocky.MyClass D = new Spocky.MyClass();
            SpotifySession = Spotify.Session.CreateInstance(D.AppKey(), "C:\\SpofityCaches", "C:\\SpofityCaches", "LinSpot");
            
			view = new Spotify.SpotifyView();
        	  
            /* Login sD = new Login(this);
             if (sD.ShowDialog() == DialogResult.OK)
             {
                 SpotifySession.LogInSync(sD.User, sD.Pass, new TimeSpan(4000));
             }*/

             SpotifySession.OnConnectionError += new SessionEventHandler(SpotifySession_OnConnectionError);
             SpotifySession.OnMusicDelivery += new MusicDeliveryEventHandler(SpotifySession_OnMusicDelivery);

             SpotifySession.OnEndOfTrack += new SessionEventHandler(SpotifySession_OnEndOfTrack);
             SpotifySession.OnPlaylistContainerLoaded += new SessionEventHandler(SpotifySession_OnPlaylistContainerLoaded);
             SpotifySession.OnAlbumBrowseComplete += new AlbumBrowseEventHandler(SpotifySession_OnAlbumBrowseComplete);
             SpotifySession.OnImageLoaded += new ImageEventHandler(SpotifySession_OnImageLoaded);
             SpotifySession.PlaylistContainer.OnContainerLoaded += new PlaylistContainerEventHandler(PlaylistContainer_OnContainerLoaded);

            while (!playlistLoaded)
             {
                 Thread.Sleep(100);
             }

             LoggedIn = true;
       
        }
        void PlaylistContainer_OnContainerLoaded(PlaylistContainer sender, PlaylistContainerEventArgs e)
        {
            playlistLoaded = true;
          //  throw new NotImplementedException();
        } 
		 
         void SpotifySession_OnAlbumBrowseComplete(Session sender, AlbumBrowseEventArgs e)
        {
        	
        	/*
        	string d = (string)e.State;
        	string x = "";
        	int i=1;
        	foreach(Track _Track in e.Result.Tracks)
        	{
        		x+="<treeitem>\n";
        		x+="<treerow uri=\""+_Track.LinkString+"\" id=\"track-"+i.ToString()+"\"> \n";
        		x+="<treecell value=\""+_Track.Name+"\"/>\n";
        		x+="<treecell value=\""+_Track.Artists[0].Name+"\"/>\n";
        		x+="<treecell type=\"progressbar\" value=\"0."+_Track.Popularity.ToString()+"\"/>\n";
        		x+="</treerow>\n";
        		x+="</treeitem>\n";
        			i++;
        	}
        	d = d.Replace("<Spotify:Album.Tracks/>",x);
        	d = d.Replace("spotify:track","http://go.go/sp_play-spotify:track");
            d = d.Replace("http://open.spotify.com/track/","http://go.go/sp_play-spotify:track:");
            using(StreamWriter SW = new StreamWriter(Program.GetAppString()+"\\views\\"+X.app+"\\main.view.php"))
            {
              	SW.Write(d);
              	SW.Close();
            }
            X.Times();*/
	                      	 
        }

         void SpotifySession_OnImageLoaded(Session sender, ImageEventArgs e)
        {
       
			if (e.Image != null)
			{
				try
				{
					string d = "C:\\covers\\"+e.Id+".jpg";
					e.Image.Save(d, System.Drawing.Imaging.ImageFormat.Jpeg);
					
				}
				catch
				{
				}
			}
        }
      
         void SpotifySession_OnPlaylistContainerLoaded(Session sender, SessionEventArgs e)
        {

            if (playlists == null)
            {
                playlists = new List<MediaChrome.Views.Playlist>();
            }
            for (int i = 0; i < SpotifySession.PlaylistContainer.CurrentLists.Length; i++ )
            {
                Spotify.Playlist _Playlist = SpotifySession.PlaylistContainer.CurrentLists[i];
                while (!_Playlist.IsLoaded) { }
                MediaChrome.Views.Playlist D = new MediaChrome.Views.Playlist(this, _Playlist.Name, _Playlist.LinkString, Host);

                D.ID = String.Format("spotify:playlist:{0}_{1}",_Playlist.LinkString.Split(':')[2],_Playlist.LinkString.Split(':')[4]);
                D.Title = _Playlist.Name;
                playlists.Add(D);
            }
                playlistLoaded = true;
                PlaylistsLoaded = true;
           		
        	
        }
        private  float pos;
        public  bool Paused
        {
            get;set;
        }

        

        public void playSamples()
        {

        }

        /// <summary>
        /// Cache of streaming data
        /// </summary>
        public Dictionary<string, StreamingBuffer> Buffer { get; set; }
         void SpotifySession_OnMusicDelivery(Session sender, MusicDeliveryEventArgs e)
        {
             
        	if (e.Samples.Length > 0)
			{
                       
				if (player == null)
				{
					//player = new AlsaPlayer(e.Rate / 2); // Buffer 500ms of audio
					player = new BassPlayer();
           
					Console.WriteLine("Player created");
				}
                

				// Don't forget to set how many frames we consumed
				e.ConsumedFrames = player.EnqueueSamples(new AudioData(e.Channels, e.Rate-12, e.Samples, e.Frames));
                
                /*X.Maximum = (float)currentTrack.Duration;
                X.Value  +=10;*/
                this.duration = e.Frames;
                this.position = e.ConsumedFrames;

			}
			else
			{
				e.ConsumedFrames = 0;
			}
        }
        public  void NextTrack()
        {
        	
        
        	
            /*if (playQueue.Count > 0)
            {
                Track D = playQueue.Dequeue();
                if (currentTrack!=null)
                {
                    playHistory.Push(currentTrack);
                }
                currentTrack = D;
               
                SpotifySession.PlayerLoad(D);
                if (File.Exists("covers\\" + D.Album.LinkString.Replace(":", "_") + ".jpg"))
                {
                    try
                    {
                        X.CoverImage = System.Drawing.Bitmap.FromFile("covers\\" + D.Album.LinkString.Replace(":", "_") + ".jpg");
                    }
                    catch { }
                }
                SpotifySession.PlayerPlay(true);
                X.PlayIndex++;
                X.Value = 1;
            }*/
        }
        public  bool paused = false;
        public  void PreviousTrack()
        {
          /*  if (playHistory.Count > 0)
            {

                Track D = playHistory.Pop();
                SpotifySession.PlayerLoad(D);
                X.CoverImage = System.Drawing.Bitmap.FromFile("covers\\" + D.Album.LinkString.Replace(":", "_") + ".jpg");

                SpotifySession.PlayerPlay(true);
                playHistory.Push(currentTrack);
                currentTrack = D;
            }*/
        }
         void SpotifySession_OnEndOfTrack(Session sender, SessionEventArgs e)
        {
        	//Console.WriteLine("End of music delivery. Flushing player buffer...");
			Thread.Sleep(1500); // Samples left in player buffer. Player lags 500 ms
		
            playbackDone.Set();
         
			try
			{

                // Raise playback finished event
                if(PlaybackFinished!=null)
                    PlaybackFinished(this, new EventArgs());
            }
			catch
			{
			
			}
		//	Console.WriteLine("Playback complete");
		
        }
        public  Stack<Track> playHistory = new Stack<Track>();
        public  Queue<Track> playQueue = new Queue<Track>();
        public  string currentView = "";
         void SpotifySession_OnConnectionError(Session sender, SessionEventArgs e)
        {
        	
        }
         public void Load(string URI)
         {
             
             
             currentTrack = Track.CreateFromLink(Link.Create("spotify:track:"+URI.Replace("spotify:track:","")));
             Thread.Sleep(100);
             
             // wait until the song has been loaded
             while (currentTrack.Error == sp_error.IS_LOADING) { }

             CurrentSong = new Song();
             CurrentSong.Name = currentTrack.Name;
             CurrentSong.Title = currentTrack.Name;
             CurrentSong.ArtistName = currentTrack.Artists[0].Name;
             CurrentSong.AlbumName = currentTrack.Album.Name;

         	SpotifySession.PlayerLoad(currentTrack);
            try
            {
                Status = "Loading '" + currentTrack.Name + " - " + currentTrack.Artists[0].Name + "' from Spotify..";
            }
            catch

            { }
         }
         public void Play()
         {
             Status = "Playing 'Media'";
         	SpotifySession.PlayerPlay(true);
            SpotifySession.PlayerPlay(true);
         	
         }
         public void Stop()
         {
         	SpotifySession.PlayerPlay(false);
         	SpotifySession.PlayerUnload();
         }
		
		public bool hasPlaylists {
			get 
			{
			 return	true;
			}
		}
		
		public MediaChrome.Views.Playlist ViewPlaylist(string Name,string PlsID)
		{
            if (PlsID == ""|| PlsID == null)
            {
                return new MediaChrome.Views.Playlist(this, "",PlsID, this.Host);
            }
            string user = PlsID.Split('_')[0];
            string id = PlsID.Split('_')[1];
			List<Song> Songs = new List<Song>();
            MediaChrome.Views.Playlist PList = new MediaChrome.Views.Playlist(this, "", PlsID, this.Host);
            Spotify.Playlist List = Spotify.Playlist.Create(SpotifySession,Link.Create(String.Format("spotify:user:{0}:playlist:{1}",user,id)));
            while(List==null){}
            while (!List.IsLoaded) { }

            for (int i = 0; i < List.CurrentTracks.Length; i++ )
            {
                Track _Track = List.CurrentTracks[i];
                try
                {
                    Song D = new Song();
                    D.Title = _Track.Name;
                    D.AlbumName = _Track.Album.Name;
                    D.ArtistName = _Track.Artists[0].Name;
                    D.Path = _Track.LinkString;
                    Songs.Add(D);
                }
                catch
                {
                }
            }
            PList.Songs = Songs;
            PList.Title = List.Name;
			return PList;
	
		}
        private void AddSongToPlaylist(Song _Song, Spotify.Playlist List,int pos)
        {
            Spotify.Search Search = SpotifySession.SearchSync(String.Format("artist:\"{0}\"  {2}", _Song.ArtistName, _Song.AlbumName, _Song.Title), 0, 4, 0, 4, 0, 6, new TimeSpan(5000000));

            // Wait until search is null
            while (Search == null) { }
            while (Search.Error == sp_error.IS_LOADING) { }
            // Add the song
            foreach (Track t in Search.Tracks)
            {
                if (t.Name.Contains(_Song.Name) && t.Artists[0].Name.Contains(_Song.ArtistName))
                {
                    List.AddTracks(new Track[] { t }, pos);
                
                }
            }
        }
		public void AddToPlaylist(string playlistID, Song _Song, int pos)
		{
			Spotify.Playlist List = Spotify.Playlist.Create(SpotifySession,Link.Create(playlistID));
            if (List.Owner.CanonicalName == SpotifySession.User.CanonicalName)
            {
                /***
                 * Find Song. It can handle querys from other media sources
                 * */
                AddSongToPlaylist(_Song, List, pos);
            }
		}

        public void AddToPlaylist(MediaChrome.Views.Playlist plst, Song _Song, int pos)
        {
            Spotify.Playlist List = Spotify.Playlist.Create(SpotifySession, Link.Create(plst.ID));
            if (List.Owner.CanonicalName == SpotifySession.User.CanonicalName)
            {
                AddSongToPlaylist(_Song, List, pos);
            }
        }
		public void RemoveFromPlaylist(string playlistID, int pos)
		{
			Spotify.Playlist List = Spotify.Playlist.Create(SpotifySession,Link.Create(playlistID));
			if(List.Owner == SpotifySession.User)
			{
		//		List.RemoveTracks(new int[]{0});
			}
		}
		
		public void MoveSongPlaylist(string playlistID,MediaChrome.Song _Song, int startLoc, int endLoc)
		{
			Spotify.Playlist List = Spotify.Playlist.Create(SpotifySession,Link.Create(playlistID));
			if(List.Owner == SpotifySession.User)
			{
				List.ReorderTracks(new int[]{startLoc},endLoc);
			}
		}
		
		public List<MediaChrome.Views.Playlist> Playlists {
			get 
			{
                if (playlists == null)
                    return new List<MediaChrome.Views.Playlist>();
				return playlists;
              

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
            switch (command)
            {
                case "get_inbox":
                    {
#if(false)
                        List<Song> Inbox = new List<Song>();
                        Spotify.Playlist inbox = Spotify.Playlist.CreateInbox(SpotifySession);
                        return Inbox;
#endif
                        break;
                    }
            }
            return false;
        }
    }
}

	public class BassPlayer
	{
		private BASSBuffer basbuffer = null;
		private STREAMPROC streamproc = null;

		public int EnqueueSamples(MCRuntime.AudioData audioData)
		{
			return EnqueueSamples(audioData.Channels, audioData.Rate, audioData.Samples, audioData.Frames);
		}

		public int EnqueueSamples(int channels, int rate, byte[] samples, int frames)
		{
			int consumed = 0;
			if (basbuffer == null)
			{
				Bass.BASS_Init(-1, rate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
				basbuffer = new BASSBuffer(2, rate, channels, 2);
				streamproc = new STREAMPROC(Reader);
				int bassId = Bass.BASS_StreamCreate(rate, channels, BASSFlag.BASS_DEFAULT, streamproc, IntPtr.Zero);
				Bass.BASS_ChannelPlay(bassId, false);
			}

			if (basbuffer.Space(0) > samples.Length)
			{
				basbuffer.Write(samples, samples.Length);
				consumed = frames;
			}

			return consumed;
		}

		private int Reader(int handle, IntPtr buffer, int length, IntPtr user)
		{
			return basbuffer.Read(buffer, length, user.ToInt32());
		}

		public void Stop()
		{ 
			//Bass.BASS_SampleStop
			Bass.BASS_Stop();
			
		}
	}

