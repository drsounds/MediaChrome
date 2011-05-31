/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 2010-11-04
 * Time: 09:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using Skybound.Gecko;
using Spotify;

namespace SpofityRuntime
{
	/// <summary>
	/// Description of View.
	/// </summary>
	public partial class View : UserControl
	{
		public void Initialize()
		{
			this.geckoWebBrowser1.Navigating += new GeckoNavigatingEventHandler(geckoWebBrowser1_Navigating);
		}
		public Process D;
		public Stack<String> Errors
		{
			get
			{
				return Me.Errors;
			}
		}
		public Skybound.Gecko.GeckoWebBrowser Browser
		{
			get
			{
				return this.geckoWebBrowser1;
			}
			set
			{
				geckoWebBrowser1 = value;
			}
		}
		Process T;
		public void T_Exited(object sender, EventArgs e)
        {
            string r = T.StandardOutput.ReadToEnd();
            if (r!="")
            {
                Errors.Push(r);
            }
            try
            {
                if (T.ExitCode == -1)
                {
                    error = true;
                }
            }
            catch
            {
            }
        }
 		public void T_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
           /* if (pData.Contains("error"))
            {
                Errors.Push("There was a error loading the page");
            }*/
        	//pData=e.Data;
        	//TreeNode AssociatedItem = CheckPersistanceInTree(currentUrl);
        	try
        	{
        	//	AssociatedItem.Text=e.Data;
        	}
        	catch
        	{
        		
        	}
        }
 		System.Windows.Forms.Timer FN;
 		System.Windows.Forms.Timer N;
	public void Times()
        {
        	
	        T.StartInfo.CreateNoWindow = false;
	        T.StartInfo.RedirectStandardOutput = false;
	                
	
	        T.StartInfo.UseShellExecute = false;
	                
	        T.EnableRaisingEvents = true;
	        T.Exited += new EventHandler(T_Exited);
	        T.OutputDataReceived += new DataReceivedEventHandler(T_OutputDataReceived);
	        T.Start();
	                
			N = new System.Windows.Forms.Timer();
	        N.Tick+=new EventHandler(N_Tick);
	        
	        N.Interval = 500;
	        N.Start();
         }
	public void LoadApp()
	{
		
	}
		 public View(Form1 Me,string URI)
       	 {
		 	
	 		InitializeComponent();
	 		
	 		FN = new System.Windows.Forms.Timer();
	 		FN.Tick+= new EventHandler(FN_Tick);
            this.URI = URI;
            this.app = this.URI.Split(':')[1];
            if(URI.Split(':').Length > 2)
            {
            	this.querystring = URI.Split(':')[2];
            }
            this.Me = Me;
            this.Browser = geckoWebBrowser1;
          	
            this.Controls.Add(Browser);
            this.Browser.Show();
      		Browser.Dock = DockStyle.Fill;
      		
      		this.Dock = DockStyle.Fill;
			this.Show();
      	  	this.Browser.Navigating += new GeckoNavigatingEventHandler(geckoWebBrowser1_Navigating);
           	 
        }
		 bool sent=false;
		 public void FN_Tick(object sender, EventArgs e)
		 {
		 	if(Ready)
		 	{
		 		geckoWebBrowser1.Navigate(Program.GetAppString()+"\\views\\"+app+"\\cache\\main."+querystring+".xul");
		 		
		 
		 	}
		 	
		 	
		 }
		 string currentUrl
		 {
		 	get
		 	{
		 		return URI;
		 	}
		 	set
		 	{
		 		URI=value;
		 	}
		 }
		 
		 public string app;
		 public string currentPath;
		 public string currentItem;
		 public Spotify.Playlist currentPlaylist
		 {
		 	get
		 	{
		 		return Me.currentPlaylist;
		 	}
		 	set
		 	{
		 		Me.currentPlaylist = value;
		 	}
		 }
		 public Track currentTrack
		 {
		 	get
		 	{
		 		return Me.currentTrack;
		 	}
		 	set
		 	{
		 		Me.currentTrack=value;
		 	}
		 }
		
