using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using GlassForms;
using Spotify;

namespace SpofityRuntime
{

     
    public partial class Form1 : GlassForms.GlassForm
    {
        public int PlayIndex
        {
            get;
            set;
        }
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
    	private Queue<Track> playQueue;
    	
		public Queue<Track> PlayQueue {
			get { return playQueue; }
			set { playQueue = value; }
		}
        public Spotify.Track currentTrack
        {
            get { return Program.currentTrack; }
            set { Program.currentTrack = value; }
        }
        private string currentUrl;
        public Dictionary<string, string> pages;
        public string ParseURI(string URI,bool browse)
        {
        	
            if (URI != null)
            {
          
                if (URI.StartsWith("resource:menu:"))
                {
                    this.ContextMenu.Tag = (object)URI.Replace("resource:menu:","");
                    this.contextMenuStrip1.Show(Cursor.Position);
                    return "";
                }
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
                    /* if (URI.StartsWith("audify:user") && URI.Contains("playlist:"))
                     {
                /*         listView3.Show();
                         listView3.Dock = DockStyle.Fill;
                         geckoWebBrowser1.Dock = DockStyle.Top;
                         geckoWebBrowser1.Height = 620;
                         Playlist X = Playlist.Create(Program.SpotifySession, Link.Create(URI.Replace("audify:", "spotify:")));
                         currentPlaylist = X;
                         listView1.Clear();
                         foreach (Track D in X.CurrentTracks)
                         {
                             var Item = listView1.Items.Add(D.Name);
                             Item.SubItems.Add(D.Artists[0].Name);
                             Item.SubItems.Add(D.Album.Name);
                             Item.SubItems.Add(D.Disc.ToString());
                            
                             Item.Tag = (object)D.LinkString;
                         }
                     }
                     else 
                     {
                         /listView3.Hide();
                      
                         geckoWebBrowser1.Dock = DockStyle.Fill;
                        
                     }
                  */
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
            Errors = new Stack<String>();
       		
        }
        Spotify.Session SpotifySession;
        public Form1(string userName,string Password)
        {
            Errors = new Stack<String>();
        	playlists=new Dictionary<string, Playlist>();
        }
       
        public Form1(string URI)
        {

            playQueue = new Queue<Track>();
            playlists = new Dictionary<string, Playlist>();
            MousePoint = new Point();
            MousePoint2 = new Point();
            back = new Stack<string>();
            forward = new Stack<string>();
            InitializeComponent();
            pages = new Dictionary<string, string>();

            SpotifySession = Program.SpotifySession;
            Errors = new Stack<String>();
            ParseURI(URI,false);
            
        }
        string uri;
        bool dragging = false;
        Point MousePoint;
        Point MousePoint2;



        // Define the CS_DROPSHADOW constant
   

        // Override the CreateParams property
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
 		
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        Stack<string> Errors { get; set; }
        [DllImport("user32.dll")]
        public static extern void     ReleaseCapture();
        private void pane1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ShowError(string text)
        {

            pane5.Show();
            this.label2.Text = text;
            flashT = 0;
        }
        public string currentSong;
        public Image CoverImage
        {
            get { return this.pictureBox1.BackgroundImage; }
            set
            {
                this.pictureBox1.BackgroundImage = value;
            }
        }
      /*  public string currentArtist
        {
            get
            {
                return lArtist.Text;
            }
            set
            {
                lArtist.Text = value;
            }
        }
        public string currentAlbum
        {
            get
            {
                return lAlbum.Text;
            }
            set
            {
                lAlbum.Text = value;
            }
        }*/
      	private int mx,my;
      	private bool list_mousedown=false;
      	
      	
        private void Form1_Load(object sender, EventArgs e)
        {
        	this.listViewX1.DragOver+=new DragEventHandler(Form1_DragOver);
        
        	TreeNode NewPlaylist = this.listViewX1.Nodes.Add("New Playlist");
        	NewPlaylist.Tag = (object)"newplaylist";
        	this.listViewX1.MouseMove+=new MouseEventHandler(Form1_MouseMove);
        	this.listViewX1.AllowDrop=true;
        	this.MouseUp+=new MouseEventHandler(Form1_MouseUp);
        	this.listViewX1.DragEnter+=new DragEventHandler(Form1_DragEnter);
        	this.listViewX1.DragDrop+=new DragEventHandler(Form1_DragDrop);
        	this.listView2.ItemDrag+=new ItemDragEventHandler(Form1_ItemDrag);
        	this.timer3.Start();
            this.geckoWebBrowser1.Navigate("javascript:window.navigator.registerProtocolHandler('spotify','http://localhost/sp_view-%s','Spotify')");
       //     string uri = Program.GetAppString() + "\\views\\Standard\\main.view";
          
       this.bmps = new Stack<Sonique>();
       
       		
          //  geckoWebBrowser1.Navigate(uri);
             DirectoryInfo D = new DirectoryInfo(Program.GetAppString() + "\\views\\");
            	foreach (DirectoryInfo R in D.GetDirectories())
            {
                if (File.Exists(R.FullName + "\\main.view"))
                {
                    if (!Directory.Exists(R.FullName + "\\cache"))
                    {
                        Directory.CreateDirectory(R.FullName + "\\cache");
                    }
                	if(!File.Exists(R.FullName + "\\hidden"))
                	{
                        FileInfo[] Rs = R.GetFiles("*.caption");
                        foreach(FileInfo Info in Rs)
                        {  
                            AddListEntry("audify:" + Info.Name + " | " + R.Name);
	                        TreeNode DF = listViewX1.Nodes.Add(R.Name);
	                        DF.Tag = (object)("audify:" + R.Name);
                        }

                        if(Rs.Length==0){
	                     AddListEntry("audify:" + R.Name + " | " + R.Name);
	                     TreeNode DF = listViewX1.Nodes.Add(R.Name);
	                     DF.Tag = (object)("audify:" + R.Name);
                        }
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

        void Form1_DragOver(object sender, DragEventArgs e)
        {
/*	if(draggingSongs)
        	{
	        	draggingSongs=false;
	        	if(tracksToAdd!=null)
	        		tracksToAdd.Clear();
        	}
        	
        
*/        	

        }

        int treeX,treeY;
        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
	  	treeX=e.X;
	  	treeY=e.Y;
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
        	
        	
        }
        
	List<Track> tracksToAdd;
        void Form1_DragDrop(object sender, DragEventArgs e)
        {
        	if(draggingSongs)
        	{
        		if(tracksToAdd==null)
        		{
        			return;
        		}
        		
        		TreeNode PostNode = listViewX1.GetNodeAt(listViewX1.PointToClient(new Point(e.X,e.Y)));
        		if(((string)PostNode.Tag)=="newplaylist")
        		{
        			if(tracksToAdd.Count>1)
        			{
        				Playlist Dn = Program.SpotifySession.PlaylistContainer.AddNewPlaylist(tracksToAdd[0].Album.Name);
        				Dn.AddTracks(tracksToAdd.ToArray(),0);
        				TreeNode NewPlaylist = listViewX1.Nodes.Add(Dn.Name);
        				NewPlaylist.Tag=(object)Dn.LinkString;
        			}
        		}
        		if(((string)PostNode.Tag).Contains("playlist__") ||( (string)PostNode.Tag).Contains("playlist:"))
        		{
        			string playlist = (string)PostNode.Tag;
        			playlist = playlist.Replace("__",":");
        			Playlist X = Spotify.Playlist.Create(Program.SpotifySession,Link.Create(playlist));
        			
        			if(tracksToAdd!=null && X.IsLoaded&& (X.Owner == Program.SpotifySession.User || X.IsCollaborative))
        			{
        				
        				X.AddTracks(tracksToAdd.ToArray(),X.CurrentTracks.Length-1);
        			}
        				
        		}
        		draggingSongs=false;
        		tracksToAdd.Clear();
        	}
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
        	if(draggingSongs)
        	{
        		e.Effect= DragDropEffects.Copy;
        		
        	}
        }
	//http://go.go/sp_view-spotify:album:6rZww4xymj5hepG6VwLPr6
	bool draggingSongs=false;
        void Form1_ItemDrag(object sender, ItemDragEventArgs e)
        {
        	tracksToAdd = new List<Track>();
        	foreach(ListViewItem Item in listView2.SelectedItems)
        	{
        		Track D = (Track)Item.Tag;
        		tracksToAdd.Add(D);
        	}
        //	listViewX1.DoDragDrop(tracksToAdd, DragDropEffects.Copy);
     
        	draggingSongs=true;
        	
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
            
           public string playlistURI;
           string currentPath="";
        private void PrintCurrentPlaylist()
        {
            
        }
         void Form1_ItemDropped(object sender, DropEventArgs e)
        {
         	int[] list = new int[e.Items.Count];
         	int ep  =0;
         	for(int i=0; i < e.Items.Count; i++)
         	{
         		
         		list[i]=e.OldPosition+ep;
         	}
         	ep=0;
         	
         	this.currentPlaylist.ReorderTracks(list,e.NewPosition);
        }
        private void GetSpotifyData(object d)
        {
            BrowseClass DX = (BrowseClass)d;
            string path = ((string)DX.URI).Replace("file:///", "");
            try
            {
                using (StreamWriter SW = new StreamWriter(Program.GetAppString() + "\\data.json"))
                {
                    try
                    {
                       
                        string Url = currentUrl.Replace("audify:", "spotify:");
                        System.Web.Script.Serialization.JavaScriptSerializer D = new System.Web.Script.Serialization.JavaScriptSerializer();
                        string playList = D.Serialize(this.currentPlaylist);
                        if(Url.StartsWith("spotify:user:"))
                        {
                            Url = Url.Replace("__",":");
                        }
                        if (Url.Replace("__",":").StartsWith("spotify:user:") && Url.Contains("playlist:"))
                        {
                            WebClient X = new WebClient();
                            string f = "";// "No description anvailable";
                            try
                            {
                             f =    X.DownloadString(new Uri("http://spotiapps.krakelin.com/playlists/" + Url.Replace(":", "__") + ".description"));
                            }
                            catch
                            {
                            }
                            
                            Playlist Plst = Playlist.Create(SpotifySession,Link.Create(Url));
                           
                            SW.Write("{ \"Description\":\""+f+"\", \"PlayIndex\":"+PlayIndex+", \"currentURI\" : \"" + Url + "\", \"Playlist\":" + D.Serialize(Plst) + "}");
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

                            SW.Write("{ \"currentItem\": "+PlayIndex+", \"currentURI\" : \"" + Url + "\", \"contents\" : " + D.Serialize(_Artist) + ",\"Albums\":" + alb + "}");

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
            DX.Ready = true;
            

        }
       
        public void Listening(object Sender,EventArgs e)
        {
            System.Windows.Forms.Timer A = (System.Windows.Forms.Timer)Sender;
            BrowseClass Source = (BrowseClass)A.Tag;
            if (Source.Ready)
            {
                CreatePage(geckoWebBrowser1, Source.EventArgs);
                A.Stop();
            }
        }
        public void PlayMedia(string[] TrackList,int PlayIndex)
        {
        	
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
                    if (currentTrack != null)
                    {
                        Program.playHistory.Push(currentTrack);
                    }
                    currentTrack = Program.playQueue.Dequeue();
                    try
                    {

                        string Cover = currentTrack.Album.LinkString;
                        if (File.Exists(Program.GetAppString() + "\\covers\\" + Cover.Replace(":", "_") + ".jpg"))
                        {
                            try
                            {
                                this.CoverImage = System.Drawing.Bitmap.FromFile(Program.GetAppString() + "\\covers\\" + Cover.Replace(":", "_") + ".jpg");
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
                  
        }
        private class BrowseClass
        {
            
            public BrowseClass(Form1 Me,string URI, Skybound.Gecko.GeckoNavigatingEventArgs E,Skybound.Gecko.GeckoWebBrowser C)
            {
                EventArgs = E;
                this.URI = URI;
                Listener = new System.Windows.Forms.Timer();
                Listener.Tick+= new EventHandler(Me.Listening);
                this.C = C;
                this.Me = Me;
                Listener.Interval = 50;
                Listener.Start();
                Listener.Tag = this;
            }
            public Form1 Me { get; set; }
            public Skybound.Gecko.GeckoWebBrowser C { get; set; }
            public Skybound.Gecko.GeckoNavigatingEventArgs EventArgs { get; set; }
            public string URI { get; set; }
            public System.Windows.Forms.Timer Listener { get; set; }
            public bool Ready { get; set; }
        }
        public void Command(string cmd)
        {
            geckoWebBrowser1.Navigate("javascript:" + cmd+"");
        }
        private void geckoWebBrowser1_Navigating(object sender, Skybound.Gecko.GeckoNavigatingEventArgs e)
        {
         
            
            string Uri = e.Uri.ToString();
            if (Uri.EndsWith(".xul") || File.Exists("file://"+Program.GetAppString()+"\\views\\"+app+"\\cache\\main."+querystring+".xul"))
            {
                e.Cancel = false;
                return;
            }
            
            if (Uri.StartsWith("spotify:"))
            {
                ParseURI(Uri,false);
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
                   
                    SpotifySession.PlaylistContainer.AddPlaylist(_Link);
                    var item = listViewX1.Nodes["playlists"].Nodes.Add(X.Name);
                    item.Tag = (object)X.LinkString;
                    return;
                }
                if(URI.StartsWith("sp_drag-"))
                {
                geckoWebBrowser1.AllowDrop=true;
             
                	e.Cancel=true;
                	URI = URI.Replace("sp_drag-","");
                	if(tracksToAdd==null)
                	{
                		tracksToAdd=new List<Track>();
                	}
                	if(URI.Contains(';'))
                	{
                		string[] d = URI.Split(';');
                		tracksToAdd.Clear();
                		foreach(string str in d)
                		{
                			if(str!="")
                				tracksToAdd.Add(Track.CreateFromLink(Link.Create(str)));
                		}
                		
                	}
                	else
                	{
                		tracksToAdd.Clear();
                		tracksToAdd.Add(Track.CreateFromLink(Link.Create(URI)));
                	
                	}
                	this.listViewX1.DoDragDrop(tracksToAdd, DragDropEffects.Copy);
                	draggingSongs=true;
                	return;
                }
                if(URI.StartsWith("sp_enddrag"))
                {
                	dragging=false;
                	tracksToAdd.Clear();
                }
                if (URI.StartsWith("sp_view-"))
                {
                    URI = URI.Replace("sp_view-", "");
                    URI = URI.Replace("/", ":");
                    ParseURI(URI, false);
                    e.Cancel = true;
                    return;                                                     
                }
                if (URI.StartsWith("sp_play-"))
                {
               
                    string[] TrackList = URI.Replace("sp_play-", "").Split('+')[0].Split('.');
                    PlayMedia(TrackList, int.Parse(URI.Split('+')[1]));
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
                ParseURI(Uri.Replace("http://parse/", ""), false);
                return;
            }
           
            Thread D = new Thread(new System.Threading.ParameterizedThreadStart(GetSpotifyData));
            try
           {
                if ((currentUrl.Contains("spotify") || currentUrl.Contains("audify")) && !e.Uri.ToString().EndsWith(".xul"))
                {
                    geckoWebBrowser1.Navigate(Program.GetAppString() + "\\wait.xul");
                    D.Start(new BrowseClass(this, currentUrl, e, geckoWebBrowser1));
                    geckoWebBrowser1.Navigate(Program.GetAppString()+"\\wait.xul");
                    e.Cancel = true;
               
                    if(currentUrl.Contains("playlist__"))
                    {
                    	
                    	tracksPending = new Queue<Track>();
                    	listView2.Items.Clear();
                    	listView2.Show();
                    	splitContainer3.Panel1.Controls.Remove(geckoWebBrowser1);
                    	splitContainer3.Panel1.Controls.Remove(listView2);
                    	this.geckoWebBrowser1.Hide();
                    	this.listView2.Hide();
                    	
                    	
                    
                    
                    	
                    	splitContainer3.Panel1.Controls.Add(listView2);
                    	listView2.Dock = DockStyle.Fill;
                    splitContainer3.Panel1.Controls.Add(geckoWebBrowser1);
                    	geckoWebBrowser1.Height=180;
                    	geckoWebBrowser1.Dock = DockStyle.Top;
                    	
                    	
                    	
                    
                    	listView2.CanDrag=true;
                    	listView2.View = View.Details;
                    
						listView2.FullRowSelect=true;
						this.geckoWebBrowser1.Show();
						this.listView2.Show();
                    
                    	Thread StartPlaylists = new Thread(DownloadPlaylist);
                    	StartPlaylists.Start();
                    	
                    }
                    else
                    {
                    	listView2.Hide();
                    	geckoWebBrowser1.Dock = DockStyle.Fill;
                    	
                    }
                }
            }
            catch
            {
            }
         
        }
        void AssertPlaylistImage(Object d)
        {
        	Playlist A = (Playlist)d;
        	
        	this.AssembleImage(A,128);
        }
  		Queue<Track> tracksPending;
        public void LVDB(object sender,EventArgs e)
        {
        	if(listView2.SelectedItems.Count < 1)
        		return;
        	
        	List<String> tracklist = new List<string>();
        		
        	for( int i=listView2.SelectedItems[0].Index; i < listView2.Items.Count; i++)
        		
        	{
        		ListViewItem D = listView2.Items[i];
        		tracklist.Add(((Track)D.Tag).LinkString);
        		
        	}
        	PlayMedia(tracklist.ToArray(),listView2.SelectedItems[0].Index);
        }
        public void listViewX1AfterSelect(object Sender,EventArgs e)
        {
        	
        }
        public void DownloadPlaylist()
        {
        	
        	tracksPending = new Queue<Track>();
        	Playlist _Playlist = 	Playlist.Create(Program.SpotifySession,Link.Create(currentUrl.Replace("audify:","spotify:").Replace("__",":")));
            this.currentPlaylist=_Playlist;  
            Thread Df = new Thread(AssertPlaylistImage);
            Df.Start((Object)_Playlist);
            Thread.Sleep(1000);
            try{
        	foreach(Track _Track in _Playlist.CurrentTracks)
        	{
        		tracksPending.Enqueue(_Track);
        		
        	}
            }catch
            {
            	DownloadPlaylist();
            }
        }
        public void UpdatePlaylist()
        {
        	if(tracksPending!=null)
        	{
	        	while(tracksPending.Count > 0)
	        	{
	        		try{
	        			
	        	
	        		Track _Track = tracksPending.Dequeue();
	        	
	            	ListViewItem _Item = listView2.Items.Add(_Track.Name);
	        		_Item.Tag = (Object)_Track;
	        		_Item.SubItems.Add(_Track.Artists[0].Name);
	        		_Item.SubItems.Add(_Track.Album.Name);
	        			}
	        		catch{}
	        	}
        	}
        }
        void CreatePage(object Sender, Skybound.Gecko.GeckoNavigatingEventArgs e)
        {
            string Uri = e.Uri.ToString();
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
                currentPath = path;
                string path2 = path.Replace(".view", "." + querystring + ".xul");
                path2 = path2.Replace("main.", "cache/main.");
                if (File.Exists(path2))
                {
                    geckoWebBrowser1.Navigate("file:///" + path2);
                    e.Cancel = true;
                    return;
                }

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
                                if (SpotifySession != null)
                                {
                                    string songs = "";
                                    this.currentPlaylist = Playlist.Create(Program.SpotifySession, Link.Create("spotify:user:" + parameters.Replace("__", ":").Replace("audify", "spotify")));
                                    int i = 0;
                                    foreach (Spotify.Track track in this.currentPlaylist.CurrentTracks)
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


                    System.Windows.Forms.Timer N = new System.Windows.Forms.Timer();
                    N.Tick += new EventHandler(N_Tick);
                    N.Interval = 50;
                    N.Start();

                    e.Cancel = true;
                    geckoWebBrowser1.Navigate("file://" + Program.GetAppString() + "\\wait.xul");
                }
                else
                {
                    WebClient d = new WebClient();
                    d.DownloadDataCompleted += new DownloadDataCompletedEventHandler(ExternalDownloadCompleted);
                    d.DownloadStringAsync(new Uri(ERSProvider.Replace("%APP", app).Replace("%QUERY", querystring)));

                }
            }          
        }
		string ERSProvider = "http://krakelin.com/views/%APP?query=%QUERY";
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
			geckoWebBrowser1.Navigate("file:///"+Program.GetAppString()+"\\views\\"+app+"\\Cache\\main.xul");
			
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
		}
        void r_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            using (StreamWriter D = new StreamWriter(Program.GetAppString() + "\\views\\"+app+"\\cache\\" + e.UserState + ".xml"))
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
        void LoadPage(object d)
        {
            
        }
        bool isWriting = true;
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
                geckoWebBrowser1.Navigate("file://"+Program.GetAppString() + "\\views\\" + app + "\\cache\\main." + querystring + ".xul");
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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace("\\Documents","") + "\\cache\\main."+querystring+".xul";
            if (File.Exists(path))
            {
             //   File.Copy(path, Program.GetAppString() + "\\views\\" + app + "\\Cache\\main." + querystring + ".xul", true);
                geckoWebBrowser1.Navigate(Program.GetAppString() + "\\views\\" + app + "\\Cache\\main." + querystring + ".xul");
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



		string pData="";
        public void T_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (pData.Contains("error"))
            {
                Errors.Push("There was a error loading the page");
            }
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
        public float Maximum
        {
            get { return this.ucPosBar1.Maximum; }
            set { this.ucPosBar1.Maximum = value; }
        }
        public float Value
        {
            get
            {
                return this.ucPosBar1.Value;
            }
            set
            {
                this.ucPosBar1.Value = value;
              
            }
        }
        public class Sonique
        {
        	public string LinkString {get;set;}
        	public Image Picture {get;set;}
        	public Sonique(Image picture)
        	{
        		Picture=picture;
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
                    string data = Program.GetAppString() + " \\views\\" + currentUrl.Split(':')[1] + "\\cache\\main." + currentUrl.Split(':')[2] + ".xul";
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
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
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
        public Image GetImage(string LinkString)
        {
        	string fileName = Program.GetAppString()+"\\covers\\"+LinkString+".jpg";
        	try
        	{
	        	if(File.Exists(fileName))
	        	{
	        		Image D = Bitmap.FromFile(fileName);
	        		return D;
	        		              
	        	}
	        	else
	        	{
	        		Bitmap D = new Bitmap(1,1);
	        		return D;
	        	}
        	}
        	catch
        	{
        		return new Bitmap(1,1);
	        	
        	}
        }
        public void AssembleImage(Playlist playlist,int square)
        {
        	if(playlist==null)
        	{
        		return;	
        	}
        	if(!playlist.IsLoaded)
        		
        	{
        		return;
        	}
        	int nins=0;
        	int nans=0;
        	List<string> toCover = new List<string>();
        	List<Bitmap> bitmaps = new List<Bitmap>();
	        	Image X = new Bitmap(square,square);
	        	Graphics G = Graphics.FromImage(X);
	        	int quote = 0;
	        	foreach(Track d in playlist.CurrentTracks)
	        	{
	        		while(!d.IsLoaded)
	        		{
	        			
	        		}
	        	
	        		if(d.Album==null)
	        		{
	        			continue;
	        		}
	        		if(!toCover.Contains(d.Album.LinkString))
	        		{
	        			
	        			if(d.Album!=null)
	        			{
	        				
	        				try
	        				{
	        					Program.SpotifySession.LoadImage(d.Album.CoverId,d.Album.LinkString.Replace(":","__"));
		        				nans++;
		        			}
	        				catch(Exception e)
	        				{
	        					
	        					continue;
        				
	        				}
	        				toCover.Add(d.Album.LinkString.Replace(":","__"));
	        			}
	        			else
	        			{
	        		
	        				continue;
	        			}
	        		}	
        			if(nans == 1 || nans == 4 || nans == 9)
        			{
        				nins=nans;
        			}
        		
        			
	        		
	        	
    			
	        			
	        		
	        	}
	        		if(square> 0 && nins >0)
	        			quote  = (square / (nins))*3 ;
	        	if(nins == 1)
	        	{
	        	
	        			G.DrawImage(GetImage(toCover[0]),new Rectangle(0,0,square,square));
	        		
	        	}
	        	if(nins == 4)
	        	{
	        		if(bitmaps.Count > 0)
	        		{
	        			G.DrawImage(GetImage(toCover[0]),new Rectangle(0,0,quote,quote));
		        		G.DrawImage(GetImage(toCover[1]),new Rectangle(quote,0,quote,quote));
		        		G.DrawImage(GetImage(toCover[2]),new Rectangle(0,quote,quote,quote));
		        		G.DrawImage(GetImage(toCover[3]),new Rectangle(quote,quote,quote,quote));
	        		}
	        	}
	        	if(nins == 9)
	        	{
	        		
	        		G.DrawImage(GetImage(toCover[0]),new Rectangle(0,0,quote,quote));
	        		G.DrawImage(GetImage(toCover[1]),new Rectangle(quote,0,quote,quote));
	        		G.DrawImage(GetImage(toCover[2]),new Rectangle(quote*2,quote,quote,quote));
	        		G.DrawImage(GetImage(toCover[3]),new Rectangle(0,quote,quote,quote));
	        		G.DrawImage(GetImage(toCover[4]),new Rectangle(quote,quote,quote,quote));
	        		G.DrawImage(GetImage(toCover[5]),new Rectangle(quote*2,quote,quote,quote));
	        		G.DrawImage(GetImage(toCover[6]),new Rectangle(0,quote*2,quote,quote));
	        		G.DrawImage(GetImage(toCover[7]),new Rectangle(quote,quote*2,quote,quote));
	        		G.DrawImage(GetImage(toCover[8]),new Rectangle(quote*2,quote*2,quote,quote));
	        		
	        		
	        	}
	        	
	        	
	        	Sonique Ds = new Form1.Sonique(X);
	        	Ds.LinkString=playlist.LinkString;
	        	Ds.Picture=X;
	        	bmps.Push(Ds);
	       	
	        	
        	
        }
        Stack<Sonique> bmps ;
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
        public int Length { get; set; }
        public int Position { get; set; }
        int flashT = 5;
        void Nonsens()
        {
        	
            try
            {
                if (File.Exists(Program.GetAppString() + "\\incoming.uri"))
                {
                    string uri = "";
                    using(StreamReader SR = new StreamReader(Program.GetAppString() + "\\incoming.uri"))
                    {
                        uri = SR.ReadToEnd();
                        SR.Close();
                    }
                    ParseURI(uri, false);
                    File.Delete(Program.GetAppString() + "\\incoming.uri");
                    Program.SetForegroundWindow(this.Handle);
                }

            }
            catch { }
            if (Value > 0)
            {
               Command("SetCurrentTrack('" + PlayIndex + "')");
          
            }
            this.ucPosBar1.Refresh();
          
            while (true)
            {
                if (Errors.Count > 0)
                {
                    string Error = Errors.Pop();
                    if (Error.Contains("\n"))
                    {
                        MessageBox.Show(Error);
                    }
                    else
                    {

                        ShowError(Error);
                    }
                }
                else
                {
                    break;
                }
            }
        }
        void Timer2Tick(object sender, EventArgs e)
        {
        	UpdatePlaylist();
           
        }
        
        void CBtn5Click(object sender, EventArgs e)
        {
        	 if (currentUrl != null)
            {
                try
                {
                    string data = Program.GetAppString() + " \\views\\" + currentUrl.Split(':')[1] + "\\cache\\main." + currentUrl.Split(':')[2] + ".xul";
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
        
        void ListViewX1_AfterSelect(object sender, TreeViewEventArgs e)
        {
        	
         try
            {
         	String URI = (string)listViewX1.SelectedNode.Tag;
         	if(URI=="newplaylist")
         	{
         		listViewX1.LabelEdit=true;
         		listViewX1.SelectedNode.BeginEdit();
         		listViewX1.AfterLabelEdit+= new NodeLabelEditEventHandler(listViewX1_AfterLabelEdit);
         	}
                ParseURI(URI,false);
            }
         catch(Exception ex)
            {
            }
        }
        

        void listViewX1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
        	if(e.Node.Tag == (Object)"newplaylist")
        	{
	        	Playlist X = Program.SpotifySession.PlaylistContainer.AddNewPlaylist(e.Label);
	        	e.CancelEdit=true;
	        	TreeNode D = new TreeNode();
	        	D.Tag=(Object)X;
	        	listViewX1.Nodes["Playlists"].Nodes.Add(D);
	        	D.Name = e.Label;
        	}
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
        TreeNode currentNode;
        private void listViewX1_DragOver(object sender, DragEventArgs e)
        {
            TreeNode T = listViewX1.GetNodeAt(new Point(e.X, e.Y));
            
            string a = (string)T.Tag;
            if (a.StartsWith("spotify:user:") && a.Contains("playlist:"))
            {
                currentNode = T;
            }



            if (e.Data.GetData(System.Windows.Forms.DataFormats.Text) != null)
            {
                e.Effect = DragDropEffects.Link;
            }
        }

        private void listViewX1_DragDrop(object sender, DragEventArgs e)
        {
        	if(draggingSongs)
        	{
        		if(tracksToAdd==null)
        		{
        			return;
        		}
        		TreeNode PostNode = listViewX1.GetNodeAt(new Point(treeX,treeY));
        		if(((string)PostNode.Tag)=="newplaylist")
        		{
        			if(tracksToAdd.Count>1)
        			{
        				Playlist Dn = Program.SpotifySession.PlaylistContainer.AddNewPlaylist(tracksToAdd[0].Album.Name);
        				Dn.AddTracks(tracksToAdd.ToArray(),0);
        				TreeNode NewPlaylist = listViewX1.Nodes.Add(Dn.Name);
        				NewPlaylist.Tag=(object)Dn.LinkString;
        			}
        		}
        		if(((string)PostNode.Tag).Contains("playlist__") ||( (string)PostNode.Tag).Contains("playlist:"))
        		{
        			string playlist = (string)PostNode.Tag;
        			playlist = playlist.Replace("__",":");
        			Playlist X = Spotify.Playlist.Create(Program.SpotifySession,Link.Create(playlist));
        			
        			if(tracksToAdd!=null && X.IsLoaded&& (X.Owner == Program.SpotifySession.User || X.IsCollaborative))
        			{
        				
        				X.AddTracks(tracksToAdd.ToArray(),X.CurrentTracks.Length-1);
        			}
        				
        		}
        		draggingSongs=false;
        		tracksToAdd.Clear();
        	}
           /* if (currentNode != null)
            {
                try
                {
                    string d = (string)currentNode.Tag;
                    if (d.StartsWith("spotify:user") && d.Contains("playlist:"))
                    {
                        string _Track = (string)e.Data.GetData(System.Windows.Forms.DataFormats.StringFormat);
                        Playlist D = Playlist.Create(Program.SpotifySession, Link.Create(d));
                        if (_Track.StartsWith("spotify:track"))
                        {
                            D.AddTracks(new Track[] { Track.CreateFromLink(Link.Create((string)_Track)) }, 0);
                        }
                    }
                }
                catch { }
            }
            if (e.Data.GetData(System.Windows.Forms.DataFormats.Text) != null)
            {

                string x =(string) e.Data.GetData(System.Windows.Forms.DataFormats.StringFormat);
                if (x.Contains("-"))
                {
                    x = x.Split('-')[1];
                }
                if (x.StartsWith("spotify:track"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                TreeView D = (TreeView)sender;
                
             
               TreeNode P = D.Nodes["Custom"].Nodes.Add(((string)e.Data.GetData(DataFormats.StringFormat)));
                P.Tag=(object)e.Data.GetData(DataFormats.Html);
            }*/
        }

        private void geckoWebBrowser1_MouseDown_1(object sender, MouseEventArgs e)
        {

        }

        private void geckoWebBrowser1_DomMouseMove(object sender, Skybound.Gecko.GeckoDomMouseEventArgs e)
        {
            e.Handled = true;
        }

        private void geckoWebBrowser1_DomMouseOver(object sender, Skybound.Gecko.GeckoDomMouseEventArgs e)
        {

        }

        private void geckoWebBrowser1_MouseCaptureChanged(object sender, EventArgs e)
        { 
           
        }

        private void geckoWebBrowser1_DocumentCompleted(object sender, EventArgs e)
        {
            
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (flashT < 5)
            {
                if (flashT % 2 == 0)
                {
                    pane5.BackgroundImage = null;
                    pane5.BackColor = Color.Gray;
                }
                else
                {
                    pane5.BackgroundImage = Properties.Resources.top_bar_blue;
                    pane5.BackColor = SystemColors.Control;
                }
                flashT++;
            }
            else
            {
                pane5.BackgroundImage = Properties.Resources.top_bar_blue;
                pane5.BackColor = SystemColors.Control;
            }
        }

        private void pane6_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void ucPosBar1_Move(object sender, EventArgs e)
        {

        }

        private void ucPosBar1_MouseUp(object sender, MouseEventArgs e)
        {
            Program.SpotifySession.PlayerSeek((int)(ucPosBar1.Value*1000));
        }

        private void cBtn2_Load(object sender, EventArgs e)
        {
      
        }

        private void cBtn6_Load(object sender, EventArgs e)
        {

        }

        private void cBtn2_Click(object sender, EventArgs e)
        {
            Program.NextTrack();
        }

        private void cBtn6_Click(object sender, EventArgs e)
        {
            Program.PreviousTrack();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
      //      this.splitContainer1.Panel2.Height = pictureBox2.BackgroundImage.Width + this.pane7.Height;
        }

        private void pane5_MouseDown(object sender, MouseEventArgs e)
        {
            pane5.Hide();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            ParseURI(Program.currentView, false);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParseURI((string)contextMenuStrip1.Tag, false);
        }

        private void cBtn1_Click(object sender, EventArgs e)
        {
            if (Program.Paused)
            {

                Program.Paused = false;
            }
            else { Program.Paused = true; }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate("spotify:standard:");
        }

        private void geckoWebBrowser1_DomMouseUp(object sender, Skybound.Gecko.GeckoDomMouseEventArgs e)
        {
           
        }

        private void listViewX1_DragEnter(object sender, DragEventArgs e)
        {

        }
	PictureBox pBN;
        private void timer3_Tick(object sender, EventArgs e)
        {
        	/*foreach(Playlist PlsList in SpotifySession.PlaylistContainer.CurrentLists)
          	{
        		
        		Program.toBeAdded.Enqueue(PlsList);
        		
        		
           		
          	}*/
        	
        	while(Program.toBeAdded.Count>0)
        	{
	        	Playlist PlsList = Program.toBeAdded.Dequeue();
			if(PlsList.Name!="")
			{
				var d = listViewX1.Nodes["Playlists"].Nodes.Add(PlsList.Name);
	       		d.Text=PlsList.Name;
	       		d.Tag=(object)PlsList.LinkString;
			}
        	}
		while(bmps.Count > 0)
        	{
			pBN = new PictureBox();
        		Sonique QF = bmps.Pop();
        		Bitmap X  =new Bitmap(QF.Picture,128,128);
        	
        	
        		this.Controls.Add(pBN);
        	
        
        		X.MakeTransparent();
        		EncoderParameters encoderParameters = new EncoderParameters(1);
   			 encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
       
        		X.Save(Program.GetAppString()+"\\playlists\\"+QF.LinkString.Replace(":","__")+".jpg",ImageFormat.Jpeg);
        		
		/*.Save
			X.Save( Program.GetAppString()+"\\playlists\\"+QF.LinkString.Replace(":","__")+".bmp");
		*/
        	}
        }
    }
     public class Pane : System.Windows.Forms.Panel
    {
         public bool Dark { get; set; }
         public Color SecondColor { get; set; }
    }
}
