using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spotify;
using System.IO;
namespace MediaChrome
{/*
    class SpotifyEngine 
    {
        public string Namespace
        {
            get
            {
                return "Spotify";
            }
        }
        public void Pause()
        {
        }
        
        BassPlayer player;
        public string GetAppString()
        {
            return Program.GetAppString();
        }
        public Form1 X;
        Spotify.Session SpotifySession
        {
            get;
            set;
        }
        public void LogOut()
        {
        }
        public void Load()
        {
        }
        public void LogIn(string name,string pass)
        {
            SpotifySession.LogIn(name, pass);
        }
        public void Stop()
        {
            SpotifySession.PlayerUnload();
        }
        public void Play()
        {
            SpotifySession.PlayerPlay(true);
        }
            
        public SpotifyEngine()
        {
            Spocky.MyClass D = new Spocky.MyClass();
            SpotifySession = Spotify.Session.CreateInstance(D.AppKey(),"C:\\temp","C:\\temp","");
            SpotifySession.OnConnectionError += new SessionEventHandler(SpotifySession_OnConnectionError);
            SpotifySession.OnMusicDelivery += new MusicDeliveryEventHandler(SpotifySession_OnMusicDelivery);

            SpotifySession.OnEndOfTrack += new SessionEventHandler(SpotifySession_OnEndOfTrack);
            SpotifySession.OnPlaylistContainerLoaded += new SessionEventHandler(SpotifySession_OnPlaylistContainerLoaded);
            SpotifySession.OnAlbumBrowseComplete += new AlbumBrowseEventHandler(SpotifySession_OnAlbumBrowseComplete);
            SpotifySession.OnImageLoaded += new ImageEventHandler(SpotifySession_OnImageLoaded);
        }
        void SpotifySession_OnAlbumBrowseComplete(Session sender, AlbumBrowseEventArgs e)
        {


            string d = (string)e.State;
            string x = "";
            int i = 1;
            foreach (Track _Track in e.Result.Tracks)
            {
                x += "<treeitem>\n";
                x += "<treerow uri=\"" + _Track.LinkString + "\" id=\"track-" + i.ToString() + "\"> \n";
                x += "<treecell value=\"" + _Track.Name + "\"/>\n";
                x += "<treecell value=\"" + _Track.Artists[0].Name + "\"/>\n";
                x += "<treecell type=\"progressbar\" value=\"0." + _Track.Popularity.ToString() + "\"/>\n";
                x += "</treerow>\n";
                x += "</treeitem>\n";
                i++;
            }
            d = d.Replace("<Spotify:Album.Tracks/>", x);
            d = d.Replace("spotify:track", "http://go.go/sp_play-spotify:track");
            d = d.Replace("http://open.spotify.com/track/", "http://go.go/sp_play-spotify:track:");
            using (StreamWriter SW = new StreamWriter(Program.GetAppString() + "\\views\\" + X.app + "\\main.view.php"))
            {
                SW.Write(d);
                SW.Close();
            }
            X.Times();

        }

        void SpotifySession_OnImageLoaded(Session sender, ImageEventArgs e)
        {

            if (e.Image != null)
            {
                try
                {
                    string d = GetAppString() + "\\covers\\" + e.State + ".jpg";
                    e.Image.Save(d, System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                catch
                {
                }
            }
        }

        public static Queue<Playlist> toBeAdded = new Queue<Playlist>();
        void SpotifySession_OnPlaylistContainerLoaded(Session sender, SessionEventArgs e)
        {
            foreach (Playlist PlsList in SpotifySession.PlaylistContainer.CurrentLists)
            {
                toBeAdded.Enqueue(PlsList);


            }

        }

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
                e.ConsumedFrames = player.EnqueueSamples(new AudioData(e.Channels, e.Rate, e.Samples, e.Frames));
                X.Maximum = (float)e.Frames;
                X.Value = (float)e.ConsumedFrames;

            }
            else
            {
                e.ConsumedFrames = 0;
            }
        }
        public static void NextTrack()
        {
            if (playQueue.Count > 0)
            {
                Track D = playQueue.Dequeue();
                if (currentTrack != null)
                {
                    playHistory.Push(currentTrack);
                }
                currentTrack = D;
                SpotifySession.PlayerLoad(D);
                X.CoverImage = System.Drawing.Bitmap.FromFile("covers\\" + D.Album.LinkString.Replace(":", "_") + ".jpg");

                SpotifySession.Pla yerPlay(true);
            }
        }
        public void PreviousTrack()
        {
            if (playHistory.Count > 0)
            {

                Track D = playHistory.Pop();
                SpotifySession.PlayerLoad(D);
                X.CoverImage = System.Drawing.Bitmap.FromFile("covers\\" + D.Album.LinkString.Replace(":", "_") + ".jpg");

                SpotifySession.PlayerPlay(true);
                playHistory.Push(currentTrack);
                currentTrack = D;
            }
        }
        void SpotifySession_OnEndOfTrack(Session sender, SessionEventArgs e)
        {
            //Console.WriteLine("End of music delivery. Flushing player buffer...");
            Thread.Sleep(1500); // Samples left in player buffer. Player lags 500 ms
            player.Stop();
            try
            {

                NextTrack();
            }
            catch
            {

            }
            //	Console.WriteLine("Playback complete");
            playbackDone.Set();
        }
        public static Stack<Track> playHistory = new Stack<Track>();
        public static Queue<Track> playQueue = new Queue<Track>();
        public static string currentView = "";
        void SpotifySession_OnConnectionError(Session sender, SessionEventArgs e)
        {

        }

    }*/
}
