using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Threading;
using System.Xml;
using System.Drawing;
using System.Collections;
namespace Board
{
    
[Serializable]
  	public class Spofity
	{

       
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
    /// Gets or sets the parent view for this Spofity instance
    /// </summary>
    public DrawBoard.View ParentView { get; set; }
        /// <summary>
        /// Delegate to manage playbacks from the view's list
        /// </summary>
        /// <param name="sender">The element sender</param>
        /// <param name="uri">The uri to the current playing</param>
        /// <returns>A boolean whether the playing could be started or not</returns>
        public delegate bool ElementPlaybackStarted(Spofity sender,Element element, String uri);


        /// <summary>
        /// occurs when an track has been choosed for playback
        /// </summary>
        public event ElementPlaybackStarted PlaybackStarted;

        /// <summary>
        /// This variable holds the collection of the elements at the Layout Element phase
        /// </summary>
        public XmlDocument LayoutElements { get; set; }

        /// <summary>
        /// The instance to the script engine that rendered the layout elements, and will continue it's lifecycle as helper
        /// scripts for various tasks after the preprocessing.
        /// </summary>
        public IScriptEngine ScriptEngine
        {
            get
            {
                return Engine.RuntimeMachine;
            }
        }

        /// <summary>
        /// The instance of the makoEngine that rendered the view.
        /// </summary>
        public MakoEngine Engine { get; set; }
        /// <summary>
        /// Play an item
        /// </summary>
        /// <param name="item"></param>
        public void PlayItem(Element item,int Position,int section)
        {
            Playlist = new Queue<Element>();
            // Get the list of elements
            List<Element> elements = this.View.Sections[section].Elements;
            /**
                * Iterate through the elements, once it has reached the one equal to the input item,
                * begin collecting all items which are classified with the type 'Entry'
                * */

            bool found=false;
            foreach (Element d in elements)
            {
                // If the current has been found and the item is an entry add it to the playlist
                if (found && d.Type == "entry")
                {
                    Playlist.Enqueue(d);
                }

                if (d == item)
                {
                    // Set the playing attribute to true
                    d.SetAttribute("__playing", "true");
                    found = true;
                    continue;
                }
                else
                {
                    d.SetAttribute("__playing", "");
                }

            }

            // Raise the playback started event
            if (PlaybackStarted != null)
                PlaybackStarted(this,item, item.GetAttribute("uri"));
        }
        /// <summary>
        /// Called by the script to initiate an ajax-like process of new elements
        /// </summary>
        /// <param name="adress"></param>
        /// <param name="callback"></param>
        /// <returns>true if sucess, false if not</returns>
        public object __downloadContentAsync(string adress, string callback)
        {
            try
            {
                // Create new content receiver
                ContentReceiver D = new ContentReceiver();

                // define parameters
                D.Adress = adress;
                D.Callback = callback;

                // Start an individual transmittion inside each content receiver
                Thread CN = new Thread(D.DownloadData);
                CN.Start(D.Adress);
                return true;
            }
            catch
            {
                return false;
            }
        }

        

        /// <summary>
        /// List with all content receivers. It are checked regulary for finished downloads.
        /// </summary>
        public List<ContentReceiver> Receivers { get; set; }

        /// <summary>
        /// Content receiver is an class which performs as an storage of downloaded content, which will be handled
        /// separetely
        /// </summary>
        public class ContentReceiver
        {
            /// <summary>
            ///  The adress to the remote or local resource
            /// </summary>
            public string Adress { get; set; }

            /// <summary>
            /// The object received. Package is an boxed false if the download were incomplete.
            /// </summary>
            public object Package { get; set; }

            /// <summary>
            /// Returns if the ContentReceiver has finished transmittion of content
            /// </summary>
            public bool Ready
            {
                get
                {
                    return Package != null;
                }
            }
            /// <summary>
            /// The callback of the event to raise together with the package once deliverd
            /// </summary>
            public string Callback { get; set; }

            /// <summary>
            /// Synchronize data is called by the javascript preparser to get an ready to use JSON parsed data. If the dat can't be parsed as JSON
            /// it will be returned as an common string
            /// </summary>
            /// <param name="receiver">An boxed instance of an ContentReceiver class</param>
            /// <returns></returns>
            public void DownloadData()
            {
                ContentReceiver Receiver = this;
                string uri = Receiver.Adress;
                // Create web request
                WebClient WC = new WebClient();
                /**
                 * Try getting data. If no data was got an all, return FALSE
                 * */
                try
                {
                    String jsonRaw = WC.DownloadString(new Uri((string)uri));

                    // Convert it to JSON
                    try
                    {
                        Jint.JintEngine Engine = new Jint.JintEngine();
                        Jint.Native.JsObject D = new Jint.Native.JsObject((object)jsonRaw);

                        // Try parse it as json, otherwise try as xml and if not retuurn it as an string
                        try
                        {
                            // Do not allow CLR when reading external scripts for security measurements
                            System.Web.Script.Serialization.JavaScriptSerializer d = new System.Web.Script.Serialization.JavaScriptSerializer();
                            object json = d.DeserializeObject(jsonRaw);
                            Receiver.Package = json;
                        }
                        catch
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(jsonRaw);
                            Receiver.Package = jsonRaw;
                        }


                    }
                    catch
                    {
                        Receiver.Package = jsonRaw;
                    }
                }
                catch
                {

                    Receiver.Package = false;
                }
            }
        }
        private int currentSection = 0;
        public int CurrentSection
        {
            get
            {
                return currentSection;

            }
            set
            {
                currentSection = value;
                this.ScrollY = 0;
                this.ScrollX = 0;
            }
        }

        /// <summary>
        /// Set item after current playing as next song.
        /// </summary>
        public void NextSong()
        {
            
            // Raise the playback event again
            this.view.Sections[CurrentSection].PlayIndex++;
        }
        /// <summary>
        /// The drawboard has an playlist stack which is used for listing of media items.
        /// </summary>
        public Queue<Element> Playlist {get;set;}

        /// <summary>
        /// The element which represents the now playing song
        /// </summary>
        public Element NowPlaying { get; set; }
            
