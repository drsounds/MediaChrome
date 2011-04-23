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

namespace Board
{
    
    public partial class DrawBoard : UserControl
    {

        public delegate bool PlaybackStartEvent(object sender, String Url);
        public event PlaybackStartEvent PlaybackRequested;
      
        public bool Snart =true;
        public Color Section
        {
            get
            {
                return  Color.LightGray;
            }
        }
        public Color SectionText
        {
            get
            {
                return  Color.White;
            }
        }
        public Color Bg
        {
            get
            {
                return Color.FromArgb(60,60,60);
            }
        }
      
        public Color TextFade
        {
            get
            {
                return Color.Gray;
            }
        }
        public Color Divider
        {
            get
            {
                return  Color.Black;
            }
        }
        public Color Fg
        {
            get
            {
                return Color.White;
            }
        }
        public Color Entry
        {
            get
            {
                return   Bg;
            }
        }
        public Color Alt
        {
            get
            {
                return Color.FromArgb(55,55,55);
            }
        }

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
            this.Click+= new EventHandler(DrawBoard_Click);
            // Assign default columnwidths
            Columns = new Dictionary<string, int>();

            // Add standard columns (title,position)
            Columns.Add("r", 30);
            Columns.Add("No", 50);
            Columns.Add("Title", 300);
            Columns.Add("Artist", 150);
            Columns.Add("Length", 50);
            Columns.Add("Album", 200);

        }
        /// <summary>
        /// Column widths. They are used for the entries. -1 means until end of size
        /// </summary>
        public Dictionary<String, int> Columns;
        void DrawBoard_Click(object sender, EventArgs e)
        {
            if (CurrentView != null)
                if (CurrentView.Content != null)
                    if (CurrentView.Content.View != null)
                        try
                        {
                            for (int i = 0; i < CurrentView.Content.View.Sections[currentSection].Elements.Count; i++)
                            {
                                
                                Element _Element = (Element)CurrentView.Content.View.Sections[currentSection].Elements[i];
                                Rectangle Boundaries = _Element.Bounds;
                                int _left = _Element.Bounds.Left;
                                int _top = _Element.Bounds.Top;
                                int _width = _Element.Bounds.Width;
                                int _height = _Element.Bounds.Height;
                                if (mouseX >= Bounds.Left && mouseX <= Bounds.Width + Bounds.Left && mouseY >= Bounds.Top && mouseY <= Bounds.Top + Bounds.Height)
                                {
                                    // If the element has an onclick handler, execute the script
                                    if (_Element.GetAttribute("onclick") != "")
                                    {
                                        CurrentView.Content.ScriptEngine.Run(_Element.GetAttribute("onclick"));
                                        return;
                                    }
                                   
                                }
                                if (_Element.Type == "entry")
                                {
                                    /**
                                     * Enumerte all columns and check for possible links
                                     * */
                                    int column_position = _left;
                                    foreach (KeyValuePair<String, int> _column in Columns)
                                    {

                                        int column = _column.Value;
                                        string title = _column.Key;
                                        if (mouseX >= column_position && mouseX <= column_position + column && mouseY >= _top && mouseY <= _top +_height)
                                        {
                                            // If the element has an href matching the column, go to it
                                            if (LinkClick != null)
                                                if (_Element.GetAttribute("href_" + title.ToLower()) != "")
                                                    LinkClick(_Element, _Element.GetAttribute("href_" + title.ToLower()));
                                        }
                                        column_position += _column.Value;

                                    }
                                }
                                if (_Element.GetAttribute("href") != "" && mouseX >= _left && mouseX <= _left + _width && mouseY >= _top && mouseY <= _top + _height)
                                {
                                    // If the itemclicked event are not null, raise it
                                    if (LinkClick != null)
                                        LinkClick(_Element, _Element.GetAttribute("href"));

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
        }
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
            public int Section { get; set; }

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
        /// Method to load an view asynchronisly
        /// </summary>
        /// <param name="address"></param>
        public void LoadViewAsync( object address)
        {
            View view = (View)address;
        
            {
                // Reset the ptop
                Element.ptop = 20;
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

                // Format the string
                ME = new MakoEngine();
                output = ME.Preprocess(rawSource,view.Argument);

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
                Spofity R = new Spofity(output,ME);
    

                // Create an proxy handler so people can interact with each spofity
                R.PlaybackStarted += new Spofity.ElementPlaybackStarted(R_PlaybackStarted);
                // Attach the content to the content tag so it are fully loaded
                view.Content = R;

                // set the  spofity's parent view to CurrentView
                R.ParentView = view;
                CurrentView = view;
            }
           
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

                View newView = new View(Uri, BaseFolder + "\\" + App + ".xml", Querystring);
               
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
        /// <summary>
        /// Method to asynchronisly download an image to an img element
        /// </summary>
        /// <param name="token"></param>
        public void DownloadImage(object token)
        {
            try
            {
                WebClient X = new WebClient();

                Image R = Bitmap.FromStream(X.OpenRead((string)token));
                Images.Add((string)token, R);
            }
            catch (Exception e)
            {
            }

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

        }
        int LEFT = 140;
        int ARTISTLEFT=550;
        int ROWHEIGHT = 20;
        int scrollX
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
        int scrollY
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
        	}
        	
        }


        

       


        

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
                return this.currentView.Content.View.Sections[currentSection].Elements;
            }
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


                foreach (Element _element in this.currentView.Content.View.Sections[currentSection].Elements)
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
        public void AddItem(String Uri, String Title, String[] Attributs, String[] uris)
        {
            Element newItem = new Element();
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
                foreach (Element d in this.currentView.Content.View.Sections[currentSection].Elements)
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
                foreach (Element d in this.currentView.Content.View.Sections[currentSection].Elements)
                {

                    if (d.Type != "entry")
                        continue;
                    d.Selected=false;
                          
                  
                }
                // Set the item at the index as selected
                int index = 0;
                foreach (Element d in this.currentView.Content.View.Sections[currentSection].Elements)
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
        /// <summary>
        /// Method to handle keys in list
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            if (!this.Focused)
                return base.IsInputKey(keyData); ;
            switch(keyData)
            {
                case Keys.Up:
                    // Move the selected element to the previous one
                    SelectedIndex--;
                    break;
                case Keys.Down:
                    // Move the selected element to the next
                    SelectedIndex++;
                    break;
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
        /// Function to draw an element on an view view
        /// </summary>
        /// <param name="_Element">The element to draw</param>
        /// <param name="ptop">The previous top position on previous views (in direct coordinates)</param>
        /// <param name="d">The graphics to draw on</param>
        /// <param name="padding">The padding to use when nesting child elements</param>
   
        /// <param name="entryship">The number of entry placed</param>
        public void DrawElement(Element _Element, Graphics d,ref int entryship,Rectangle Bounds,int padding)
        {
           
            /**
             * Get screen coordinates of the element
             * */
          

            int height = Bounds.Height;
            int left = Bounds.Left;
            int top =  Bounds.Top;
            int width = Bounds.Width;
            /**
             * For consistency we apply selected rules to all kind of elements, not only the entry element but
             * also to preserve worrk in the future
             * */

            // Decide color of the entry wheather it selected or not.
            Color EnTry = (entryship % 2) == 1 ? Entry : Alt;

            if (_Element.Selected == true)
            {
                EnTry = Color.FromArgb(211, 255, 255);
                // If the control is inactive draw it gray
                if (!this.Focused)
                {
                    EnTry = Color.Gray;
                }
            }
            Color ForeGround = MainForm.FadeColor(-0.2f, Fg);
            if (_Element.Selected == true)
            {
                
                ForeGround = Color.DarkBlue;
                if (!this.Focused)
                {
                    ForeGround = Color.DarkGray;
                }
               
            }
            
            // Font size for the font
            // Try to find text size otherwise apply default
            int textSize = 8;
            int.TryParse(_Element.GetAttribute("size"), out textSize);
            if (textSize == 0)
                textSize = 8;

            String fontName = "MS Sans Serif";
            if (_Element.GetAttribute("font") != "")
            {
                fontName = _Element.GetAttribute("font");
            }
            // if element is hovred, set the font to be underlined
            bool hovered = (_Element.GetAttribute("hover") == "true");
            Font labelFont = new Font(fontName, textSize, hovered ? FontStyle.Underline : FontStyle.Regular);



            

            // 

            // draw selection background but currently only for entry
            
           //     d.FillRectangle(new SolidBrush(EnTry), new Rectangle(left, top, width, height));
            /**
            * For all types of element
            * */
            switch (_Element.Type)
            {

                case "entry":

                    
                  
                    /*      if (_Element.GetAttribute("position") == ("absolute"))
                          {
                              int _left = int.Parse(_Element.GetAttribute("left"));
                              int _top = int.Parse(_Element.GetAttribute("top"));
                              int _width = int.Parse(_Element.GetAttribute("width"));
                              int _height = int.Parse(_Element.GetAttribute("height"));

                              d.FillRectangle(new SolidBrush(EnTry), new Rectangle(_left - scrollX, _top - scrollY, _width, _height));
                              d.DrawString(_Element.GetAttribute("no"), new Font("MS Sans Serif", 8), new SolidBrush(Spirit.MainForm.FadeColor(-0.4f, ForeGround)), new Point(_left + 1 - scrollX, _top + 2 - scrollY));

                              d.DrawString(_Element.GetAttribute("title"), new Font("MS Sans Serif", 8), new SolidBrush(ForeGround), new Point(_left + 20 - scrollX, _top + 2 - scrollY));
                              d.DrawString(_Element.GetAttribute("author"), new Font("MS Sans Serif", 8), new SolidBrush(ForeGround), new Point(_left + 320 - scrollX, _top + 2 - scrollY));
                              d.DrawString(_Element.GetAttribute("collection"), new Font("MS Sans Serif", 8 ), new SolidBrush(ForeGround), new Point(_left + 435 - scrollX, _top + 2 - scrollY));
                              top += ROWHEIGHT;
                              entryship++;
                              break;
                          }*/
                    // For some unknown reason ptop crashes
                    // TODO: Fixthis this in another way
                   // top -= height / 2;
                    
                        d.FillRectangle(new SolidBrush(EnTry), new Rectangle(left, top, width, height));
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
                            String title = Column.Key;

                            // If cursor is inside the boundaries, the column has an link then underline the font
                            if (mouseX >= column_position && mouseX <= column_position + column && mouseY >= top && mouseY <= top + height &&_Element.GetAttribute("href_"+title.ToLower())!="")  
                            {
                                setFont = new Font(setFont, FontStyle.Underline);
                            }
                           d.DrawString(_Element.GetAttribute(Column.Key.ToLower()), setFont, new SolidBrush(ForeGround), new Point( column_position, top + 2));
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


                case "header":
                    entryship = 0;
                    d.DrawString(_Element.GetAttribute("title"), labelFont, new SolidBrush(Fg), new Point(left, top));
                   
                    break;
                case "img":

                    Image Rs = null;
                    Images.TryGetValue(_Element.GetAttribute("src"), out Rs);

                    // If image is not null, do not show any picture
                    if (Rs != null)
                    {
                        d.DrawImage(Rs, new Rectangle(left, top, width, height));
                    }
                        // But if the element hasn't been called before, start an downloading of the image
                    else if (!_Element.FirstCall)
                    {

                        Thread D = new Thread(DownloadImage);
                        D.Start((object)_Element.GetAttribute("src"));
                    }
                    break;


                case "label":

                    
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
                   
                    break;
                case "button":
                    break;
                case "section":
                    d.FillRectangle(new SolidBrush(Section), new Rectangle(0, top, width, height));
                    d.DrawString(_Element.GetAttribute("text"), new Font("Arial Black", 10), new SolidBrush(Fg), new Point(left, top));
                   
                    break;
                case "divider":
           
                    d.DrawLine(new Pen(Divider), left, top, left + width, top);
                 
                    break;
                case "space":

                    break;
            }
            // Say the element has been called for the first time
            _Element.FirstCall = true;
            // Draw all child elements with coordinates relative to the current element (nesting)
            foreach (Element rt in _Element.Elements)
            {

                // Get bounds
                Rectangle ElementBounds = new Rectangle( _Element.Left +  Bounds.Left + padding, _Element.Top + Bounds.Top + padding, Bounds.Width - (padding * 2), Bounds.Height - (padding * 2));

                DrawElement(rt, d, ref entryship, ElementBounds, padding);
            }
            // increase ptop
            
        }
        /// <summary>
        /// Distance between tabs
        /// </summary>
        int tab_distance = 1;

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
                return (int)Math.Round(((float)this.Height / (float)TotalHeight) * (spaceHeight - scrollbar_size));
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
            p.FillRectangle(new LinearGradientBrush(new Point(0,0),new Point(0,columnheader_height),Color.FromArgb(210,210,210),Color.FromArgb(200,200,200)),new Rectangle(point,new Size(this.Width-scrollbar_size,columnheader_height)));
            // draw border line
            p.DrawLine(new Pen(Color.Black), new Point(0, point.Y + columnheader_height - 1), new Point(this.Width - scrollbar_size, point.Y + columnheader_height - 1));
            p.DrawLine(new Pen(Color.White), new Point(0,point.Y ), new Point(this.Width-scrollbar_size, point.Y ));

            //draw all columns

            // current position of the column header
            int current_position = point.X;
            int text_top = 2;

            // Draw headers
            foreach (KeyValuePair<String, int> column in Columns)
            {
                
                p.DrawString(column.Key, new Font("MS Sans Serif", 8, FontStyle.Bold), new SolidBrush(Color.White), new Point(current_position, text_top+point.Y+1));
                p.DrawString(column.Key, new Font("MS Sans Serif", 8, FontStyle.Bold), new SolidBrush(Color.Black), new Point(current_position, text_top+point.Y));
                current_position += column.Value;
            }

        }

        /// <summary>
        /// Draw inside an certain view
        /// </summary>
        /// <param name="p">The graphics buffer</param>
        /// <param name="CurrentView"> The view to base from</param>

        

        
        public void Draw(Graphics p)
        {
            try
            {
                this.pictureBox1.Left = this.Width / 2 - this.pictureBox1.Width / 2;
                this.pictureBox1.Top = this.Height / 2 - this.pictureBox1.Height / 2;
                // Show progress icon if current view is loading...
                this.pictureBox1.Visible = this.CurrentView.Content == null;
                this.BackColor = Color.FromArgb(50, 50, 50);
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

                /**
                 * If the currentView isn't null begin draw all elements on the board
                 * */
                if (CurrentView != null)
                    if (CurrentView.Content != null)
                        if (CurrentView.Content.View != null)
                            for (int i = 0; i < CurrentView.Content.View.Sections[currentSection].Elements.Count; i++)
                            {
                                // Calculate the view coordinates of the element

                                Element _Element = CurrentView.Content.View.Sections[currentSection].Elements[i];
                                Rectangle ScreenCoordinates = _Element.GetCoordinates(scrollX, scrollY, this.Bounds, 0);
                                // Draw the element and it's children
                                DrawElement(_Element, d, ref entryship, ScreenCoordinates, 3);







                            }







                /***
                 * Draw the tab header on top
                 * */


                // draw an bounding rectangle
                d.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, this.Bounds.Width, tabbar_height));
                d.DrawLine(new Pen(Color.FromArgb(128, 128, 128)), new Point(0, tabbar_height), new Point(this.Bounds.Width, tabbar_height));
                if (CurrentView != null)
                    if (CurrentView.Content != null)
                        // Draw all section bar
                        for (int i = 0; i < CurrentView.Content.View.Sections.Count; i++)
                        {
                            Section section = CurrentView.Content.View.Sections[i];
                            // if you are at the current section draw the panes


                            if (currentSection == i)
                            {

                                // Draw section tab, load if nulll
                                if (sectionTab == null)
                                {
                                    sectionTab = Resource1.tab;
                                }
                                // draw the tab bar
                                d.DrawImage(sectionTab, new Rectangle(tabbar_start + i * (tab_width + tab_distance), 1, tab_width, tabbar_height));

                                // draw the tab background
                                d.DrawString(section.Name, new Font("MS Sans Serif", 8), new SolidBrush(Color.FromArgb(255, 255, 211)), new Point(tabbar_start + ((tab_distance + tab_width) * i) + tab_text_margin, tab_text_margin / 5));
                            }
                            else
                            {
                                // Draw section tab, load if nulll
                                if (inactive_section_tab == null)
                                {
                                    inactive_section_tab = Resource1.inactive;
                                }
                                // draw the tab bar
                                d.DrawImage(inactive_section_tab, new Rectangle(tabbar_start + ((tab_distance + tab_width) * i), 1, tab_width, tabbar_height - 1));

                                d.DrawString(section.Name, new Font("MS Sans Seif", 8), new SolidBrush(Color.Black), new Point(tabbar_start + ((tab_distance + tab_width) * i) + tab_text_margin, tab_text_margin / 5));
                                d.DrawString(section.Name, new Font("MS Sans Seif", 8), new SolidBrush(Color.White), new Point(tabbar_start + ((tab_distance + tab_width) * i) + tab_text_margin, +tab_text_margin / 5 - 1));
                            }
                        }
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


                    d.DrawImage(Resource1.scrollbar_thumb, new Rectangle(this.Width - scrollbar_size, scrollbar_size + scrollTop, scrollbar_size, bar_offset), new Rectangle(0, 0, scrollbar_size, bar_offset), GraphicsUnit.Pixel);

                    d.DrawImage(Resource1.scrollbar_thumb, new Rectangle(this.Width - scrollbar_size, scrollbar_size + scrollTop + bar_offset, scrollbar_size, +thumbHeight), new Rectangle(0, bar_offset, scrollbar_size, 3), GraphicsUnit.Pixel);
                    d.DrawImage(Resource1.scrollbar_thumb, new Rectangle(this.Width - scrollbar_size, scrollbar_size + scrollTop + bar_offset + thumbHeight, scrollbar_size, bar_offset), new Rectangle(0, Resource1.scrollbar_thumb.Height - bar_offset, scrollbar_size, bar_offset), GraphicsUnit.Pixel);
                }
                /***
                * If the Section is an list, draw listheaders
                * */
                try
                {
                    if(CurrentView.Content!=null)
                    if (CurrentView.Content.View.Sections[currentSection].List)
                    {
                        // Get the first entry element
                        Element firstEntry = null;
                        foreach (Element elm in CurrentView.Content.View.Sections[currentSection].Elements)
                        {
                            if (elm.Type == "entry")
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
                }

                catch
                {
                }
                /***
                 * Render the image
                 * */
                R.Render();
            }
            catch
            {
            }
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

        // Mouse coordinates
        int masX, masY;
        public string dragURI = "";
        private void Artist_MouseDown(object sender, MouseEventArgs e)
        {
            // If mouse pointer is inside the scrollbar begin handle it
            if (e.X >= Width - this.scrollbar_size)
            {
                // If the pointer points on the thumb start scrolling mode
                if (e.Y >= scrollTop && e.Y <= scrollTop + thumbHeight)
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
                foreach (Element _Elm in CurrentView.Content.View.Sections[currentSection].Elements)
                {
                    _Elm.Selected = false;
                }
                foreach (Element _Element in CurrentView.Content.View.Sections[currentSection].Elements)
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
                        _Element.Selected = true;
                        dragURI = _Element.GetAttribute("href");
                      
                    }

                  
                        
                 }

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

                
            }
            catch
            {
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
          
            if (dragURI != "" && dragURI != null)
            {
                diffX = Diff(e.X,masX);
                diffY = Diff(e.Y, masY);
            }
            if (diffX > 10 || diffY > 10)
            {
            	if(!dragging)
            	{
            		
	         //  	  	DataObject D = new DataObject(DataFormats.StringFormat,"s");
        	//		DoDragDrop(D, DragDropEffects.Copy);;
            		
            	}
            	diffX = 0;
                diffY = 0;
               // dragging=true;
            }
            int entryship = 0;
            int top = 20;
            this.Cursor = Cursors.Default;
            try
            {
                foreach (Element _Element in CurrentView.Content.View.Sections[currentSection].Elements)
                {
                   /**
                    * Get element bounds
                    * */
                    Rectangle Bounds = _Element.GetCoordinates(scrollX, scrollY, new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height),0);
                  	int _left = Bounds.Left;
                    int _top = Bounds.Top;
                 	int _width = Bounds.Width;
                 	int _height = Bounds.Height;

                    if (e.Y >= _top && e.Y <= _height + _top && e.X >= _left && e.X <= _left + _width && _Element.GetAttribute("href") != "")
                    {
                        this.Cursor = Cursors.Hand;
                        // Set element to be hovered
                    }
                    if (_Element.Type == "entry")
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
                	/* if(_Element.Type == "entry")
                    {
                	 	if((e.X >= ARTISTLEFT+_left) && e.X <= ARTISTLEFT+_left+this.CreateGraphics().MeasureString(_Element.GetAttribute("author"),new Font("MS Sans Serif",8)).Width && _Element.GetAttribute("uri@author")!="")
                    	{
                    		this.Cursor = Cursors.Hand;	
                    	}
                    }*/
                }
            }
            catch
            {
            }
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
                    foreach (Element c in this.CurrentView.Content.View.Sections[currentSection].Elements)
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
        /// This function calculates the total height of all it's content
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
         //   timer1.Stop();
        }

