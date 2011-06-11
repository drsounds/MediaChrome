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
using MediaChrome.SocialNetworking;
using GlassForms;

using MediaChrome;
using MediaChrome.Views;
using Board;

namespace MediaChrome
{

     
    public partial class Form1 : Form
    {
        #region SongQuery

        Dictionary<String, MediaChrome.Views.Playlist> playlistBuffer;
        public Dictionary<String, MediaChrome.Views.Playlist> PlaylistBuffer
        {
            get
            {
                if (playlistBuffer == null)
                    playlistBuffer = new Dictionary<string, MediaChrome.Views.Playlist>();
                return playlistBuffer;
            }
            set
            {
                if (playlistBuffer == null)
                    playlistBuffer = new Dictionary<string, MediaChrome.Views.Playlist>();
                playlistBuffer = value;
            }
        }

        /// <summary>
        /// Indicating a song is under resolution
        /// </summary>
        bool resolvingSong = false;

        /// <summary>
        /// The social network used for the software. Currently NULL
        /// </summary>
        ISocialNetwork SocialNetwork { get; set; }

        /// <summary>
        /// Underfined.
        /// </summary>
        Dictionary<String,String> Querys { get; set; }
        /// <summary>
        /// Resolve an song from available services
        /// </summary>
        /// <param name="dr"></param>
        void _ResolveSong(Object dr)
        {
            resolvingSong = true;

            Song _Song = (Song)dr;

            if (Querys != null)
                if (Querys.ContainsKey("service"))
                {
                    if (Program.MediaEngines.ContainsKey(Querys["service"]))
                    {
                        IPlayEngine Engine = Program.MediaEngines[Querys["service"]];
                        CurrentPlayer = Engine;
                        watchSong = Engine.RawFind(_Song);
                        resolvingSong = false;
                        NotifyNewSong(_Song);
                        return;
                        /* */
                    }
                }

            /**
             * Elsewhere, iterate through installed music services
             * and check if there is a matching track to play the song on
             * */
            foreach (IPlayEngine Engine in Program.MediaEngines.Values)
            {

                Song f = Engine.RawFind(_Song);

                if (f != null)
                {
                    CurrentPlayer = Engine;
                    resolvingSong = false;
                    watchSong = f;
                    currentPlayer = Engine;
                    Engine.Load((f.Path));

                    Engine.Play();
                    NotifyNewSong(_Song);
                    return;
                }


                 

                



            }
            resolvingSong = false;
            ResolvingSongThread = null;

            ShowMessage("There was no service found for the song");
            board.NotifyError(_Song.Path);
            this.NextSong();
        }

        private void ShowMessage(string p)
        {
           // MessageBox.Show("None of your music service could play this song");
        //    pane5.Show();
          //  textBox1.Text = p;
            ErrorMessage = p;
        }

        /// <summary>
        /// Thread used for song resolution among service
        /// </summary>
        public Thread ResolvingSongThread { get; set; }
        /// <summary>
        /// Check if an song exist
        /// </summary>
        /// <param name="_Song"></param>
        /// <returns></returns>
        public bool Song_Exists(Song _Song)
        {




            if (Querys != null)
                if (Querys.ContainsKey("service"))
                {
                    if (Program.MediaEngines.ContainsKey(Querys["service"]))
                    {
                        IPlayEngine Engine = Program.MediaEngines[Querys["service"]];
                        currentPlayer = Engine;
                        watchSong = Engine.RawFind(_Song);
                        _Song.Checked = true;
                        return true;
                        /* */
                    }
                }

            /**
             * Elsewhere, iterate through installed music services
             * and check if there is a matching track to play the song on
             * */
            foreach (IPlayEngine Engine in Program.MediaEngines.Values)
            {

                Song f = Engine.RawFind(_Song);

                if (f != null)
                {
                    CurrentPlayer = Engine;
                    _Song.Checked = true;
                    //CurrentPlayer = f;
                    return true;
                }


                /*  currentTrack = f;

                  currentPlayer = Engine;
                  Engine.Load((currentTrack.Path));
                  currentTrack = _Song;
                  Engine.Play();
*/


            }
            return false;
        }