  	 	public int ScrollY =0;
  	 	public int ScrollX =0;
	    public delegate void ActionEvent();
	    public event ActionEvent BeginLoading;
	    public event ActionEvent FinishedLoading;
	    public bool Loaded {get;set;}
	    private View view;
	    public View View
	    {
	        get
	        {
	            return view;
	        }
	        set
	        {
	            view = value;
	        }
	    }
	    private string uri;
	    public string URI
	    {
	        get
	        {
	            return uri;
	        }
	        set
	        {
	            uri = value;
	        }
	    }
	    Stream RawData;
	    public void Process()
	    {
	            
	
	    }
	    private  int topPos=0;
	    public int TopPos
	    {
	        get
	        {
	        	return topPos;
	        }
	        set{
	        	topPos=value;
	        }
	    }
	    private int countItems=0;
	    public int CountItems
	    {
	        get
	        {
	        	return countItems;
	        }
	        set
	        {
	        	countItems=value;
	        }
	    }
            
	    public static int ITEM_HEIGHT = 20;
	    public static int LIST_LEFT = 140;	
	    public  static void SecElement(ref Element X,Spofity R)
	    {
	        int topPos = R.TopPos;
	        if(X.GetAttribute("position")==("absolute"))
        	{
        		try
        		{
        		X.Top = int.Parse(X.GetAttribute("top").Replace("@top",topPos.ToString()));
        		X.Left = int.Parse(X.GetAttribute("left"));
        		}
        		catch
        		{
        			X.Top = 0;
        			X.Left = 0;
        		}
        	}
        	else
        	{
        		X.Top = topPos;
        		X.Left = 0;
        	}
	                		
	                	switch(X.Type)
	                	{
	                				
	                		case "sp:space":
	                			
	                			topPos+=int.Parse(X.GetAttribute("distance"));
	                				
	                			break;
	                		case "sp:entry":
	                			X.Height = ITEM_HEIGHT;
	                			if(X.GetAttribute("position")!="absolute")
	                			{
	                				X.Left = LIST_LEFT;
	                				X.Width =-1;
	                				X.Top = topPos;
	                			}
	                			
	                				
	                			break;
	                		case "sp:header":
	                			if(X.GetAttribute("position")!="absolute")
	                			{
	                				X.Left = LIST_LEFT;
	                				X.Width =-1;
	                				X.Top = topPos;
	                			}
	                			break;
	                		case "sp:label":
	                			if(X.GetAttribute("position")!="absolute")
	                			{
	                				X.Left = LIST_LEFT;
	                				X.Width =-1;
	                				X.Top = topPos;
	                			}
	                					break;
	                		case "sp:section":
	                			X.Height = ITEM_HEIGHT;
	                			if(X.GetAttribute("position")!="absolute")
	                			{
	                				
	                				X.Left = LIST_LEFT;
	                			}
	                			X.Width = -1;
	                			break;
	                		case "sp:image":
	                			X.Width = int.Parse(X.GetAttribute("width"));
	                			X.Height = int.Parse(X.GetAttribute("height"));
	                			break;
	                	}
	                	if(X.GetAttribute("position")!="absolute")
	                	{
	                			topPos+=ITEM_HEIGHT;
	                			R.topPos+=ITEM_HEIGHT;
	                	}
	                R.CountItems++;
	    }
	    public void Serialize()
	    {
	           
	            
	    }
        /// <summary>
        /// This method will convert the xml attributes to element attributes
        /// </summary>
        /// <param name="elm">The target element to manage (reference)</param>
        /// <param name="node">The element to inherit the attributes from</param>
        public void AppendElementAttributes(ref Element elm, XmlElement node)
        {
            foreach (XmlAttribute _attribute in node.Attributes)
            {
                // Lower attribute names so we don't get any trouble
                elm.SetAttribute(_attribute.Name.ToLower(), _attribute.Value);
            }
            /**
             * 2011-04-25 17:12 
             * The song name screwed up so much so we decided to change many things in the application!
             * Now attributes to the element can be done through sub elements!
             * */
            foreach (XmlNode CT in node.ChildNodes)
            {
                if (CT.GetType() == typeof(XmlElement))
                {
                    elm.SetAttribute( CT.Name.ToLower(), CT.InnerText);
                }
            }
        }
        /// <summary>
        /// Function to parse an css clausul.
        /// </summary>
        /// <param name="expressions">Ann CSS inline expression { inside }</param>
        /// <returns>An dictionary of expressions inside</returns>
        public Dictionary<String, String> ParseCssString(String expressions)
        {
            // Dictionary for the output values
            Dictionary<String, String> Output = new Dictionary<string, string>();

            // Create an string builder for the expression to put data on
            StringBuilder Attribute = new StringBuilder();
            StringBuilder Value = new StringBuilder();

            // boolean indicating you're on the expression side (after first : token)
            bool inExpression = false;
            for (int i = 0; i < expressions.Length; i++)
            {
                // Get current char
                char c = expressions[i];

                // If not in an expression create an attribute expression
                if (!inExpression)
                {
                    // If c has reached an : switch over to expression mode
                    if (c == ':')
                    {
                        inExpression = true;
                        continue;
                    }

                    // else append an c
                    Attribute.Append(c);
                }
                else
                {
                    // if c reached an ; end the expression create 
                    // an entry to the dictionary with trimmed whitespaces
                    if (c == ';')
                    {
                        Output.Add(Attribute.ToString().Trim(), Value.ToString().Trim());

                        // Reset all variables

                        inExpression = false;

                        // flush all buffers
                        Attribute = new StringBuilder();
                        Value = new StringBuilder();
                        continue;
                    }
                    Value.Append(c);
                }
            }
            return Output;

        }
        /// <summary>
        /// Fetch data from either hard drive or remote web address
        /// </summary>
        /// <param name="address"></param>
        /// <returns>the text data if success, "ERROR "+Message if failed</returns>
        public String SynchronizeData(String address)
        {
            // If address starts with http: loockup it at an remote source and downloda it if possible
            if (address.StartsWith("http:"))
            {
                WebClient WC = new WebClient();
                try
                {
                    String data = WC.DownloadString(address);
                    return data;
                }
                catch(Exception e)
                {
                    return "ERROR: "+e.Message ;
                }
            }
            else
            {
                try
                {
                    String result = "";
                    // Grab data from local harddrive
                    using (StreamReader SR = new StreamReader(address))
                    {

                        result = SR.ReadToEnd();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    return "ERROR: "+e.Message;
                }
            }
        }
        #region ScriptFunctions

        /// <summary>
        /// callback for manipulation of layout elements during runtime
        /// </summary>
        /// <param name="name">the name of the tag</param>
        public XmlNodeList __getElementsByTagName(string name)
        {
            return LayoutElements.GetElementsByTagName(name);
        }
        /// <summary>
        /// callback for manipulation of layout elements during runtime
        /// </summary>
        /// <param name="name">the name of the tag</param>
        public object __getElementId(string name)
        {
            object cf =  GetElementById(LayoutElements.DocumentElement,name);
            return cf;
        }
        /// <summary>
        /// Method to set inner content of an element from scripside
        /// </summary>
        /// <param name="element"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public object __setContent(object element, string content)
        {
            XmlElement elm = (XmlElement)element;
            elm.InnerText = content;
            return null;
        }
        /// <summary>
        /// Method to set an attribute to an certin value on an layout element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public object __setAttribute(object element, string attribute,string value)
        {
            XmlElement elm = (XmlElement)element;
            elm.SetAttribute(attribute, value);
            return null;
        }
    /// <summary>
    /// Own implementation of getElementById as XmlDocument's getelementsbyId could not be
    /// used without DTD
    /// </summary>
    /// <param name="src">the xml document to work on</param>
    /// <param name="ID">the id of the element to find</param>
    /// <remarks>This function is recursive</remarks>
    /// <returns></returns>
        public XmlElement GetElementById(XmlElement src, string val)
        {

            XmlElement getElement = null;
            if (src.HasAttribute("id"))
                if (src.GetAttribute("id") == val)
                    return src;
            foreach (XmlElement Node in src.ChildNodes)
            {
                if (Node.NodeType == XmlNodeType.Element)
                {
                    XmlElement elm = (XmlElement)Node;
                     getElement = GetElementById(elm, val);
                     if (getElement != null)
                         return getElement;
                         
                }
            }
            return null;
        }

        /// <summary>
        /// This method should be called by the scripting engine after an modification so the element
        /// buffer will be rebuilt with the changes in the layout engine.
        /// </summary>
        public object __commit_changes()
        {
            Render();
            return null;
        }
        #endregion
        /// <summary>
        /// DATE: 2011-04-20
        /// If the tag name is %Inflater the element can inflate another view,
        /// preparse it according to arbitrary parameters and then
        /// return it to this layer. 
        /// </summary>
        /// <param name="CT">The element to use as inflater caller</param>
        /// <param name="host">The element to inflate from</param>
        /// <remarks>
        /// The script running when inflating the child view is not able to interact with the host session,
        /// and have their own scope.
        /// </remarks> 
        public void InflateView(Section srcSection,Element item, XmlNode CT)
        {
                
                
                // Create additional makoengine
                MakoEngine ME = new MakoEngine();
                if (this.MakoGeneration != null)
                    this.MakoGeneration(ME, new EventArgs());
                XmlElement _Element = (XmlElement)CT;
                    String ViewMako = "";
                // Load the view by the attribute src but if the name starts with component_ inflate instead it from the tag name in the folder components
                    if (CT.Name.StartsWith("component_"))
                    {
                        ViewMako = SynchronizeData(CT.Name.Replace("component_", ""));
                    }
                    else
                    {
                        ViewMako = SynchronizeData(_Element.GetAttribute("src"));
                    }

                // If the operation failed leave the function
                if (ViewMako.StartsWith("ERROR:"))  
                    return;

                // Add all properties of the element as arg_{attribute} to the child layer
                foreach (XmlAttribute attr in CT.Attributes)
                {
                    ME.RuntimeMachine.SetVariable("arg_" + attr.Name, attr.Value);
                }

                   
                // Otherwise preprocess the layer.
                    String Result = ME.Preprocess(ViewMako,"",true);

                // then inflate the data
                // Create xml document
                XmlDocument d = new XmlDocument();
                d.LoadXml(Result);

                // All inflates can only have one section
                XmlNodeList Sections = d.GetElementsByTagName("section");
                XmlElement iSection = (XmlElement)Sections[0];
                {
                    // Create new section
                    Section _Section = new Section(this);

                    
                       
                    // Inflate all elements of the subsection into the list
                    RenderSection(srcSection, iSection);
                        
                }
              
        }


            
        /// <summary>
        /// This is an recursive method which will create elements to the 
        /// Board to view.
        /// </summary>
        /// <remarks>This function calls iself. The amount of elements supported is limited to enasure portably and does not follow the whole w3c web standard</remarks>
        /// <param name="C">The base element to begin on</param>
        /// <param name="iSection">The section to start with</param>
        /// <param name="srcSection">The section for the elements to apply on, used by the inflater</param>
        public Element RenderElements(Section srcSection, Element C, XmlElement iSection)
        {
            // Iterate through the fields and create elements
            XmlNodeList items = iSection.ChildNodes;
            foreach (XmlNode node in items)
            {
                if (node.GetType() != typeof(XmlElement))
                    continue;
                XmlElement item = (XmlElement)node;
                // Create various kind of controls depending of node type
                Element CT = new Element(srcSection,this.ParentBoard);

                // If nothing else specified, the element top should be managed by the drawing cache
                CT.SetAttribute("top", "@TOP");

                // Assert an style template if provided
                if (item.Name == "inflate")
                {
                    InflateView(srcSection,CT, node);
                    continue;
                }
                     
                Dictionary<String, String> Style = item.HasAttribute("style") ? 
                    ParseCssString(item.GetAttribute("style")) : 
                    new Dictionary<String,String>();

                // Append all custom attributes first
                AppendElementAttributes(ref CT, item);
                #region InflaterService
                   
                    
                    
                #endregion

                // Set the type of the element to the item's tag definition
                CT.SetAttribute("type", item.Name);
                CT.SetAttribute("text", item.InnerText);
                CT.Data = item.InnerText;

                // By default the top of the element should be set according to the page settings
                CT.SetAttribute("top", "@TOP");

                // Convert attribute bounds to native ones
                CT.AssertBounds(false);

                // The integer specifies the top position. @TOP token means this variable should set the height
                   
                    
                // Tweek the element behaviour according to some specific tag names
                 
                switch (item.Name.ToLower())
                {
                    case "p":
                        CT.SetAttribute("type", "label");
                        break;
                    case "h1":
                          
                           
                       
                         
                        CT.SetAttribute("size","15");
                        break;
                    case "button":
                            
            
                          
                        CT.SetAttribute("text", item.InnerText);
                        CT.Data = item.InnerText;
                          
                        break;
                    case "img":

                        CT.SetAttribute("type", "image");
                           
                        break;
                }
                CT.AssertBounds(false);
                C.Elements.Add(CT);

                // Do this for children
                CT= RenderElements(srcSection,CT, item);

            }
            return C;
        }
        /// <summary>
        /// This is an recursive method which will create elements on an section
        /// Board to view.
        /// </summary>
        /// <remarks>This function calls iself. The amount of elements supported is limited to enasure portably and does not follow the whole w3c web standard</remarks>
        /// <param name="C">The base element to begin on</param>
        /// <param name="iSection">The section to start with</param>
        public Section RenderSection(Section C, XmlElement iSection)
        {
            // Iterate through the fields and create elements
            XmlNodeList items = iSection.ChildNodes;
            foreach (XmlNode node in items)
            {
                if (node.GetType() != typeof(XmlElement))
                    continue;
                XmlElement item = (XmlElement)node;
                // Create various kind of controls depending of node type
                Element CT = new Element(C,this.ParentBoard);

                // If nothing else specified, the element top should be managed by the drawing cache
                CT.SetAttribute("top", "@TOP");

                // Assert an style template if provided
                if (item.Name == "inflate")
                {
                    InflateView(C,CT, node);
                    continue;
                }

                // Assert an style template if provided

                Dictionary<String, String> Style = item.HasAttribute("style") ?
                    ParseCssString(item.GetAttribute("style")) :
                    new Dictionary<String, String>();

                // Append all custom attributes first
                AppendElementAttributes(ref CT, item);
                CT.AssertBounds(false);
                // Set the type of the element to the item's tag definition
                CT.SetAttribute("type", item.Name);
                CT.Type = item.Name;
                CT.SetAttribute("text", item.InnerText);
                CT.Data = item.InnerText;

                // By default the top of the element should be set according to the page settings
                CT.SetAttribute("top", "@TOP");

                // The integer specifies the top position. @TOP token means this variable should set the height


                // Tweek the element behaviour according to some specific tag names

                switch (item.Name.ToLower())
                {
                    case "p":
                        CT.SetAttribute("type", "label");
                        break;
                    case "h1":




                        CT.SetAttribute("size", "15");
                        break;
                    case "button":



                        CT.SetAttribute("text", item.InnerText);
                        CT.Data = item.InnerText;

                        break;
                    case "img":

                        CT.SetAttribute("type", "image");

                        break;
                }
                C.Elements.Add(CT);
                CT.AssertBounds(false);

                // Do this for children
                RenderElements(C,CT, item);

            }
            return C;
        }

	    public Thread loadThread;
	    public void LoadData()
	    {
	
	        if(BeginLoading!=null)
	        BeginLoading();
	        loadThread = new Thread(Process);
	        loadThread.Start();
	    }
        /// <summary>
        /// Render the layoutElements into real elements
        /// </summary>
        public void Render()
        {
            try
            {

                // Reset the ptop
                Element.ptop = 20;
                // Clear current buffer 
                this.view.Sections.Clear();


                // Create xml document
                XmlDocument d = this.LayoutElements;
                if (d == null)
                    return;
                this.View = new View();
                // iterate through all sections of the page
                XmlNodeList Sections = d.GetElementsByTagName("section");
                foreach (XmlElement iSection in Sections)
                {
                   

                    // If the section element not has the root element as parent skip it
                    if (iSection.ParentNode.Name != "view")
                        continue;
                    // Create new section
                    Section _Section = new Section(this);

                    // Set the section's reorder mode
                    if (iSection.HasAttribute("reorder"))
                        if (iSection.GetAttribute("reorder") == "true")
                            _Section.Reorder = true;

                    // set section name
                    _Section.Name = iSection.GetAttribute("name");

                    // set section as an list (show listheaders) if list attribute exists
                    _Section.List = iSection.HasAttribute("list");

                    // Render element sections
                    _Section = RenderSection(_Section, iSection);

                    this.view.Sections.Add(_Section);


                }

                SetScriptFunctionality();
                LoadData();
            }
            catch
            {

            }
        }
        /// <summary>
        /// Hashcode of the LayoutElements XMLDocument
        /// </summary>
        private int xmlHashCode = 0;

    /// <summary>
    /// Returns whether the LayoutElements has been changed since last occuration
    /// </summary>
        public void CheckPendingChanges()
        {
            
                /** If the hashcode of the LayoutElements is different from the one stored
                 * there is an update ongoing
                 * */
                if (xmlHashCode != LayoutElements.GetHashCode())
                {
                    xmlHashCode = LayoutElements.GetHashCode();

                    // Update the views
                 
                }
                
            
        }
        /// <summary>
        /// Should be called from an background thread to update the layout elements to new ones. 
        /// </summary>
        public void UpdateAsync()
        {
           
#if (EXPERIMENTAL)
           
            /**
             * Preprocess the page again
             * */
            String r = this.Engine.Preprocess(this.TemplateCode,this.Parameter,false);

            /**
             * Only refresh the layout elements and thus the render elements
             * if the new rendering results in changes and not an error token
             * */
            if(r != "NONCHANGE" && !r.StartsWith("ERROR:"))
            {
           
                XmlDocument XD = new XmlDocument();
                XD.LoadXml(r);

                // Render new time
                this.LayoutElements = XD;
                this.Render();
            }

#endif
        }
        /// <summary>
        /// Method to configure scripts dom manipulation functions on layout element
        /// </summary>
        public void SetScriptFunctionality()
        {
            // Set the wrapper function so people can manipulate the layout element level
            this.ScriptEngine.SetFunction("$", new Func<string, object>(__getElementId));
            // Set the commit function so people can turn the modified layout element stadge into an object element stadge
            this.ScriptEngine.SetFunction("commit", new Func<object>(__commit_changes));
            // Set an function so script can change attribute of an element
            this.ScriptEngine.SetFunction("setAttribute", new Func<object, string, string, object>(__setAttribute));

            // Set an function so script can change inner content of an element
            this.ScriptEngine.SetFunction("setContent", new Func<object, string, object>(__setContent));

            // Set an ajax like function
            this.ScriptEngine.SetFunction("downloadContent", new Func<string, string, object>(__downloadContentAsync));
        }
        
        /// <summary>
        /// The drawboard this view is attached to
        /// </summary>
        public DrawBoard ParentBoard { get; set; }
        
	    /// <summary>
	    /// Load an custom HTML page into the special section.
        /// 
        /// It have to be preparsed by the MakoEngine.
	    /// </summary>
        /// <remarks>Objects must now call Initialize to make it work perform. The reason for change is to make it able to set event handlers for MakoCreation</remarks>
	    /// <param name="data">The ready preprocessed data from Mako alt. common html data</param>
	    public Spofity(DrawBoard parentBoard)
	    {

            ParentBoard = parentBoard;
	        
	
	
	
	    }
        /// <summary>
        /// Holds the preprocessed template
        /// </summary>
        public String TemplateCode { get; set; }
        /// <summary>
        /// The view instances parameter
        /// </summary>
        public String Parameter { get; set; }

    /// <summary>
    /// Initializes the view
    /// </summary>
    /// <param name="data"></param>
    /// <param name="engine"></param>
    /// <param name="pretemplate">The mako syntaxed template. Used for recurring updates</param>
        public void Initialize(string parameter,string pretemplate, string data, MakoEngine engine)
        {
            this.Receivers = new List<ContentReceiver>();

            /**
             * 
             * Associate instance data
             * */

            this.Parameter = parameter;

            this.Engine = engine;
            this.TemplateCode = pretemplate;
            SetScriptFunctionality();

            // Create xml document
            XmlDocument d = new XmlDocument();


                d.LoadXml(data.Replace(";",""));

            // Set the xmlHashCode to the xmldocument's instance so it won't be any collision
            this.xmlHashCode = d.GetHashCode();
            // Set this layoutelements to the xmldocument loaded
            this.LayoutElements = d;


            this.View = new View();
            // iterate through all sections of the page
            XmlNodeList Sections = d.GetElementsByTagName("section");
            foreach (XmlElement iSection in Sections)
            {

                // If the section element not has the root element as parent skip it
                if (iSection.ParentNode.Name != "view")
                    continue;
                // Create new section
                Section _Section = new Section(this);

                // Set the section's reorder mode
                if (iSection.HasAttribute("reorder"))
                    if (iSection.GetAttribute("reorder") == "true")
                        _Section.Reorder = true;

                // Append nowplaying handler
                _Section.PlaybackItemChanged += new ElementPlaybackStarted(_Section_PlaybackItemChanged);

                // set section name
                _Section.Name = iSection.GetAttribute("name");

                // set section as an list (show listheaders) if list attribute exists
                _Section.List = iSection.HasAttribute("list");

                // Render element sections
                _Section = RenderSection(_Section, iSection);

                this.view.Sections.Add(_Section);
                Element.ptop = 20;


            }

            /***
             * 2011-04-23 23:03
             * Load toolbar
             * */
            XmlNodeList toolItems = d.GetElementsByTagName("toolbar");

            // If the toolbar has elements inflate them
            if (toolItems.Count > 0)
            {
                XmlNodeList ToolItems = ((XmlElement)toolItems[0]).GetElementsByTagName("item");
                
                // Extract all items
                foreach (XmlElement item in ToolItems)
                {
                    if (item.GetType() == typeof(XmlElement))
                    {
                        XmlElement Item = (XmlElement)item;

                        // dummy toolsection
                        Section toolSection = new Section(this);
                        Element _Item = new Element(toolSection,this.ParentBoard);

                        // set item type according to tag name
                        _Item.Type = Item.Name;

                        // Attach the element's attributes
                        AppendElementAttributes(ref _Item, item);

                        // if the item has the type menu inflate it's menuitems
                        if (_Item.GetAttribute("type") == "menu")
                        {
                            XmlNodeList MenuItems = item.GetElementsByTagName("menuitem");
                            foreach (XmlElement menuItem in MenuItems)
                            {
                                Element _menuItem = new Element(toolSection,this.ParentBoard);
                                _menuItem.Type = "menuitem";
                                // Append xml attributes
                                AppendElementAttributes(ref _menuItem, menuItem);
                                _Item.Elements.Add(_menuItem);
                            }
                           
                        }

                        // add the item to the menubar
                        this.view.Toolbar.Items.Add(_Item);


                    }
                }
            }

            LoadData();
        }

        bool _Section_PlaybackItemChanged(Spofity sender, Element element, string uri)
        {
            if(this.PlaybackStarted!=null)
                return this.PlaybackStarted(sender, element, uri);
            return false;
        }
	 
	    void Spofity_FinishedLoading()
	    {	
	
	          
	    }
	}
	
