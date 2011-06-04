﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Spotify;

namespace MediaChrome
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

        /// <summary>
        /// Media engines
        /// </summary>
        public static Dictionary<String, MediaChrome.IPlayEngine> MediaEngines = new Dictionary<string, MediaChrome.IPlayEngine>();

        /// <summary>
        /// Social networks
        /// </summary>
        public static Dictionary<String, MediaChrome.SocialNetworking.ISocialNetwork> SocialNetworks = new Dictionary<string,SocialNetworking.ISocialNetwork>();

        public static Form1 mainForm;
        [STAThread]
        static void Main(string[] arguments)
        {
            mainForm = new Form1();
            
            /**
             * Add media engines
             * */
            MediaEngines.Add("spotify", new MediaChrome.SpotifyPlayer());
         //   MediaEngines.Add("mp3", new MediaChrome.MP3Player());
        //   MediaEngines.Add("youtube", new MediaChrome.Youtube());
            MediaEngines.Add("mp3", new MediaChrome.MP3Player());

            /**
             * Add social networks
             * */
            SocialNetworks.Add("facebook", new MediaChrome.SocialNetworking.Facebook());


            // Add next song event handling
            foreach(MediaChrome.IPlayEngine Engine in MediaEngines.Values)
            {
                Engine.PlaybackFinished+=new EventHandler(Engine_PlaybackFinished);
               
            }
            Application.Run(mainForm);
            
        } 
		static Process T;
   

        static void  Engine_PlaybackFinished(object sender, EventArgs e)
        {
            // Next song in the list
           
            mainForm.NextSong();
        } 
        
        static void SpotifySession_OnAlbumBrowseComplete(Session sender, AlbumBrowseEventArgs e)
        {
        	
        	
        	
	                      	 
        }

        static void SpotifySession_OnConnectionError(Session sender, SessionEventArgs e)
        {
        	
        }
        public static string GetAppString()
        {
            return Application.ExecutablePath.Replace(".EXE",".exe").Replace("\\SpofityRuntime.exe", "");
        }
    }
}