        /// <summary>
        /// Play and item and lookup the item from avaliable sources
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public bool PlayItem(String Query)
        {
            Query = Query.Replace("http://music/,", "music://");
            /*    if (ResolvingSongThread != null)
                    return false;*/
#if (nobug)
            /// <summary>
            /// Stop the current player to play the media, but first
            /// be sure that there is a current instance of a class witihn the IMediaEngine
            /// </summary>
            if (currentPlayer != null)
            {
                currentPlayer.Stop();
                foreach (Control d in Program.mainForm.Playboard.Controls)
                {
                    if (d.GetType() == currentPlayer.MediaControl.GetType())
                    {
                        d.Hide();
                    }
                }
            }
#endif

            // Get engine

            String engine = Query.Split(':')[0];
        
            /**
             * If the song was not assigned to an specific
             * service, eg. an abstract uri, try find an apporiate
             * service to find it on.
             * */
          

            IPlayEngine D = null;
            if (Query.StartsWith("music:") || Query.StartsWith("song:")||Query.StartsWith("http:"))
            {
                Song _Song = Song.GetSongFromURI(Query);
                /***
                 * First try with the current player
                 * */



                /***
                * If a default service is assigned, try the service
                * before attempting to find it on other service
                * */
                if (currentPlayer != null)
                {
                    Song r = (currentPlayer.RawFind(_Song));

                    if (r != null)
                    {
                        
                        currentPlayer.Load(r.Path);
                        currentPlayer.Play();
                        NotifyNewSong(r);

                        // Exit, we are ready now
                        return true;
                    }
                }

                /**
                 * If it were not an hit, try match the
                 * song with other services instead
                 * */
         
                
                if (_Song.ID != null && _Song.ID != "" && _Song.ProposedEngine != "" && _Song.ProposedEngine != null)
                {
                    try
                    {
                        IPlayEngine Df = Program.MediaEngines[_Song.ProposedEngine];
                        CurrentPlayer = Df;
                        CurrentPlayer.Load(Df.Namespace + ":" + _Song.ID);
                        NotifyNewSong(_Song);
                        // crrentTrack = _Song;
                        return true;
                    }
                    catch
                    {

                    }

                }

                /**
                 * And in the worst case, do an check with all installed service to 
                 * find something useful, if this won't go, tell the user
                 * */

                ResolvingSongThread = new Thread(_ResolveSong);
                ResolvingSongThread.Start((object)_Song);
               


            




                return true;
            }
            else
            {
               
                /**
                 * If the query points directly to an engine's 
                 * instance, try to handle it directly
                 **/


          
                /// <summary>
                /// Get the player from the list
                /// </summary>
                /// 
                if (Program.MediaEngines.ContainsKey(engine))
                {
                    D = Program.MediaEngines[engine];
                }

            }
            if (D != null)
            {

                D.MediaControl.Dock = DockStyle.Fill;
                D.MediaControl.Show();
                D.MediaControl.Enabled = true;
                /// <summary>
                /// Remove the engine specification of the Query URI
                /// </summary>
                String Path = Query;
                /// <summary>
                /// Send it to the media player
                /// </summary>
                D.Load((Path));
                /// <summary>
                /// Play the media
                /// </summary>
                D.Play();
                CurrentPlayer = D;
                NotifyNewSong(new Song() { Title=Path,Artist=Path  });


            }
            else
            {
                board.NotifyError(Query);
                ShowMessage("There is no media handler for the media");
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Returns if you are connected to the internet
        /// </summary>
        public bool ConnectedToInternet { get; set; }
        
        /// <summary>
        /// Notifies the user that an new song are playing
        /// </summary>
        /// <param name="song">The song to tell out</param>
        public void NotifyNewSong(Song song)
        {
            PlaybackNotifier R = new PlaybackNotifier(currentPlayer, song);
            R.Show();
        }
        private MediaChrome.IPlayEngine currentPlayer;

        public MediaChrome.IPlayEngine DefaultPlayer
        {
            get
            {
                return Program.MediaEngines[MediaChrome.Settings.Default.DefaultPlayer];
            }
            set
            {
                try
                {
                    MediaChrome.Settings.Default.DefaultPlayer = value.Namespace;
                    Properties.Settings.Default.Save();

                    if (DefaultPlayer.Image != null)
                    {
                        this.pictureBox1.BackgroundImage = DefaultPlayer.Icon;

                    }
                    // Get current section
                    Board.Section section = this.treeview.CurrentView.Content.View.Sections[0];

                    this.treeview.Navigate("spotify:menu:1", "spotify", "views");
                    section.ptop = 120;

                    try
                    {
                        // Append playlists to the section view
                        foreach (MediaChrome.Views.Playlist d in DefaultPlayer.Playlists)
                        {
                            Board.Element elm = new Board.Element(section, treeview);
                            elm.SetAttribute("type", "entry");

                            elm.SetAttribute("height", "16");
                            elm.SetAttribute("top", "@TOP");
                            elm.SetAttribute("title", d.Title);
                            elm.SetAttribute("shref", d.ID);
                            elm.Tag = (object)d;

                            section.AddElement(elm, section.Parent);

                            /**
                             * Add the playlist to the public cache
                             * */
                            if (!PlaylistBuffer.ContainsKey(d.ID))
                            {

                                PlaylistBuffer.Add(d.ID, d);
                            }
                        }
                    }
                    catch { }
                }
                catch { }
            }
        }

        /// <summary>
        /// The current engine which plays an song.
        /// </summary>
        public MediaChrome.IPlayEngine CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                currentPlayer = value;
               
            }
        }
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
            