	public class UL
	{
	    public UL()
	    {
	        lis = new List<LI>();
	    }
	    [XmlElement("li")]
	    private List<LI> lis;
	    public List<LI> Lis
	    {
	        get
	        {
	            return lis;
	        }
	        set
	        {
	            lis = value;
	        }
	    }
	    public class LI
	    {
	        public LI()
	        {
	            lis = new List<LI>();
	        }
	        [XmlElement("li")]
	        private List<LI> lis;
	        public List<LI> Lis
	        {
	            get
	            {
	                return lis;
	            }
	            set
	            {
	                lis = value;
	            }
	        }
	    }
	}
	
	[XmlRoot("html")]
	public class HTML : View
	{
	
	
	    private List<Section> sections;
	    [XmlElement("p")]
	    public List<Section> Sections
	    {
	        get
	        {
	            return sections;
	        }
	        set
	        {
	            sections = value;
	        }
	    }
	}
	[XmlRoot("view")]
	[Serializable]
	public class View
	{
	    public View()
	    {
	        sections = new List<Section>();
	        Sets = new List<Set>();
            Toolbar = new Toolbar();
	    }
        public Toolbar Toolbar { get; set; }
	    private List<Section> sections;
	    [XmlElement("section")]
	    public List<Section> Sections
	    {
	        get
	        {
	            return sections;
	        }
	        set
	        {
	            sections = value;
	        }
	    }
	    [XmlElement("set")]
	    public List<Set> Sets { get; set; }
	/*    private List<UL> uls;
	    public List<UL> Uls
	    {
	        get
	        {
	            return uls;
	        }
	        set
	        {
	            uls = value;
	        }
	    }*/
	    /* [XmlAttribute("name")]
	    public string Name;
	    [XmlAttribute("url")]
	    public string Url;*/
	
	}
    public class Toolbar
    {
        public List<Element> Items { get; set; }
        public Toolbar()
        {
            Items = new List<Element>();
        }
    }
    /// <summary>
    /// Element class.
    /// </summary>
	public class Section
	{
        /// <summary>
        /// Rebuilds the collection
        /// </summary>
        public void RebuildList()
        {
            // reset ptop
            Element.ptop = 20;
            foreach (Element ct in this.elements)
            {
                ct.SetAttribute("left", ct.OldLeft.ToString());
                ct.SetAttribute("top", ct.OldTop.ToString());
                ct.AssertBounds(true);
            }
        }
        /// <summary>
        /// Gets or sets whether the entries in the view can be reordered.
        /// </summary>
        public bool Reorder { get; set; }
        /// <summary>
        /// In order to be able to use filter, an instance of an inherited class will assert the filtered query
        /// </summary>
        public interface  IViewFilter
        {
            /// <summary>
            /// Must be implemented to filter
            /// </summary>
            /// <param name="src"></param>
            /// <returns>Wheather the element is visible or not</returns>
             bool FilterElement(Element src,string query);
        }

