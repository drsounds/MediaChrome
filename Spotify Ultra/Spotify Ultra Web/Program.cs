using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Spotify;
namespace SpofityRuntime
{
    static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetForegroundWindow(IntPtr hwndParent);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
		public static Spotify.Session SpotifySession ;
		 public static BassPlayer player;
		 	public static AutoResetEvent playbackDone = new AutoResetEvent(false);
		private static AutoResetEvent loggedOut = new AutoResetEvent(false);
		public static Track currentTrack = null;
		public static bool Sucess=false;

        public static Dictionary<String, MediaChrome.IPlayEngine> MediaEngines = new Dictionary<string, MediaChrome.IPlayEngine>();

		public static Login DK;
        [STAThread]
        static void Main(string[] arguments)
        {
            MediaEngines.Add("mp3",new MediaChrome.MP3Player());
            if (Process.GetProcessesByName("SpofityRuntime").Length > 0)
            {
                IntPtr Already = FindWindowEx((IntPtr)null, (IntPtr)null, "", "ERS");
               
                if (arguments.Length > 0)
                {



                    using (StreamWriter SW = new StreamWriter(GetAppString() + "\\incoming.uri"))
                    {
                        SW.Write(arguments[0]);
                        SW.Close();
                    }
                //    MessageBox.Show("A");
                    return;
                }
                
            }
        	Spocky.MyClass D = new Spocky.MyClass();
            SpotifySession = Spotify.Session.CreateInstance(D.AppKey(), Application.CommonAppDataPath + "\\SpofityCache", Application.CommonAppDataPath + "\\SpofityCache", "LinSpot");
        	 Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DK = new Login();
            
            DK.ShowDialog();
            if(!Sucess)
            {
            	return;
            } 
        	SpotifySession.OnConnectionError+= new SessionEventHandler(SpotifySession_OnConnectionError);
        	SpotifySession.OnMusicDelivery+= new MusicDeliveryEventHandler(SpotifySession_OnMusicDelivery);
            
        	SpotifySession.OnEndOfTrack+= new SessionEventHandler(SpotifySession_OnEndOfTrack);
        	SpotifySession.OnPlaylistContainerLoaded+= new SessionEventHandler(SpotifySession_OnPlaylistContainerLoaded);
        	SpotifySession.OnAlbumBrowseComplete+= new AlbumBrowseEventHandler(SpotifySession_OnAlbumBrowseComplete);
        	SpotifySession.OnImageLoaded+= new ImageEventHandler(SpotifySession_OnImageLoaded);
            
           
            
            if (arguments.Length > 0)
            { 
            	X = new Form1(arguments[0]);
                Application.Run(X);
            }
            else
            {
            	X = new Form1();
            	Application.Run(X);
            }
        } 
		static Process T;
        static void SpotifySession_OnAlbumBrowseComplete(Session sender, AlbumBrowseEventArgs e)
        {
        	
        	
        	
	                      	 
        }

        static void SpotifySession_OnImageLoaded(Session sender, ImageEventArgs e)
        {
        	
			if (e.Image != null)
			{
				try
				{
					string d = GetAppString()+"\\covers\\"+e.State+".jpg";
					e.Image.Save(d, System.Drawing.Imaging.ImageFormat.Jpeg);
					
				}
				catch
				{
				}
			}
        }
       	public static Form1 X;
        public static Queue<Playlist> toBeAdded = new Queue<Playlist>();
        
        static void SpotifySession_OnPlaylistContainerLoaded(Session sender, SessionEventArgs e)
        {	
        	foreach(Playlist PlsList in SpotifySession.PlaylistContainer.CurrentLists)
	            {
	        		
	        		toBeAdded.Enqueue(PlsList);
	        		
	        		
	           		
	            }
        	
        }
        private static float pos;
        public static bool Paused
        {
            get
            {
                return Program.paused;
            }
            set
            {
                Program.paused = value;
                SpotifySession.PlayerPlay(!Program.paused);
                
            }
        }
        static void SpotifySession_OnMusicDelivery(Session sender, MusicDeliveryEventArgs e)
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

                X.Maximum = (float)currentTrack.Duration;
                X.Value  +=10;

			}
			else
			{
				e.ConsumedFrames = 0;
			}
        }

        /// <summary>
        /// Occurs when the current song has reached it's ending
        /// </summary>
        public static void NextTrack()
        {
            // Call the next song to the Spofity handling the Song
            X.NextSong();
        }
        public static bool paused = false;
        public static void PreviousTrack()
        {
            if (playHistory.Count > 0)
            {

                Track D = playHistory.Pop();
                SpotifySession.PlayerLoad(D);
                //X.CoverImage = System.Drawing.Bitmap.FromFile("covers\\" + D.Album.LinkString.Replace(":", "_") + ".jpg");

                SpotifySession.PlayerPlay(true);
                playHistory.Push(currentTrack);
                currentTrack = D;
            }
        }
        static void SpotifySession_OnEndOfTrack(Session sender, SessionEventArgs e)
        {
        	//Console.WriteLine("End of music delivery. Flushing player buffer...");
			Thread.Sleep(1500); // Samples left in player buffer. Player lags 500 ms
		
            playbackDone.Set();
         
			try
			{
              
                NextTrack();
            }
			catch
			{
			
			}
		//	Console.WriteLine("Playback complete");
		
        }
        public static Stack<Track> playHistory = new Stack<Track>();
        public static Queue<Track> playQueue = new Queue<Track>();
        public static string currentView = "";
        static void SpotifySession_OnConnectionError(Session sender, SessionEventArgs e)
        {
        	
        }
        public static string GetAppString()
        {
            return Application.ExecutablePath.Replace(".EXE",".exe").Replace("\\SpofityRuntime.exe", "");
        }
    }
}