		public Spotify.Session SpotifySession
		{
			get
			{
				return Program.SpotifySession;
			}
		}
        public Form1 Me { get; set; }
         public Skybound.Gecko.GeckoNavigatingEventArgs EventArgs { get; set; }
        public string URI { get; set; }
        public System.Windows.Forms.Timer Listener { get; set; }
        public bool Ready { get; set; }
        public bool error;
		private void geckoWebBrowser1_Navigating(object sender, Skybound.Gecko.GeckoNavigatingEventArgs e)
        {
         
        	Skybound.Gecko.GeckoWebBrowser geckoWebBrowser1 = (GeckoWebBrowser)sender;
            string Uri = e.Uri.ToString();
            if (Uri.EndsWith(".xul") || File.Exists("file://"+Program.GetAppString()+"\\views\\"+app+"\\cache\\main."+querystring+".xul"))
            {
                e.Cancel = false;
                return;
            }
	         
            if (Uri.StartsWith("spotify:"))
            {
                Me.ParseURI(Uri,false);
                e.Cancel = true;
            }
            
            
            if (Uri.StartsWith("http://go.go/axrequest:"))
            {
                string f = Uri.Replace("http://go.go/axrequest:", "");
                WebClient r = new WebClient();

                r.DownloadStringCompleted += new DownloadStringCompletedEventHandler(r_DownloadStringCompleted);
                r.DownloadStringAsync(new Uri(f.Split('$')[0]), f.Split('$')[1]);
                e.Cancel = true;
                return;
            }
         	string URI = Uri;

            if (URI.StartsWith("http://go.go/"))
            {
               
                URI = URI.Replace("http://go.go/", "");
                if (URI.StartsWith("sp_subscribe-"))
                {
                    Link _Link = Link.Create(URI.Replace("sp_subscribe-","").Replace("__", ":"));
                    Playlist X = Playlist.Create(SpotifySession,_Link);
                   
                    Program.SpotifySession.PlaylistContainer.AddPlaylist(_Link);
                 /*   var item = Me.listViewX1.Nodes["playlists"].Nodes.Add(X.Name);
                    item.Tag = (object)X.LinkString;*/
                 // TODO: OnSubscribing
                    return;
                }

                if (URI.StartsWith("sp_view-"))
                {
                    URI = URI.Replace("sp_view-", "");
                    URI = URI.Replace("/", ":");
                    Me.ParseURI(URI, false);
                    e.Cancel = true;
                    return;                                                     
                }
                if (URI.StartsWith("sp_play-"))
                {
               
                    string[] TrackList = URI.Replace("sp_play-", "").Split('+')[0].Split('.');
                    Me.PlayIndex = int.Parse(URI.Split('+')[1]);
                    Program.playQueue.Clear();
                    
                    foreach (string _track in TrackList)
                    {
                        try
                        {
                            Track Dd = Track.CreateFromLink(Link.Create(_track));

                            Program.playQueue.Enqueue(Dd);
                        }
                        catch { }
                    }
                    if (Me.currentTrack != null)
                    {
                        Program.playHistory.Push(Me.currentTrack);
                    }
                    Me.currentTrack = Program.playQueue.Dequeue();
                    try
                    {

                        string Cover = Me.currentTrack.Album.LinkString;
                        if (File.Exists(Program.GetAppString() + "\\covers\\" + Cover.Replace(":", "_") + ".jpg"))
                        {
                            try
                            {
                                Me.CoverImage = System.Drawing.Bitmap.FromFile(Program.GetAppString() + "\\covers\\" + Cover.Replace(":", "_") + ".jpg");
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch
                    {
                    }
                  
                      Program.SpotifySession.PlayerLoad(currentTrack);
                    Program.SpotifySession.PlayerPlay(true);
                    Program.currentView = currentUrl;
                  
                    e.Cancel = true;
                    return;
                }
          
            }
            if (Uri.StartsWith("http:"))
            {
                e.Cancel = true;
                Process.Start(Uri);
                return;
            }
            if (Uri.StartsWith("http://parse//"))
            {
                e.Cancel = true;
                Me.ParseURI(Uri.Replace("http://parse/", ""), false);
                return;
            }
            
 			
            /*Thread D = new Thread(new System.Threading.ParameterizedThreadStart(GetSpotifyData));
            try
           {
                if ((currentUrl.Contains("spotify") || currentUrl.Contains("audify")) && !e.Uri.ToString().EndsWith(".xul"))
                {
                	Browser.Show();
                    Browser.Navigate(Program.GetAppString() + "\\wait.xul");
                    CreatePage(sender,e);
                    D.Start();
                    
                    Browser.BringToFront();
                    
                    e.Cancel = true;
                  
                }
            }
            catch(Exception ex)
            {
            	MessageBox.Show(ex.Message);
            }
         */
        }
		void GenerateView(string baseFile)
		{
			FN.Start();
			GetSpotifyData();
			CreatePage(baseFile);
			
			N_Tick(new object(),new EventArgs());
			Ready=true;
		}
		string querystring="";
		public void UpdatePage()
		{
			GenerateView(Program.GetAppString()+"\\views\\"+app+"\\main."+querystring+".view");
		
		}
		void CreatePage(String URI)
        {
           string Uri =  URI;
            Uri = Uri.Replace("%3F", "?");

            if (Uri.Contains(".view"))
            {



                string[] f;
                if (Uri.Contains("?"))
                {
                    f = Uri.Split('?');
                }
                else
                {
                    f = new string[] { Uri, "" };

                }

                if (querystring == "")
                {
                    querystring = f[1];
                }
                string parameters = querystring;
                string path = (string)f[0].Replace("file:///", "");
                currentPath = path;
                string path2 = path.Replace(".view", "." + querystring + ".xul");
                path2 = path2.Replace("main.", "cache/main.");
              

                Playlist CurrentPlaylist;

                T = new Process();
                T.StartInfo.FileName = (Program.GetAppString() + "\\php\\php.exe");
                if (this.currentUrl.Contains("spotify:playlist:"))
                {
                    Spotify.Link R = Spotify.Link.Create(this.currentUrl.Replace("__", ":").Replace("audify", "spotify"));
                    Spotify.Playlist currentPlaylist = Spotify.Playlist.Create(SpotifySession, R);
                }
                T.StartInfo.Arguments = " \"" + (string)f[0].Replace("file:///", "") + ".php\" \"" + querystring + "\" \""+Program.GetAppString()+"\" ";
                currentItem = f[0].Replace(".view", "." + querystring + ".xul");

                if (File.Exists(path))
                {


                    using (StreamReader SR = new StreamReader(path))
                    {

                        using (StreamWriter SW = new StreamWriter(path + ".php"))
                        {
                            string header = (new StreamReader("top.inc")).ReadToEnd();
                            string footer = (new StreamReader("bottom.inc")).ReadToEnd();
                            string d = (header + SR.ReadToEnd() + footer).Replace("$argv[1]", parameters);
                            int pr = 0;


                            try
                            {

                            }
                            catch (Exception ex)
                            {

                            }
                            if (d.Contains("<Spotify:Album.Image/>"))
                            {
                                if (SpotifySession != null)
                                {
                                    Album F = Spotify.Album.CreateFromLink(Link.Create("spotify:album:" + parameters));
                                    SpotifySession.LoadImage(F.CoverId, null);

                                }
                            }
                            if (d.Contains("<$SPOTIFY:Playlist/>"))
                            {
                                if (Program.SpotifySession != null)
                                {
                                    string songs = "";
                                    Me.currentPlaylist = Playlist.Create(Program.SpotifySession, Link.Create("spotify:user:" + parameters.Replace("__", ":").Replace("audify", "spotify")));
                                    int i = 0;
                                    foreach (Spotify.Track track in Me.currentPlaylist.CurrentTracks)
                                    {
                                        songs += "<treeitem>\n<treerow  id=\"track-" + i.ToString() + "\" ondoubleclick=\"self.location='http://go.go/sp_playlist-" + currentUrl + "-" + i.ToString() + "-0'\">\n<treecell label=\"" + track.Name + "\"/>";
                                        songs += "\n<treecell  label=\"" + track.Artists[0].Name + "\"/>";
                                        songs += "\n<treecell label=\"" + track.Album.Name + "\"/>";
                                        songs += "</treerow>";
                                        songs += "</treeitem>\n";
                                        i++;
                                    }
                                    d = d.Replace("<$SPOTIFY:Playlist/>", songs);
                                    d = d.Replace("$Spotify.Playlist.Name", currentPlaylist.Name);
                                    d = d.Replace("$Spotify.Playlist.Author", currentPlaylist.Owner.DisplayName);
                                    WebClient F = new WebClient();
                                    try
                                    {
                                        string dae = F.DownloadString("http://app.krakelin.com/playlists/" + parameters);
                                        string[] a = dae.Split('&');
                                        d = d.Replace("$Spotify.Playlist.Description", a[0]);
                                        d = d.Replace("$Spotify.Playlist.ImageURL", a[1]);
                                    }
                                    catch
                                    {
                                        d = d.Replace("$Spotify.Playlist.Description", "");
                                        d = d.Replace("$Spotify.Playlist.ImageURL", "");
                                    }

                                }
                            }
                            d = d.Replace("spotify:track", "http://go.go/sp_play-spotify:track");
                            d = d.Replace("http://open.spotify.com/track/", "http://go.go/sp_play-spotify:track:");


                            SW.Write(d.Replace("$VIEW",app));
                            SW.Close();

                        }
                        SR.Close();
                    }

                    T.StartInfo.Arguments = " \"" + (string)f[0].Replace("file:///", "") + ".php\" \"" + querystring + "\" ";
                    currentItem = f[0].Replace(".view", "." + querystring + ".xul");
                    currentItem = currentItem.Replace("main.", "cache/main.");
                    T.StartInfo.CreateNoWindow = true;
                    T.StartInfo.RedirectStandardOutput = true;


                    T.StartInfo.UseShellExecute = false;

                    T.EnableRaisingEvents = true;
                    T.Exited += new EventHandler(T_Exited);
                    T.OutputDataReceived += new DataReceivedEventHandler(T_OutputDataReceived);
                    T.Start();


                   
                    while(!T.HasExited)
                    {
                    	
                    }
                    
                }
                else
                {
                    WebClient d = new WebClient();
                    d.DownloadDataCompleted += new DownloadDataCompletedEventHandler(ExternalDownloadCompleted);
                    d.DownloadStringAsync(new Uri(ERSProvider.Replace("%APP", app).Replace("%QUERY", querystring)));

                }
            }
		}
            private void GetSpotifyData()
        	{ 
           
	            string path = ((string)URI).Replace("file:///", "");
	            try
	            {
	                using (StreamWriter SW = new StreamWriter(Program.GetAppString() + "\\views\\"+app+"\\"+querystring+".data.json"))
	                {
	                    try
	                    {
	                       
	                        string Url = Me.currentUrl.Replace("audify:", "spotify:");
	                        System.Web.Script.Serialization.JavaScriptSerializer D = new System.Web.Script.Serialization.JavaScriptSerializer();
	                        string playList = D.Serialize(Me.currentPlaylist);
	                        if(Url.StartsWith("spotify:user:"))
	                        {
	                            Url = Url.Replace("__",":");
	                        }
	                        if (Url.Replace("__",":").StartsWith("spotify:user:") && Url.Contains("playlist:"))
	                        {
	                            WebClient X = new WebClient();
	                            string f = "No description anvailable";
	                            try
	                            {
	                             f =    X.DownloadString(new Uri("http://spotiapps.krakelin.com/playlists/" + Url.Replace(":", "__") + ".description"));
	                            }
	                            catch
	                            {
	                            }
	                            
	                            Playlist Plst = Playlist.Create(SpotifySession,Link.Create(Url));
	                           
	                            SW.Write("{ \"Description\":\""+f+"\", \"PlayIndex\":"+Me.PlayIndex+", \"currentURI\" : \"" + Url + "\", \"Playlist\":" + D.Serialize(Plst) + "}");
	                        }
	                        if (Url.StartsWith("spotify:search:"))
	                        {
	                            Search X = Program.SpotifySession.SearchSync(Url.Replace("spotify:search:", ""), 0, 120, 0, 120, 0, 120, new TimeSpan(510000000));
	                            Search Y = Program.SpotifySession.SearchSync("artist:" + Url.Replace("spotify:search:", ""), 120, 120, 120, 120, 120, 120, new TimeSpan(510000000));
	                            Search Z = Program.SpotifySession.SearchSync("album:" + Url.Replace("spotify:search:", ""), 120, 120, 120, 120, 120, 120, new TimeSpan(510000000));
	                            string cList = Url.Contains("spotify:search:") ? D.Serialize((object)X) : "null";
	                            foreach (Track _Track in X.Tracks)
	                            {
	                                if (!File.Exists(Program.GetAppString() + "\\covers\\" + _Track.Album.LinkString.Replace(":", "_") + ".jpg"))
	                                {
	                                    Program.SpotifySession.LoadImage(_Track.Album.CoverId, (object)Program.GetAppString() + "\\covers\\" + _Track.Album.LinkString.Replace(":", "_") + ".jpg");
	                                }
	                            }
	                            string cArtists = Url.Contains("spotify:search:") ? D.Serialize((object)Y) : "null";
	                            string cAlbums = Url.Contains("spotify:search:") ? D.Serialize((object)Z) : "null";
	                            SW.Write("{ \"currentURI\" : \"" + Url + "\", \"contents\" : { \"Albums\": " + cAlbums + ",\"Artists\":" + cArtists + ", \"Playlist\":" + playList + ", \"Search\":" + cList + "}}");
	                        }
	                        if (Url.StartsWith("spotify:artist:"))
	                        {
	
	                            int f = 0;
	                            Artist F = Spotify.Artist.CreateFromLink(Link.Create(Url));
	                            ArtistBrowse _Artist = Program.SpotifySession.BrowseArtistSync(F, new TimeSpan(1150000));
	
	                            string alb = "[";
	                            foreach (Album _Album in _Artist.Albums)
	                            {
	
	                                AlbumBrowse X = Program.SpotifySession.BrowseAlbumSync(_Album, new TimeSpan(1150000));
	
	                                if (!File.Exists(Program.GetAppString() + "\\covers\\" + _Album.LinkString.Replace(":", "_") + ".jpg"))
	                                {
	                                    if (X != null)
	                                    {
	                                        Program.SpotifySession.LoadImage(_Album.CoverId, X.Album.LinkString.Replace(":", "_"));
	                                    }
	                                }
	                                if (X != null)
	                                {
	                                    alb += D.Serialize(X) + ",";
	                                }
	                            }
	                            alb += "]";
	                            alb = alb.Replace(",]", "]");
	
	                            SW.Write("{ currentItem: "+Me.PlayIndex+", \"currentURI\" : \"" + Url + "\", \"contents\" : " + D.Serialize(_Artist) + ",\"Albums\":" + alb + "}");
	
	                        }
	                        if (Url.StartsWith("spotify:album"))
	                        {
	                            AlbumBrowse _Artist = Program.SpotifySession.BrowseAlbumSync(Album.CreateFromLink(Link.Create(Url)), new TimeSpan(1500000));
	
	                            SW.Write("{ \"currentURI\" : \"" + Url + "\", \"contents\" : " + D.Serialize(_Artist) + "}");
	
	                        }
	                      /*  if (Url.StartsWith("spotify:album:"))
	                        {
	                            ArtistBrowse _Artist = Program.SpotifySession.BrowseArtistSync(Spotify.Artist.CreateFromLink(Link.Create(Url)), new TimeSpan(150000));
	                            SW.Write("{ \"currentURI\" : \"" + Url + "\", \"contents\" : " + D.Serialize(_Artist) + "}");
	
	                        }
	                        /*    ArtistBrowse A = null;
	                        AlbumBrowse B = null;
	                        Playlist C = null;
	                        try
	                        {
	                            A = Program.SpotifySession.BrowseArtistSync(Spotify.Artist.CreateFromLink(Link.Create(Url)), new TimeSpan(3000));
	                            B = Program.SpotifySession.BrowseAlbumSync(Spotify.Album.CreateFromLink(Link.Create(Url)), new TimeSpan(3000));
	                            C = Spotify.Playlist.Create(Program.SpotifySession, Link.Create(Url));
	                        }
	                        catch
	                        { }*/
	
	                        /*    string ArtistX = Url.Contains("spotify:artist:") ? D.Serialize((object)A) : "null";
	                            string AlbumX = Url.Contains("spotify:album:") ? D.Serialize((object)B) : "null";
	                            string Playlist = Url.Contains("spotify:album:") ? D.Serialize((object)C) : "null";*/
	                        string Artist = Url.Contains("spotify:artist:") ? D.Serialize((object)Spotify.Artist.CreateFromLink(Link.Create(Url))) : "null";
	                        SW.Close();
	                    }
	                    catch
	                    {
	                        SW.Close();
	                    }
	                }
	
	            }
	            catch
	            {
	               Errors.Push("Browsing already in progress. try again later");
	                return;
         	   }
           	 	sent=true;
            

        }
            string ERSProvider = "http://krakelin.com/views/%APP?query=%QUERY";
		
        void r_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            using (StreamWriter D = new StreamWriter(Program.GetAppString() + "\\views\\"+app+"\\cache\\" + e.UserState + ".xml",false))
            {
                try
                {
                    try
                    {
                        XmlDocument F = new XmlDocument();
                        F.LoadXml(e.Result);
                    }
                    catch
                    {
                        try
                        {
                            D.WriteLine("<data>" + e.Result + "</data>");
                        }
                        catch
                        {
                        }

                    }
                    finally
                    {
                        D.WriteLine("" + e.Result.Replace("<?phpxml", "<?xml") + "");

                    }


                    D.Close();
                }
                catch(Exception ex){
                    D.WriteLine("<data><error>");
                    D.Close();
                }
            }
        }
        bool isWriting=false;
        public void N_Tick(object sender, EventArgs e)
        {
            
              string DS = Program.GetAppString() + "\\views\\"+app+"\\cache\\main." + querystring + ".xul";
            if (File.Exists(DS) && !File.Exists(Program.GetAppString() + "\\views\\" + app + "\\cache\\writing." + querystring + ".xul"))
            {
            	string d = "";
            	try
            	{
	            	using(StreamReader DR = new StreamReader(Program.GetAppString() + "\\views\\"+app+"\\cache\\main." + querystring + ".xul"))
	            	{
	            		d = DR.ReadToEnd();
	            		int prx=0;
		            /*	while((prx = d.IndexOf("sp_image-spotify:album:",prx))!=-1)
		              	{
			              	int xr = d.IndexOf("\"",prx);
			              	string directLink = d.Substring(prx,xr-prx).Replace("sp_image-","");
			              	Album dxx = Album.CreateFromLink(Spotify.Link.Create(directLink));
			              	try
			              	{
			              		SpotifySession.LoadImage(dxx.CoverId,"A");
			              		d = d.Replace("sp_image-"+directLink,"../../img/"+dxx.LinkString+".jpg");
			              	}
			              	catch
			              	{
			              		
			              	}
			              	prx+=xr-prx;
			              	
		              	}*/

                        /*	d = d.Replace("spotify:track","http://go.go/sp_play-spotify:track");
                            d = d.Replace("spotify:album","http://go.go/sp_view-spotify:album");
                            d = d.Replace("spotify:artist","http://go.go/sp_view-spotify:artist");
                            d=d.Replace("http://open.spotify.com/track","http://go.go/sp_view-spotify:track");
                            d=d.Replace("http://open.spotify.com/album","http://go.go/sp_view-spotify:album");
                            d=d.Replace("http://open.spotify.com/artist","http://go.go/sp_view-spotify:artist");*/
                        DR.Close();
			              	
	            	
	            	}
            	}
            	catch
            	{
            		return;
            	}
                isWriting = true;
            	using(StreamWriter SW = new  StreamWriter(Program.GetAppString() + "\\views\\" + app + "\\cache\\main." + querystring + ".xul",false))
	            {
	            	SW.Write(d);
	            	SW.Close();
	            }
                isWriting = false;
              
                try
                {
             //       Me.pages.Add("audify:" + app + ":" + querystring, currentItem);
                }
                catch
                {
                }
      
                querystring = "";
               
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace("\\Documents","") + "\\cache\\main."+querystring+".xul";
            if (File.Exists(path))
            {
             //   File.Copy(path, Program.GetAppString() + "\\views\\" + app + "\\Cache\\main." + querystring + ".xul", true);
                Browser.Navigate(Program.GetAppString() + "\\views\\" + app + "\\Cache\\main." + querystring + ".xul");
                try
                {
                 //   Me.pages.Add("audify:" + app + ":" + querystring, currentItem);
                }
                catch
                {
                } 
               	File.Delete(path);
                querystring = "";
                
            }
          
           
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
    
         
          
            
            if (error) 
            {

                error = false;
                if (File.Exists(Program.GetAppString() + "\\views\\" + app + "\\cache\\main." + querystring + ".xul"))
                {
                    using (StreamReader SRs = new StreamReader(Program.GetAppString() + "\\views\\" + app + "\\cache\\main." + querystring + ".xul"))
                    {
                        using (StreamReader SR = new StreamReader(Program.GetAppString() + "\\views\\error.view"))
                        {
                            using (StreamWriter SW = new StreamWriter(Program.GetAppString() + "\\views\\error.xul"))
                            {
                                /*   string f = SR.ReadToEnd();
                                   string g = SRs.ReadToEnd();
                                   try
                                   {
                                       SW.Write(f.Replace("$errorDescription", f.Substring(f.IndexOf("Fatal error:"), f.Length - f.IndexOf("Fatal error:"))));
                                   }
                                   catch
                                   {
                                       SW.Write(f.Replace("$errorDescription", g));
                                   }
                                   SW.Close();*/
                                try
                                {
                               //     Me.ShowError(T.StandardError.ReadToEnd() + " " + T.StandardOutput.ReadToEnd());
                                }
                                catch
                                {
                                }
                            }
                            SR.Close();
                        }
                        SRs.Close();
                    }
                    
                }
                else
                {
                    using (StreamReader SR = new StreamReader(Program.GetAppString() + "\\views\\error.view"))
                    {
                        using (StreamWriter SW = new StreamWriter(Program.GetAppString() + "\\views\\error.xul"))
                        {
                            SW.Write(SR.ReadToEnd().Replace("$errorDescription", T.StandardOutput.ReadToEnd()));
                            SW.Close();
                        }
                        SR.Close();
                    }
                }
                Browser.Navigate("file:///" + Program.GetAppString() + "\\views\\error.xul");
                
            }
            if (finished == true)
            {
            	Browser.Navigate(currentItem);
                finished = false;
            }
        }
        
        bool finished=false;
		void ExternalDownloadCompleted(object sender,DownloadDataCompletedEventArgs e)
		{
			DirectoryInfo X = Directory.CreateDirectory(Program.GetAppString()+"\\views\\"+app);
			using(StreamWriter SW = new StreamWriter(Program.GetAppString()+"\\views\\"+app+"\\Cache\\main."+querystring+".xul"))
			{
				SW.WriteLine((new StreamReader(Program.GetAppString()+"top.inc").ReadToEnd()));
				SW.WriteLine(e.Result);
				SW.WriteLine((new StreamReader(Program.GetAppString()+"bottom.inc").ReadToEnd()));
				SW.Close();
			}
			Browser.Navigate("file:///"+Program.GetAppString()+"\\views\\"+app+"\\Cache\\main.xul");
			
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
		}		
		void GeckoWebBrowser1Click(object sender, EventArgs e)
		{
			
		}
	}
}