        /// <summary>
        /// An temporary view if filtered.
        /// </summary>
        public List<Element> FilterView 
        {
            get;
            set;
        }
        /// <summary>
        /// An instance of the abstract class ViewFilter
        /// </summary>
        public IViewFilter Filter { get; set; } 
        /// <summary>
        /// Generates an filter view according to the query
        /// </summary>
        /// <param name="query"></param>
        public void GenerateFilterView(string query)
        {
            
            if (query == ""||query == null)
            {
                FilterView = null;
                return;
            }
            FilterView = new List<Element>();

            /**
             * If no instance of the filter class is set, return
             * */
            if (Filter == null)
                return;

            // Reset ptop
            Element.ptop = 20;

            // build view according to filter
            foreach (Element ct in this.elements)
            {
                if (Filter.FilterElement(ct,query))
                {
                    Element copy = ct.Copy();
                    FilterView.Add(copy);
                }
            }
           
        }
        private string filterQuery;
        
        /// <summary>
        /// Gets and sets the filtering query. 
        /// </summary>
        public String FilterQuery {
            get
            {
                return filterQuery;
                
            }
            set
            {
                filterQuery = value;
                GenerateFilterView(value);
            }
        
        }

        /// <summary>
        /// Occurs when playback has been interrupted.
        /// </summary>
        public event Spofity.ElementPlaybackStarted PlaybackItemChanged;
        /// <summary>
        /// The parent Spofity hosting the view
        /// </summary>
        public Spofity Parent { get; set; }
        /// <summary>
        /// Gets the current playing entry
        /// </summary>
        public Element NowPlaying
        {
            get
            {
                foreach (Element d in this.Elements)
                {
                    if (d.Type == "entry" && d.GetAttribute("__playing")!="")
                        return d;
                }
                return null;
            }
        }

