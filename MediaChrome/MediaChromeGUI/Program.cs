using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;

using System.Text;
using System.Reflection;
using MediaChrome;
namespace MediaChromeGUI
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
	
		// public static BassPlayer player;
		 	public static AutoResetEvent playbackDone = new AutoResetEvent(false);
		private static AutoResetEvent loggedOut = new AutoResetEvent(false);
	
		public static bool Sucess=false;

        /// <summary>
        /// Media engines
        /// </summary>
        public static Dictionary<String, MediaChrome.IPlayEngine> MediaEngines = new Dictionary<string, MediaChrome.IPlayEngine>();

        /// <summary>
        /// Social networks
        /// </summary>
        public static Dictionary<String, MediaChrome.SocialNetworking.ISocialNetwork> SocialNetworks = new Dictionary<string,MediaChrome.SocialNetworking.ISocialNetwork>();

        public static void LoadSocialNetworks(string Location)
        {
            // Return if the folder does not exists
            if (!Directory.Exists(Location))
                return;
            DirectoryInfo DI = new DirectoryInfo(Location);
            foreach (DirectoryInfo di in DI.GetDirectories())
            {
                LoadEngine<MediaChrome.SocialNetworking.ISocialNetwork>(di, Program.SocialNetworks);
            }
        }

        /// <summary>
        /// Loads an set of media providers into MediaChrome's provider list
        /// </summary>
        /// <param name="Location"></param>
        public static void LoadEngines(string Location)
        {
            DirectoryInfo DI = new DirectoryInfo(Location);
            if (!Directory.Exists(Location))
                return;
            foreach (DirectoryInfo dir in DI.GetDirectories())
            {
                LoadEngine<IPlayEngine>(dir,Program.MediaEngines); 
            }
        }
      
        /// <summary>
        /// Loads the specified engine into the stack
        /// </summary>
        /// <param name="Dir">the directory of the engine</param>
        /// <returns>An boolean indicating the load was sucessfull or failed</returns>
        public static bool LoadEngine<T>(DirectoryInfo Dir,Dictionary<string,T> Collection)
        {
            try
            {
                // Copy all depencies to the application working directory
                foreach (FileInfo fi in Dir.GetFiles("*.dll"))
                {
                     try
                    {
                        if (fi.Name != Dir.Name + ".dll")
                        {
                            if(!File.Exists( AppDomain.CurrentDomain.BaseDirectory + "\\" + fi.Name))
                            File.Copy(fi.FullName, AppDomain.CurrentDomain.BaseDirectory + "\\" + fi.Name);
                        }
                            Assembly.LoadFrom(fi.FullName);
                        
                    }
                    catch            
                    {
                    }
                }
                Assembly assembly = Assembly.LoadFrom(Dir.FullName + "\\" + Dir.Name + ".dll");

                Type type = assembly.GetType("MediaChrome." + Dir.Name);
                if (type == null)
                    type = assembly.GetType("MCRuntime." + Dir.Name);


                

                object c = (object)Activator.CreateInstance(type);
                T IE = (T)c;
                Collection.Add(Dir.Name, IE);
                
                return true;

            }
            catch
            {
                return false;
            }
        }


        public static string StorageFolder(string folder)
        {
            return Properties.Settings.Default.StorageFolder +"\\"+folder;
        }
        public static void CreateDirectories()
        {
            try
            {
                if (!Directory.Exists(Properties.Settings.Default.StorageFolder))
                {
                    Directory.CreateDirectory(Properties.Settings.Default.StorageFolder);
                }
                if (!Directory.Exists(Properties.Settings.Default.StorageFolder + "\\Providers"))
                {
                    Directory.CreateDirectory(Properties.Settings.Default.StorageFolder + "\\Providers");
                }
                if (!Directory.Exists(Properties.Settings.Default.StorageFolder + "\\SocialNetworks"))
                {
                    Directory.CreateDirectory(Properties.Settings.Default.StorageFolder + "\\SocialNetworks");
                }
                if (!Directory.Exists(Properties.Settings.Default.StorageFolder + "\\views"))
                {
                    Directory.CreateDirectory(Properties.Settings.Default.StorageFolder + "\\views");
                }
            }
            catch { }
        }
        public static Form1 mainForm;
       [STAThread]
        static void Main(string[] arguments)
        {
            Application.SetCompatibleTextRenderingDefault(true);
            // Create directories
            CreateDirectories();

            // Load engines
            LoadEngines(StorageFolder("Providers"));
            LoadSocialNetworks(StorageFolder("SocialNetworks"));

            MediaEngines.Add("spotify", new  MCRuntime.spotify());
            MediaEngines.Add("mp3", new MediaChromeGUI.MP3Player());
            //   MediaEngines.Add("youtube", new MediaChrome.Youtube());
            //        MediaEngines.Add("mp3", new MediaChrome.MP3Player());
            
            Application.EnableVisualStyles();
            mainForm = new Form1();
            

            /**
             * Add media engines
             * */
            

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
        
        
      
    }
}
