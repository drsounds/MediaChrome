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
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetForegroundWindow(IntPtr hwndParent);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
		public static Spotify.Session SpotifySession ;
		 public static BassPlayer player;
		 	private static AutoResetEvent playbackDone = new AutoResetEvent(false);
		private static AutoResetEvent loggedOut = new AutoResetEvent(false);
		private static Track currentTrack = null;
		public static bool Sucess=false;
		public static Login DK;
        static void Main(string[] arguments)
        {
        	
        	Spocky.MyClass D = new Spocky.MyClass();
        	SpotifySession = Spotify.Session.CreateInstance(D.AppKey(),"C:\\cache","C:\\Cache","LinSpot");
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
            IntPtr Already = FindWindowEx((IntPtr)null, (IntPtr)null, "", "Spofity");
            if (Already != (IntPtr)0)
            {
                if (arguments.Length > 0)
                {
               

               
                    using (StreamWriter SW = new StreamWriter(GetAppString()+ "\\incoming.uri"))
                    {
                        SW.Write(arguments[0]);
                        SW.Close();
                    }
                    SetForegroundWindow(Already);
                    return;
                }
            }
            Skybound.Gecko.Xpcom.Initialize(GetAppString() + "\\xulrunner");
           
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
            X.Times();
	                      	 
        }

        static void SpotifySession_OnImageLoaded(Session sender, ImageEventArgs e)
        {
        	
			if (e.Image != null)
			{
				try
				{
					
					e.Image.Save(e.Id+".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
					
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
				e.ConsumedFrames = player.EnqueueSamples(new AudioData(e.Channels, e.Rate, e.Samples, e.Frames));
			}
			else
			{
				e.ConsumedFrames = 0;
			}
        }

        static void SpotifySession_OnEndOfTrack(Session sender, SessionEventArgs e)
        {
        	//Console.WriteLine("End of music delivery. Flushing player buffer...");
			Thread.Sleep(1500); // Samples left in player buffer. Player lags 500 ms
			player.Stop();
			try
			{
				Track D = X.playQueue.Dequeue();
				SpotifySession.PlayerLoad(D);
				SpotifySession.PlayerPlay(true);
			}
			catch
			{
				player = null;
			}
		//	Console.WriteLine("Playback complete");
			playbackDone.Set();
        }

        static void SpotifySession_OnConnectionError(Session sender, SessionEventArgs e)
        {
        	MessageBox.Show("Failed to log in");
        }
        public static string GetAppString()
        {
            return Application.ExecutablePath.Replace(".EXE",".exe").Replace("\\SpofityRuntime.exe", "");
        }
    }
}
