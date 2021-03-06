﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Board
{
    
    public partial class DrawBoard : UserControl
    {
        /// <summary>
        /// Set default colors
        /// </summary>
        public void SetColors()
        {
       /*     Section = Color.FromArgb(97, 97, 97);
            SectionTextShadow = Color.FromArgb(119, 119, 119);
            TextFade = Color.Gray;
            Fg = Color.FromArgb(10, 10, 10);
            Divider = Color.FromArgb(138, 138, 138);
            Alt = Color.FromArgb(211, 211, 211);*/
            Section = Color.FromArgb(97, 97, 97);
            SectionTextShadow = Color.FromArgb(119, 119, 119);
            TextFade = Color.Gray;
            Fg = Color.FromArgb(221, 221, 221);
            Divider = Color.FromArgb(0,0,0);
            Alt = Color.FromArgb(3, 3, 3);
            //this.Background = Resource1.view_bg;
        }
        #region Delegates and events


        /// <summary>
        /// Delegate for playback start event handlers
        /// </summary>
        /// <param name="sender">the board where the event is happening on</param>
        /// <param name="Url">An url to the object which request playback</param>
        /// <returns>should return true if the playback can be started, if not return FALSE</returns>
        public delegate bool PlaybackStartEvent(object sender, String Url);
        public event PlaybackStartEvent PlaybackRequested;

        /// <summary>
        /// Delegate for events relating to navigation
        /// </summary>
        /// <param name="sender">the Spofity instance resposible for the new view</param>
        /// <param name="uri">the uri for navigation</param>
        public delegate bool NavigateEventHandler(object sender, string uri);

        public event NavigateEventHandler Navigating;

        /// <summary>
        /// Occurs when the board is navigating to an new uri. 
        /// </summary>
        public event NavigateEventHandler AfterNavigating;

        /// <summary>
        /// Occurs when the navigation has been finished. The first parameteer sender wwill provide an instance of MakoEngine
        /// </summary>
        public event NavigateEventHandler BeginNavigating;
        public bool Snart =true;
        #endregion
        #region Colors

        /// <summary>
        /// Gets or sets if the rows should aternate or have dividers
        /// </summary>
        [DefaultValue(true)]
        public bool Alternate
        {
            get;
            set;
        }
        public Image Background
        {
            get;
            set;
        }
        public Color Section
        {
            get;
            set;
        }
      
        public Color SectionTextShadow
        {
            get;
            set;
        }

        public Color SectionText
        {
            get { return Bg; }
        }

        public Color Bg
        {
            get
            {
                return BackColor;
            }
        }
    
        public Color TextFade
        {
            get;
            set;
        }
        
        public Color Divider
        {
            get;
            set;
        }
   
        public Color Fg
        {
            get;
            set;
        }
        public Color Entry
        {
            get { return Bg; }
        }
        
        public Color Alt
        {
            get;
            set;
        }
        #if(nobug)
        [DefaultValue(Color.FromArgb(97, 97, 97))]
        public Color Section
        {
            get;
            set;
        }
        [DefaultValue(Color.FromArgb(119, 119, 119))]
        public Color SectionTextShadow
        {
            get;
            set;
        }
      
        public Color SectionText
        {
            get{return Bg;}
        }
    
        public Color Bg
        {
           get
            {
                return BackColor;
            }
        }
        [DefaultValue(Color.Gray)]
        public Color TextFade
        {
            get;
            set;
        }
        [DefaultValue(Color.FromArgb(38, 38, 38))]
        public Color Divider
        {
            get;
            set;
        }
        [DefaultValue(Color.FromArgb(188,188,188))]
        public Color Fg
        {
            get;
            set;
        }
        public Color Entry
        {
            get{return Bg;}
        }
        [DefaultValue(Color.FromArgb(44,44,44))]
        public Color Alt
        {
            get;
            set;
        }
#endif
        #endregion
        int rowtop=0;
        /// <summary>
        /// The view hiearchy. All views called has their address attached to it, before reloading checking the list for possible
        /// view already exist
        /// </summary>
        public Dictionary<String, View> Views {get;set;}

        /// <summary>
        /// History of view for stack
        /// </summary>
        public Stack<View> History { get; set; }
        /// <summary>
        /// Forward history
        /// </summary>
        public Stack<View> Post {get;set;}

        /// <summary>
        /// Current section on the specific view
        /// </summary>
        public int currentSection
        {
            get
            {
                try
                {
                    return this.currentView.Section;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    this.currentView.Section = value;
                }
                catch
                {
                }
            }
        }

        public View CurrentView { get { return currentView; } set { currentView = value; } }
        public DrawBoard()
        {
        	Views = new Dictionary<string, View>();
            Post = new Stack<View>(); 
            History = new Stack<View>();
            InitializeComponent();
            Images = new Dictionary<String,Image>();
            AllowDrop=true;
          //  this.Click+= new EventHandler(DrawBoard_Click);
            // Assign default columnwidths
            Columns = new Dictionary<string, int>();

            // Add standard columns (title,position)
            Columns.Add("r", 30);
            Columns.Add("No", 50);
            Columns.Add("Title", 300);
            Columns.Add("Artist", 150);
            Columns.Add("Length", 50);
            Columns.Add("Album", 200);
            SetColors();

        }
        /// <summary>
        /// Returns the instance to the current active section
        /// </summary>
        public Section CurSection
        {
            get
            {
                if (CurrentView != null)
                    if (CurrentView.Content != null)
                        if (CurrentView.Content.View != null)
                            return CurrentView.Content.View.Sections[currentSection];
                return null;
            }
        }

        

        /// <summary>
        /// Get the list of elements for the current view
        /// </summary>
        public List<Element> ViewBuffer
        {
            get
            {
                
                if (CurrentView != null)
                    if (CurrentView.Content != null)
                        if (CurrentView.Content.View != null)
                        {
                            List<Element> viewBuffer = CurrentView.Content.View.Sections[currentSection].Elements;
                            // if filter view is defined show it
                            if (this.CurrentView.Content.View.Sections[currentSection].FilterView != null)
                                viewBuffer = this.CurrentView.Content.View.Sections[currentSection].FilterView;
                            return viewBuffer;
                        }
                        return null;
            }
            
        }
        /// <summary>
        /// Filter the view according to the query
        /// </summary>
        /// <param name="query">The query to filter</param>
        /// <param name="filter">An instance to an implemented IViewFilter class for filter rules</param>
        public void Filter(string query, Section.IViewFilter filter)
        {
            if (CurSection != null)
            {
                CurSection.Filter = filter;
                CurSection.FilterQuery = query;
            }
        }
        /// <summary>
        /// Raises a click to the specified elemetn if the mouseX/Y is in bounds
        /// </summary>
        /// <param name="_Element"></param>
        /// <param name="mouseX"></param>
        /// <param name="mouseY"></param>
        public void ElementClick(Element _Element,int mouseX,int mouseY)
        {
            Rectangle Boundaries = _Element.Bounds;
            int _left = _Element.Bounds.Left;
            int _top = _Element.Bounds.Top;
            int _width = _Element.Bounds.Width;
            int _height = _Element.Bounds.Height;
            
                // If the element has an onclick handler, execute the script
                if (_Element.GetAttribute("onclick") != "")
                {
                    CurrentView.Content.ScriptEngine.Run(_Element.GetAttribute("onclick"));
                    return;
                }

            

            if (_Element.Entry)
            {
                /**
                 * Enumerte all columns and check for possible links
                 * */
                int column_position = _left;
                foreach (KeyValuePair<String, int> _column in Columns)
                {

                    int column = _column.Value;
                    string title = _column.Key;
                    if (mouseX >= column_position && mouseX <= column_position + column && mouseY >= _top && mouseY <= _top + _height)
                    {
                        // If the element has an href matching the column, go to it
                        if (LinkClick != null)
                            if (_Element.GetAttribute("href_" + title.ToLower()) != "")
                                LinkClick(_Element, _Element.GetAttribute("href_" + title.ToLower()));
                    }
                    column_position += _column.Value;

                }
            }
            if (_Element.GetAttribute("href") != "" )
            {
                // If the itemclicked event are not null, raise it
                if (LinkClick != null)
                    LinkClick(_Element, _Element.GetAttribute("href"));

            }
        }
        /// <summary>
        /// Column widths. They are used for the entries. -1 means until end of size
        /// </summary>
        public Dictionary<String, int> Columns;
        void DrawBoard_Click(object sender, EventArgs e)
        {
            if (HoveredElement != null)
            {
                ElementClick(HoveredElement, mouseX, mouseY);
            }
#if(nobug)
            if (CurrentView != null)
                if (CurrentView.Content != null)
                    if (CurrentView.Content.View != null)
                        try
                        {



                            /**
                             * Process the current hovered element
                             * */

                            Element _Element = HoveredElement;
                            if(_Element!=null)
                                ElementClick(_Element, mouseX, mouseY);
                                
                            /**
                             * Capture toolbaritemss
                             * */

                            int menupadding = 20;
                            int size = 0;
                            Graphics d = this.CreateGraphics();
                            foreach (Element _elm in this.currentView.Content.View.Toolbar.Items)
                            {
                                size += menupadding + (int)d.MeasureString(_elm.GetAttribute("title"), new Font(FontFace, 9)).Width + menupadding;
                            }
                            int toolBarindent = scrollbar_size;
                           // Get start position of toolbar 
                           int position = this.Width - toolBarindent - size;
                           foreach (Element _elm in this.currentView.Content.View.Toolbar.Items)
                           {
                               int prePosition=position;
                        
                              position += menupadding+ (int)d.MeasureString(_elm.GetAttribute("title"), new Font(FontFace, 9)).Width+menupadding;

                               // If mouseover mark the item
                               if (mouseX >= prePosition && mouseX <= position && mouseY < tabbar_height)
                               {
                                   // if item is an menu create an context menu
                                   if (_elm.GetAttribute("type") == "menu")
                                   {
                                       // Create contextmenu
                                       ContextMenu _cm = new ContextMenu();
                                       
                                       // Create Child items
                                       foreach (Element menuItem in _elm.Elements)
                                       {
                                           ToolMenuItem MenuItem = new ToolMenuItem(this);


                                           // Associate the event with the menu item    
                                           MenuItem.Tag = menuItem.GetAttribute("onclick");

                                           // set the menuitem's text to the elements text
                                           MenuItem.Text = menuItem.GetAttribute("title");
                                           _cm.MenuItems.Add(MenuItem);
                                       }

                                       // Show the menu
                                       _cm.Show(this, new Point(prePosition-menupadding, tabbar_height));

                                       // stop here
                                       return;
                                   }
                                   CurrentView.Content.ScriptEngine.Run(_elm.GetAttribute("onclick"));
                               }
                           }

                
                        }
                        catch (Exception ex)
                        {
                        }
#endif

            }

            /// <summary>
            /// Shortcut menuitem class, for instansiating shortcut menus from scripts
            /// </summary>
            public class ToolMenuItem : MenuItem
            {
                public Board.DrawBoard Host;
                public ToolMenuItem(Board.DrawBoard host)
                {
                    Host = host;
                    this.Click += new EventHandler(ToolMenuItem_Click);
                }
                void ToolMenuItem_Click(object sender, EventArgs e)
                {
                    String tag = (string)((ToolMenuItem)sender).Tag;

                    // Execute function associated with the menu
                    Host.CurrentView.Content.Engine.RuntimeMachine.Run(tag);
                }
            }

            /// <summary>
            /// Should do an action on the javascript handler
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
       

        public event EventHandler Loaded;
        public void LoadPage(string URI)
        {
        	if(Views.ContainsKey(URI))
        	{
        		/*foreach(Spofity d in History)
        		{
        			if(d.URI == URI)
        			{
        				Spofity T =null;
        				while((T=History.Pop())!=null)
        				{
        					
        					if(T.URI == URI)
        					{
        						this.CurrentView = T;
        						break;
        					}
        					Post.Push(T);
        				}
        				
        				
        			}
        		}*/
        		this.History.Push(this.CurrentView);
        		this.CurrentView = Views[URI];
        		
        		return;
        	}
            Thread D = new Thread(LoadContent);
            D.Start((object)URI);
        }
        View currentView;
        public void GoForward()
        {
            // If there is views at post
            if (Post.Count > 0)
            {
                // push the current view to the history
                History.Push(this.CurrentView);
                // Get the next view at post
                this.currentView = Post.Pop();
            }
        }
        public void GoBack()
        {
            // If there is any history
            if (History.Count > 0)
            {
                // Put the current view to the post stack
                Post.Push(this.CurrentView);
                // set current view to the forward stack
                this.currentView = History.Pop();
            }
            if(ItemClicked != null)
        	this.ItemClicked(this,"@back");
        }
        /// <summary>
        /// Raises when an object with an href clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="link"></param>
        public delegate void LinkClicked(object sender, String hRef);
        /// <summary>
        /// Delegate for handling object click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ItemClick(object sender, String uri);

        /// <summary>
        /// Delegate for handling object double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ItemDoubleClick(object sender,EventArgs e);

        /// <summary>
        /// Even raised when an user clicks an object
        /// </summary>
        public event ItemClick ItemClicked;
        /// <summary>
        /// Raises when an user clicks an link
        /// </summary>
        public event LinkClicked LinkClick;
        
        /// <summary>
        /// Force update of an certain instance of a view
        /// </summary>
        /// <param name="srcView">The view instance</param>
        public void UpdateView(View srcView)
        {
            // invalidate the current content of the view as we're going to replace it
            srcView.Content = null;

            // Then do it again
            Thread d = new Thread(LoadViewAsync);
            d.Start((object)srcView);
        }

        /// <summary>
        /// Class of an view instance
        /// </summary>
        public class View
        {
            public View(string name,String address, string argument)
            {
                this.Name = name;
                this.Address = address;
                this.Argument = argument;
                this.Section = 0;
            }
            /// <summary>
            /// The section of the view
            /// </summary>
            public int Section 
            {
                get
                {
                    try
                    {
                        return this.Content.CurrentSection;
                    }
                    catch
                    {
                        return 0;
                    }
                }
                set
                {
                    try
                    {
                        this.Content.CurrentSection = value;
                    }
                    catch
                    {
                    }
                }
            }
               

            /// <summary>
            /// The name of the view, for measure
            /// </summary>
            public String Name;
            /// <summary>
            /// Gets if the View has been loaded, eg. the Content is not null
            /// </summary>
            public bool Loaded
            {
                get
                {
                    return Content != null;
                }
            }
            /// <summary>
            /// The adress to the view
            /// </summary>
            public String Address;

            /// <summary>
            /// The argument provided to the view
            /// </summary>
            public String Argument;

            /// <summary>
            /// The Spofity class instance of the content
            /// </summary>
            public Spofity Content;
        }
        /// <summary>
        /// Delegate which manage events for mako creation
        /// </summary>
        /// <param name="sender">the current instance to mako</param>
        /// <param name="e">eventargs</param>
        public delegate void MakoCreateEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Occurs when the mako template engine has been init
        /// </summary>
        public event MakoCreateEventHandler MakoGeneration;
        /// <summary>
        /// Method to load an view asynchronisly
        /// </summary>
        /// <param name="address"></param>
        public void LoadViewAsync( object address)
        {
            View view = (View)address;
        
            {
              
                // Get the uri
                String uri = view.Address;

                try
                {
                    // Load and preprocess the page

                    // If the page comes from the internet download it or load it from harddrive
                    if (uri.StartsWith("http://"))
                    {
                        WebClient WC = new WebClient();
                        rawSource = WC.DownloadString(new Uri(uri));
                    }
                    else
                    {
                        using (StreamReader SR = new StreamReader((string)uri))
                        {
                            rawSource = SR.ReadToEnd();

                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Invalid view");
                }
              
                // Create mako and ask for initialization code
                ME = new MakoEngine();
                if (MakoGeneration != null)
                    MakoGeneration(ME, new EventArgs());

                // If there is an prenavigated event handler defined, raise it
                if (BeginNavigating != null)
                    BeginNavigating(ME, ((View)address).Address);

                output = ME.Preprocess(rawSource,view.Argument,false);

                /**
                 * If something did go wrong during the load, eg returns an string starting with the
                 * prefix error, cancel the page and show an message to the user indicating the
                 * template engine could not load the page.
                 * */
                if (output.StartsWith("ERROR:"))
                {
                    // Create an viewerror message
                    ViewError form = new ViewError("",output);

                    // Show the form as modal
                    form.ShowDialog();

                    // Cancel the operation
                    return;
                }

                // Finaly start the view
                Spofity R = new Spofity(this);

                R.MakoGeneration += new Spofity.MakoCreateEventHandler(R_MakoGeneration);
                R.Initialize(((View)address).Argument,rawSource,output, ME);

                // Create an proxy handler so people can interact with each spofity
                R.PlaybackStarted += new Spofity.ElementPlaybackStarted(R_PlaybackStarted);
                // Attach the content to the content tag so it are fully loaded
                view.Content = R;

                // set the  spofity's parent view to CurrentView
                R.ParentView = view;
                CurrentView = view;

                // If there is an afternavigated event handler defined, raise it
                if (AfterNavigating != null)
                    AfterNavigating(R, ((View)address).Address);


            }
           
        }

        void R_MakoGeneration(object sender, EventArgs e)
        {
            this.MakoGeneration(sender, e);
        }

        /// <summary>
        /// Class for play queue
        /// </summary>
        public class PlayQueue
        {
            public View SourceView { get; set; }
            public Queue<Element> Songs { get; set; }
            public Spofity Source { get; set; }
        }

        /// <summary>
        /// Current play queue
        /// </summary>
        public PlayQueue CurrentQueue;

        /// <summary>
        /// Proxy event handler for playback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="elm"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        bool R_PlaybackStarted(object sender,Element elm, string uri)
        {
            // Get the sender
            Spofity d = (Spofity)sender;

            // set an current Play queue
            PlayQueue queue = new PlayQueue();
            queue.Songs = d.Playlist;
            queue.Source = d;
            queue.SourceView = d.ParentView;
           


            // Raise playback requested
            bool sucess =  PlaybackRequested(sender, uri);

            // If the song can be played set the new queue as the default queue
            if (sucess)
            {
                CurrentQueue = queue;
            }
            return sucess;
        }

       /* /// <summary>
        /// Method to load page directly with the data provided
        /// </summary>
        /// <param name="name">The name of the view</param>
        /// <param name="uri">The uri to the view (file or remote)</param>
        /// <param name="argument">The argument sending to the views preparser</param>
        public void ReadPage(String name,String uri,String argument)
        {
          
            
            // Format the string
            ME = new MakoEngine();
            output = ME.Preprocess(rawSource);

            // Create an new view
            View C = new View(name,uri, argument);

            // If the view is already in the list, show it
            if(Views.ContainsKey(name))
            { 
                // View already registred
                CurrentView = C;
                return;
               
            }
            else
            {
                // The view is new
                this.Views.Add(uri, C);
                // Put it into the new thread
                Thread LoadView = new Thread(LoadViewAsync);
                LoadView.Start((object)C);
                
         
            }
            
          
        }*/

        /// <summary>
        /// Public method to navigate.
        /// </summary>
        /// <param name="Uri">The name of the view</param>
        /// <param name="nspace">Namespace for the uri (xx:)</param>
        /// <param nme="BaseFolder">The base folder where the .xml view files reside</param>
        public void Navigate(String Uri,String nspace,String BaseFolder)
        {
            if (Navigating != null)
            {
                // If the navigating event returns false dismiss
                if (!Navigating(this, Uri))
                    return;
            }
            // Push current view and clear future
            History.Push(currentView);
            Post.Clear();
            // Check if the view is already inside the stack
            if(Views.ContainsKey(Uri))
            {
                this.CurrentView = Views[Uri];
            }
            else
            {
                // Split the uri and fetch the two parts, the namespace, the view's namespace and the parameter
                String[] Parts = Uri.Replace(nspace+":",null).Split(':');

                String App = Parts[0];
                String Querystring = Uri.Replace(nspace + ":" + App + ":", "");

                View newView = new View(Uri, BaseFolder + "\\" + App + ".xml", Uri);
               
                // Add the pending view to the view list
                this.Views.Add(Uri,newView);

                // Create the thread and load the view
                Thread viewLoader = new Thread(LoadViewAsync);
                viewLoader.Start((object)newView);

                this.currentView = newView;
            }

        }
        public void LoadContent( object Uri)
        {
            
          
            

        }
        internal static Object Mutex = new Object();
        /// <summary>
        /// Method to asynchronisly download an image to an img element
        /// </summary>
        /// <param name="token">A element to use</param>
        public void DownloadImage(object token)
        {
            Element elm = (Element)token;
            string address = elm.GetAttribute("src");
            try
            {
                // Get image string
                // create dummy bitmap to indicate the image is going to load


                elm.Bitmap = Resource1.release;
                elm.ImagePending = true;
            }
            catch { } 

            /**
             * Lock the function so it cannot be run simultany
             * */
         //   lock (Mutex)
            {


                
            
                /**
                 * We can set an imagedownload event to handle and redirect image requests.
                 * */
                if (BeginDownloadImage != null)
                {
                
                    // Create event args
                    ImageDownloadEventArgs args = new ImageDownloadEventArgs();
                    args.Adress = address;
                    BeginDownloadImage(this, args);
                    if (args.Cancel)
                        return;
                    // Check for changed address
               
                    elm.Bitmap=args.Bitmap;
                    return;
                }

                // if the address points to an local file (not starting with http:) start an local file process instead
                if (address.StartsWith("http:"))
                {
                    try
                    {
                        // Create an webclient and download the image from the internet and read it into an bitmap stream
                        WebClient X = new WebClient();

                        Image  cf= Bitmap.FromStream(X.OpenRead((string)token));
                    
                        // Add the bitmap to the list
                        elm.Bitmap = cf;
                        
                    }
                    catch (Exception e)
                    {
                    }
                }
                else
                
                {
                    try
                    {
                        // Load the local image into an bitmap stream
                    

                        // add it to the images list
                    
                    
                    }
                    catch
                    {
                        try
                        {

                        }
                        catch { }
                    }
                }
   
            }

        }
        
        /// <date>2011-04-25 12:04</date>
        /// <summary>
        /// Draws an image
        /// </summary>
        /// <param name="image">The image to draw</param>
        /// <param name="Bounds">The bounds of the image</param>
        /// <param name="g">The graphics</param>
        /// <param name="hasShadow">Decides whether image should have an shadow</param>
        public void DrawImage(Image image,Rectangle Bounds,Graphics g,bool hasShadow)
        {
             /** If drop shadow is specified draw it
              * */
            int shadowOffset = 8;
            if (hasShadow)
            {
                // the shadow chunk
                int sQuad = 4;

                // the offset
                int sOffset = 4;

                // the shadow layer
                Bitmap shadow = Resource1.shadow;
                // draw the left top corner
                g.DrawImage(shadow, new Rectangle(Bounds.Left - sQuad, Bounds.Top - sQuad, sQuad, sQuad), new Rectangle(0, 0, sQuad, sQuad), GraphicsUnit.Pixel);

                // draw the right top corner
                g.DrawImage(shadow, new Rectangle(Bounds.Left + Bounds.Width, Bounds.Top - sQuad, sQuad, sQuad), new Rectangle(shadow.Width - sQuad, 0, sQuad, sQuad), GraphicsUnit.Pixel);

                // size of vertical sides
                Size verticalSize = new Size(sQuad, Bounds.Height + shadowOffset - sQuad * 2);

                // size of horizontal sides
                Size horizontalSize = new Size(Bounds.Width + shadowOffset - sQuad * 2, sQuad);

                // draw the bottom left corner
                g.DrawImage(shadow, new Rectangle(Bounds.Left - sQuad, Bounds.Top + Bounds.Height, sQuad, sQuad), new Rectangle(0, shadow.Height - sQuad, sQuad, sQuad), GraphicsUnit.Pixel);

                // draw the bottom right corner
                g.DrawImage(shadow, new Rectangle(Bounds.Left + Bounds.Width, Bounds.Top + Bounds.Height, sQuad, sQuad), new Rectangle(shadow.Width - sOffset, shadow.Height - sQuad, sQuad, sQuad), GraphicsUnit.Pixel);


                // fill the left side
                g.DrawImage(shadow, new Rectangle(new Point(Bounds.Left - sQuad, Bounds.Top), verticalSize), new Rectangle(0, sQuad, sQuad, sQuad), GraphicsUnit.Pixel);

                // fill the right side
                g.DrawImage(shadow, new Rectangle(new Point(Bounds.Width + Bounds.Left, Bounds.Top), verticalSize), new Rectangle(shadow.Height - sQuad, sQuad, sQuad, sQuad), GraphicsUnit.Pixel);
                //  g.DrawImage(shadow,new Rectangle(Bounds.Left-shadowOffset,Bounds.Top-shadowOffset,Bounds.Width+shadowOffset+2,Bounds.Height+shadowOffset+2));

                // fill the top side

                g.DrawImage(shadow, new Rectangle(new Point(Bounds.Left, Bounds.Top - sQuad), horizontalSize), new Rectangle(sQuad, 0, sQuad, sQuad), GraphicsUnit.Pixel);

                // fill the bottom side

                g.DrawImage(shadow, new Rectangle(new Point(Bounds.Left, Bounds.Top + Bounds.Height), horizontalSize), new Rectangle(sQuad, shadow.Height - sQuad, sQuad, sQuad), GraphicsUnit.Pixel);

            }

            // Draw the image
            g.DrawImage(image, Bounds);

           

        }

        public void DrawString(String str, Font font, bool shadow,Graphics g)
        {
        }

        Dictionary<String,Image> Images;
        /// <summary>
        /// Executes when the loading of the page has been finished and downloading images
        /// </summary>
        void D_FinishedLoading()
        {

            foreach (Section d in CurrentView.Content.View.Sections)
            {
                foreach (Element _Elm in d.Elements)
                {
                    if (_Elm.Type == "img")
                    {
                        Image C = null;
                        if (!Images.TryGetValue(_Elm.GetAttribute("src"), out C))
                        {
                            Thread D = new Thread(DownloadImage);
                            D.Start((object)_Elm.GetAttribute("src"));
                        }
                    }
                }
            }
            if(this.Loaded!=null)
            this.Loaded(this, new EventArgs());
            
        }
        
        private void Artist_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }
        public event ScrollEventHandler Scrolling;
        int LEFT = 140;
        int ARTISTLEFT=550;
        int ROWHEIGHT = 20;
        public int scrollX
        {
        	get
        	{
                try
                {
                    return this.CurrentView.Content.ScrollX;
                }
                catch { return 0; }
        	}
        	set
        	{
                this.CurrentView.Content.ScrollX = value;
        	} 
        	
        }
        public int scrollY
        {
        	get
        	{
                try
                {
                    if (CurrentView == null)
                        return 0;
                    return this.CurrentView.Content.ScrollY;
                }
                catch
                {
                    return 0;
                }
        	}
        	set
        	{
                
        		if(CurrentView==null)
        			return;
                try
                {
                    this.CurrentView.Content.ScrollY = value;
                    
                }
                catch { }
                AssertScroll();
        	}
        	
        }


        

       


        public class ImageDownloadEventArgs
        {
            public String Adress {get;set;}
            public bool Cancel { get; set; }
            public Image Bitmap { get; set; }
        }

        /// <summary>
        /// Delegate for event relating to image downlods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ImageDownloadEventHandler(object sender, ImageDownloadEventArgs e);

        /// <summary>
        /// Occurs when an image is begin to be downloaded. Runs on another thread
        /// </summary>
        public event ImageDownloadEventHandler BeginDownloadImage;

        /// <summary>
        /// Event args for element move event handler
        /// </summary>
        public class ElementMoveEventArgs
        {
            public Element Element {get;set;}
            public int OldPosition {get;set;}
        }
        /// <summary>
        /// Delegate for element move event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ElementMoveEventHandler(object sender, ElementMoveEventArgs e);

        /// <date>2011-04-24 15:21</date>
        /// <summary>
        /// Event args for item reordering
        /// </summary>
        public class ItemReorderEventArgs
        {
            /// <summary>
            /// If in begin mode, Gets and sets whether the operation should be cancelled or not
            /// </summary>
            public bool Cancel { get; set; }
            public ItemReorderEventArgs()
            {
                Collection = new List<Element>();
            }
            /// <summary>
            /// Old position
            /// </summary>
            public int OldPos {get;set;}
            
            /// <summary>
            /// New Position
            /// </summary>
            public int NewPosition {get;set;}

            /// <summary>
            /// The collection of elements to reorder
            /// </summary>
            public List<Element> Collection {get;set;}
        }
        /// <date>2011-04-24 15:21</date>
        /// <summary>
        /// Delegate for item reordering
        /// </summary>
        public delegate void ItemReorderEvenHandler(object sender, ItemReorderEventArgs e);
        /// <summary>
        /// Occurs when items are begin to be reordered.
        /// </summary>
        public event ItemReorderEvenHandler BeginReorder;

        /// <summary>
        /// Occura when items has finished reordering
        /// </summary>
        public event ItemReorderEvenHandler FinishedReorder;

        
        /// <summary>
        /// Function to get an specific element by ID.
        /// </summary>
        /// <param name="ID">the id of the element</param>
        /// <returns>The element if found, null otherwise</returns>
        public Element GetElementById(string ID)
        {
            foreach (Element elm in CurrentCollection)
            {
                if (elm.GetAttribute("ID") == ID)
                    return elm;
            }
            return null;
        }

       

        /// <summary>
        /// An shortcut to the current collection of elements
        /// </summary>
        public List<Element> CurrentCollection
        {
            get
            {
                return this.ViewBuffer;
            }
        }
        /// <date>2011-04-24 15:17 </date>
        /// <summary>
        /// Function to get an element underlying the position. Relative to scrollX/scrollY
        /// </summary>
        /// <param name="point"></param>
        /// <returns>The element on the spot or NULL if not</returns>
        public Element GetItemAtPos(System.Drawing.Point point)
        {
            mouseX = point.X;
            mouseY = point.Y;
            if(ViewBuffer!=null)
            foreach (Element d in ViewBuffer)
            {
                Rectangle Bounds = d.GetCoordinates(scrollX, scrollY, new Rectangle(0, 0, this.Width, this.Height),0);
                if (mouseX >= Bounds.Left && mouseX <= Bounds.Width + Bounds.Left && mouseY >= Bounds.Top && mouseY <= Bounds.Top + Bounds.Height)
                {
                    // Return the element
                    return d;
                }
            }
            return null;
        }
        /// <summary>
        /// Method to get an particular item of type entry at an certain index (only items of type entry included)
        /// </summary>
        /// <returns>An element by type entry at the specified position or NULL if no elements where found or an exception was thrown</returns>
        /// <param name="pos">The index of the element</param>
        /// <remarks>Only elements of type entries is accessible.</remarks>
        public Element GetItemAt(int pos)
        {
            try{
                // counter of entries
                int counter = 0;

                /**
                 * Enumerate all elements until the element of the desired index is found
                 * */


                foreach (Element _element in this.ViewBuffer)
                {
                    // If the element is not an entry, skip it
                    if (_element.Type != "entry")
                        continue;
                    
                    // If the row is the same as specified, return it
                    if (counter == pos)
                        return _element;
                    
                    counter++;

                }
                return null;
            }catch(Exception e){
                return null;
            }

        }

        /// <summary>
        /// Play next song
        /// </summary>
        public void NextSong()
        {
            GetPlayingSection().Parent.NextSong();
        }
        int diffX = 0;
        int diffY = 0;
        int top = 0;
        BufferedGraphicsContext D;
        /// <summary>
        /// Method to add new items to the list at runtime. Only applies to item of type 'entry'
        /// </summary>
        /// <param name="Uri">The uri of the item</param>
        /// <param name="Title">The title of the item</param>
        /// <param name="Attributs">String array of attributes</param>
        /// <param name="uris">String array of uris (hrefs) of attributes</param>
        public Element AddItem(String Uri, String Title, String[] Attributs, String[] uris,int width,int height)
        {
            Element newItem = new Element(CurSection,this);
            newItem.Type = "entry";
            newItem.SetAttribute("title", Title);
            newItem.SetAttribute("width", width.ToString());

            newItem.SetAttribute("height", height.ToString());
            newItem.SetAttribute("uri", Uri);
            newItem.SetAttribute("href", Uri);
            newItem.SetAttribute("type", "entry");
            
            this.CurSection.AddElement(newItem,this.currentView.Content);
            return newItem;
        }
        /// <summary>
        /// Gets and sets the selected index . Returns -1 if no entries was found. Applies only with elements of type "entry".
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                // index counter
                int i=0;
                bool foundSelected = false;

                
                foreach (Element d in this.ViewBuffer)
                {
                    if (d.Type != "entry")
                        continue;
                    // if the previous gave foundSelected set this to selected
                  

                        if (d.Selected==true)
                            return i;
                   
                    i++;
                }
                return -1;
            }
            set
            {
                if (!this.Focused)
                    return  ;
                // Deactivate the selected items
                foreach (Element d in this.ViewBuffer)
                {

                    if (d.Type != "entry")
                        continue;
                    d.Selected=false;
                          
                  
                }
                // Set the item at the index as selected
                int index = 0;
                foreach (Element d in this.ViewBuffer)
                {
                    if (d.Type != "entry")
                        continue;
                    // if index meets the setter, mark it as selected
                    if(index==value)
                        d.Selected= true;

                    index++;
                 


                }
            }

        }
        public void RemoveAllPlaying()
        {
            foreach (View d in this.Views.Values)
            {
                if (d.Content != null)
                {
                    if (d.Content.View != null)
                    {
                        if (d.Content.View.Sections != null)
                        {
                            foreach (Section t in d.Content.View.Sections)
                            {
                                for (int i = 0; i < t.Elements.Count; i++)
                                {
                                    if (t.Elements[i].Entry)
                                    {
                                        if (t.Elements[i].GetAttribute("__playing") == "true")
                                        {
                                            t.Elements[i].SetAttribute("__playing", "");

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Get the section which has the current playing item
        /// </summary>
        /// <returns></returns>
        public Section GetPlayingSection()
        {
            foreach (View d in this.Views.Values)
            {
                if (d.Content != null)
                {
                    foreach (Section t in d.Content.View.Sections)
                    {
                        foreach (Element _elm in t.Elements)
                        {
                            if (_elm.Entry)
                            {
                                if (_elm.GetAttribute("__playing") == "true")
                                {
                                    return t;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
        public List<Element> SelectedItems
        {
            get
            {
                List<Element> selectedItems = new List<Element>();
                foreach (Element elm in this.ViewBuffer)
                {
                    if (elm.Selected)
                        selectedItems.Add(elm);
                }
                return selectedItems;
            }
        }

  

        /// <summary>
        /// Decides if control has focus
        /// </summary>
        public new bool Focus { get; set; }


       

        /// <summary>
        /// Method to handle keys in list
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            if (!this.Focus)
                return base.IsInputKey(keyData); ;
            switch(keyData)
            {
              
                case Keys.Enter:
                    if (this.SelectedItems.Count > 0)
                        if (PlaybackRequested != null)
                        {
                            // Set no any items as playing
                            RemoveAllPlaying();
                            // Set the current item as playing
                            this.SelectedItems[0].SetAttribute("__playing", "true");
                            PlaybackRequested(this.SelectedItems[0], this.SelectedItems[0].GetAttribute("uri"));
                        }
                    return true;
                case Keys.Up:
                    // Move the selected element to the previous one
                    SelectedIndex--;
                    if(this.SelectedItems.Count > 0)
                      this.SelectedItems[0].AssertSelection();
                    return true;
                    break;
                case Keys.Down:
                    // Move the selected element to the next
                    SelectedIndex++;
                    if (this.SelectedItems.Count > 0)
                        this.SelectedItems[this.SelectedItems.Count-1].AssertSelection();
                    return true;
                
                default:
                    return base.IsInputKey(keyData);
            }
            return base.IsInputKey(keyData);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw(this.CreateGraphics());


           

        }
        /// <summary>
        /// Scroll X (Not in use yeat)
        /// </summary>
        public int ScrollX
        {
            get
            {
                return scrollX;
            }
            set
            {
                scrollX = value;
            }
        }

        
        public enum ParseMode
        {
            Beginning,Attribute,Value
        }
        /// <summary>
        /// Parses an tag and result it as an element
        /// </summary>
        /// <param name="src">The source string</param>
        /// <param name="i">Current position of the string</param>
        /// <returns>An element</returns>
        private Element ParseTag(string src, ref int i)
        {
            i++;
            Element Result = new Element(this.CurSection,this);             // The result element to inflate
             /**
             * Current character mode 
             * [ 0 = Beginning of tag,
             *   1 = At attribute name,
             *   2 = inside attribute value ]
             *   */
                ParseMode currentState = ParseMode.Beginning;

                /**
                 * If the cursor is inside an "" this variable
                 * will be true, and if so it will skip the whitespace
                 * */

                
                bool insideString = false;                                  // Denotates if the current pointer is in the string
                StringBuilder elementBuffer = new StringBuilder();
                StringBuilder valueBuffer = new StringBuilder();                 // The buffer of chars for the current token
                StringBuilder attributeBuffer = new StringBuilder();
            StringBuilder InsideBuffer = new StringBuilder();
                String bufferReady = "";                                    // The buffer of prevous ready string (attribute name)

                String elementName = ""; 
            
            string tag = src;


            bool insideElement=false;                                       // denotates whether inside the elemetn
            for (;i < src.Length;i++)
            {
                
                // current character
                char token = src[i];
    
                    if (token == '"')
                    {
                        insideString = !insideString;
                        continue;
                    }
                    /** 
                     * If inside the element assert the inner bounds
                     * */
                    if(insideElement)
                    {
                        // If the inside string reaches it's end return to to end mode
                        if(token == '<')
                        {
                            // Set result data to inner content
                            Result.Data=InsideBuffer.ToString();
                            insideString = false;
                            i += elementName.Length + 2;
                            return Result;
                        }
                        // Append the token
                        InsideBuffer.Append(token);
                    }
                    // If the current position is on an /> return
                    if(src[i] == '/' && src[i+1]=='>' && !insideString && !insideElement)
                    {
                        i += 2;
                        return Result;
                    }
            
                    switch (currentState)
                    {
                        case ParseMode.Attribute:
                            // if the current token is an whitespace and previous was it skip the current token
                            if(src[i-1]==' '&&token==' ')
                                continue;
                            if (token == '=' && !insideString)
                            {
                                /** Flush the buffer and move the content
                                 * to the attribute bufffer */
                                
                              
                                // Set parse mode to attribute
                                currentState = ParseMode.Value;
                                continue;
                                

                            }
                            attributeBuffer.Append(token);
                            continue;
                            
                        case ParseMode.Value:
                            if ((token == ' ' || token == '>' )&& !insideString)
                            {
                                
                                // Get the value
                                String value = valueBuffer.ToString();
                               
                                // Create element's attribute
                                Board.Attribute d = new Attribute() { name = attributeBuffer.ToString(), value = value };
                                attributeBuffer.Clear();
                                // add the attribute to the element
                                Result.Attributes.Add(d);
                                
                                // If not inside string and reach > or / return
                                if ((token == '>' || token == '/') && !insideString)
                                {
                                    insideElement = true;
                                    continue;
                                }
                                else
                                {
                                    currentState = ParseMode.Attribute;
                                }

                                // Clear value buffer
                                valueBuffer.Clear();
                                continue;
                            }
                            if(!insideString)
                            {}
                            valueBuffer.Append(token);
                            break;
                        case ParseMode.Beginning:
                            if (token == ' ')
                            {
                                if(i>0)
                                if (tag[i - 1] == ' ')
                                    continue;
                                elementName = elementBuffer.ToString();

                                // Set the type of element to the element's tag
                                Result.Type = elementName;
                                Result.SetAttribute("type",elementName);

                                // Set parse mode to attribute
                                currentState = ParseMode.Attribute;
                                continue;
                            }
                            // Append the char to the element name
                            elementBuffer.Append(token);
                            break;
                       

                    }

                    // Otherwise append the char to the current buffer
                    


                }
            return Result;
            }


        
        /// <summary>
        /// Method to dynamically draw text and include subsets of elements inside the markup of the text
        /// </summary>
        /// <param name="elm"></param>
        /// <param name="font">The font to use (measuring scale)</param>
        /// <param name="g">The graphics to use</param>
        /// <param name="position">Rectangle of position</param>
        /// <param name="left">Left position of character start</param>
        /// <param name="row">row on character start</param>
        public void DrawText(String xml,Element elm,Font font,Graphics g,Brush fontBrush,Rectangle position,ref int entryship,ref int left,ref int row)
        {
            List<Element> elementsToShow = new List<Element>();
            foreach (Element d in elm.Elements)
            {
                elementsToShow.Add(d);
            }
  


            // Denotaes if inside XML
           bool insideXML = false;

            // Position inside element
           int elmPos = 0;

            // Current element length
           int currentElmLength = 0;
            // string buffer
            StringBuilder buffer = new StringBuilder();

            int tagLevel = 0;
           
            // Iterate through all characters and build the text
            for (var i = 0; i < xml.Length; i++)
            {
                
                // Get the current char
                char d = xml[i];
               
                // if a newline break
                if (d == '\n')
                {
                    row += 8;
                    left = 0;
                    continue;
                }
                if (d == '\t')
                {
                    left += 10;
                }
                
                // boolean indicating sub elements were found
                bool elmFound = false;
                if (d == '<')
                {
                    // if char indicates on < start parse an tag
                    Element _elm = ParseTag(xml, ref i);
                    _elm.Font = font;
                    
                    // Create the bounds for the new element
                    Rectangle elementBounds = new Rectangle(elm.Left, elm.Top -scrollY, _elm.Width, _elm.Height);
                    /**
                     * If the cursor position is inside bounds of the element 
                     * set the element as hovered */
                    if (_elm.Data != null)
                    {
                        elementBounds.Width = (int)GetTextWidth(_elm.Data, _elm.Font, g);
                        elementBounds.Height = (int)GetTextHeight(_elm.Data, _elm.Font, g);
                    }
                    if (mouseX >= elementBounds.Left +left && mouseX <= elementBounds.Right+left &&
                        mouseY >= elementBounds.Top +row && mouseY <= elementBounds.Bottom +top+ row)
                    {
                        this.HoveredElement = _elm;
                    }
                    DrawElement(_elm, g, ref entryship,elementBounds , 0, ref left, ref row);
                    
                  
                    continue;
                }

                
               
                if (!elmFound&&tagLevel < 1)
                {
                    if (d == '>')
                        continue;
                    if (d == ' ')
                        left += 5;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.DrawString(d.ToString(), font, fontBrush, position.Left + left, position.Top + row);
                    left += (int)Math.Floor(GetTextWidth(d.ToString(), font,g));
                }
               
                // if the character exceeds the width separate it to new row
                if (left >= this.Width-10)
                {
                    row += (int)(g.MeasureString(d.ToString(), font).Height*1.5);
                    left = 0;
                }
                elmPos++;
            
            }
        
        }

        private float GetTextWidth(string p, System.Drawing.Font font,Graphics d)
        {
            return d.MeasureString(p, font).Width - 3;
        }

        private float GetTextHeight(string p, System.Drawing.Font font, Graphics d)
        {
            return d.MeasureString(p, font).Height;
        }
        /// <summary>
        /// The vertical scroll range
        /// </summary>
        public int ScrollY
        {
            get
            {
                return scrollY;
            }
            set
            {
                scrollY = value;
            }
        }



        /// <summary>
        /// The default typeface to use for the view
        /// </summary>
        public string FontFace
        {
            get
            {
                return Font.FontFamily.Name;
            }
            set
            {
                Font = new Font(value, Font.Size, Font.Style);
            }
        }

        /// <summary>
        /// Function to draw an element on an view view
        /// </summary>
        /// <param name="_Element">The element to draw</param>
        /// <param name="ptop">The previous top position on previous views (in direct coordinates)</param>
        /// <param name="d">The graphics to draw on</param>
        /// <param name="padding">The padding to use when nesting child elements</param>
   
        /// <param name="entryship">The number of entry placed</param>
        public void DrawElement(Element _Element, Graphics d,ref int entryship,Rectangle Bounds,int padding,ref int t_left,ref int t_row)
        {
            
          
            /**
             * Get screen coordinates of the element
             * */
          

            int height = Bounds.Height;
            int left = Bounds.Left;
            int top =  Bounds.Top ;
            int width = Bounds.Width;

            /**
             * Check if the mouse is inside the element's bounds 
             * and if so set it as the hover element. The bounds are relative to the element's text
             * position
             * */

            if (mouseX >= Bounds.Left + t_left && mouseX <= Bounds.Right + t_left &&
                mouseY >= Bounds.Top + t_row && mouseY <= Bounds.Bottom + t_row)
                this.HoveredElement = _Element;


            /**
             * For consistency we apply selected rules to all kind of elements, not only the entry element but
             * also to preserve worrk in the future
             * */

            // Decide color of the entry wheather it selected or not.
            Color EnTry = (entryship % 2) == 1 ? Entry : Alt;
            if(_Element.Entry)
            if (_Element.Selected == true)
            {
                EnTry = Color.FromArgb(169, 217, 254);
                // If the control is inactive draw it gray
                if (!this.Focused)
                {
                    EnTry = Color.Gray;
                }
            }
            Color ForeGround = Fg;
            if(_Element.Entry)
            if (_Element.Selected == true)
            {
                
                ForeGround = Color.DarkBlue;
                if (!this.Focused)
                {
                    ForeGround = Color.DarkGray;
                }
               
            }
            
         /*   // Font size for the font
            // Try to find text size otherwise apply default
            int textSize = 0;
            int.TryParse(_Element.GetAttribute("size"), out textSize);
            if (textSize == 0)
                textSize = 8;

            String fontName = this.Font.FontFamily;
            if (_Element.GetAttribute("font") != "")
            {
                fontName = _Element.GetAttribute("font");
            }*/

            // Assert font properties
            _Element.AssertFont();
            // if element is hovred, set the font to be underlined
            bool hovered = (_Element.GetAttribute("hover") == "true");
            Font labelFont = new Font(this.Font.FontFamily,this.Font.Size/100,FontStyle.Regular,GraphicsUnit.Inch);
            if (_Element.Font != null)
                labelFont = _Element.Font;




            // Reset entryship if element is not an entry
            if (!_Element.Entry)
                entryship = 0;
            // 

            // draw selection background but currently only for entry
            
           //     d.FillRectangle(new SolidBrush(EnTry), new Rectangle(left, top, width, height));
            /**
            * For all types of element
            * */
            switch (_Element.Type)
            {
                case "br":
                    t_row += (int)d.MeasureString("ABCD", _Element.Font).Height;
                    t_left = 0;
                    return;
                 

                case "entry":

                    
                  
                    /*      if (_Element.GetAttribute("position") == ("absolute"))
                          {
                              int _left = int.Parse(_Element.GetAttribute("left"));
                              int _top = int.Parse(_Element.GetAttribute("top"));
                              int _width = int.Parse(_Element.GetAttribute("width"));
                              int _height = int.Parse(_Element.GetAttribute("height"));

                              d.FillRectangle(new SolidBrush(EnTry), new Rectangle(_left - scrollX, _top - scrollY, _width, _height));
                              d.DrawString(_Element.GetAttribute("no"), new Font(FontFace, 8), new SolidBrush(Spirit.MainForm.FadeColor(-0.4f, ForeGround)), new Point(_left + 1 - scrollX, _top + 2 - scrollY));

                              d.DrawString(_Element.GetAttribute("title"), new Font(FontFace, 8), new SolidBrush(ForeGround), new Point(_left + 20 - scrollX, _top + 2 - scrollY));
                              d.DrawString(_Element.GetAttribute("author"), new Font(FontFace, 8), new SolidBrush(ForeGround), new Point(_left + 320 - scrollX, _top + 2 - scrollY));
                              d.DrawString(_Element.GetAttribute("collection"), new Font(FontFace, 8 ), new SolidBrush(ForeGround), new Point(_left + 435 - scrollX, _top + 2 - scrollY));
                              top += ROWHEIGHT;
                              entryship++;
                              break;
                          }*/
                    // For some unknown reason ptop crashes
                    // TODO: Fixthis this in another way
                   // top -= height / 2;
                  //  if (Alternate)
                    {
                        d.FillRectangle(new SolidBrush(Color.FromArgb(65,EnTry)), new Rectangle(left, top, width, height));
                    }
                  /*  else
                    {
                        d.DrawLine(new Pen(Color.FromArgb(15,Divider)), new Point(left, top + height), new Point(width, top + height));
                    }
                    */
                /* d.DrawString(_Element.GetAttribute("no"), labelFont, new SolidBrush(MainForm.FadeColor(-0.4f, ForeGround)), new Point(LEFT + 1, top));

                          d.DrawString(_Element.GetAttribute("title"), labelFont, new SolidBrush(ForeGround), new Point(left + 20, top + 2));
                          d.DrawString(_Element.GetAttribute("author"), labelFont, new SolidBrush(ForeGround), new Point(left + ARTISTLEFT, top + 2));
                          d.DrawString(_Element.GetAttribute("collection"), labelFont, new SolidBrush(ForeGround), new Point(left + 435, top + 2));
                          */
                    // draw all attributes specified by the column handlers:

                        int column_position = left;
                        foreach (KeyValuePair<string, int> Column in Columns)
                        {
                           

                            // FOnt for use to the column
                            Font setFont = labelFont;
                            int column = Column.Value;
                            String title = Column.Key.ToLower();
                            // Set special color if provided
                            Color fg = ForeGround;
                            if (_Element.GetAttribute("color_" + title) != "" && !_Element.Selected)
                            {
                                fg = ColorTranslator.FromHtml(_Element.GetAttribute("color_" + title));
                            }
                            // If cursor is inside the boundaries, the column has an link then underline the font
                            if (mouseX >= column_position && mouseX <= column_position + column && mouseY >= top && mouseY <= top + height &&_Element.GetAttribute("href_"+title.ToLower())!="")  
                            {
                                setFont = new Font(setFont, FontStyle.Underline);
                            }
                           d.DrawString(_Element.GetAttribute(Column.Key.ToLower()), setFont, new SolidBrush(fg), new Point( column_position, top + 2));
                       
                            /***
                             * 2011-04-25 11:08  STOCKHOLM 
                             * Draw child elements!
                             * */
                           foreach (Element _elm in _Element.Elements)
                           {
                               if (_elm.GetAttribute("elm") == "")
                                   continue;
                               // If the element's column attribute maatch the entry one draw it on the specified column
                               if (_elm.GetAttribute("column") == Column.Key.ToString())
                               {
                                   // The bounds of the element. The elements position should be relative to the entry's position
                                   Rectangle elmBounds = new Rectangle(column_position,top+2,_elm.Width,_elm.Height);
                                   /**
                                    * The sub item should not affect the current entryship so
                                    * we create an new dummy _entryship value assigned to zero
                                    * so we can pass it*/
                                   int _entryship = 0;
                                   DrawElement(_elm, d,ref _entryship, elmBounds, 0,ref t_left,ref t_row);
                               }
                           }
                             column_position += column;
                            
                        }

                       

                    // If the entry is playing, draw the playback icon

                    if (_Element.GetAttribute("__playing") == "true")
                    {
                        Bitmap speaker_icon = Resource1.speaker;

                        // If spekaer is selected draw the selected icon
                        if(_Element.Selected)
                            speaker_icon=Resource1.speaker_selected;
                        d.DrawImage(speaker_icon, new Rectangle(left, top, 16, 16));
                    }
                    entryship++;
              
                    break;

               

                
                case "img":
                case "image":
                    bool hasShadow=_Element.GetAttribute("shadow")!="";
                  
                   // Images.TryGetValue(_Element.GetAttribute("src"), out Rs);
                   
                    if (!_Element.ImageRequested)
                    {
                        // Set image requested to true
                        _Element.ImageRequested = true;
                        Thread D = new Thread(DownloadImage);
                        D.Start((object)_Element);
                    }
                
                    if (_Element.Bitmap == null)
                        _Element.Bitmap = Resource1.release;
                  
                     DrawImage(_Element.Bitmap, new Rectangle(left+t_left, top+t_row, width, height),d,hasShadow);
                        
                    
                        // But if the element hasn't been called before, start an downloading of the image
                   
                    break;


                default:
                  
                    
                 // Try use custom color, otherwise by default
                    Color Foreground = Fg;

                   
                    String strColor = _Element.GetAttribute("color");
                    if (strColor != "")
                    {
                        Foreground = ColorTranslator.FromHtml(strColor);
                    }
                    // if the element is an link underline the text
                    if (mouseX >= _Element.Left && mouseY >= _Element.Top && mouseX < _Element.Left + _Element.Width && mouseY < _Element.Top + _Element.Height && _Element.GetAttribute("href")!="")
                    {
                        Foreground = Color.White;
                        labelFont = new Font(labelFont, FontStyle.Underline);
                    }
                  
                       /*

                    if (_Element.GetAttribute("position") == "absolute")
                    {

                        d.DrawString(_Element.Data, labelFont, new SolidBrush(Foreground), new RectangleF(left, top, width, height));
                        break;
                    }
                    if(_Element.GetAttribute("shadow")=="true")
                    {
                     d.DrawString(_Element.GetAttribute("text"), labelFont, new SolidBrush(Color.Black), new Point(left, top+2));
                    }
                    d.DrawString(_Element.GetAttribute("text"), labelFont, new SolidBrush(Foreground), new Point(left, top));

                    /**
                     * Get hyperlinks
                     * */
                    int row=0;
                    if(_Element.Data!=null)
                        DrawText((_Element.Data),_Element,labelFont,d, new SolidBrush(ForeGround), new Rectangle(new Point(left, top + 2),new Size(width,height)),ref entryship,ref  t_left,ref t_row);

                       
                   
                    break;
                case "button":
                    break;
                case "section":
                   
                  //  d.FillRectangle(new SolidBrush(Section), new Rectangle(0, top, width, height));
                    d.DrawImage(Resource1.sectionbar, 0, top, width, height);  
                  d.DrawString(_Element.Data, new Font(labelFont.FontFamily, 15.0f, FontStyle.Bold, GraphicsUnit.Pixel), new SolidBrush(SectionTextShadow), new Point(left + 30, top + 1));
                    d.DrawString(_Element.Data, new Font(labelFont.FontFamily, 15.0f,FontStyle.Bold, GraphicsUnit.Pixel), new SolidBrush(SectionText), new Point(left + 30, top));
                   
                    break;
                case "divider":
           
                    d.DrawLine(new Pen(Divider), left, top-height/2, left + width, top-height/2);
                 
                    break;
                case "space":

                    break;
                case "div":
                    Color bg = Bg;
                    try
                    {
                       bg= ColorTranslator.FromHtml(_Element.GetAttribute("bgcolor"));
                    }
                    catch
                    {
                    }
                    d.FillRectangle(new SolidBrush(bg), Bounds);
                    break;

            }
            // Say the element has been called for the first time
         
            // Draw all child elements with coordinates relative to the current element (nesting)
            if (_Element.Type == "div")
            {
                foreach (Element rt in _Element.Elements)
                {

                    // Get bounds
                    Rectangle ElementBounds = new Rectangle(_Element.Left + Bounds.Left + padding, _Element.Top + Bounds.Top + padding, Bounds.Width - (padding * 2), Bounds.Height - (padding * 2));

                    DrawElement(rt, d, ref entryship, ElementBounds, padding,ref t_left,ref t_row);
                }
            }
            // increase ptop
            
        }
        /// <summary>
        /// Distance between tabs
        /// </summary>
        int tab_distance = 10;

        /// <summary>
        /// Bitmap for the inactive section tab
        /// </summary>
        public Bitmap inactive_section_tab;
       
        /// <summary>
        /// Bitmap for the section tab
        /// </summary>
        public Bitmap sectionTab;
        
         /// <summary>
         ///  the horizontal position where the tab starts
         /// </summary>
        int tabbar_start = 10;

        /// <summary>
        ///  height of the bounding_box
        /// </summary>
        int tabbar_height = 21;

        /// <summary>
        /// Width for active section tab
        /// </summary>
        int tab_width = 71;



        /// <summary>
        /// Primary size for the scrollbar 
        /// </summary>
        

        
        int scrollbar_size = 18;
        int tab_text_margin = 10;

        int bar_offset = 8;
        int bar_height = 10;

        // The height of the scrollbar space
        int spaceHeight
        {
            get
            {
                return this.Height - scrollbar_size * 2;
            }
        }

        // The relativity to the space height and the total length of items
        int thumbHeight
        {
            get
            {
                return (int)Math.Round(((float)this.Height / (float)TotalHeight) * (spaceHeight - scrollbar_size*2));
            }
        }

        // Get top for the scrollbar
        int scrollTop
        {
            get
            {
                return (int)Math.Round(((float)this.Height / (float)TotalHeight) * scrollY);
            }
        }
        /// <summary>
        /// Height of columnheader
        /// </summary>
        int columnheader_height = 16;
        /// <summary>
        /// Method t odraw the ColumnHeaders
        /// </summary>
        /// <param name="p">The graphic engine to use</param>
        /// <param name="point"> The point to draw the columnheader on</param>
        private void DrawHeaders(Graphics p, System.Drawing.Point point)
        {

            // fill the columnheader background
            int scrollOffset = (this.ItemOffset > 0 ?scrollbar_size:  0);
            p.FillRectangle(new LinearGradientBrush(new Point(point.X,point.Y),new Point(point.X,point.Y+columnheader_height),Color.FromArgb(210,210,210),Color.FromArgb(180,180,180)),new Rectangle(point,new Size(this.Width-scrollOffset,columnheader_height)));
            // draw border line
            p.DrawLine(new Pen(Color.Black), new Point(0, point.Y + columnheader_height - 1), new Point(this.Width - scrollOffset, point.Y + columnheader_height - 1));
            p.DrawLine(new Pen(Color.White), new Point(0,point.Y ), new Point(this.Width-scrollOffset, point.Y ));

            //draw all columns

            // current position of the column header
            int current_position = point.X;
            int text_top = 2;

            // Draw headers
            foreach (KeyValuePair<String, int> column in Columns)
            {
                
                p.DrawString(column.Key, new Font(FontFace, 8, FontStyle.Bold), new SolidBrush(Color.White), new Point(current_position, text_top+point.Y+1));
                p.DrawString(column.Key, new Font(FontFace, 8, FontStyle.Bold), new SolidBrush(Color.Black), new Point(current_position, text_top+point.Y));
                current_position += column.Value;
            }

        }

        private Element hoveredElement;
        /// <summary>
        /// The element the mouse cursor is hovering on
        /// </summary>
        public Element HoveredElement
        {
            get
            {
                return hoveredElement;
            }
            set
            {
                hoveredElement = value;
                if (dragging)
                    return;
                if (hoveredElement.GetAttribute("href") != "" )
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Gets the space of the scrollY
        /// </summary>
        int scroll_space
        {
            get
            {
                return (this.Height - scrollbar_size );
            }
        }
      
        /// <summary>
        ///  Gets the free space of the thumb.
        /// </summary>

        int free_height
        {
            get
            {
                return scroll_space - ThumbSize;
            }
        }

        /// <summary>
        /// Gets the size of the thumb
        /// </summary>
        int ThumbSize
        {
            get
            {
                return (int) ((float)ScrollOffset * (float)scroll_space);
            }
        }
        /// <summary>
        /// Gets the scroll offset
        /// </summary>
        float ScrollOffset
        {
            get
            {
                return (float)this.Height / ((float)TotalHeight );
            }
        }

        /// <summary>
        /// The step of the scrollY in the offset
        /// </summary>
        float ScrollStep
        {
            get
            {
                if(TotalHeight>0)
                  return( this.free_height * ScrollOffset )*scrollY ;
                return 0;
            }
           
        }

        /// <summary>
        /// Returns the maximum scrollY
        /// </summary>
        int MaxScrollY
        {
            get
            {
                return (this.TotalHeight - this.Height);
            }
        }
        /// <summary>
        /// Sets the scroll Y
        /// </summary>

        int ThumbScrollY
        {
            get
            {
                return (int)(((float)scrollY / MaxScrollY) * (this.free_height));
            }
            set
            {
                if(value*ScrollStep <= MaxScrollY && value*ScrollStep >=0)
                scrollY = (int)(value * ScrollStep);
            }
        }

        /// <summary>
        /// Determines if an reordering progress is ongoing.
        /// </summary>
        private bool reoredering = false;

        /// <summary>
        /// Draw inside an certain view. Mouse events are measured here instead of mousemove
        /// </summary>
        /// <param name="p">The graphics buffer</param>
        /// <param name="CurrentView"> The view to base from</param>


        
        // integer for the current tab which is hovered
        private int hovered_tab = -1;

        /// <summary>
        /// Draws the view
        /// </summary>
        /// <param name="p">The graphics to draw with</param>
        public void Draw(Graphics p)
        {
            
      //      try
            {
          /*      this.pictureBox1.Left = this.Width / 2 - this.pictureBox1.Width / 2;
                this.pictureBox1.Top = this.Height / 2 - this.pictureBox1.Height / 2;
                // Show progress icon if current view is loading...
                this.pictureBox1.Visible = this.CurrentView.Content == null;*/
             // Color.FromArgb(50, 50, 50);

                if (CurrentView != null)
                {
                    if (CurrentView.Content != null)
                        if (CurrentView.Content.View == null)
                            CurrentView.Content.Serialize();
                }

                int entryship = 0;

                // Top increases after all elements to get a page feeling 
                // (next element below the previous, when elements has an @TOP value as top paramater (-1))
                int ptop = 20;

                if (D == null)
                    D = new BufferedGraphicsContext();
                BufferedGraphics R = D.Allocate(p, new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height));
                Graphics d = R.Graphics;
                d.FillRectangle(new SolidBrush(Bg), new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height));
                
                // Draw background image

                if (this.Background != null)
                    d.DrawImage(this.Background, new Rectangle(0, 0, this.Width, this.Height));
                /**
                 * If the currentView isn't null begin draw all elements on the board
                 * */
                if (CurrentView != null)
                    if (CurrentView.Content != null)
                        if (CurrentView.Content.View != null)
                            for (int i = 0; i < ViewBuffer.Count; i++)
                            {
                                // Calculate the view coordinates of the element

                                Element _Element = ViewBuffer[i];
                                Rectangle ScreenCoordinates = _Element.GetCoordinates(scrollX, scrollY, this.Bounds, 0);
                                // Draw the element and it's children
                                if (ScreenCoordinates.Bottom < 0 || ScreenCoordinates.Top > this.Height)
                                    continue;
                                int t_left = 0, t_row =  0;
                                DrawElement(_Element, d, ref entryship, ScreenCoordinates, 3,ref t_left,ref t_row);







                            }




                /**
                 * If reordering draw lines
                 * */
                if (reoredering)
                {

                  
                }
                /***
                 * Draw the tab header on top
                 * */


                // draw an bounding rectangle
                d.DrawImage(Resource1.toolbar, new Rectangle(0, 0, (int)Math.Round(this.Width * 1.1f), 22));
                //d.DrawLine(new Pen(Color.FromArgb(128, 128, 128)), new Point(0, tabbar_height), new Point(this.Bounds.Width, tabbar_height));

                // Position counter for tabbar. Useful for meausing toolitems
                int position_counter = tabbar_start;
                if (CurrentView != null)
                    if (CurrentView.Content != null)
                        if (CurrentView.Content.View != null)
                        {
                            // reset hovered tab
                            hovered_tab = -1;
                            // draw the bar
                            position_counter = tabbar_start;
                            // Draw all section bar
                            for (int i = 0; i < CurrentView.Content.View.Sections.Count; i++)
                            {
                                Section section = CurrentView.Content.View.Sections[i];
                                int tab_width = (int)d.MeasureString(section.Name, new Font(FontFace, 10)).Width;


                                // If the mouse cursor is pointing on an tab, raise it
                                if (mouseX >= position_counter && mouseX <= position_counter + tab_width + tab_distance * 2 &&
                                    mouseY < tabbar_height

                                    )
                                {
                                    hovered_tab = i;
                                }



                                // if you are at the current section draw the panes


                                if (currentSection == i)
                                {

                                    // Draw section tab, load if nulll
                                    if (sectionTab == null)
                                    {
                                        sectionTab = Resource1.tab;
                                    }
                                    // draw the tab bar
                                    d.DrawImage(sectionTab, new Rectangle(position_counter, 1, tab_width + tab_distance * 2, tabbar_height));

                                    // draw the tab background
                                    d.DrawString(section.Name, new Font(FontFace, 10), new SolidBrush(Color.FromArgb(255, 255, 211)), new Point(position_counter + tab_distance, tab_text_margin / 5));
                                }
                                else
                                {


                                    // draw the tab bar
                                    d.DrawImage(Resource1.tab_separator, new Rectangle(position_counter + tab_width + tab_distance * 2, 0, 2, tabbar_height - 1));


                                    d.DrawString(section.Name, new Font(FontFace, 10), new SolidBrush(Color.White), new Point(position_counter + tab_distance, tab_text_margin / 5));
                                    d.DrawString(section.Name, new Font(FontFace, 10), new SolidBrush(Color.Black), new Point(position_counter + tab_distance, +tab_text_margin / 5 - 1));
                                }
                                position_counter += tab_width + tab_distance * 2;
                            }
                        }
                        else
                        {
                        }
#if (nobug)
                /**
                 * Draw the scrollbar
                 * */
                /**
                 * If the height of the total items is more than the visible space draw the scrollbar
                 * */

                if (this.ItemOffset > 0)
                {
                    d.DrawImage(Resource1.scrollbar_up, new Rectangle(this.Width - scrollbar_size, 1, scrollbar_size, scrollbar_size));

                    // The space of the scrollbar
                    d.DrawImage(Resource1.scrollbar_space, new Rectangle(this.Width - scrollbar_size, Resource1.scrollbar_up.Height, scrollbar_size, this.Height));
                    d.DrawImage(Resource1.scrollbar_down, new Rectangle(this.Width - scrollbar_size, this.Height - scrollbar_size, scrollbar_size, scrollbar_size));


                    // Draw the upper part of the thumb

                    /**
                     * Get the height of the scrollbar
                     * */

                    // The position of the scrollbar graphically

                    



                /*    d.DrawImage(Resource1.scrollbar_thumb, new Rectangle(this.Width - scrollbar_size, scrollbar_size + scrollTop, scrollbar_size, bar_offset), new Rectangle(0, 0, scrollbar_size, bar_offset), GraphicsUnit.Pixel);

                    d.DrawImage(Resource1.scrollbar_thumb, new Rectangle(this.Width - scrollbar_size, scrollbar_size + scrollTop + bar_offset, scrollbar_size, +thumbHeight), new Rectangle(0, bar_offset, scrollbar_size, 3), GraphicsUnit.Pixel);
                    d.DrawImage(Resource1.scrollbar_thumb, new Rectangle(this.Width - scrollbar_size, scrollbar_size + scrollTop + bar_offset + thumbHeight, scrollbar_size, bar_offset), new Rectangle(0, Resource1.scrollbar_thumb.Height - bar_offset, scrollbar_size, bar_offset), GraphicsUnit.Pixel);
               */
                    d.DrawImage(Resource1.scrollbar_thumb, new Rectangle(this.Width - scrollbar_size, scrollbar_size+ThumbScrollY, scrollbar_size, +thumbHeight), new Rectangle(0, bar_offset, scrollbar_size, 3), GraphicsUnit.Pixel);
             
                }
#endif
                /***
                * If the Section is an list, draw listheaders
                * */
                if(CurrentView!=null)
                if (CurrentView.Content != null)
                    if (CurrentView.Content.View != null)
                        if (CurrentView.Content.View.Sections[currentSection].List)
                        {
                            // Get the first entry element
                            Element firstEntry = null;
                            foreach (Element elm in ViewBuffer)
                            {
                                if (elm.Entry)
                                {
                                    firstEntry = elm;
                                    break;
                                }
                            }
                            // If an first entry was found draw the headers straight above it or on the top of the view
                            if (firstEntry != null)
                            {
                                Rectangle bounds = firstEntry.GetCoordinates(scrollX, scrollY, new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height), 0);

                                // if the first entry top were above the visible coordinates draw it on the top
                                if (bounds.Top < tabbar_height + bounds.Height)
                                {
                                    DrawHeaders(d, new Point(0, +tabbar_height));
                                }
                                else
                                {
                                    DrawHeaders(d, new Point(0, bounds.Top - bounds.Height));
                                }
                            }
                        }
                /***
                 * Draw toolbar and menubar
                 * */
                if (ViewExist())
                {
                    // Calculate the width of the menubar
                    int size = 0;
                    int menupadding = 30;
                    // current position of item
                    
                   /* int position=position_counter;
                    foreach (Element _elm in this.currentView.Content.View.Toolbar.Items)
                    {
                        
                        // preposition
                        int preposition = position;

                        
                        // Width of the string
                        int string_width = (int)d.MeasureString(_elm.GetAttribute("title"), new Font(FontFace, 9)).Width ;

                        int width = string_width + menupadding * 2;
                        d.FillRectangle(new SolidBrush(Color.FromArgb(43, 43, 43)), new Rectangle(preposition, 0, width, tabbar_height-2));

                        // If mouse is over, draw bounding box

                        if (mouseX >= preposition && mouseX <= preposition + width && mouseY <= tabbar_height)
                        {
                            d.FillRectangle(new SolidBrush(Color.FromArgb(87,87,87)), new Rectangle(preposition, 3, (preposition+width) - preposition, tabbar_height -2));
                        }

                        // If the element is an menu draw menu
                        if (_elm.GetAttribute("type") == "menu")
                        {
                            d.DrawImage(Resource1.dropdown, new Point(position + 3, 2));
                        }
                        d.DrawString(_elm.GetAttribute("title"), new Font(FontFace, 9), new SolidBrush(Color.White), new Point(position + menupadding, 2));
                        position += menupadding +string_width+ menupadding;
                       
                        
                    }
                    
                   foreach (Element _elm in this.currentView.Content.View.Toolbar.Items)
                   {
                       int prePosition = position;

                       // Draw text and measure the position for the next entry


                       // The item is an menu, draw the menu image

                       d.DrawString(_elm.GetAttribute("text"), new Font(FontFace, 9), new SolidBrush(Color.White), new Point(position, 2));
                       position += menupadding + (int)d.MeasureString(_elm.GetAttribute("text"), new Font(FontFace, 9)).Width + menupadding;
                       if (_elm.Type == "menu")
                       {
                           d.DrawImage(Resource1.dropdown, new Point(2, position - menupadding));
                       }
                       // If mouseover mark the item
                       if (mouseX >= prePosition && mouseX <= position && mouseY < tabbar_height)
                       {
                           d.DrawRectangle(new Pen(Color.FromArgb(255, 255, 211)), new Rectangle(prePosition, -1, position - prePosition, tabbar_height + 3));
                       }
                   }
                    */
                   
                   
                   foreach (Element _elm in this.currentView.Content.View.Toolbar.Items)
                   {
                       size += menupadding+ (int)d.MeasureString(_elm.GetAttribute("title"), new Font(FontFace, 9)).Width + menupadding;
                   }

                   int toolBarindent = scrollbar_size;
                   // Get start position of toolbar 
                   int position = this.Width - toolBarindent - size;
                   foreach (Element _elm in this.currentView.Content.View.Toolbar.Items)
                   {
                       int prePosition=position;
                        
                        // Draw text and measure the position for the next entry
                         

                       // The item is an menu, draw the menu image
                        
                       d.DrawString(_elm.GetAttribute("title"), new Font(FontFace, 9), new SolidBrush(Color.White), new Point(position+menupadding, 2));
                       position += menupadding+ (int)d.MeasureString(_elm.GetAttribute("title"), new Font(FontFace, 9)).Width+menupadding;
                       if (_elm.Type == "menu")
                       {
                           d.DrawImage(Resource1.dropdown, new Point( position-menupadding,2));
                       }
                       // If mouseover mark the item
                       if (mouseX >= prePosition && mouseX <= position && mouseY < tabbar_height)
                       {
                           Rectangle bounds = new Rectangle(prePosition, 2, position - prePosition, tabbar_height-4);
                           d.FillRectangle(new SolidBrush(Color.FromArgb(50, 50, 50)), bounds);
                           d.DrawRectangle(new Pen(Color.FromArgb(155,155,155)),bounds);
                           d.DrawString(_elm.GetAttribute("title"), new Font(FontFace, 9),new SolidBrush(Color.White), new Point(menupadding + prePosition, 2));
                       }
                   }

                }
                /***
                 * Render the image
                 * */
                R.Render();
            }
      //      catch
            {
            }
        }

        /// <summary>
        /// Scrollbar belonging to the view
        /// </summary>
        public Scrollbar ScrollBarY { get; set; }
        /// <summary>
        /// Returns if an active view exists
        /// </summary>
        /// <returns></returns>
        public bool ViewExist()
        {
            if (CurrentView != null)
                if (CurrentView.Content != null)
                    if (CurrentView.Content.View != null)
                        return true;
            return false;
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta <= -120)
            {
            	scrollY += -(e.Delta);
            }
            if (e.Delta >= 120)
            {
            	scrollY -= (e.Delta);
            }
            if (scrollY < 0)
                scrollY = 0;
            if (scrollY > ItemOffset)
            {
                scrollY = TotalHeight - this.Height ;
            }
        }
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            if (se.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (se.NewValue - se.OldValue > 0)
                {
                    scrollY += 10;
                }
                if (se.NewValue - se.OldValue < 0)
                {
                    scrollY -= 10;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the cursor is attached (is scrolling) on the scrollbar thumb if the returned offset is more than -1.
        /// </summary>
        int scrolling = -1;


        /// <summary>
        /// Element grabbed for drag'n drop
        /// </summary>
        List<Element> GrabbedElements { get; set; }

        // Mouse coordinates
        int masX, masY;
        public string dragURI = "";
        private void Artist_MouseDown(object sender, MouseEventArgs e)
        {
            Focus = true;
            // If mouse pointer is inside the scrollbar begin handle it
            if (e.X >= Width - this.scrollbar_size)
            {
                // If the pointer points on the thumb start scrolling mode
                if (e.Y >= scrollTop - scrollbar_size && e.Y <= scrollTop + thumbHeight + scrollbar_size)
                {
                    scrolling = e.Y - scrollTop;
                    
                }
            

                return;
            }

            masX = e.X;
            masY = e.Y;

          


            int entryship = 0;
            int top = 20;
            try
            {

                /**
                 * If shift is pressed extend the selection range
                 * */
                if (ModifierKeys == Keys.Shift)
                {
                    if (SelectedItems != null)
                    {
                        if (SelectedItems.Count > 0)
                        {
                            Element _elm = GetItemAtPos(new Point(e.X, e.Y));       // The element under the cursor
                            int index = ViewBuffer.IndexOf(_elm);                   // Index of the element

                            // Get current selection index
                            int curIndex = ViewBuffer.IndexOf(SelectedItems[0]);

                            // If the item below the cursor is below the already selected 
                            if (index > curIndex)
                            {
                                // Get the last index of the selected items
                                int lastIndex = ViewBuffer.IndexOf(SelectedItems[SelectedItems.Count - 1]);


                                // If the last item has an index larger than than the current index, reduce the size of the selection
                                if (lastIndex > curIndex)
                                {
                                    for (var i = lastIndex; i < curIndex; i--)
                                    {
                                        Element __elm = ViewBuffer[i];
                                        if (__elm.Entry)
                                        {
                                            __elm.Selected = false;
                                        }
                                    }
                                    return;
                                }
                                else // Otherwise enlarge the selection
                                {
                                    for (var i = lastIndex; i < index; i++)
                                    {
                                        Element __elm = ViewBuffer[i];
                                        if (__elm.Entry)
                                        {
                                            __elm.Selected = true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                /**
                 * If CTRL is pressed, do not deselect all items
                 * */
                if (!(ModifierKeys == Keys.Control))
                {
                    foreach (Element _Elm in ViewBuffer)
                    {
                        _Elm.Selected = false;
                    }
                }
                foreach (Element _Element in ViewBuffer)
                {
                    /**
                     * Element ptop are set on the draw so we can get an hint of where it's bounds are
                     * residing
                     * */
                   
                    Rectangle ScreenBounds = _Element.GetCoordinates(scrollX, scrollX , new Rectangle(0,0,this.Bounds.Width,this.Bounds.Height), 0);
                     int _left = ScreenBounds.Left;
                     int _top = ScreenBounds.Top - scrollY;
                     int _width = ScreenBounds.Width == -1 ? this.Width : ScreenBounds.Width;
                     int _height = ScreenBounds.Height == -1 ? this.Height : ScreenBounds.Height;
               
                    if (e.X >= _left  && e.X <= _left + _width  && e.Y >= _top  && e.Y <= _top + _height )
                    {
                        // If ctrl is pressed toggle the selection
                        if (ModifierKeys == Keys.Control)
                        {
                            _Element.Selected = !_Element.Selected;
                        }
                        else
                        {
                            _Element.Selected = true;
                        }
                        dragURI = _Element.GetAttribute("href");
                        
                      
                    }

                  
                        
                 }
                /***
                 * Handle tab click
                 * */
                if (hovered_tab > -1)
                {
                    this.currentSection = hovered_tab;
                }
#if (nbug)
                /**
                 * Handle tab clicks so user can change the tab
                 * */
                for (int i = 0; i < CurrentView.Content.View.Sections.Count; i++)
                {
                    // get section at i's position
                    Section section = CurrentView.Content.View.Sections[i];
                    // get section coordinates
                    int left = tabbar_start + (tab_width + tab_distance) * i;
                    int right = left + tab_width;
                    int _top = 1;
                    int bottom = tabbar_height;

                    // If the pointer is inside the bounds of the tab, change to it's section
                    if (e.X >= left && e.X <= right && e.Y >= _top && e.Y <= bottom)
                    {
                        currentSection = i;
                    }
                }

#endif
            }
            catch
            {
            }

                


            /**
           * Get element for dragging (only those with attribute 'draggable')
           * */
            Element ct = HoveredElement;
            if (ct != null)
            {
               /* if (ct.GetAttribute("href") != "")
                {
                    DataObject D = new DataObject(DataFormats.StringFormat, ct.GetAttribute("href"));
                    DoDragDrop(D, DragDropEffects.Copy);
                    return;
                }*/
                // If the element has an dragUri set attach it
                if (ct.GetAttribute("draguri") !="")
                {


                    DataObject D = new DataObject(DataFormats.StringFormat, ct.GetAttribute("draguri")) ;
                    DoDragDrop(D, DragDropEffects.Copy);
                    return;
                }
                   

                /**
                    * DRAGGING RULES:
                    * Inside the control the GrabbedEleemnts will be handled.
                    * Outside the control a list of the uris to the elements will be passed
                    * */

                   // If the item is an entry and the list is marked as reordable, begin the process
                if ( ct.Entry)
                {
                    
                    
                    // Raise the begin drag handle
                    if (this.BeginReorder != null && this.CurSection.Reorder )
                    {
                        ItemReorderEventArgs args = new ItemReorderEventArgs();
                        args.Collection = GrabbedElements;

                        this.BeginReorder(this, args);

                        // Cancel reording if the event wants it

                        if (args.Cancel)
                            return;
                        
                       


                    }
                    
                   

                    GrabbedElements = new List<Element>();
                    GrabbedElements.AddRange(this.SelectedItems);
                    
                    
                    // Compile a string with all uris for external handling
                    
                
                    DataObject D = new DataObject(DataFormats.StringFormat,UriToStrings(GrabbedElements));
                    DoDragDrop(D, DragDropEffects.Copy);
                }
            }
        }

        /// <summary>
        /// The raw data before mako preprocess
        /// </summary>
        String rawSource;

        /// <summary>
        /// The output of Mako
        /// </summary>
        String output;

        /// <summary>
        /// The mako engine
        /// </summary>
        MakoEngine ME;

        /// <summary>
        /// Method to load an page, preprocess it with mako and show it.
        /// </summary>
        /// <param name="file"></param>
            
        private int Diff(int x, int y)
        {
            return x > y ? x - y : y - x;
        }
        bool dragging=false;
        public void Drag(object c)
        {
        	
        }
//         DoDragDrop(GrabbedElements,DragDropEffects.Copy);

        /// <summary>
        /// Convert the list of elements to an csv list of uris
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public string UriToStrings(List<Element> elements)
        {
            StringBuilder buffer = new StringBuilder();
            foreach(Element d in elements)
            {
                buffer.Append(d.GetAttribute("uri")+"\n");
            }
            return buffer.ToString();
        }
        private void Artist_MouseMove(object sender, MouseEventArgs e)
        {
            // If in scrolling mode, eg the scroll offset is more than -1
            if (scrolling > -1)
            {
                // If cursor inside the scrollhandle
                if (e.Y > scrollbar_size && e.Y <= Height - scrollbar_size)
                {
                    int pos = e.Y  -scrolling ;
                    
                    // Set scrollY 
                    scrollY = (this.TotalHeight / this.Height) * pos;
                    if (scrollY < 0)
                        scrollY = 0;
                    if (scrollY >= this.TotalHeight - this.Height)
                        scrollY = this.TotalHeight - this.Height;
                }
                    return;
            }

            // if the mouse is inside the scrollbar boundies return
            if (e.X >= Width - scrollbar_size)
                return;
        	if(dragging)
        		return;
            mouseX = e.X;
            mouseY = e.Y;
          
                /**
                 * Drag and drop handling
                 * */

            // If grabbed element is not null begin prepare a drag'n drop operation
            if (GrabbedElements != null)
            {
                diffX = Diff(e.X,masX);
                diffY = Diff(e.Y, masY);
            }
            if (diffX > 10 || diffY > 10)
            {
            	if(!dragging)
            	{
                    
            		
            	}
            	diffX = 0;
                diffY = 0;
               // dragging=true;
            }
            int entryship = 0;
            int top = 20;
        //    this.Cursor = Cursors.Default;
#if (nobug)
            try
            {
                foreach (Element _Element in ViewBuffer)
                {
                   /**
                    * Get element bounds
                    * */
                    Rectangle Bounds = _Element.GetCoordinates(scrollX, scrollY, new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height),0);
                  	int _left = Bounds.Left;
                    int _top = Bounds.Top;
                 	int _width = Bounds.Width;
                 	int _height = Bounds.Height;

                  
                    if (_Element.Entry)
                    {
                        /*
                         * Enumerte all columns and check for possible links
                          * */
                         int column_position = _left;
                         foreach (KeyValuePair<String, int> _column in Columns)
                         {
                            
                             int column = _column.Value;
                             string title = _column.Key;
                             if (mouseX >= column_position && mouseX <= column_position + column && mouseY >= _top && mouseY <= _top + _height && _Element.GetAttribute("href_"+title.ToLower())!="")
                             {
                             
                                 // If the element has an href matching the column, set the cursor to hand
                                 if (_Element.GetAttribute("href_" + title.ToLower()) != "")
                                 {
                                     this.Cursor = Cursors.Hand;

                                    
                                 }
                                     
                             }
                             // increase the column position
                             column_position += column;
                         }
                    }
                	/* if(_Element.Entry)
                    {
                	 	if((e.X >= ARTISTLEFT+_left) && e.X <= ARTISTLEFT+_left+this.CreateGraphics().MeasureString(_Element.GetAttribute("author"),new Font(FontFace,8)).Width && _Element.GetAttribute("uri@author")!="")
                    	{
                    		this.Cursor = Cursors.Hand;	
                    	}
                    }*/
                }
            }
            catch
            {
            }
#endif
        }

        /// <summary>
        /// Returns an new int the value has no any higher value in the specified collection
        /// </summary>
        /// <param name="e">the number to look for</param>
        /// <param name="collection">the collection to look in</param>
        /// <returns></returns>
        public bool IsHigher(int e, List<int> collection)
        {
            // Counter for storing the last highest value
            int counter = 0;
            
            // If collection is empty return 1
            if (collection.Count < 1)
                return true;

            foreach (int d in collection)
            {
                // if the new int is higher than the counter add it
                if (d >= counter)
                {
                    counter++;
                }
            }
            return e > counter;
        }
        /// <summary>
        /// This function calculates the scrol offset of items. Returns -1 if there is an problem
        /// </summary>
        public int TotalHeight
        {
            get
            {
                try
                {
                    // The height of the visible board 
                    int viewHeight = this.Bounds.Height;
                    
                    // The integer which will add all element heights
                    int elementTotalHeight = 0;

                    // The outside height
                    int outsideHeight = 0;
                    /**
                     * Position of last object
                     * */
                    int lastPosition = 0;
                    // calculate the total height of all items
                    foreach (Element c in this.ViewBuffer)
                    {
                        // Check if this position is higher than any previous one and add it if so

                        int newpos = ( c.Top + c.Height ) - lastPosition;
                        // Add the item's top if the item's top is not equal to -1 (@TOP)
                        lastPosition = c.Top + c.Height;

                        elementTotalHeight += newpos;

                    }
                    // If the total elements filling is higher than the view's visible space.
                    if(elementTotalHeight < viewHeight)
                     
                    {
                       elementTotalHeight = viewHeight;
                    }
                    // Return the offset
                    return elementTotalHeight;

                }
                catch
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// Gets the difference between the total height of 
        /// the elements and the height of the visible boundary
        /// </summary>

        public int ItemOffset
        {
            get
            {
                try
                {
                    return TotalHeight-this.Bounds.Height;
                }
                catch
                {
                    return -1;
                }
            }
        }
        int mouseX = 0;
        int mouseY = 0;
        private void Artist_Leave(object sender, EventArgs e)
        {
            this.Focus = false;
         //   timer1.Stop();
        }

        private void Artist_Enter(object sender, EventArgs e)
        {
       //     timer1.Stop();
        }

        private void Artist_Paint(object sender, PaintEventArgs e)
        {
            this.Draw(e.Graphics);
        }

        private void Artist_MouseLeave(object sender, EventArgs e)
        {
         //   timer1.Stop();
        }

        private void Artist_DoubleClick(object sender, EventArgs e)
        {
            timer1.Start();
           
            int entryship = 0;
            int top = 20;
            this.Cursor = Cursors.Default;
            // Enumerator used for the playitem callback
            int i=0;
            try
            {
                foreach (Element _Element in ViewBuffer)
                {
                    // Get object bounds
                    Rectangle Bounds = _Element.GetCoordinates(scrollX, scrollY, this.Bounds, 0);
                    if (mouseX >= Bounds.Left && mouseX <= Bounds.Width + Bounds.Left && mouseY >= Bounds.Top && mouseY <= Bounds.Top + Bounds.Height)
                    {
                        // If the element has an onclick handler, execute the script
                        if (_Element.GetAttribute("ondblclick") !="")
                        {
                            CurrentView.Content.ScriptEngine.Run(_Element.GetAttribute("ondblclick"));
                            return;
                        }
                       
                    }
                    switch (_Element.Type)
                    {
                        case "entry":
                           
                            /**
                             * If cursor is inside the entry's bounds activate it
                             * */
                            if (mouseX >= Bounds.Left && mouseX <= Bounds.Width + Bounds.Left && mouseY >= Bounds.Top && mouseY <= Bounds.Top + Bounds.Height)
                            {

                                CurrentView.Content.PlayItem(_Element, i, currentSection);
                                   
                               
                            }

                            break;
                        case "header":

                            
                            break;
                        case "img":
                            break;
                        case "label":
                            //    d.DrawString(_Element.GetAttribute("text"), new Font(FontFace, 8), new SolidBrush(Fg), new Point(int.Parse(_Element.GetAttribute("x")) - scrollX, int.Parse(_Element.GetAttribute("y")) - scrollY));
                            break;
                        case "button":
                            break;
                        case "section":
                            // d.FillRectangle(new SolidBrush(Section), new Rectangle(0, top - scrollY, this.Width, ROWHEIGHT));
                            // d.DrawString(_Element.GetAttribute("text"), new Font(FontFace, 8), new SolidBrush(Fg), new Point(LEFT - scrollX, top + 4 - scrollY));
                            top += 40;
                            break;
                        case "divider":
                            top += 30;
                            //  d.DrawLine(new Pen(Color.Black), 0, top - scrollY, this.Width, top - scrollY);
                            top += 30;
                            break;
                        case "space":
                            int dist = 0;
                            if (_Element.GetAttribute("distance") != "")
                            {
                                int.TryParse(_Element.GetAttribute("distance"), out dist);
                                if (dist > 0)
                                {
                                    top += dist;
                                    break;
                                }
                            }
                            top += 30;
                            break;
                    }

                    // increase the enumerator
                    i++;


                }
            }
            catch
            {
            }
        }
    
        
        void DrawBoardMouseUp(object sender, MouseEventArgs e)
        {
            if (reoredering)
            {
                // If the dragged element is an grabbed element and moved inside
                if (GrabbedElements != null)
                {

                   
                   

                }
            }
       		dragging=false;
            reoredering = false;
            scrolling = -1;

            /**
             * Drop item dynamically if supported
             * */
           
        }
        /// <date>2011-04-24 16:18</date>
        /// <summary>
        /// Remove an entry at specified point. (index only applies to entries)
        /// </summary>
        /// <param name="pos">The index of the entry</param>
        public void RemoveEntryAt(int pos)
        {
            // define starting index
            int index = 0;
            foreach (Element ct in ViewBuffer)
            {
                // only enumerate if the element is an type of entry
                if (ct.Entry)
                {
                    // if index is as the index, remove the item
                    if (index == pos)
                    {
                        ViewBuffer.Remove(ct);

                        // break and return
                        return;

                    }
                    pos++;
                }
            }
        }

        /// <date>2011-04-24 16:24</date>
        /// summary>
        /// An virtual list of the items in the view, but only those entries are included
        /// </summary>
        public List<Element> Entries
        {
            get
            {
                // Allocate list for only entries
                List<Element> entries = new List<Element>();
                foreach (Element cf in this.ViewBuffer)
                {
                    if (cf.Entry)
                        entries.Add(cf);
                }
                return entries;
            }
        }
        
        /// <summary>
        /// Converts the real index into an index for the entry
        /// </summary>
        /// <param name="index"></param>
        /// <returns> the real index of the entry, -1 if failed</returns>
        public int EntryIndexToRealIndex(int index)
        {
            try
            {
                // Get the element from the specified index
                Element cf = Entries[index];
                int realIndex = ViewBuffer.IndexOf(cf);
                return realIndex;
            }
            catch
            {
                return -1;
            }
            
        }
        /// <summary>
        /// Convert the physical index to real index
        /// </summary>
        /// <param name="index">the real index</param>
        /// <returns>The virtual entry index, -1 if failed or the index points to an item of not an entry</returns>
        public int RealIndexToEntryIndex(int index)
        {
            try
            {
                // Get the element from the entries
                Element entry = ViewBuffer[index];
                if (entry.Type != "entry")
                    return -1;
                int virtualIndex = Entries.IndexOf(entry);
                return virtualIndex;
            }
            catch
            {
                return -1;
            }
        }

        /// <date>2011-04-24 16:18</date>
        /// <summary>
        /// Insert item at position which is synchronised with the range of items only by entries
        /// </summary>
        /// <param name="elements"></param>
        public void InsertEntryAt(List<Element> elements, int pos)
        {
            // define starting index
            int index = 0;
            foreach (Element ct in ViewBuffer)
            {
                // only enumerate if the element is an type of entry
                if (ct.Entry)
                {
                    // if index is as the index, insert the item
                    if (index == pos)
                    {
                        // Get physical index of the item
                        int realIndex = ViewBuffer.IndexOf(ct);
                        // insert the collection here
                        ViewBuffer.InsertRange(realIndex,elements);
                        // break and return
                        return;
                    }
                    index++;
                }
            }
        }
        /// <date>2011-04-24 16:18</date>
        /// <summary>
        /// Insert item at position which is synchronised with the range of items only by entries
        /// </summary>
        /// <param name="elements"></param>
        public void InsertEntryAt(Element elm, int pos)
        {
            List<Element> elements = new List<Element>() { elm };
            InsertEntryAt(elements, pos);
        }
        void DrawBoardDragDrop(object sender, DragEventArgs e)
        {
        	 
            dragging=false;

         
            /***
             * If grabbedelements is not null, we treat there is a item
             * moving action ongoing. Otherwise we treat this as an regular
             * drop operation. The grabbed element collection should belong to
             * the source drag'n drop source, for example the playlist view*/

            
             if (GrabbedElements != null)
             {
                 // Old index of songs (if reordering)
                 int oldIndex = -1;
                 // Denotates if moving element or handling outside data (uris)
                 bool elementMode = this.GrabbedElements != null;
                 // Get destination element
                 Element targetPos = GetItemAtPos(new Point(mouseX, mouseY));

                 // if no target element was found cancel
                 if (targetPos == null)
                     return;
                 // Only if the targetPos represents an element of type entry it should procced
                 if (targetPos.Type != "entry")
                     return;
                 // Get the index of the element (but we use the index for all types of items, not only entries)
                 int index = this.ViewBuffer.IndexOf(targetPos);

                 // Old index is the position of the first element in the collection
                 if (elementMode)
                 {
              

                     oldIndex = this.ViewBuffer.IndexOf(GrabbedElements[0]);

                     // Remove the elements from it's old location if it doesn't exists
                     foreach (Element cf in GrabbedElements)
                         ViewBuffer.Remove(cf);
                     // Insert the new elements
                     ViewBuffer.InsertRange(index, GrabbedElements);
                 }
                 else
                 {
                 }
                 // Rebuild the list
                 this.CurSection.RebuildList();



                 // Raise the itemdrag event
                 if (FinishedReorder != null)
                 {

                     // Create event args and pass the indexes, but only representing them as entries list
                     ItemReorderEventArgs reorderArgs = new ItemReorderEventArgs();
                     reorderArgs.Collection.AddRange(GrabbedElements);
                     reorderArgs.OldPos = RealIndexToEntryIndex(oldIndex);
                     reorderArgs.NewPosition = RealIndexToEntryIndex(index);

                     // Pass the event
                     FinishedReorder(this, reorderArgs);
                     
                 }



                 // Nullify the grabbed elements
                 GrabbedElements = null;

                 

             }
             /**
              * If the drop event is set, raise the drop operation
              * */
             if (DropElement != null)
             {
                 Element cf = this.GetItemAtPos(this.PointToClient(new Point(e.X, e.Y)));

                 // Make the args for the drag operation
                 ElementDragEventArgs args = new ElementDragEventArgs();

                 args.Destination = cf;
                 args.Index = this.CurSection.Elements.IndexOf(cf);
                 args.Position = this.PointToClient(new Point(e.X, e.Y));
                 args.Section = this.CurSection;
                 args.DragArgs = e;
                 
                 // Raise the event
                 DropElement(this, args);

                 // Set the effect to this effect
                 e.Effect = args.AllowedEffects;
                 return;
             }






      
            

        }

        private void DrawBoard_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        /// <summary>
        /// Check all ready receivers, run and delete them those are completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timrDownloadCheck_Tick(object sender, EventArgs e)
        {
            // Check all downloads for content
            foreach (View d in this.Views.Values)
            {
                // Get the spofity instance
                Spofity t = d.Content;
                
                // List of contentReceivers to delete after they're ready
                List<Spofity.ContentReceiver> delete = new List<Spofity.ContentReceiver>();
                foreach (Spofity.ContentReceiver receiver in t.Receivers)
                {
                    // check if receiver has an object
                    if (receiver.Package != null)
                    {
                        // Set an pointer to the package for the script instance
                        t.ScriptEngine.SetVariable("__package", receiver.Package);

                        // Run the callback
                        t.ScriptEngine.Run(receiver.Callback + "(__package);");
                        // Schedule the receiver for deletetion
                        delete.Add(receiver);
                    }

                   
                    
                }

                // Delete all receivers
                foreach (Spofity.ContentReceiver receiver in delete)
                {
                    t.Receivers.Remove(receiver);
                }
            }
        }
        /// <summary>
        /// Update the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrViewUpdate_Tick(object sender, EventArgs e)
        {
            /***
             * Re-render the layout elements
             * */
         /*   if(this.currentView!=null)
                
                if (this.CurrentView.Content != null)
                {
                    Thread c = new Thread(currentView.Content.UpdateAsync);
                    c.Start();
                    
                }*/
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /*if (CurrentView != null)
                if (CurrentView.Content != null)
                {
                    Thread r = new Thread(CurrentView.Content.CheckPendingChanges);
                    r.Start();
                }*/
        }

        private void DrawBoard_DragEnter(object sender, DragEventArgs e)
        {
            dragging = true;
            e.Effect = DragDropEffects.Copy;
            if (GrabbedElements != null)
            {
                if (this.BeginReorder != null)
                {  // Create reorder args
                    ItemReorderEventArgs args = new ItemReorderEventArgs();
                    args.Collection = GrabbedElements;

                    this.BeginReorder(this, args);
                    if (!args.Cancel)
                        e.Effect = DragDropEffects.Copy;
                    return;
                }
            }  
        }



        /// <summary>
        /// Event args for elementDragEvent
        /// </summary>
        public class ElementDragEventArgs
        {
            public DragEventArgs DragArgs { get; set; }
            /// <summary>
            /// The element currently hovering
            /// </summary>
            public Element Destination { get; set; }

            /// <summary>
            /// The position of the mouse cursor, in client coordinates
            /// </summary>
            public Point Position { get; set; }

            /// <summary>
            /// The index of the item
            /// </summary>
            public int Index { get; set; }

            /// <summary>
            /// The view the element belongs to
            /// </summary>
            public View View { get; set; }

            /// <summary>
            /// The section the event is raising on
            /// </summary>
            public Section Section { get; set; }

            /// <summary>
            /// The allowed effect for the operation
            /// </summary>
            public DragDropEffects AllowedEffects;

            public bool Cancel { get; set; }
        }

        /// <summary>
        /// Event handler for dragging over child elements
        /// </summary>
        /// <param name="sender">the sender board</param>
        /// <param name="e"></param>
        public delegate void ElementDragEventHandler(object sender, ElementDragEventArgs e);

        public event ElementDragEventHandler DragOverElement;
        public event ElementDragEventHandler DropElement;
        private void DrawBoard_DragOver(object sender, DragEventArgs e)
        {
            // If grabbed elements is not null and beginreorder event handler is set, do the tasks
            if (GrabbedElements != null && this.CurSection.Reorder)
            {
                // Get the element for the position
                Element df = GetItemAtPos(this.PointToClient(new Point(e.X, e.Y)));
                if (df != null)
                {
                    // If the DragOverEvent is set, fire it. If the allowed effect is set to none, cancel the operation
                    if (DragOverElement != null)
                    {
                        // Create event args
                        ElementDragEventArgs args = new ElementDragEventArgs();
                        args.DragArgs = e;
                        args.Section = this.CurSection;
                        args.View = this.CurrentView;
                        args.Index = CurSection.Elements.IndexOf(df);
                        args.Position = this.PointToClient(new Point(e.X, e.Y));
                        args.Destination = df;

                        // raise the event
                        DragOverElement(this, args);

                        e.Effect = args.AllowedEffects;
                        // If the allowed effects has been changed to zero, cancel
                        if (args.AllowedEffects == DragDropEffects.None)
                        {
                            return;
                        }
                    }
                    // get the element's bounds
                    Rectangle o_bounds = df.GetCoordinates(scrollX, scrollY, new Rectangle(0, 0, this.Width, this.Height), 0);

                    // Draw the line
                    this.CreateGraphics().DrawLine(new Pen(Color.White), new Point(0, o_bounds.Top + o_bounds.Height), new Point(this.Width - scrollbar_size, o_bounds.Top + o_bounds.Height));

                }
                
                e.Effect =  DragDropEffects.Copy  ;
                return;

            }
            // Otherwise give drag'ndrop effect none
            e.Effect = DragDropEffects.None;
        }

        private void DrawBoard_DragLeave(object sender, EventArgs e)
        {
            dragging = false;
        }

        private void DrawBoard_Scroll(object sender, ScrollEventArgs e)
        {
           
        }

        /// <summary>
        /// Updates the scrollbar
        /// </summary>
        private void AssertScroll()
        {
            if (this.ScrollBarY != null)
            {
                if (this.TotalHeight - this.Height == 0)
                {
                    ScrollBarY.Position = 0;
                    ScrollBarY.ThumbHeight = 0;
                    ScrollBarY.Hide();
                    return;
                }
                ScrollBarY.Show();
                ScrollBarY.Position = ((float)this.scrollY / ((float)this.TotalHeight - (float)this.Height));
                ScrollBarY.ThumbHeight = ((float)this.Height / (float)this.TotalHeight);

            }
        }
        private void DrawBoard_SizeChanged(object sender, EventArgs e)
        {
            AssertScroll();
        }
    }
   
}
