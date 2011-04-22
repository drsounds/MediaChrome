using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Spotify;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace SpofityRuntime
{
    public partial class Form1 : Form
    {
        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        // Override the CreateParams property
    /*    protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                
                return cp;
            }
        }*/
    	public Queue<Track> playQueue;
    	
		public Queue<Track> PlayQueue {
			get { return playQueue; }
			set { playQueue = value; }
		}
		public Spotify.Track currentTrack;
        private string currentUrl;
        public Dictionary<string, string> pages;
        public string ParseURI(string URI,bool browse)
        {
            if (URI != null)
            {
            	if(!URI.Contains(":"))
            	{
            		URI="audify:search:"+URI;
            	}
            	if(URI.StartsWith("http://open.spotify.com/"))
            	{
            		URI = URI.Replace("http://open.spotify.com/","spotify:");
            		URI = URI.Replace("/",":");
            	}
            	
                if (URI.StartsWith("audify:")||URI.StartsWith("spotify:"))
                {
                    URI = URI.Replace(".", "");
                    if(!URI.Contains("sp_view-"))
                    {
                    	URI = URI.Replace("spotify:", "audify:");
                    }
                   
                 
                    string application = URI.Split(':')[1];
                    app = application;
                    string query="";
                    if(URI.Split(':').Length > 2)
                    {
                 		URI= "audify:"+application+":"+URI.Replace("audify:"+application+":","").Replace(":","__");
                    }
                    else
                    {
                    	
                    }
                    try
                    {
               			query = URI.Split(':')[2];
                    }
                    catch
                    {
                    	
                    }
                	if (File.Exists(Program.GetAppString() + "\\views\\" + application + "\\main.view"))
                 	{
                     if (!browse)
                     {
                         back.Push(GenerateURI(geckoWebBrowser1.Url.ToString()));
                         forward.Clear();

                         CheckButtons();
                     }
                     querystring = query;
				 	currentUrl = URI;
                     geckoWebBrowser1.Navigate(Program.GetAppString() + "\\views\\" + application + "\\main.view");
                    
                     return URI + "|" + application + " + " + query + "";
                 }
                 else
                 {
                     ShowError("This URI does not exists");
                 }
                }
               
               
            }
            return "ZERO";
        }
        public string userName;
        public string passWord;
        public Stack<String> back;
        public Stack<string> forward;
       
        public Form1()
        {
        	playQueue= new Queue<Track>();
        	playlists=new Dictionary<string, Playlist>();
            MousePoint = new Point();
            MousePoint2 = new Point();
            back = new Stack<string>();
            forward = new Stack<string>();
            InitializeComponent();
            pages = new Dictionary<string, string>();
       
            SpotifySession = Program.SpotifySession;
       		
       		
        }
        Spotify.Session SpotifySession;
        public Form1(string userName,string Password)
        {
    
        	playlists=new Dictionary<string, Playlist>();
        }
       
        public Form1(string URI)
        {
        	playQueue= new Queue<Track>();
        	playlists=new Dictionary<string, Playlist>();
            MousePoint = new Point();
            MousePoint2 = new Point();
            back = new Stack<string>();
            forward = new Stack<string>();
            InitializeComponent();
            pages = new Dictionary<string, string>();
            uri = URI;
            SpotifySession = Program.SpotifySession;
            
           
        }
        string uri;
        bool dragging = false;
        Point MousePoint;
        Point MousePoint2;
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
       
        [DllImport("user32.dll")]
        public static extern void     ReleaseCapture();
        private void pane1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ShowError(string text)
        {
            pane5.Show();
            label2.Text = text;
        }
        public string currentSong;
       
        private void Form1_Load(object sender, EventArgs e)
        {
       //     string uri = Program.GetAppString() + "\\views\\Standard\\main.view";
            Skybound.Gecko.GeckoPreferences.User["capability.policy.default.XMLHttpRequest.open"] = "allAccess";
            Skybound.Gecko.GeckoPreferences.User["network.protocol-handler.expose.spotify"] = true;
            Skybound.Gecko.GeckoPreferences.User["network.protocol-handler.expose.axrequest"] = true;
            Skybound.Gecko.GeckoPreferences.User["gecko.handlerService.schemes.spotify.0.name"] = "at";
            Skybound.Gecko.GeckoPreferences.User["gecko.handlerService.schemes.spotify.0.uriTemplate"] = "http://open.spotify.com/%s";
            Skybound.Gecko.GeckoPreferences.User["gecko.handlerService.schemes.axrequest.0.name"] = "at";
            Skybound.Gecko.GeckoPreferences.User["gecko.handlerService.schemes.axrequest.0.uriTemplate"] = "http://open.spotify.com/%s";
          //  geckoWebBrowser1.Navigate(uri);
             DirectoryInfo D = new DirectoryInfo(Program.GetAppString() + "\\views\\");
            	foreach (DirectoryInfo R in D.GetDirectories())
            {
                if (File.Exists(R.FullName + "\\main.view"))
                {
                	if(!File.Exists(R.FullName + "\\hidden"))
                	{
	                    AddListEntry("audify:" + R.Name + " | " + R.Name);
	                    TreeNode DF = listViewX1.Nodes.Add(R.Name);
	                    DF.Tag = (object)("audify:" + R.Name);
                	}
                }
                

            }

            listViewX1.Nodes.Add("-");
            if (this.uri != "")
            {
                ParseURI(this.uri, true);
            }
           	ParseURI("spotify:standard",false);
           	try{
           		
           	}
           	catch
           	{
           		
           	}
         
        }

        private void geckoWebBrowser1_Click(object sender, EventArgs e)
        {

        }

        private void listViewX1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void geckoWebBrowser1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void geckoWebBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {

        }

        private void geckoWebBrowser1_DomClick(object sender, Skybound.Gecko.GeckoDomEventArgs e)
        {
 
        }
        public Dictionary<string, Playlist> playlists;
        public string app = "";
        string currentItem = "";
        public string querystring = "";
        bool finished = false;
        public event EventHandler playerReady;
           public Playlist currentPlaylist;
           string currentPath="";
        private void geckoWebBrowser1_Navigating(object sender, Skybound.Gecko.GeckoNavigatingEventArgs e)
        {
            
            string Uri = e.Uri.ToString();
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
         	
            if(URI.StartsWith("http://go.go/"))
        	{
        		URI = URI.Replace("http://go.go/","");
        		if(URI.StartsWith("sp_view-"))
        		{
        			URI=URI.Replace("sp_view-","");
        			URI=URI.Replace("/",":");
        			ParseURI(URI,false);
        			e.Cancel=true;
        			return;
        		}
        		if(URI.StartsWith("sp_play-"))
    		   {
        			Track F =Track.CreateFromLink(Spotify.Link.Create(URI.Replace("sp_play-","")));
        			currentTrack=F;
    				if(F.IsAvailable)
    				{
    					try
    					{
    						SpotifySession.PlayerUnload();
    					}
    					catch
    					{
    						
    					}
    		   			SpotifySession.PlayerLoad(F);
    		   		
    		   			SpotifySession.PlayerPlay(true);
    		   			
    		  		 	
    		  		
    		   			return;
    				}else
    				{
    					ShowError("This song is not anvailable in your region or at this time");
    					e.Cancel=true;
    					return;
    				}
        		   	return;
    		   }
        		if(URI.StartsWith("sp_playalbum-"))
    		   {
        		/*	e.Cancel=true;
        			string[] token = Uri.Split('-');
        			string uri = token[1];
        			int trackID = int.Parse(token[2]);
        			int albumID = int.Parse(token[3]);
        			Playlist X = null;
        			try
        			{
        				currentPlaylist = playlists[token[1]];
        			}
        			catch
    				{
        				
    				}
        			ArtistBrowse F = SpotifySession.BrowseAlbumSync(Album.CreateFromLink(SpotifySession,Spotify.Link.Create(token[1])),new TimeSpan(500));
    			
    				playQueue.Clear();
    				for (int i=trackID;i<F.Tracks[token[1]].CurrentTracks.Length ;i++ ) 
    				{
    					playQueue.Enqueue(F);
    				}
    				if(F.IsAvailable)
    				{
    					try
    					{
    						SpotifySession.PlayerUnload();
    					}
    					catch
    					{
    						
    					}
    		   			SpotifySession.PlayerLoad(F);
    		   		
    		   			SpotifySession.PlayerPlay(true);
    		   			
    		  		//  geckoWebBrowser1.ExecuteCommand("track(1)");
    		  		geckoWebBrowser1.Navigate("javascript:track('"+trackID.ToString()+"','"+albumID.ToString()+"')");
    		   			 return;
    				}
    				else
    				{
    					ShowError("This song is not anvailable in your region or at this time");
    					e.Cancel=true;
    					return;
    				}
    				currentTrack=F;
    				*/
    				
    				
    			}
        		if(URI.StartsWith("sp_playlistplay-"))
    		   {
        			e.Cancel=true;
        			string[] token = Uri.Split('-');
        			string uri = token[1];
        			int trackID = int.Parse(token[2]);
        			
        			Playlist X = null;
        			try
        			{
        				currentPlaylist = playlists[token[1]];
        			}
        			catch
    				{
    					X= Playlist.Create(SpotifySession,Spotify.Link.Create(token[1]));
    					playlists.Add(uri,X);
    					currentPlaylist = playlists[token[1]];
    				}
    				currentPlaylist = playlists[token[1]];
    				Track F = playlists[token[1]].CurrentTracks[trackID];
    				playQueue.Clear();
    				for (int i=trackID;i<playlists[token[1]].CurrentTracks.Length ;i++ ) 
    				{
    					playQueue.Enqueue(F);
    				}
    				currentTrack=F;
    				if(F.IsAvailable)
    				{
    					try
    					{
    						SpotifySession.PlayerUnload();
    					}
    					catch
    					{
    						
    					}
    		   			SpotifySession.PlayerLoad(F);
    		   		
    		   			SpotifySession.PlayerPlay(true);
    		   			
    		  		//  geckoWebBrowser1.ExecuteCommand("track(1)");
    		  		geckoWebBrowser1.Navigate("javascript:track('"+trackID.ToString()+"')");
    		   			 return;
    				}else
    				{
    					ShowError("This song is not anvailable in your region or at this time");
    					e.Cancel=true;
    					return;
    				}
    				
    			}
    			else
    			{
    				ShowError("This song is not anvailable in your region or at this time");
    				return;
    			
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
                ParseURI(Uri.Replace("http://parse/", ""), false);
                return;
            }
            Uri = Uri.Replace("%3F", "?");
            
            if (Uri.Contains(".view"))
            {
               

              
                string[] f;
                if (Uri.Contains('?'))
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
                currentPath=path;
                string path2 = path.Replace(".view", "." + querystring + ".xul");
                if (File.Exists(path2))
                {
                    geckoWebBrowser1.Navigate("file:///" + path2);
                    e.Cancel = true;
                    return;
                }
               	
                Playlist CurrentPlaylist;
                
                T = new Process();
                T.StartInfo.FileName = (Program.GetAppString() + "\\php\\php.exe");
                if(this.currentUrl.Contains("spotify:playlist:"))
                {
	                Spotify.Link R = Spotify.Link.Create(this.currentUrl.Replace("__",":").Replace("audify","spotify"));
			        Spotify.Playlist currentPlaylist = Spotify.Playlist.Create(SpotifySession,R);
	            }
                T.StartInfo.Arguments = " \"" + (string)f[0].Replace("file:///", "") + ".php\" \"" + querystring + "\" ";
                currentItem = f[0].Replace(".view", "." + querystring + ".xul");
	                
                if(File.Exists(path))
                {
                	
               
	                 using(StreamReader SR = new StreamReader(path))
	                 {
	                      using(StreamWriter SW = new StreamWriter(path+".php"))
	                      {
	                          string header = (new StreamReader("top.inc")).ReadToEnd();
	                          string footer = (new StreamReader("bottom.inc")).ReadToEnd();
	                          string d = (header + SR.ReadToEnd() + footer).Replace("$argv[1]", parameters);
	                          int pr = 0;
	                    
	                          
	                          try
	                          {
		                          
	                          }
	                          catch(Exception ex)
	                          {
	                             	
	                          }
	                          if(d.Contains("<Spotify:Album.Image/>"))
	                          {
	                          	if(SpotifySession!=null)
	                          	{
	                          		Album  F = Spotify.Album.CreateFromLink(Link.Create("spotify:album:"+parameters));
	                          		SpotifySession.LoadImage(F.CoverId,null);
	                          		  
	                          	}
	                          }
	                          if(d.Contains("<$SPOTIFY:Playlist/>"))
	                          {
	                          	if(SpotifySession!=null)
	                          	{
		                          	string songs = "";
		                          	this.currentPlaylist = Playlist.Create(Program.SpotifySession,Link.Create("spotify:user:"+parameters.Replace("__",":").Replace("audify","spotify")));
		                         	int i=0;
		                          	foreach(Spotify.Track track in this.currentPlaylist.CurrentTracks)
		                          	{
		                          		songs+="<treeitem>\n<treerow  id=\"track-"+i.ToString()+"\" ondoubleclick=\"self.location='http://go.go/sp_playlist-"+currentUrl+"-"+i.ToString()+"-0'\">\n<treecell label=\""+track.Name+"\"/>";
		                          		songs+="\n<treecell  label=\""+track.Artists[0].Name+"\"/>";
		                          		songs+="\n<treecell label=\""+track.Album.Name+"\"/>";
		                          		songs+="</treerow>";
		                          		songs+="</treeitem>\n";
		                          		i++;
		                          	}
		                          	d=d.Replace("<$SPOTIFY:Playlist/>",songs);
		                          	d=d.Replace("$Spotify.Playlist.Name",currentPlaylist.Name);
		                          	d=d.Replace("$Spotify.Playlist.Author",currentPlaylist.Owner.DisplayName);
		                          	WebClient F = new WebClient();
		                          	try
		                          	{
		                          		string dae = F.DownloadString("http://app.krakelin.com/playlists/"+parameters);
		                          		string[] a = dae.Split('&');
		                          		d=d.Replace("$Spotify.Playlist.Description",a[0]);
		                          		d=d.Replace("$Spotify.Playlist.ImageURL",a[1]);
		                          	}
		                          	catch
		                          	{
		                          		d=d.Replace("$Spotify.Playlist.Description","");
		                          		d=d.Replace("$Spotify.Playlist.ImageURL","");
		                          	}
	                          	
	                          	}
	                          }
	                          d = d.Replace("spotify:track","http://go.go/sp_play-spotify:track");
	                          d = d.Replace("http://open.spotify.com/track/","http://go.go/sp_play-spotify:track:");
	                      	  
	                          
	                          SW.Write(d);
	                          SW.Close();
	
	                      }
	                      SR.Close();
	                }
	
	                T.StartInfo.Arguments = " \"" + (string)f[0].Replace("file:///", "") + ".php\" \"" + querystring + "\" ";
	                currentItem = f[0].Replace(".view", "." + querystring + ".xul");
	                T.StartInfo.CreateNoWindow = true;
	                T.StartInfo.RedirectStandardOutput = true;
	                
	
	                T.StartInfo.UseShellExecute = false;
	                
	                T.EnableRaisingEvents = true;
	                T.Exited += new EventHandler(T_Exited);
	                T.OutputDataReceived += new DataReceivedEventHandler(T_OutputDataReceived);
	                T.Start();
	                
	
	                System.Windows.Forms.Timer N = new System.Windows.Forms.Timer();
	                N.Tick+=new EventHandler(N_Tick);
	                N.Interval = 100;
	                N.Start();
	              
	                e.Cancel = true;
	                geckoWebBrowser1.Navigate("file://" + Program.GetAppString() + "\\wait.xul");
                }else{
                	WebClient d = new WebClient();
                	d.DownloadDataCompleted+= new DownloadDataCompletedEventHandler(ExternalDownloadCompleted);
                	d.DownloadStringAsync(new Uri(ERSProvider.Replace("%APP",app).Replace("%QUERY",querystring)));
                	
                }
            }            
        }
		string ERSProvider = "http://krakelin.com/views/%APP?query=%QUERY";
		void ExternalDownloadCompleted(object sender,DownloadDataCompletedEventArgs e)
		{
			DirectoryInfo X = Directory.CreateDirectory(Program.GetAppString()+"\\views\\"+app);
			using(StreamWriter SW = new StreamWriter(Program.GetAppString()+"\\views\\"+app+"\\main."+querystring+".xul"))
			{
				SW.WriteLine((new StreamReader(Program.GetAppString()+"top.inc").ReadToEnd()));
				SW.WriteLine(e.Result);
				SW.WriteLine((new StreamReader(Program.GetAppString()+"bottom.inc").ReadToEnd()));
				SW.Close();
			}
			geckoWebBrowser1.Navigate("file:///"+Program.GetAppString()+"\\views\\"+app+"\\main.xul");
			
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
		}
        void r_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            using (StreamWriter D = new StreamWriter(Program.GetAppString() + "\\views\\" + app + " \\" + e.UserState + ".xml"))
            {
                try{
                    XmlDocument F = new XmlDocument();
                    F.LoadXml(e.Result);
                }
                catch{
                    D.WriteLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
                    D.WriteLine("<data><![CDATA[" + e.Result + "]]></data>");
                   
                   
                }
                finally
                {
                    D.WriteLine("" + e.Result + "");
                    
                }
               
                    
                D.Close();
            }
        }
        void LoadPage(object d)
        {
            
        }
        public void Times()
        {
        	
	        T.StartInfo.CreateNoWindow = false;
	        T.StartInfo.RedirectStandardOutput = false;
	                
	
	        T.StartInfo.UseShellExecute = false;
	                
	        T.EnableRaisingEvents = true;
	        T.Exited += new EventHandler(T_Exited);
	        T.OutputDataReceived += new DataReceivedEventHandler(T_OutputDataReceived);
	        T.Start();
	                
	
	        System.Windows.Forms.Timer N = new System.Windows.Forms.Timer();
	        N.Tick+=new EventHandler(N_Tick);
	        N.Interval = 500;
	        N.Start();
        }
        StreamReader F;
        public void N_Tick(object sender, EventArgs e)
        {
            
            System.Windows.Forms.Timer F = (System.Windows.Forms.Timer)sender;
            if (File.Exists(Program.GetAppString() + "\\main." + querystring + ".xul"))
            {
            	string d = "";
            	try
            	{
	            	using(StreamReader DR = new StreamReader(Program.GetAppString() + "\\main." + querystring + ".xul"))
	            	{
	            		d = DR.ReadToEnd();
	            		int prx=0;
		            	while((prx = d.IndexOf("sp_image-spotify:album:",prx))!=-1)
		              	{
			              	int xr = d.IndexOf("\"",prx);
			              	string directLink = d.Substring(prx,xr-prx).Replace("sp_image-","");
			              	Album dxx = Album.CreateFromLink(Spotify.Link.Create(directLink));
			              	try
			              	{
			              		SpotifySession.LoadImage(dxx.CoverId,"A");
			              		d = d.Replace("sp_image-"+directLink,"../../"+dxx.CoverId+".jpg");
			              	}
			              	catch
			              	{
			              		
			              	}
			              	prx+=xr-prx;
			              	
		              	}
		            
		            	d = d.Replace("spotify:track","http://go.go/sp_play-spotify:track");
		            	d = d.Replace("spotify:album","http://go.go/sp_view-spotify:album");
		            	d = d.Replace("spotify:artist","http://go.go/sp_view-spotify:artist");
		            	d=d.Replace("http://open.spotify.com/track","http://go.go/sp_view-spotify:track");
		            	d=d.Replace("http://open.spotify.com/album","http://go.go/sp_view-spotify:album");
		            	d=d.Replace("http://open.spotify.com/artist","http://go.go/sp_view-spotify:artist");
		            	DR.Close();
			              	
	            	
	            	}
            	}
            	catch
            	{
            		return;
            	}
            	 using(StreamWriter SW = new  StreamWriter(Program.GetAppString() + "\\views\\" + app + "\\main." + querystring + ".xul", true))
	            	{
	            		SW.Write(d);
	            		SW.Close();
	            	}
                geckoWebBrowser1.Navigate(currentItem);
                try
                {
                    pages.Add("audify:" + app + ":" + querystring, currentItem);
                }
                catch
                {
                }
      
                querystring = "";
                F.Stop();
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace("\\Documents","") + "\\main."+querystring+".xul";
            if (File.Exists(path))
            {
                File.Copy(path, Program.GetAppString() + "\\views\\" + app + "\\main." + querystring + ".xul", true);
                geckoWebBrowser1.Navigate(currentItem);
                try
                {
                    pages.Add("audify:" + app + ":" + querystring, currentItem);
                }
                catch
                {
                } File.Delete(path);
                querystring = "";
                F.Stop();
            }
           
        }
        public Process T;
        bool error = false;
        public void T_Exited(object sender, EventArgs e)
        {
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



		string pData="";
        public void T_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
        	pData=e.Data;
        	TreeNode AssociatedItem = CheckPersistanceInTree(currentUrl);
        	try
        	{
        		AssociatedItem.Text=e.Data;
        	}
        	catch
        	{
        		
        	}
        }
        
        bool animate = false;
        bool volcano = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
        	
            try
            {
                if (File.Exists(Program.GetAppString() + "\\incoming.uri"))
                {
                    string uri = (new StreamReader(Program.GetAppString() + "\\incoming.uri")).ReadToEnd();
                    ParseURI(uri,false);
                }
                
            }
            catch { }
            if (error) 
            {

                error = false;
                if (File.Exists(Program.GetAppString() + "\\views\\" + app + "\\main." + querystring + ".xul"))
                {
                    using (StreamReader SRs = new StreamReader(Program.GetAppString() + "\\views\\" + app + "\\main." + querystring + ".xul"))
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
                                    ShowError(T.StandardError.ReadToEnd() + " " + T.StandardOutput.ReadToEnd());
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
                geckoWebBrowser1.Navigate("file:///" + Program.GetAppString() + "\\views\\error.xul");
                
            }
            if (finished == true)
            {
                geckoWebBrowser1.Navigate(currentItem);
                finished = false;
            }
        }

        void F_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
       
            Skybound.Gecko.GeckoElement Elm = (Skybound.Gecko.GeckoElement)e.UserState;
            Elm.InnerHtml = e.Result;
        }

        private void geckoWebBrowser1_LocationChanged(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBox1.Text.StartsWith("audify:"))
            {
                ParseURI(textBox1.Text,false);
            }
            else
            {
                ParseURI("audify:search:" + textBox1.Text,false);
            }
        }

        private void listViewX1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                ParseURI((string)listViewX1.SelectedNode.Tag,false);
            }
            catch
            {
            }
        }
        private TreeNode CheckPersistanceInTree(string Uri)
        {
            foreach(TreeNode f in listViewX1.Nodes )
            {
                if ((string)f.Tag == Uri)
                {
                    return f;
                }
            }
            return null;
        }
        private void listViewX1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                ParseURI((string)listViewX1.SelectedNode.Tag, false);
            }
            catch
            {
            }
        }

        private void textBox1_SearchClicked(object sender, EventArgs e)
        {
            string ListData = "";
            try
            {

                ListData = ParseURI((string)textBox1.Text, false);
            }
            catch
            {
            }
            finally
            {
                try
                {
                    TreeNode X = AddListEntry(ListData);
                    //X.isIsSelected = true;
                }
                catch { }
            }
        }
        private TreeNode AddListEntry(string ListData)
        {
            try
            {
                string[] adress = ListData.Split('|');
                TreeNode F = CheckPersistanceInTree(adress[0]);
                if (F == null)
                {
                    string app = adress[0].Split(':')[1];
                    string[] pass = adress[1].Split('+');
                    var d = listViewX1.Nodes.Add(pass[1]);
                    d.Tag = (object)adress[0];
                    try
                    {
                        imageList1.Images.Add(adress[0], Image.FromFile(Program.GetAppString() + "\\views\\" + app + "\\icon.png"));
                        d.ImageKey = adress[0];
                    }
                    catch
                    {
                        d.ImageIndex = -1;
                    }
                    F = d;
                    return F;
                }
                else
                {
                    return F;
                }
            }
            catch
            {
            }
            return null;
        }
        private void geckoWebBrowser1_Click_1(object sender, EventArgs e)
        {

        }

        private void geckoWebBrowser1_DomClick_1(object sender, Skybound.Gecko.GeckoDomEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pane5.Hide();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GoBack();
        }
        private string GenerateURI(string adress)
        {
        	try{
            string d;
           adress= adress.Replace("..", ".");
            string app = "";
            try
            {
                
                d = adress.Split('\\')[adress.Split('\\').Length-1].Split('.')[1];
                app = adress.Split('\\')[adress.Split('\\').Length - 2];
            }
            catch
            {
                d = adress.Split('/')[adress.Split('/').Length-1].Split('.')[1];
                app = adress.Split('/')[adress.Split('/').Length - 2];
            }
            string Url = "audify:";
            
            if(d!="xul")
            {
                Url += app+ ":" + d;
            }
            else
            {
                Url +=app;
            }
            return Url;
        	}catch{
        		return "spotify:home:a";
        	}
        }
        private void CheckButtons()
        {
/*
            linkLabel1.Enabled = back.Count > 0;
            linkLabel2.Enabled = forward.Count > 0;*/
        }
        private void GoBack()
        {
            try
            {
                string currentUrl = GenerateURI(geckoWebBrowser1.Url.ToString());
                forward.Push(  currentUrl );
                string backURL = back.Pop();
                ParseURI(backURL, true);
                
            }
            catch
            {
            }
            CheckButtons();
        }
        private void GoForward()
        {
            try
            {
                
                string currentUrl = GenerateURI(geckoWebBrowser1.Url.ToString());
                back.Push(currentUrl);
                string backURL = forward.Pop();
                ParseURI(backURL, true);
                

            }
            catch
            {
            }
            CheckButtons();
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GoForward();
        }

        private void geckoWebBrowser1_Navigated(object sender, Skybound.Gecko.GeckoNavigatedEventArgs e)
        {
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (currentUrl != null)
            {
                try
                {
                    string data = Program.GetAppString() + " \\views\\" + currentUrl.Split(':')[1] + "\\main." + currentUrl.Split(':')[2] + ".xul";
                    if (File.Exists(data))
                    {
                        File.Delete(data);
                        ParseURI(currentUrl, true);
                        back.Pop();
                        forward.Clear();
                    }
                }
                catch
                {
                }
            }
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
      
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pane1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

        } 

        private void pane1_MouseMove(object sender, MouseEventArgs e)
        {
        
        }

        private void pane1_Move(object sender, EventArgs e)
        {

        }

        private void pane1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pane1_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void pane1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
          
        }
        bool resizing = false;
        private void pane6_MouseDown(object sender, MouseEventArgs e)
        {
            resizing = true;
            MousePoint = new Point(e.X, e.Y);

        }

        private void pane6_MouseMove(object sender, MouseEventArgs e)
        {
            if (resizing)
            {
                this.Width = Cursor.Position.X - this.Left;
                this.Height = Cursor.Position.Y - this.Top;
            }
        }

        private void pane6_MouseUp(object sender, MouseEventArgs e)
        {
            resizing = false;
        }

        private void pane6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listViewX1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {


        }

        private void listViewX1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void geckoWebBrowser1_Click_2(object sender, EventArgs e)
        {

        }

        private void listViewX1_SelectedIndexChanged_3(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        void GeckoWebBrowser1DomMouseDown(object sender, Skybound.Gecko.GeckoDomMouseEventArgs e)
        {
        	this.ActiveControl = geckoWebBrowser1;
        }
        
        void GeckoWebBrowser1DomClick(object sender, Skybound.Gecko.GeckoDomEventArgs e)
        {
        	
        }
        
        void Form1Enter(object sender, EventArgs e)
        {

        }
        
        void Form1Activated(object sender, EventArgs e)
        {
        	if(geckoWebBrowser1.Focused)
        	{
        	this.textBox1.Focus(); 
        	this.geckoWebBrowser1.Focus();
        	}
        }
        
        void SplitContainer1Scroll(object sender, ScrollEventArgs e)
        {
        	splitContainer1.Update();
        }
	     public string currentArtist
	     {
	     	get{return lArtist.Text;}
	     	set{lArtist.Text=value;}
	    
	     }
	     public string currentAlbum
	     {
	     	get{return lAlbum.Text;}
	     	set{lAlbum.Text=value;}
	    
	     }
        void CBtn1Load(object sender, EventArgs e)
        {
        
        	if(currentTrack!=null)
        	{
        		SpotifySession.PlayerLoad(currentTrack);
        		SpotifySession.PlayerPlay(true);
        		
        	}
        	else{
        		
        	}
       
        }
        
        void Timer2Tick(object sender, EventArgs e)
        {
        	while(true)
        	{
        		try
        		{
        			Playlist PlsList = Program.toBeAdded.Dequeue();
        			if(PlsList.Name!="")
        			{
        				var d = listViewX1.Nodes["Playlists"].Nodes.Add(PlsList.Name);
		           		d.Text=PlsList.Name;
		           		d.Tag=(object)PlsList.LinkString;
        			}
        		}
        		catch
        		{
        			break;
        		}
        	}
        }
        
        void CBtn5Click(object sender, EventArgs e)
        {
        	 if (currentUrl != null)
            {
                try
                {
                    string data = Program.GetAppString() + " \\views\\" + currentUrl.Split(':')[1] + "\\main." + currentUrl.Split(':')[2] + ".xul";
                    if (File.Exists(data))
                    {
                        File.Delete(data);
                        ParseURI(currentUrl, true);
                        back.Pop();
                        forward.Clear();
                    }
                }
                catch
                {
                }
            }
        }
        
        void CBtn3Click(object sender, EventArgs e)
        {
        	GoBack();
        }
        
        void CBtn4Click(object sender, EventArgs e)
        {
        	GoForward();
        }
        
        void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
        {
        
        }
        
        void ListViewX1Layout(object sender, LayoutEventArgs e)
        {
        	
        }
        
        void ListViewX1MouseUp(object sender, MouseEventArgs e)
        {
        	try
            {
                ParseURI((string)listViewX1.SelectedNode.Tag,false);
            }
            catch
            {
            }
        }
    }
}