            playlistsToAdd = new Stack<Board.Element>();
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

       
        /// <summary>
        /// Play next song
        /// </summary>
        public void NextSong()
        {
            board.NextSong();
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
        /// Used by scripts to get an playlist
        /// </summary>
        /// <param name="source"></param>
        /// <returns>an playlist object, FALSE if failed</returns>
        public object __getPlaylist(string source,string id)
        {
            try
            {
                /**
                 * If the playlist is already in the buffer, load it
                 * */
                String Key = String.Format("{0}:playlist:{1}",source,id); // The playlist ID
           

                // Get engine for playlist
              
                MediaChrome.IPlayEngine engine = Program.MediaEngines[source];

                return engine.ViewPlaylist(id, id);
                
            }
            catch
            {
                return false;
            }
        }
        public object __import_music()
        {
            MediaChrome.ImportLibrary frmImport = new MediaChrome.ImportLibrary();
            frmImport.ShowDialog();
            return null;
        }
        /// <summary>
        /// Method to start an external web page in an external browser outside SpotifyUltra. Used by scripts
        /// </summary>
        /// <param name="uri">The url</param>
        /// <returns></returns>
        public object __extern(string uri)
        {
            Process.Start(uri);
            return null;
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }
        public Stack<Board.Element> playlistsToAdd;
        private const int WM_SETREDRAW = 0x000B;


          private const int WM_INVALIDATE = 0x0402 + 100;

      
        /// <summary>
        /// Lock window from update
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern long LockWindowUpdate(IntPtr hWnd);


        protected override void NotifyInvalidate(Rectangle invalidatedArea)
        {
           // invalidatedArea = new Rectangle(0, 0, 0, 0);
        }
        protected override void OnInvalidated(InvalidateEventArgs e)
        {

        }

        private Board.Skin skin;
        public Board.Skin Skin
        {
            get
            {
                return skin;
            }
            set
            {
                skin = value;
            
                // Set skin attributes
                Panel header = panel2;
                Panel footer = panel4;
                Panel panelt = pane5;
                skin.SkinControl((Control)header, "Header");
                skin.SkinControl((Control)footer, "Footer");
                skin.SkinControl((Control)panelt, "ErrorBar");
                board.Skin = skin;
                treeview.Skin = skin;
            
            }
        }


        /// <summary>
        /// Query for radio
        /// </summary>
        /// <param name="engine">engine to us</param>
        /// <param name="query">query for radio</param>
        /// <returns></returns>
        public object __radioQuery(string engine, string query)
        {
            if (currentPlayer != null)
            {
                return this.currentPlayer.QueryRadio(query);
            }
            else
            {
                return new List<Song>();
            }
        }


        /// <summary>
        /// Will split the window when mouse move if true
        /// </summary>
        /// 
      
        bool splitting1 = false;
        bool splitting2 = false;
       
        private void Form1_Load(object sender, EventArgs e)
        {

            
            dragSongs = new List<Song>();
            Lock();
            this.Invalidate();
            // Initalize scrollbars
            Board.Scrollbar scrollBar1 = new Board.Scrollbar();
            Board.Scrollbar scrollBar2 = new Board.Scrollbar();
        
            scrollBar2.Dock = DockStyle.Right;
            scrollBar1.Dock = DockStyle.Left;
            splitter1 = new Panel();
     /*       splitter2 = new Panel();
            splitter2.Width = 2;
            splitter2.BackColor = Color.Black;
            splitter2.MouseMove += new MouseEventHandler(splitter2_MouseMove);
            splitter2.MouseDown += new MouseEventHandler(splitter2_MouseDown);*/
            // Width of sidebar
            int treeViewWidth = 220;
            // Create treeview
            treeview =  new Board.DrawBoard();
            board = new Board.DrawBoard();
           
            // add right scrollbar
            this.panel3.Controls.Add(scrollBar2);
            this.panel3.Controls.Add(board);
            scrollBar2.Host = board;
            this.board.ScrollBarY = scrollBar2;
   //         playlistView = new Board.DrawBoard();
            
     //       playlistView.Dock = DockStyle.Right;
      //      this.panel3.Controls.Add(splitter2);
        //    splitter2.Dock = DockStyle.Right;
          //  this.panel3.Controls.Add(playlistView);
       //     playlistView.Width = 500;
         //   playlistView.Navigate("mediachrome:playlists:d", "mediachrome", "views");
          //  playlistView.ItemClicked += new Board.DrawBoard.ItemClick(playlistView_ItemClicked);
           // playlistView.MouseMove += new MouseEventHandler(playlistView_MouseMove);
           // playlistView.DropElement += new Board.DrawBoard.ElementDragEventHandler(playlistView_DropElement);
            
         board.Click += new EventHandler(board_Click);
            board.LinkClick += new Board.DrawBoard.LinkClicked(board_LinkClick);
            board.BeginNavigating += new Board.DrawBoard.NavigateEventHandler(board_BeforeNavigating);
            treeview.Dock = DockStyle.Left;
            treeview.DragDrop += new DragEventHandler(treeview_DragDrop);
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

            this.panel3.Controls.Add(scrollBar1);
            scrollBar1.Host = treeview;
            treeview.ScrollBarY = scrollBar1;
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
            this.board.Navigating += new Board.DrawBoard.NavigateEventHandler(board_Navigating);
            // assign makogeneration initialization code
            this.board.MakoGeneration += new Board.DrawBoard.MakoCreateEventHandler(board_MakoGeneration);
            this.treeview.DragOverElement+=new Board.DrawBoard.ElementDragEventHandler(playlistView_DragOverElement);
           // this.treeview.DropElement+=new Board.DrawBoard.ElementDragEventHandler(playlistView_DropElement);
            treeview.AllowDrop = true;
     //       playlistView.DragOverElement += new Board.DrawBoard.ElementDragEventHandler(playlistView_DragOverElement);

            // Set image download event handler
            board.BeginDownloadImage += new Board.DrawBoard.ImageDownloadEventHandler(board_BeginDownloadImage);
            // Navigate to start page
            board.Navigate("spotify:home:1", "spotify", "views");
            board.PlaybackRequested += new Board.DrawBoard.PlaybackStartEvent(board_PlaybackRequested);
            treeview.Navigate("spotify:menu:1", "spotify", "views");
            treeview.DragOverElement += new Board.DrawBoard.ElementDragEventHandler(treeview_DragOverElement);
            treeview.DropElement += new Board.DrawBoard.ElementDragEventHandler(treeview_DropElement);
             /**
              * Add items on treeview
              * 
              */

            // Add standard list items
           
           
           /***
            * Assign the Spotify skin to this application
            * */
            this.Skin = new Board.Skin(String.Format("skins\\{0}\\{0}.xml", Settings.Default.Skin));
            board.InvalidView += new Board.DrawBoard.ViewErrorEventHandler(board_InvalidView);
           /**
            * Set event handlers for added elements
            * */
            this.board.ElementAdded += new Board.DrawBoard.ElementParseEvent(board_ElementAdded);
            this.board.ElementParsing += new DrawBoard.ElementParseEvent(board_ElementParsing);
            DefaultPlayer = Program.MediaEngines[MediaChrome.Settings.Default.DefaultPlayer];

        }

        void board_ElementParsing(object Sender, DrawBoard.ElementParseEventArgs e)
        {
            board_ElementAdded(Sender, e);
        }

        /// <summary>
        /// Occurs when rendering elements
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        void board_ElementAdded(object Sender, Board.DrawBoard.ElementParseEventArgs e)
        {
            // if element is an entry, assign an song item to it
            if (e.Element.Type == "entry")
            {
                /***
                 * Append an song element with element's attributes
                 * so it easier can be handled with playlisst management
                 * */
                Song d = new Song();
                d.Title = e.Element.GetAttribute("title");
                d.Artist = e.Element.GetAttribute("artist");
                d.AlbumName = e.Element.GetAttribute("album");
                d.Path = e.Element.GetAttribute("uri");
                e.Element.Tag = d;
            }
        }
        private string errorMessage;
        public String ErrorMessage
        {
            get;
            set;
        }
        void board_InvalidView(object sender, Board.DrawBoard.ViewErrorArgs e)
        {
            ErrorMessage = e.Message;
        }

        void playlistView_DragOverElement(object sender, Board.DrawBoard.ElementDragEventArgs e)
        {
            e.AllowedEffects = DragDropEffects.Copy;
        }

        /// <summary>
        /// Helper class for adding an song
        /// </summary>
        public class AddSong
        {
            /// <summary>
            /// Lists of Uris
            /// </summary>
            public string Uris { get; set; }
            /// <summary>
            /// Playlist
            /// </summary>
            public Playlist Playlist { get; set; }
        }
        /// <summary>
        /// Add songs to playlist by thread. If the Playlist object
        /// from AddSong object is NULL, it will create an new playlist for add on.
        /// </summary>
        /// <param name="param"></param>
        public void AddSongs(object param)
        {
            if (CurrentPlayer == null)
            {
                ShowMessage("An media engine must be active in order to create playlist");
                return;
            }
            AddSong p = (AddSong)param;
            String Uris = p.Uris;
            Playlist plsss = p.Playlist;

            // If plsss is null, create new playlists instead
            if (plsss == null)
            {
                /*
                // Ask for the name of the playlist
                AddPlaylist ap = new AddPlaylist();
                if (ap.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(ap.PlaylistName))
                {

                    plsss = CurrentPlayer.CreatePlaylist(ap.PlaylistName);
                }
                else
                {
                    return;
                }*/
                plsss = CurrentPlayer.CreatePlaylist(String.Format("New Playlist {0}{1}{2}{3}{4}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));

            }
            
            if (Uris.Contains("\n"))
            {
                String[] uris = Uris.Split('\n');
                foreach (String uri in uris)
                {
                    if (String.IsNullOrEmpty(uri))
                        continue;
                    plsss.Add(Song.GetSongFromURI(uri, Program.MediaEngines.Values.ToList()), plsss.Songs.Count - 1);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(Uris))
                    return;
                plsss.Add(Song.GetSongFromURI(Uris, Program.MediaEngines.Values.ToList()), plsss.Songs.Count - 1);
            }
        }
        /// <summary>
        /// Internal song for drag
        /// </summary>
        private List<Song> dragSongs;
        void playlistView_DropElement(object sender, Board.DrawBoard.ElementDragEventArgs e)
        {
            try
            {
                // If the type of element is an playlist, add the song
                if (e.Destination.Tag.GetType() == typeof(Playlist))
                {
                    Playlist plsss = (Playlist)e.Destination.Tag;

                    /**
                     * Copy the song to the playlist from the dragSong buffer
                     * */
                    if (dragSongs != null)
                    {
                        foreach (Element r in board.DraggingElements)
                        {
                                Song d = (Song)r.Tag;
                                plsss.Add(d, 0);
                            
                        }

                    }
#if(old)
                    /**
                     * Convert the URI list to songs
                     * */
                    String Uris = (string)e.DragArgs.Data.GetData(DataFormats.StringFormat);

                    /**
                     * Add it to an playlist on an separate thread
                     * */
                    AddSong ads = new AddSong() { Playlist = plsss, Uris = Uris };
                    Thread d = new Thread(AddSongs);
                    d.Start(ads);
#endif


                }
            }
            catch
            {
            }
        }

        void playlistView_MouseMove(object sender, MouseEventArgs e)
        {
          
        }

        void splitter2_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        void splitter2_MouseDown(object sender, MouseEventArgs e)
        {
            splitting2 = true;
        }

        void splitter1_mousedown(object sender, MouseEventArgs e)
        {
         
        }

        void playlistView_ItemClicked(object sender, string uri)
        {
            board.Navigate(uri, uri.Split(':')[0], "views");
        }

        private void Lock()
        {
//            throw new NotImplementedException();
        }
        /// <summary>
        /// The current namespace for browsing (the engine to use for the particular view)
        /// </summary>
        public string CurrentNamespace { get; set; }
        /// <summary>
        /// Converts spotify track objects to MediaChrome song objects
        /// </summary>
        /// <param name="uri">the uri to the song</param>
        /// <returns>A mediachrome song</returns>
       
        /// <summary>
        /// Browse to an view, relative to the underlying IPlayEngine
        /// </summary>
        /// <param name="uri"></param>
        public void Browse(string uri)
        {


            if (!uri.Contains(":"))
            {
                this.board.Navigate(String.Format("spotify:search:{0}", uri), "spotify", "views");
            }
            if (uri.StartsWith("mc:") || uri.StartsWith("mediachrome:"))
            {
                uri = uri.Replace("mc:", "").Replace("mediachrome:", "");

            }
            String Engine = uri.Split(':')[0];
#if(obsolute)
           

            // Remove engine from uri
            uri = uri.Replace(Engine + ":", "");
            /**
             * If uri starts with playlist: open an universal playlist
             * */
            if (uri.StartsWith("playlist:"))
            {

                /***
                 * Get a playlist by parsing the uri; eg.
                 * 
                 * playlist:<name>:[<entry(artist:album:title)>]
                 * */
                uri = uri.Replace("playlist:", "");

                String name = uri.Split(':')[0];

                // Remove the name of the playlist
                uri = uri.Replace(name + ":", "");

                // Create the playlist
                MediaChrome.Views.Playlist playlist = Program.MediaEngines["mp3"].CreatePlaylist(name);
                playlist.ID = "mp3:playlist:" + name;


                String[] tokens = uri.Split(':');
                var pos =0 ;
                for (var i = 0; i < tokens.Length; i += 3)
                {
                    Song d = new Song();

                    String query = String.Format("song://{0}/{1}/{2}", tokens[i], tokens[i+1], tokens[i+2]);

                    d = Song.GetSongFromURI(query);
                    playlist.Add(d,pos);
                    pos++;
                }
                this.__navigate(playlist.ID);
                return;
            }
#endif
            /**
             * Find an media engine matching the parameter
             * */
            bool foundEngine = false;
            foreach (MediaChrome.IPlayEngine engine in Program.MediaEngines.Values)
            {
                if (Engine == engine.Namespace)
                {
                    foundEngine = true;
                    break;
                }
            }
            /**
             * If no matching engine was found,
             * start an search on the active engine
             * */
            if (!foundEngine)
            {
                if (this.CurrentPlayer != null)
                {
                    this.board.Navigate(this.currentPlayer.Namespace + ":search:" + uri, this.CurrentPlayer.Namespace, "views");
                }
                return;
            }

            // Otherwise do an special query
          
            
            // If engine is http browse a webpage
            if (Engine == "http")
            {
                Process.Start(uri);
                return;
            }
            this.CurrentNamespace=Engine;
            this.board.Navigate(uri, Engine, "views");
        }
        void treeview_DropElement(object sender, Board.DrawBoard.ElementDragEventArgs e)
        {
      
             string data = (string)e.DragArgs.Data.GetData(DataFormats.StringFormat);
            /**
             * Check if the element dropping into is representing an playlist, if so add the track
             * to the playlist
             * */
            // if internal drag song buffer is activated, move the songs by it instead
             if (board.DraggingElements != null)
             {
                 foreach (Element _elm in board.DraggingElements)
                 {
                     try
                     {
                         Song d = (Song)_elm.Tag;
                         MediaChrome.Views.Playlist Playlist = (MediaChrome.Views.Playlist)e.Destination.Tag;
                         Playlist.Add(d, 0);
                     }
                     catch
                     {
                     }
                 }
                 dragSongs.Clear();
                 return;
             }

           if(data!=null&&e.Destination!=null)
            {
               // Parse the list of uris
                if (data.Contains("\n"))
                {
                    String[] links = data.Split('\n');
                    foreach (String track in links)
                    {
                        if(!String.IsNullOrEmpty(track))
                     //    if (track.StartsWith("spotify:track:"))
                        {
                          // Get song of the element
                                

                                // Get playlist attachment
                                MediaChrome.Views.Playlist Playlist = (MediaChrome.Views.Playlist)e.Destination.Tag;
                                // Get mediachrome song of the track
                                MediaChrome.Song song = Playlist.Engine.ConvertSongFromLink(track);
                                // Add the song to the playlist
                                Playlist.Add(song, e.Index);
                            
                        }
                    }
                }
                else
                {
                    
                
                            // Get playlist attachment
                            MediaChrome.Views.Playlist Playlist = (MediaChrome.Views.Playlist)e.Destination.Tag;
                            // Get mediachrome song of the track
                            MediaChrome.Song song = Playlist.Engine.ConvertSongFromLink(data);
                            // Add the song to the playlist
                            Playlist.Add(song, e.Index);
                       
                    
                }
                
            }
        }

        void treeview_DragOverElement(object sender, Board.DrawBoard.ElementDragEventArgs e)
        {
            e.AllowedEffects = DragDropEffects.Copy;

            // Handle song drop
             string data = (string)e.DragArgs.Data.GetData(DataFormats.StringFormat);
           if(data!=null)
            {
                if (data.StartsWith("spotify:"))
                {

                }
            }
        }

        void treeview_DragDrop(object sender, DragEventArgs e)
        {
           

        }

        /// <summary>
        /// Adds divider to the listview
        /// </summary>
        private void AddDivider()
        {
             Board.Element Splitter = this.treeview.AddItem("","",new String[]{},new String[]{},-1,10);
            Splitter.Type = "divider";
            Splitter.SetAttribute("type", "divider");
            Splitter.Height = 10;
            
        }
        private static object Mutex = new object();
       
        /// <date>2011-04-25 14:59</date>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void board_BeginDownloadImage(object sender, Board.DrawBoard.ImageDownloadEventArgs e)
        {
           
            
            

        }

        bool board_Navigating(object sender, string uri)
        {
            
        
            // If the string starts with "spotify:" go to an specified adress, otherwise recall seearch intent
            if (uri.Split(':').Length < 2 )
            {

                board.Navigate("spotify:search:" + uri, "spotify", "views");
                return false;
            }
            else
            {

                return true;
            }
        }

        /// <summary>
        /// This function checks all media engines for music
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<MediaChrome.Song> FindMusic(string query)
        {
            // Create new list for all songs
            List<MediaChrome.Song> searchResult = new List<MediaChrome.Song>();

            /**
             * If the current player has not been set, search on all service
             * */
            if (CurrentPlayer == null)
            {
                // iterate through all sources
                foreach (MediaChrome.IPlayEngine Engine in Program.MediaEngines.Values)
                {
                    List<MediaChrome.Song> songs = Engine.Find(query);
                    searchResult.AddRange(songs);

                }
            }
            else
            {
                
                 List<MediaChrome.Song> songs = CurrentPlayer.Find(query);
                    searchResult.AddRange(songs);
            }
            return searchResult;
        }

        /// <summary>
        /// This function checks all media engines for music
        /// </summary>
        /// <param name="service">The service to search on</param>
        /// <param name="query">The search query</param>
        /// <returns></returns>
        public List<MediaChrome.Song> FindMusic(string service,string query)
        {
            // Create new list for all songs
            List<MediaChrome.Song> searchResult = new List<MediaChrome.Song>();

            /**
             * If the current player has not been set, search on all service
             * */
            
                // Search if the service is available
            if (Program.MediaEngines.ContainsKey(service))
            {
                MediaChrome.IPlayEngine Engine = Program.MediaEngines[service];

                List<MediaChrome.Song> songs = Engine.Find(query);
                searchResult.AddRange(songs);


            }
            
            
            return searchResult;
        }

        #region Mako Wrapper functions
        /// <summary>
        /// Gets your friends on the network
        /// </summary>
        /// <returns>An List of User objects resembling your friends, FALSE if failed</returns>
        public object __getFriends()
        {
            try
            {
                return SocialNetwork.Friends;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an object for your social feed usable in the client scripts.
        /// </summary>
        /// <returns>FALSE if failed, an SocialFeed object if success</returns>
        public object __getFeed()
        {
            try
            {
                return SocialNetwork.LastUpdates;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets an object for your social feed usable in the client scripts.
        /// </summary>
        /// <param name="query">Search query to use</param>
        /// <returns>FALSE if failed, an SocialFeed object if success</returns>
        public object __getFeed(string query)
        {
            try
            {
                return SocialNetwork.GetLastestUpdateFromQuery(query);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Return an feed for the particular user.
        /// </summary>
        /// <returns>an SocialFeed instance with the user's feed data or FALSE if fails</returns>
        public object __getUserFeed(string userName)
        {
            try
            {
                return SocialNetwork.GetFeedFromUser(userName);
            }
            catch
            {
                return false;
            }
     
        }
        /// <summary>
        /// Return an feed for the particular user.
        /// </summary>
        /// <param name="query">The query to match</param>
        /// <param name="userName">The user to match</param>
        /// <returns>an SocialFeed instance with the user's feed data or FALSE if fails</returns>
        public object __getUserFeed(string userName,string query)
        {
            try
            {
                return SocialNetwork.GetFeedFromUser(userName);
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// Returns  an artist profile from the script
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>An JS object representing the artist</returns>
        public object __getArtist(string engine,string ID)
        {
     
            return Program.MediaEngines[engine].GetArtist(ID);

        }
        /// <summary>
        /// Gets the album from the specified URI, from script
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>An JS object representing the album</returns>
        public object __getAlbum(string engine,string ID)
        {
            
            return Program.MediaEngines[engine].GetAlbum(ID);
        }
        /// <summary>
        /// Gets the album from the specified URI, from script
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>An JS object representing the album</returns>
        public object __getAlbum(string ID) 
        {
            // Get the engine used
            String engine = ID.Split(':')[0];
            return Program.MediaEngines[engine].GetAlbum(ID);
        }
        /// <summary>
        /// Script wrapper for the function
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public object __findMusic( string query)
        {
            return FindMusic(query);
        }

        /// <summary>
        /// Checks against the media sources if the link has an media
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public object __isValidMedia(string link)
        {
            // if the link is empty return
            if (String.IsNullOrEmpty(link))
                return false;
            foreach (IPlayEngine engine in Program.MediaEngines.Values)
            {
                if (link.StartsWith(engine.AudioSignature))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Script wrapper for the function
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public object __findMusic(string service, string query)
        {
            /**
             * IF service is defined with * 
             * find on all service
             * */
            if (service == "*")
            {
                FindMusic(query);
            }
            return FindMusic(service,query);
        }

        /// <summary>
        /// Used by javascript to navigate through views
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public object __navigate(string url)
        {
            this.Browse(url);
               // board.Navigate(url, "spotify", "views");
            // TODO: Add more handlers
            return null;
        }

        /// <summary>
        /// Set an active section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public object __setActiveSection(string section)
        {
            this.board.currentSection = int.Parse(section);
            return null;
        }


        /// <summary>
        /// Method to initialize all features available for subscripts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void board_MakoGeneration(object sender, EventArgs e)
        {
              Board.MakoEngine d = (Board.MakoEngine)sender;
              d.RuntimeMachine.SetFunction("queryLocalFiles",new Func<string, object>(__GetLocalFiles));
              d.RuntimeMachine.SetFunction("getAlbum", new Func<string, object>(__getAlbum));
              d.RuntimeMachine.SetFunction("getAlbum", new Func<string,string, object>(__getAlbum));
              d.RuntimeMachine.SetFunction("getArtist", new Func<string, string, object>(__getArtist));
              d.RuntimeMachine.SetFunction("findMusic", new Func<string,string, object>(__findMusic));
              d.RuntimeMachine.SetFunction("getFeed", new Func<object>(__getFeed));
              d.RuntimeMachine.SetFunction("getUserFeed", new Func<string,object>(__getUserFeed));
              d.RuntimeMachine.SetFunction("isValidMedia", new Func<string, object>(__isValidMedia));
              d.RuntimeMachine.SetFunction("getFeed", new Func< string, object>(__getFeed));
              d.RuntimeMachine.SetFunction("getUserFeed", new Func<string, string, object>(__getUserFeed));
              d.RuntimeMachine.SetFunction("findMusic", new Func<string, object>(__findMusic));
              d.RuntimeMachine.SetFunction("getPlaylist", new Func<string,string, object>(__getPlaylist));
              d.RuntimeMachine.SetFunction("importMusic", new Func<object>(__import_music));
              d.RuntimeMachine.SetFunction("navigate", new Func<string,object>(__navigate));
              d.RuntimeMachine.SetFunction("radioQuery", new Func<string, string, object>(__radioQuery));
              d.RuntimeMachine.SetFunction("getCurrentPlaylist", new Func< object>(__getCurrentPlaylist));
              d.RuntimeMachine.SetFunction("ownPlaylist", new Func<object>(__ownPlaylist));
              d.RuntimeMachine.SetFunction("extern", new Func<string, object>(__extern));
              d.RuntimeMachine.SetFunction("section", new Func<string, object>(__setActiveSection));

        }   

        /// <summary>
        ///  Used by the script to get local files
        /// </summary>
        /// <param name="query">the query for get local files</param>
        /// <returns></returns>

        public object __GetLocalFiles(string query)
        {
            LocalLibrary A = new LocalLibrary();
            return A.GetFilesFromQuery(query);
        }

        public object __ownPlaylist()
        {
            if (CurrentPlaylist != null)
                return true;
            return false; 
        }
        /// <summary>
        /// Used by scripts to get current playlist
        /// </summary>
        /// <returns></returns>
        public object __getCurrentPlaylist()
        {

            return CurrentPlaylist;
        }
        #endregion

        /// <summary>
        /// Current spotify playlist
        /// </summary>
        private MediaChrome.Views.Playlist CurrentPlaylist { get; set; }
        /// <summary>
        /// Returns the current Spotify Session
        /// </summary>
      
        bool board_BeforeNavigating(object sender, string uri)
        {
           // If the uri starts with spotify:user:xx:playlist: load the playlist
            if (uri.StartsWith("spotify:user:") && uri.Contains("playlist:"))
            {
                MediaChrome.Views.Playlist plst = Program.MediaEngines["spotify"].ViewPlaylist("Name", uri);
                CurrentPlaylist = plst;

                

            }
            return true;
        }

        void treeview_MouseUp(object sender, MouseEventArgs e)
        {
            splitting1 = false;
            splitting2 = false;
        }

        void board_MouseUp(object sender, MouseEventArgs e)
        {
            splitting1 = false;
            splitting2 = false;
        }

        void splitter1_MouseUp(object sender, MouseEventArgs e)
        {
            splitting1 = false;
            splitting2 = false;
        }

        void board_MouseMove(object sender, MouseEventArgs e)
        {
            if (splitting1)
            {
                treeview.Width = e.X + treeview.Width;
            
              
            } if (splitting2)
            {
                board.Width = e.X - board.ClientRectangle.Left;
            }
        }

        void treeview_MouseMove(object sender, MouseEventArgs e)
        {
            if (splitting1)
            {
                treeview.Width = e.X;
                board.Dock = DockStyle.Fill;
            }
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
            Browse(hRef);
              
            
        }

        void board_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// View filter 
        /// </summary>
        public class ContentFilter : Board.Section.IViewFilter
        {
            public bool FilterElement(Board.Element elm,string query)
            {
                if (elm.Type != "entry")
                    return true;
                // The attributes to filter
                String[] attributes = { "artist", "album", "title" };
                foreach (String attribute in attributes)
                {
                    // if query contains space separate all words
                    if (query.Contains(" "))
                    {
                        String[] qs = query.Split(' ');
                        foreach (string q in qs)
                            if (elm.GetAttribute(attribute).ToLower().Contains(q))
                                return true;
                        return false;
                    }
                    if (elm.GetAttribute(attribute).ToLower().Contains(query))
                    {
                        return true;
                    }
                }
                return false;

            }
        }
        void treeview_LinkClick(object sender, string hRef)
        {
            board.Navigate(hRef, hRef.Split(':')[0], "views");
        }

        bool board_PlaybackRequested(object sender, string Url)
        {
            
            /**
             * Stop the current playing song
             * */
            if (CurrentPlayer != null)
                CurrentPlayer.Stop();

            /**
             * Try to see whatever if there is an compatible
             * engine for handling the playback!
             * */
           

            PlayItem(Url);
#if (nobug)
            // If url starts with Spotify:local: (eg. local file) load it another way. More handlers will be implemented soon as this
            // will be an mediachrome instance
            if(Url.StartsWith("spotify:local:"))
            {
                // convert uri string to parseable array [artist,title,album]
                string[] data = Url.Replace("spotify:local:","").Split(':');

                // Make query
                string query = String.Format("SELECT name,artist,album,path FROM song WHERE artist='{0}' AND album='{1}' AND name='{2}'",data[0].Replace("+"," "),data[1].Replace("+"," "),data[2].Replace("+"," "));

                // Retrieve result (the function result an boxed instance as it are called from scripts too)
                List<MediaChrome.Song> files = (List<MediaChrome.Song>)__GetLocalFiles(query);

                // if no song was found return FALSE
                if (files.Count == 0)
                    return false;

                // play the first result
                axWindowsMediaPlayer1.URL = files[0].Path.Replace("mp3:","");
                axWindowsMediaPlayer1.Ctlcontrols.play();
                return true;
            }

            /**
             * If Url starts with Spotify: call the spotify handler
             * */
            try
            {
                if (Url.StartsWith("spotify:"))
                {
                    var mediaEngine = Program.MediaEngines["spotify"];
                    this.CurrentPlayer = mediaEngine;
                    mediaEngine.Load(Url);
                    mediaEngine.Play();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

            /**
             * Get the identifier for which kind of media engine the song belongs to
             * */
            String startHandler = Url.Split(':')[0];

            /** 
             * Decide which media engine which should held responsible for the playback 
             * of the media entry
             * */
            try
            {
                this.CurrentPlayer = Program.MediaEngines[startHandler];
                this.CurrentPlayer.Load(Url.Replace(startHandler+":",""));
                this.CurrentPlayer.Play();
                return true;
            }
            catch
            {
                return false;
            }

           /*
            // TODO: Add more media handlers

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
                nowPlayingView = Sender;*/
#endif
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
            if (splitting2)
            {
                board.Width = e.X - board.ClientRectangle.Left;
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
         
           public string playlistURI;
           string currentPath="";
        private void PrintCurrentPlaylist()
        {
            
        }
      
       
      
        
        public void PlayMedia(string[] TrackList,int PlayIndex)
        {
        	
                  
        }
        
        
        public List<View> Processing { get; set; }
        
        public void listViewX1AfterSelect(object Sender,EventArgs e)
        {
        	
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
            Browse(textBox1.Text); 

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
#if (nobug)
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
#endif
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
            get;
            set;

        }
        public string currentAlbum
        {
            get;
            set;

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
        	//UpdatePlaylist();
           
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
        
#if(nobug)
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
#endif
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
            
        }

        private void pane6_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void ucPosBar1_Move(object sender, EventArgs e)
        {

        }
       
        private void ucPosBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if(CurrentPlayer!=null)
            CurrentPlayer.Seek((int)(ucPosBar1.Value*1000));
        }

        private void cBtn2_Load(object sender, EventArgs e)
        {
      
        }

        private void cBtn6_Load(object sender, EventArgs e)
        {

        }

        private void cBtn2_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer != null)
                CurrentPlayer.Stop();
        }

        private void cBtn6_Click(object sender, EventArgs e)
        {

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
         
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void cBtn1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (CurrentPlayer != null)
                CurrentPlayer.Play();
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

        private void Form1_Shown(object sender, EventArgs e)
        {
          
        }

        private void cBtn8_Click(object sender, EventArgs e)
        {
            // Create import library form
            MediaChrome.ImportLibrary frmImport = new MediaChrome.ImportLibrary();
            frmImport.ShowDialog();
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ucSearch2_KeyUp(object sender, KeyEventArgs e)
        {
          //  board.Filter(ucSearch2.Text, new ContentFilter());
        }

        private void cBtn8_Load(object sender, EventArgs e)
        {
        
        }

        private void cBtn8_Click_1(object sender, EventArgs e)
        {
            this.NextSong();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
#if(nobug)
            try
            {
                // Get if the spotify playlists are loaded
                if (Program.MediaEngines["spotify"].PlaylistsLoaded)
                {
                    /**
                     * Add standard menu items
                     * */
                    Thread.Sleep(100);
                    Dictionary<String, String> CFH = new Dictionary<string, string>();
                    CFH.Add("spotify:home:a", "Home");
                    CFH.Add("spotify:local:a", "Local Music");

                    foreach (KeyValuePair<string, string> menuitem in CFH)
                    {
                        Board.Element _home = this.treeview.AddItem(menuitem.Key, menuitem.Value, new String[] { }, new String[] { },500,16);
                        _home.SetAttribute("href", menuitem.Value);
                    }
              


                    foreach (MediaChrome.Views.Playlist playlist in Program.MediaEngines["spotify"].Playlists)
                    {
                        // Add the playlist element


                        Board.Element cf = this.treeview.AddItem(playlist.ID, playlist.Title, new String[] { }, new String[] { },500,16);
                        cf.SetAttribute("href", playlist.ID);

                        // Set the playlist as an attachment
                        cf.Attachment=playlist;
                    
                       
                    }
                    timer2.Stop();

                }
               
            }
            catch { }
#endif

        }

        private void cBtn8_Load_1(object sender, EventArgs e)
        {
            

        }

        private void cBtn4_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Engines D = new Engines();
            D.Host = this;
            D.Left = this.ClientRectangle.Left + this.Left;
            D.Top = this.PointToScreen(new Point(0, this.panel2.Top + this.panel2.Height)).Y;
            D.Show();
           

        }

        public Song watchSong { get; set; }

        private void Form1_Leave(object sender, EventArgs e)
        {
         LockWindowUpdate(this.Handle);
        }
        int WMSZ_BOTTOM = 6;
        int WMSZ_BOTTOMLEFT =7;
        int WMSZ_BOTTOMRIGHT =8;
        int WMSZ_LEFT = 1;
        int WMSZ_RIGHT = 2;
        int WMSZ_TOP = 3;
        int WMSZ_TOPLEFT = 4;
        int WMSZ_TOPRIGHT = 5;
        int WM_SIZING = 0x0214;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
      
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

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int nTaskBarHeight = Screen.PrimaryScreen.Bounds.Bottom - Screen.PrimaryScreen.WorkingArea.Bottom;
            this.MaximumSize = new Size(this.Width, this.Height - nTaskBarHeight);
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;

        }

        private void cBtn5_DragDrop(object sender, DragEventArgs e)
        {
            // Create new playlist by default
            try
            {
                // Ttry to extract uris from the string
                String str = (String)e.Data.GetData(DataFormats.StringFormat);
                if (!String.IsNullOrEmpty(str))
                {
                    // Start the operation on an new thread
                    AddSong asv = new AddSong();
                    asv.Uris = str;
                    Thread d = new Thread(AddSongs);
                    d.Start(asv);
                }
            }
            catch
            {
            }
        }

        private void cBtn5_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                // Ttry to extract uris from the string
                String str = (String)e.Data.GetData(DataFormats.StringFormat);
                if (!String.IsNullOrEmpty(str))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            catch
            {
            }
        }
        private string ZeroFill(int no)
        {
            return no > 9 ? no.ToString() : String.Format("0{0}", no);
        }
        private void timer3_Tick_1(object sender, EventArgs e)
        {
           if(CurrentPlayer!=null)
            try
            {
                this.ucPosBar1.Maximum = (float)(float)currentPlayer.Duration;
                this.ucPosBar1.Value = (float)currentPlayer.Position;


                // Determine start and ending time in m:ss
                TimeSpan StartTime = new TimeSpan(0, 0, 0, this.currentPlayer.Position);
                TimeSpan EndTime = new TimeSpan(0, 0, 0, (int)this.currentPlayer.Duration);
   
                // Display current position in m:ss
                this.label1.Text = String.Format("{0}:{1}", StartTime.Minutes, ZeroFill(StartTime.Seconds));
                

                // Display length in m:ss
                this.label4.Text = String.Format("{0}:{1}", EndTime.Minutes, ZeroFill(EndTime.Seconds));
                
            }
            catch { }
        }

        private void cBtn8_Click_2(object sender, EventArgs e)
        {
            try
            {
                if (currentPlayer.Paused)
                {
                    currentPlayer.Play();
                    cBtn8.Img = Properties.Resources.pause;
                    
                }
                else
                {
                    currentPlayer.Pause();
                    cBtn8.Img = Properties.Resources.play2;
                }
            }
            catch { }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ErrorMessage = "";
        }

        private void timer4_Tick_1(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ErrorMessage))
            {
                pane5.Show();
                label2.Text= ErrorMessage;
            }
            else
            {
                pane5.Hide();
            }
            
        }

        private void cBtn1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void cBtn2_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SIZING, WMSZ_TOPLEFT, 0);

        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SIZING, WMSZ_TOPRIGHT, 0);

        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SIZING, WMSZ_TOP, 0);

        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SIZING, WMSZ_BOTTOM, 0);

        }

        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SIZING, WMSZ_BOTTOMLEFT, 0);

        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SIZING, WMSZ_BOTTOMRIGHT, 0);

        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void cBtn2_Click_1(object sender, EventArgs e)
        {
            this.board.PreviousSong();
        }

        private void cBtn1_Click_1(object sender, EventArgs e)
        {
            this.board.NextSong();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
       //     board.Filter(textBox1.Text, new ContentFilter());
        }

        private void ucPosBar1_MouseDown(object sender, MouseEventArgs e)
        {
            timer3.Stop();
        }

        private void ucPosBar1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (CurrentPlayer != null)
                CurrentPlayer.Seek(ucPosBar1.Value);
            timer3.Start();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

    }
     public class Pane : System.Windows.Forms.Panel
    {
         public bool Dark { get; set; }
         public Color SecondColor { get; set; }
    }
     public class ExPanel : Panel
     {
         protected override void NotifyInvalidate(Rectangle invalidatedArea)
         {
      //       invalidatedArea = new Rectangle(0, 0, 0, 0);
             base.NotifyInvalidate(invalidatedArea);
         }
         protected override void OnInvalidated(InvalidateEventArgs e)
         {
          ///   e = new InvalidateEventArgs(new Rectangle(0, 0, 0, 0));
          ///   
             base.OnInvalidated(e);
         }
         protected override void OnPaintBackground(PaintEventArgs e)
         {
           /*  foreach (Control c in this.Controls)
             {
                 Bitmap d = new Bitmap(c.Width, c.Height);
                 e.Graphics.DrawImage(d, c.ClientRectangle);
             }*/
             if(BackgroundImage!=null)
             e.Graphics.DrawImage(this.BackgroundImage, new Rectangle(0, 0, Width, Height));

             base.OnPaintBackground(e);
         }
     }
}
