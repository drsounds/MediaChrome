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
        public class View
        {
            public View(String uri, string adress)
            {
                URI = uri;
                Adress = adress;
            }
            public String URI { get; set; }
            public String Adress { get; set; }
            public String Querystring {get;set;}
        }

        
     
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
        public String BaseURL = "http://localhost/mediachrome/Spotify Ultra/Spotify Ultra/Spotify Ultra Web/bin/debug/wamp/www/views/%app.php?param=%p&id=%id";

        public Dictionary<String, View> Views { get; set; }
        public View CurrentView {get;set;}
        public Dictionary<string, string> pages;
        public string ParseURI(string URI,bool browse)
        {
            
            return "ZERO";
        }
        public string userName;
        public string passWord;
   
       
        public Form1()
        {
            InitializeComponent();
            this.SuspendLayout();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.ResumeLayout();
        }
        Spotify.Session SpotifySession;
        public Form1(string userName,string Password)
        {
            
        }
       
        public Form1(string URI)
        {

        }
        string uri;
        bool dragging = false;
        Point MousePoint;
        Point MousePoint2;



        // Define the CS_DROPSHADOW constant
   

        
 		
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
        }
        public void NextSong()
        {
            nowPlayingView.NextSong();
        }
        
        /// <summary>
        /// The board to draw on
        /// </summary>
        Board.DrawBoard board;

        /// <summary>
        /// The selection listView
        /// </summary>
        Board.DrawBoard treeview;

        /// <summary>
        /// Splitter
        /// </summary>
        Panel splitter1;

        /// <summary>
        /// Will split the window when mouse move if true
        /// </summary>
        bool splitting1 = false;
        private void Form1_Load(object sender, EventArgs e)
        {

            splitter1 = new Panel();
            // Width of sidebar
            int treeViewWidth = 220;
            // Create treeview
            treeview =  new Board.DrawBoard();
            board = new Board.DrawBoard();
            board.Click += new EventHandler(board_Click);
            board.LinkClick += new Board.DrawBoard.LinkClicked(board_LinkClick);

            treeview.Dock = DockStyle.Left;
            splitter1.Dock = DockStyle.Left;
            splitter1.Cursor = Cursors.VSplit;
            this.MouseMove+=new MouseEventHandler(Form1_MouseMove);

            splitter1.Width = 2;
            splitter1.BackColor = Color.Black;
            board.Dock = DockStyle.Fill; 
            // add the treeview to the form
            
            treeview.Width = treeViewWidth;
            // Initialize drawboard
            

            this.panel3.Controls.Add(board);
            this.panel3.Controls.Add(splitter1);
            
            
            this.panel3.Controls.Add(treeview);
            this.treeview.Columns.Clear();
            // Add treview columns
            this.treeview.Columns.Add("Icon",10);
            this.treeview.Columns.Add("Title", 50);
           // Set splitter activator

            this.splitter1.MouseDown += new MouseEventHandler(splitter1_MouseDown);

            // Set splitter move events

            this.board.MouseMove += new MouseEventHandler(board_MouseMove);
            this.splitter1.MouseMove += new MouseEventHandler(splitter1_MouseMove);
            this.treeview.MouseMove += new MouseEventHandler(treeview_MouseMove);

            // Set release callbacks for the splitter

            this.splitter1.MouseUp += new MouseEventHandler(splitter1_MouseUp);
            this.board.MouseUp += new MouseEventHandler(board_MouseUp);
            this.treeview.MouseUp += new MouseEventHandler(treeview_MouseUp);

            // assign link click for menu
            treeview.LinkClick += new Board.DrawBoard.LinkClicked(treeview_LinkClick);

           
            
            // Navigate to start page
            board.Navigate("spotify:home:1", "spotify", "views");
            board.PlaybackRequested += new Board.DrawBoard.PlaybackStartEvent(board_PlaybackRequested);
            treeview.Navigate("spotify:menu:1", "spotify", "views");
           
        }

        void treeview_MouseUp(object sender, MouseEventArgs e)
        {
            splitting1 = false; 
        }

        void board_MouseUp(object sender, MouseEventArgs e)
        {
            splitting1 = false; 
        }

        void splitter1_MouseUp(object sender, MouseEventArgs e)
        {
            splitting1 = false;
        }

        void board_MouseMove(object sender, MouseEventArgs e)
        {
            if (splitting1)
                treeview.Width = e.X + treeview.Width;
        }

        void treeview_MouseMove(object sender, MouseEventArgs e)
        {
            if(splitting1)
            treeview.Width = e.X ;
        }

        void splitter1_MouseMove(object sender, MouseEventArgs e)
        {
            if (splitting1)
            {
                int left = e.X + splitter1.Left;
                treeview.Width = left;
            }
        }


        void splitter1_MouseDown(object sender, MouseEventArgs e)
        {
            splitting1 = true;
        }

        void board_LinkClick(object sender, string hRef)
        {
            if (hRef.StartsWith("spotify:"))
            {
                board.Navigate(hRef, "spotify", "views");
            }
        }

        void board_Click(object sender, EventArgs e)
        {
            
        }
       


        void treeview_LinkClick(object sender, string hRef)
        {
            board.Navigate(hRef, "spotify", "views");
        }

        bool board_PlaybackRequested(object sender, string Url)
        {
           
            Spotify.Track track = Track.CreateFromLink(Link.Create(Url.Replace("spotify:","spotify:")));
            
            // if track was correctly received, start playback
           
                if (track == null)
                    return true;
            // wait for the song to be loaded
            //    while (track.Error == sp_error.IS_LOADING) { }
                Program.currentTrack = track;
                Program.SpotifySession.PlayerLoad(track);
                sp_error ct = Program.SpotifySession.PlayerPlay(true);
            // Set the nowplaying view to the view provided
                Board.Spofity Sender = (Board.Spofity)sender;
                nowPlayingView = Sender;
            return true;
            
        }
        /// <summary>
        /// The view which holds the current playing song
        /// </summary>
        Board.Spofity nowPlayingView = null;
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

        /// <summary>
        /// Occurs when the mouse moves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // if splitting resize the controls
            if (splitting1)
            {
                treeview.Width = e.X;
            }

	    	treeX=e.X;
	  	    treeY=e.Y;
        }


        /// <summary>
        /// Occurs when the mouse button has pressed up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // End splitting
            splitting1 = false;
        	
        }
        
	List<Track> tracksToAdd;
        void Form1_DragDrop(object sender, DragEventArgs e)
        {
        	
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

       
        public Dictionary<string, Playlist> playlists;
        public string app = "";
        string currentItem = "";
        public string querystring
        {
            get
            {
                return this.CurrentView.Querystring;
            }
            set
            {
                this.CurrentView.Querystring = value;
            }
        }
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
        
        
        public List<View> Processing { get; set; }
        
        public void listViewX1AfterSelect(object Sender,EventArgs e)
        {
        	
        }
        Queue<Track> tracksPending;
        public void DownloadPlaylist()
        {
        	
        	tracksPending = new Queue<Track>();
        	Playlist _Playlist = 	Playlist.Create(Program.SpotifySession,Link.Create(currentUrl.Replace("spotify:","spotify:").Replace("__",":")));
            this.currentPlaylist=_Playlist;  
          //  Thread Df = new Thread(AssertPlaylistImage);
          //  Df.Start((Object)_Playlist);
          //  Thread.Sleep(1000);
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
	        	
	            	ListViewItem _Item = listViewX1.Items.Add(_Track.Name);
	        		_Item.Tag = (Object)_Track;
	        		_Item.SubItems.Add(_Track.Artists[0].Name);
	        		_Item.SubItems.Add(_Track.Album.Name);
	        			}
	        		catch{}
	        	}
        	}
        }
        public String currentUrl
        {
            get
            {
                return this.CurrentView.URI;
            }
            set
            {
                this.CurrentView.URI = value;
            }
        }
     
        void LoadPage(object d)
        {
            
        }
        bool isWriting = true;
       
        StreamReader F;
        
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
       

        void F_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
       
          
        
        }

        private void geckoWebBrowser1_LocationChanged(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBox1.Text.StartsWith("spotify:"))
            {
                ParseURI(textBox1.Text,false);
            }
            else
            {
                ParseURI("spotify:search:" + textBox1.Text,false);
            }
        }

        private void listViewX1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }
        private ListViewItem CheckPersistanceInTree(string Uri)
        {
            foreach(ListViewItem f in listViewX1.Items )
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
            // If the string starts with "spotify:" go to an specified adress, otherwise recall seearch intent
            if (!textBox1.Text.StartsWith("spotify:"))
            {
                board.Navigate("spotify:search:" + textBox1.Text, "spotify", "views");
            }
            else
            {
                board.Navigate(textBox1.Text, "spotify", "views");
            }

        }
        private ListViewItem AddListEntry(string ListData)
        {
            try
            {
                string[] adress = ListData.Split('|');
                ListViewItem F = CheckPersistanceInTree(adress[0]);
                if (F == null)
                {
                    string app = adress[0].Split(':')[1];
                    string[] pass = adress[1].Split('+');
                    var d = listViewX1.Items.Add(pass[1]);
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

       

        private void button1_Click(object sender, EventArgs e)
        {
            pane5.Hide();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GoBack();
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
            

            }
            catch
            {
            }
            CheckButtons();
        }
        private void AssignView()
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GoForward();
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
      
    
        
        void Form1Enter(object sender, EventArgs e)
        {

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
        	
            
        }
        void Timer2Tick(object sender, EventArgs e)
        {
        	UpdatePlaylist();
           
        }
        
        void CBtn5Click(object sender, EventArgs e)
        {
          
            
        }
        
        void CBtn3Click(object sender, EventArgs e)
        {
        	board.GoBack();
        }
        
        void CBtn4Click(object sender, EventArgs e)
        {
            board.GoForward();
        }
        
        void ListViewX1_AfterSelect(object sender, TreeViewEventArgs e)
        {
        	
         try
            {
         	String URI = (string)listViewX1.SelectedItems[0].Tag;
         	if(URI=="newplaylist")
         	{
         		listViewX1.LabelEdit=true;
         		listViewX1.SelectedItems[0].BeginEdit();
         		//listViewX1.AfterLabelEdit+= new NodeLabelEditEventHandler(listViewX1_AfterLabelEdit);
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
	        	ListViewItem D = new ListViewItem();
	        	D.Tag=(Object)X;
	        	listViewX1.Items["Playlists"].SubItems.Add(D.Text);
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
                ParseURI((string)listViewX1.SelectedItems[0].Tag,false);
            }
            catch
            {
            }
        }
        ListViewItem currentNode;
        private void listViewX1_DragOver(object sender, DragEventArgs e)
        {
            ListViewItem T = listViewX1.GetNodeAt(new Point(e.X, e.Y));
            
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
        		ListViewItem PostNode = listViewX1.GetNodeAt(new Point(treeX,treeY));
        		if(((string)PostNode.Tag)=="newplaylist")
        		{
        			if(tracksToAdd.Count>1)
        			{
        				Playlist Dn = Program.SpotifySession.PlaylistContainer.AddNewPlaylist(tracksToAdd[0].Album.Name);
        				Dn.AddTracks(tracksToAdd.ToArray(),0);
        				ListViewItem NewPlaylist = listViewX1.Items.Add(Dn.Name);
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
                
             
               ListViewItem P = D.Nodes["Custom"].Nodes.Add(((string)e.Data.GetData(DataFormats.StringFormat)));
                P.Tag=(object)e.Data.GetData(DataFormats.Html);
            }*/
        }

        private void geckoWebBrowser1_MouseDown_1(object sender, MouseEventArgs e)
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
        	
        
        }

        private void ucSearch1_KeyUp(object sender, KeyEventArgs e)
        {
               }

        private void button1_Click_2(object sender, EventArgs e)
        {
          
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
           }

        private void ucSearch1_Load(object sender, EventArgs e)
        {

        }

        private void CBtn6Click(object sender, EventArgs e)
        {
         
        }

        private void listViewX1_Browse(object sender, string uri)
        {
          }
       
       

        private void geckoWebBrowser1_Click_3(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Boolean indicating the splitter is at dragging
        /// </summary>
        bool Dragging = false;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
          
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.ResizeRedraw = false;
            
         
            
        }

        private void cBtn3_Load(object sender, EventArgs e)
        {

        }
    }
     public class Pane : System.Windows.Forms.Panel
    {
         public bool Dark { get; set; }
         public Color SecondColor { get; set; }
    }
}