        /// <summary>
        /// The index of the current item
        /// </summary>
        public int PlayIndex
        {
            get
            {
                // counter
                int i=0;

                // only increase counter if element is an entry
                foreach (Element d in this.Elements)
                {
                    if (d.Type == "entry")
                    {
                        if(d.GetAttribute("__playing")!="")
                            return i;
                        i++;
                    }
                        
                }
                return i;
            }
            set
            {
                
                foreach (Element d in this.Elements)
                {
                    if (d.Type == "entry")
                    {
                        if (d.GetAttribute("__playing") != "")
                        {
                            d.SetAttribute("__playing","");
                        }
                        
                    }

                }
                int counter =0;
                foreach (Element elm in this.elements)
                {
                    if (elm.Type == "entry")
                    {
                        if (counter == value)
                        {
                            elm.SetAttribute("__playing", "true");
                            if (this.PlaybackItemChanged != null)
                                this.PlaybackItemChanged(this.Parent, elm, elm.GetAttribute("uri"));
                        }
                        counter++;
                    }
                    
                }
            }
        }
        /// <summary>
        /// Gets and sets the selected index . Returns -1 if no entries was found. Applies only with elements of type "entry".
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                // index counter
                int i = 0;
                bool foundSelected = false;
                foreach (Element d in this.Elements)
                {
                    if (d.Type != "entry")
                        continue;
                    // if the previous gave foundSelected set this to selected


                    if (d.Selected == true)
                        return i;

                    i++;
                }
                return -1;
            }
            set
            {
           
                // Deactivate the selected items
                foreach (Element d in this.Elements)
                {

                    if (d.Type != "entry")
                        continue;
                    d.Selected = false;


                }
                // Set the item at the index as selected
                int index = 0;
                foreach (Element d in this.Elements)
                {
                    if (d.Type != "entry")
                        continue;
                    // if index meets the setter, mark it as selected
                    if (index == value)
                        d.Selected = true;

                    index++;



                }
            }

        }
        /// <summary>
        /// Returns an entry at the specified index.
        /// </summary>
        /// <remarks>Only elements of type "entry" is indexed</remarks>
        /// <param name="index">The position of the element to find</param>
        /// <returns>An element if found at the location, NULL otherwise</returns>
        public Element EntryAt(int index)
        {
            int i=0;
            foreach (Element d in this.elements)
            {
                
                    

                if (d.Type == "entry")
                {
                    if (index == i)
                        return d;
                    i++;
                }

            }
            return null;
        }

        /// <summary>
        /// Gets and sets if the Element is in an list mode. A list mode will draw column headers straight before the
        /// first entry element and associate it with screen top if the element are outside the screen boundary.
        /// </summary>
        public bool List { get; set; }
        /// <summary>
        /// Definite count of items. Obsolute
        /// </summary>
	    public int CountItems {get;set;}

        /// <summary>
        /// Short way to add element
        /// </summary>
        /// <param name="d"></param>
        /// <param name="X"></param>
	    public void AddElement(Element d,Spofity X)
	    {
	    	this.elements.Add(d);
	    	CountItems++;
	    	Board.Spofity.SecElement(ref d,X);
	    	/*if(d.GetAttribute("position")!="absolute")
	    	{
		    	switch(d.GetAttribute("type"))
		    	{
		    		case "sp:entry":
		    			d.Height=32;
		    			d.Top = X.TopPos;
		    			X.TopPos+=32;
		    				
		    			break;
		    		case "sp:divider":
		    			d.Height = 48;
		    			d.Top = X.TopPos+2;
		    			X.TopPos+=50;
		    			break;
		    		case "sp:header":
		    			d.Height = 48;
		    			d.Top = X.TopPos;
		    			X.TopPos+=32;
		    			break;
		    		case "sp:section":
		    			d.Height = 48;
		    			d.Top = X.TopPos;
		    			X.TopPos+=32;
		    			break;
		    	}
	    	}*/
	    		
	    }
	    public Section(Spofity parent)
	    { 
            Parent = parent;
	        elements = new List<Element>();
	        Sets = new List<Set>();
	    }
	    private string name;
	    [XmlElement("set")]
	    public List<Set> Sets { get; set; }
	    [XmlAttribute("name")]
	    public string Name
	    {
	        get
	        {
	            return name;
	        }
	        set
	        {
	            name = value;
	        }
	    }
	    private List<Element> elements;
	    [XmlElement("element")]
	    public List<Element> Elements
	    {
	        get
	        {
	            return elements;
	        }
	        set
	        {
	            elements = value;
	        }
	    }
	}
	public class Attribute
	{
	    [XmlAttribute("name")]
	    public string name;
	    [XmlAttribute("value")]
	    public string value;
	}
	
	public class Set
	{
	    [XmlElement("entry")]
	    public List<Entry> Entries { get; set; }
	    public Set()
	    {
	        Entries = new List<Entry>();
	    }
	    [XmlAttribute("image")]
	    public string Image { get; set; }
	}
	
	public class Entry
	{
	    [XmlAttribute("href")]
	    public string Href { get; set; }
	    [XmlAttribute("title")]
	    public string Title { get; set; }
	    [XmlAttribute("type")]
	    public string Type { get; set; }
	    [XmlAttribute("artist")]
	    public string Artist { get; set; }
	}

    /// <summary>
    /// An element represents the object drawn on the Board class in an particular view
    /// </summary>
	public class Element 
	{
        /// <summary>
        /// Gets the instance of the elements which it wa copied from or Null if it wasn't an copy. Used in the filter system
        /// </summary>
        public Element Original { get; set; }


        /// <summary>
        /// Gets whether this elements is an copy of another element or an original. Used in the filter system
        /// </summary>
        public bool IsCopy
        {
            get
            {
                return Original != null;
            }
        }

        /// <summary>
        /// Copies the instance to a new element
        /// </summary>
        /// <returns>An instance of an copy of this instance</returns>
        public Element Copy()
        {
            Element dc = new Element(this.ParentSection,this.ParentSection.Parent.ParentBoard);
            dc.type = this.type;
            dc.Parent = this.Parent;
            dc.Original = this;
            // Copy the attributes to the new instance
            foreach (Attribute at in this.attributes)
                dc.SetAttribute(at.name, at.value);
            // Copy the bounds
            dc.SetAttribute("left",this.OldLeft.ToString());
            dc.SetAttribute("top", this.OldTop.ToString());
            dc.SetAttribute("width",this.Width.ToString());
            dc.SetAttribute("height", this.Height.ToString());
            dc.AssertBounds(true);
            return dc;
        }

        public int OldLeft { get; set; }
        public int OldTop { get; set; }
       
        /// <summary>
        /// Gets and sets whether the object has been called. Currently this property is used to call image download
        /// handler when it tries to draw at the first time but prevent it are done always.
        /// </summary>
        public bool FirstCall;

        /// <summary>
        /// Gets and set the persistency. If true, the ptop will not be 
        /// changed after appending but will use the last valueo of it.
        /// </summary>
        public bool Persistent { get; set; }

        public Element Parent { get; set; }
        /// <summary>
        /// Returns coordinates coordinates for the object bounds
        /// </summary>
        /// <param name="scrollX">scrollX coordinate on view's state</param>
        /// <param name="scrollY">scrollY coordinate on view's state</param>
        /// <param name="top">Reference to top integer to set an dynamtic top positon (if asserted @TOP (-1))</param>
        /// <param name="Bounds">The bounds the object is residing in</param>
        /// <param name="padding">Padding rules applied to the workspace object relying in</param>
        /// <returns>an boolean wheather the object is inside the visible screen bounds</returns>
        /// 
        public Rectangle GetCoordinates(int scrollX,int scrollY,Rectangle Bounds,int padding)
        {
            Element _Element = this;
                
            int left = _Element.Left - scrollX;
            int top =  _Element.Top - scrollY ;
            int width = _Element.Width > 0 ?  _Element.Width :  Bounds.Width - left;
            int height = _Element.Height > 0 ? _Element.Height : this.Height;
            Rectangle Rect = new Rectangle(left, top, width, height);
                
            return Rect;
        }
        /// <summary>
        /// Function to measure wheather the element is inside the screen bounds, eg. visible
        /// </summary>
        /// <param name="scrollX">the scrollX position</param>
        /// <param name="scrollY">the scrollY position</param>    
        /// <param name="Bounds">The bounds the object is residing in</param>
        /// <param name="padding">Padding rules applied to the workspace object relying in</param>
        /// <returns></returns>
        public bool InsideScreen(int scrollX,int scrollY,Rectangle srcBounds,int padding)
        {
            Rectangle Bounds = GetCoordinates(scrollX, scrollY,srcBounds,padding);
            int top = Bounds.Top;
            int height = Bounds.Height;
            int Left = Bounds.Left;
            int Top = Bounds.Top;
            if (!(top >= 0 && top + height <= this.Height /*&& left >= scrollX && left+width <= scrollX*/))
                return true;
            return false;
        }
        /// <summary>
        /// Stylesheet applied to the element. Not yet in use, only declared
        /// </summary>
        public Dictionary<String, String> stylesheet;
        /// <summary>
        /// ptop is to auto-align elements which has no valid top location - eg. smaller 
        /// than zero and should be applied in this way. Default is 20 so we ge an margin at the top
        /// </summary>
        public static int ptop = 20;

        /// <summary>
        /// Assign bounds of the object according to the parameters that is set in the textual parameter list (attributes)
        /// </summary>
        public void AssertBounds(bool copy)
        {
                
            int top = 0;
            int left, width, height;

            // Try get integers from the attributes
            int.TryParse(GetAttribute("top"),out top);
            int.TryParse(GetAttribute("left"), out left);
            int.TryParse(GetAttribute("width"), out width);
            int.TryParse(GetAttribute("height"),out height);
            if (!copy)
            {
                OldLeft = left;
                OldTop = top;
            }
            // Get if the element is persisten
            bool persistent = false;
            bool.TryParse(GetAttribute("persistent"),out persistent);
            Persistent = persistent;
                
            // If the top variable is still below one, assign the top to the ptop variable and increase ptop iself   
            if (top < 1)
            {
                top = Element.ptop;
                    if (!Persistent)
                    {
                       
                        Element.ptop += height /2 ;
                        if (copy)
                            Element.ptop += height / 2;
                    }
                    else
                    {
                       

                    }
                   
            }
            // Apply the values to the native width/height markup
            this.Width = width;
            this.Left = left;
            this.Height = height;
            this.Top = top;
        }

        /// <summary>
        /// Gets the object's bounds in absolute rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Left, Top, Width, Height);
            }
        }

        /// <summary>
        /// Gets and set the top of the element
        /// </summary>
	    public int Top {get;set;}

        /// <summary>
        /// Gets and set the left position of the element
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Gets and sets the width of the element. Width below one is considered as filling width.
        /// </summary>
	    public int Width {get;set;}

        /// <summary>
        /// Gets and sets the height of the element. Values below one is considered as filling vertically.
        /// </summary>
	    public int Height {get;set;}

        /// <summary>
        /// Gets and sets the raw data of the object. For example an text node will has its content stored in this
        /// property.
        /// </summary>
        [XmlElement("data")]
        public string Data
        {
            get;
            set;
        }
        
        /// <summary>
        /// The drawboard the element is drawn on
        /// </summary>
        public DrawBoard ParentHost { get; set; }

        /// <summary>
        /// Function to set an attribute of the node.
        /// </summary>
        /// <param name="name">the name of the attribute</param>
        /// <param name="value">the value of the attribute</param>
        public void SetAttribute(String name, string value)
        {
            
            /** boolean indicating wheather an matching attribute was found, 
                * if not add an to the collection
                * */
            bool found = false;
            foreach (Attribute d in attributes)
            {
                if (d.name == name)
                {
                    d.value = value;
                    found = true;
                }
            }
            
            // If the attribute is an type set the type property to the value
            if (name == type)
            {
                this.type = value;
            }

            // If the attribute was not found create an new attribute
            if (!found)
            {
                Attribute _attr = new Attribute();
                _attr.name = name;
                _attr.value = value;
                this.attributes.Add(_attr);
            }

            // If the element is an copy, give it's original the value
            if (this.IsCopy)
                this.Original.SetAttribute(name, value);

          
               
        }
        public Section ParentSection { get; set; }
	    public Element(Section parentSection,DrawBoard parentBoard)
	    {
            this.ParentSection = parentSection;
            this.ParentHost = parentBoard;
	        attributes = new List<Attribute>();
            Elements = new ElementCollection();
	    }

        private bool selected;
        /// <summary>
        /// Gets and sets whether the object is selected on the graphical board
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                // If this is an copy set the parent to selected to
                if (IsCopy)
                    this.Original.selected = true;

               


            }
        }
        /// <summary>
        /// Scroll so the selected will be visible
        /// </summary>
        public void AssertSelection()
        {
            // Get the coordinates for the item
            Rectangle objCoordinates = this.GetCoordinates(this.ParentSection.Parent.ParentBoard.scrollX, this.ParentSection.Parent.ParentBoard.scrollY, new Rectangle(0, 0, this.ParentSection.Parent.ParentBoard.Width, this.ParentSection.Parent.ParentBoard.Height), 0);

            // if the item reach the end 
            if (objCoordinates.Top + objCoordinates.Height >= this.ParentSection.Parent.ParentBoard.Bounds.Height)
            {

                this.ParentHost.scrollY += this.Height;
            }

            // if the object is bellow the beginning
            if (objCoordinates.Top < objCoordinates.Height)
                this.ParentHost.scrollY -= this.Height;

        }
	    private string type;

        /// <summary>
        /// Gets and sets the type of element. 
        /// </summary>
	    [XmlAttribute("type")]
	    public string Type
	    {
	        get
	        {
	            return type;
	        }
	        set
	        {
	            type = value;
	        }
	    }
        /// <summary>
        /// An list of arbitrary attributes applied to the object. These attributes provides the properties coming from
        /// the underlying input and are managed by the application. 
        /// </summary>
	    private List<Attribute> attributes;
	    [XmlElement("attribute")]
	    public List<Attribute> Attributes
	    {
	        get
	        {
	            return attributes;
	        }
	        set
	        {
	            attributes = value;
	        }
	    }
        /// <summary>
        /// Class for defining children element collection
        /// </summary>
        public class ElementCollection : IEnumerable
        {
            public ElementCollection()
            {
                elements = new List<Element>();
            }
            int position = -1;
            IEnumerator System.Collections.IEnumerable .GetEnumerator()
            {
                return elements.AsEnumerable<Element>().ToArray().GetEnumerator();
            }
               
            public Element Parent {get;set;}
            private List<Element> elements;
            [XmlElement("element")]
            public Element this[int index]
            {
                get
                {
                    return elements[index];
                }
                set 
                {
                    elements[index] = value;
                        
                }
            }
            public void Add(Element elm)
            {
                elm.Parent = Parent;
                elements.Add(elm);
            }
        }

        /// <summary>
        /// Child elements of the element. All elements can contain own child nodes, but their behaviour and functionality
        /// depends of the target implementation.
        /// </summary>
        public ElementCollection Elements { get; set; }
	    public string GetAttribute(string name)
	    {
	        foreach (Attribute a in attributes)
	            {
	            if (a.name == name)
	            {
	                return a.value;
	            }
	        }
	        return "";
	    }
	      
	       
	}
}