        private void Artist_Enter(object sender, EventArgs e)
        {
       //     timer1.Stop();
        }

        private void Artist_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
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
                foreach (Element _Element in CurrentView.Content.View.Sections[currentSection].Elements)
                {
                    // Get object bounds
                    Rectangle Bounds = _Element.GetCoordinates(scrollX, scrollY, this.Bounds, 0);
                    if (mouseX >= Bounds.Left && mouseX <= Bounds.Width + Bounds.Left && mouseY >= Bounds.Top && mouseY <= Bounds.Top + Bounds.Height)
                    {
                        // If the element has an onclick handler, execute the script
                        if (_Element.GetAttribute("ondblclick") != null)
                        {
                            CurrentView.Content.ScriptEngine.Run(_Element.GetAttribute("ondblclick"));
                        }
                        return;
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
                            //    d.DrawString(_Element.GetAttribute("text"), new Font("MS Sans Serif", 8), new SolidBrush(Fg), new Point(int.Parse(_Element.GetAttribute("x")) - scrollX, int.Parse(_Element.GetAttribute("y")) - scrollY));
                            break;
                        case "button":
                            break;
                        case "section":
                            // d.FillRectangle(new SolidBrush(Section), new Rectangle(0, top - scrollY, this.Width, ROWHEIGHT));
                            // d.DrawString(_Element.GetAttribute("text"), new Font("MS Sans Serif", 8), new SolidBrush(Fg), new Point(LEFT - scrollX, top + 4 - scrollY));
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
       		dragging=false;
            scrolling = -1;
        }
        
        void DrawBoardDragDrop(object sender, DragEventArgs e)
        {
        	 dragging=false;
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
    }
   
}