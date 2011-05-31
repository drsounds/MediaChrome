using System;
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

namespace Board
{
    
    public partial class DrawBoard : UserControl
    {
    	
	    
        public delegate void ItemClick(object Sender, String Url);
        public event ItemClick ItemClicked;
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
        public int currentSection = 0;
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
        }
		
        void DrawBoard_Click(object sender, EventArgs e)
        {
        	 if(CurrentView!=null)
                 if(CurrentView.Content!=null)
            	if(CurrentView.Content.View!=null)
                    for (int i = 0; i < CurrentView.Content.View.Sections[currentSection].CountItems; i++)
                {

                    Element _Element = (Element)CurrentView.Content.View.Sections[currentSection].Elements[i];
        	 		int _left = _Element.Left - scrollX;
                	int _top = _Element.Top - scrollY;
                    int _width = _Element.Width == -1 ? this.Width : _Element.Width;
                    int _height = _Element.Height == -1 ? this.Height :  _Element.Height;
        	 		 if(_Element.Type == "sp:entry")
                    {
                	 	if((mouseX >= ARTISTLEFT+_left) && mouseX <= ARTISTLEFT+_left+this.CreateGraphics().MeasureString(_Element.GetAttribute("author"),new Font("MS Sans Serif",8)).Width && _Element.GetAttribute("uri@author")!=""&&mouseY >= _top && mouseY <= _top + _height)
                    	{
                	 		if(ItemClicked!=null)
                	 			ItemClicked(this,_Element.GetAttribute("uri@author"));
                	 		
                    	}
                    }
        	 		
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
        	History.Push(this.CurrentView);
        	this.currentView=Post.Pop();
        }
        public void GoBack()
        {
        	Post.Push(this.CurrentView);
        	this.currentView=History.Pop();
        	this.ItemClicked(this,"@back");
        }
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
            try
            {
                // Reset the ptop
                Element.ptop = 20;
                // Get the uri
                String uri = view.Address;


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

                // Format the string
                ME = new MakoEngine();
                output = ME.Preprocess(rawSource,view.Argument);


                // Finaly start the view
                Spofity R = new Spofity(output);

                // Attach the content to the content tag so it are fully loaded
                view.Content = R;
                CurrentView = view;
            }
            catch
            {
            }
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
                return this.CurrentView.Content.ScrollX;
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
        		if(CurrentView==null)
        			return 0;
        		return this.CurrentView.Content.ScrollY;
        	}
        	set
        	{
        		if(CurrentView==null)
        			return;
                this.CurrentView.Content.ScrollY = value;
        	}
        	
        }
        int diffX = 0;
        int diffY = 0;
        int top = 0;
        BufferedGraphicsContext D;
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
            }
            Color ForeGround = MainForm.FadeColor(-0.2f, Fg);
            if (_Element.Selected == true)
            {
                ForeGround = Color.DarkBlue;
            }
            

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
                              d.DrawString(_Element.GetAttribute("collection"), new Font("MS Sans Serif", 8), new SolidBrush(ForeGround), new Point(_left + 435 - scrollX, _top + 2 - scrollY));
                              top += ROWHEIGHT;
                              entryship++;
                              break;
                          }*/

                    d.FillRectangle(new SolidBrush(EnTry), new Rectangle(left, top, width, height));
                    d.DrawString(_Element.GetAttribute("no"), new Font("MS Sans Serif", 8), new SolidBrush(MainForm.FadeColor(-0.4f, ForeGround)), new Point(LEFT + 1, top));

                    d.DrawString(_Element.GetAttribute("title"), new Font("MS Sans Serif", 8), new SolidBrush(ForeGround), new Point(left + 20, top + 2));
                    d.DrawString(_Element.GetAttribute("author"), new Font("MS Sans Serif", 8), new SolidBrush(ForeGround), new Point(left + ARTISTLEFT, top + 2));
                    d.DrawString(_Element.GetAttribute("collection"), new Font("MS Sans Serif", 8), new SolidBrush(ForeGround), new Point(left + 435, top + 2));
                    entryship++;
              
                    break;


                case "header":
                    entryship = 0;
                    d.DrawString(_Element.GetAttribute("title"), new Font("MS Sans Serif", 12), new SolidBrush(Fg), new Point(left, top));
                   
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

                    // Try to find text size otherwise apply default
                    int textSize = 8;
                    int.TryParse(_Element.GetAttribute("size"),out textSize);
                    if (textSize == 0)
                        textSize = 8;
                    Font labelFont = new Font("MS Sans Serif", textSize);

                    // Try use custom color, otherwise by default
                    Color Foreground = Fg;

                   
                    String strColor = _Element.GetAttribute("color");
                    if (strColor != "")
                    {
                        Foreground = ColorTranslator.FromHtml(strColor);
                    }
                    if (mouseX >= _Element.Left && mouseY >= _Element.Top && mouseX < _Element.Left + _Element.Width && mouseY < _Element.Top + _Element.Height)
                    {
                        Foreground = Color.White;
                    }


                    if (_Element.GetAttribute("position") == "absolute")
                    {

                        d.DrawString(_Element.Data, labelFont, new SolidBrush(Foreground), new RectangleF(left, top, width, height));
                        break;
                    }
                    d.DrawString(_Element.GetAttribute("text"), new Font("MS Sans Serif", 8), new SolidBrush(Foreground), new Point(left, top));
                   
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
        /// Draw inside an certain view
        /// </summary>
        /// <param name="p">The graphics buffer</param>
        /// <param name="CurrentView"> The view to base from</param>
        public void Draw(Graphics p)
        {

            this.BackColor = Color.FromArgb(50, 50, 50);
            if (CurrentView != null)
            {  
                if(CurrentView.Content!=null)
                    if (CurrentView.Content.View == null)
                       CurrentView.Content.Serialize();
            }

            int entryship = 0;
            
            // Top increases after all elements to get a page feeling 
            // (next element below the previous, when elements has an @TOP value as top paramater (-1))
            int ptop=20 ;
            
            if (D == null)
                D = new BufferedGraphicsContext();
            BufferedGraphics R = D.Allocate(p, this.Bounds);
            Graphics d = R.Graphics;
            d.FillRectangle(new SolidBrush(Bg), this.Bounds);

            /**
             * If the currentView isn't null begin draw all elements on the board
             * */
            if(CurrentView!=null)
                if(CurrentView.Content != null)
                if (CurrentView.Content.View != null)

                    for (int i = 0; i < CurrentView.Content.View.Sections[currentSection].Elements.Count; i++)
                {
                    // Calculate the view coordinates of the element

                    Element _Element = CurrentView.Content.View.Sections[currentSection].Elements[i];
                    Rectangle ScreenCoordinates = _Element.GetCoordinates(scrollX, scrollY , this.Bounds, 0);
                    // Draw the element and it's children
                     DrawElement(_Element, d, ref entryship, ScreenCoordinates,3);
                   

                  
                   



                }
            
          
            R.Render();
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

        // Mouse coordinates
        int masX, masY;
        public string dragURI = "";
        private void Artist_MouseDown(object sender, MouseEventArgs e)
        {
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
                   
                    Rectangle ScreenBounds = _Element.GetCoordinates(scrollX, scrollX , this.Bounds, 0);
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
                dragging=true;
            }
            int entryship = 0;
            int top = 20;
            this.Cursor = Cursors.Default;
            try
            {
                foreach (Element _Element in CurrentView.Content.View.Sections[currentSection].Elements)
                {
                  	int _left = _Element.Left;
                 	int _top = _Element.Top - scrollY;
                 	int _width = _Element.Width == -1 ? this.Width : _Element.Width;
                 	int _height = _Element.Height == -1 ? this.Height : _Element.Height;
                	if (e.Y >= _top && e.Y <= _height+_top + 20 && e.X >= _left && e.X >= _left+_width  && _Element.GetAttribute("link")=="true")
                    {
                        this.Cursor = Cursors.Hand;
                   }
                	 if(_Element.Type == "entry")
                    {
                	 	if((e.X >= ARTISTLEFT+_left) && e.X <= ARTISTLEFT+_left+this.CreateGraphics().MeasureString(_Element.GetAttribute("author"),new Font("MS Sans Serif",8)).Width && _Element.GetAttribute("uri@author")!="")
                    	{
                    		this.Cursor = Cursors.Hand;	
                    	}
                    }
                }
            }
            catch
            {
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
            try
            {
                foreach (Element _Element in CurrentView.Content.View.Sections[currentSection].Elements)
                {
                    switch (_Element.Type)
                    {
                        case "entry":
                            if (_Element.GetAttribute("position") == ("absolute"))
                            {
                                int _left = int.Parse(_Element.GetAttribute("left"));
                                int _top = int.Parse(_Element.GetAttribute("top"));
                                int _width = int.Parse(_Element.GetAttribute("width"));
                                int _height = int.Parse(_Element.GetAttribute("height"));
                                if (mouseX >= _left - scrollX && mouseX <= _left + _width - scrollX && mouseY >= _top - scrollY && mouseY <= _top + _height - scrollY)
                                {
                                    if (ItemClicked != null)
                                        ItemClicked(this, _Element.GetAttribute("href"));
                                }
                                break;
                            }
                            if (mouseX >= LEFT - scrollX && mouseY >= top - scrollY && mouseY <= top - scrollY + ROWHEIGHT)
                            {

                                if (ItemClicked != null)
                                    ItemClicked(this, _Element.GetAttribute("href"));
                               
                            }

                            break;
                        case "header":

                            if (mouseY >= top - scrollY && mouseY <= top - scrollY + 20 && mouseX >= LEFT - scrollY)
                            {
                                this.Cursor = Cursors.Hand;
                            }
                            top += 40;
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




                }
            }
            catch
            {
            }
        }
    
        
        void DrawBoardMouseUp(object sender, MouseEventArgs e)
        {
       		dragging=false;
        }
        
        void DrawBoardDragDrop(object sender, DragEventArgs e)
        {
        	 dragging=false;
        }
    }
   
}
