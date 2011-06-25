using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using MediaChrome.ServicesSelector;
using BasicFTPClientNamespace;
namespace MediaChrome
{
    public partial class AddOnManager : Form
    {
        /// <summary>
        /// Creates an new add-on manager
        /// </summary>
        /// <param name="host">Host for addons</param>
        /// <param name="title">Title</param>
        /// <param name="downloadFolder">Folder for storage of add-ons</param>
        /// <param name="user">User name</param>
        /// <param name="pass">password</param>
        public AddOnManager(string host,string title,string downloadFolder,string Description, string user,string pass)
        {
            InitializeComponent();
            this.Host=host;
            this.Text = title;
            this.DownloadDir = downloadFolder;
            this.textBox1.Text = user;
            this.textBox2.Text = pass;
            PaneVisible = false;
            button3_Click(this, new EventArgs());

        }
        public AddOnManager()
        {
            InitializeComponent();
        }
        private bool paneVisible;
        public bool PaneVisible
        {
            get
            {
                return paneVisible;
            }
            set
            {
                paneVisible = value;
                this.panel3.Visible = PaneVisible;
                this.progressBar1.Top = PaneVisible ? this.panel3.Top : this.label4.Top + this.label4.Height;
                this.panel2.Top = this.progressBar1.Top + this.progressBar1.Height + 20;
            }
        }
        private bool changesApplied;

        /// <summary>
        /// Indicates where changes is applied
        /// </summary>
        public bool ChangesApplied
        {
            get
            {
                return changesApplied;
            }
            set
            {
                
                changesApplied = value;
                this.button2.Text = changesApplied ? "Restart" : "Close";
            }
        }
        private void EngineManager_Load(object sender, EventArgs e)
        {

        }
        public class EngineDescriptor
        {
            /// <summary>
            /// Title of engine
            /// </summary>
            public string Title;

            public string Description;
            public string Author;
            public string Host;
            public string Namespace;
            public string Address;
            public bool Installed;

        }
        public static List<string> ListFilesFromFtp(string address,NetworkCredential cred)
        {
            // Create pointer to browse web request
            FtpWebRequest d = (FtpWebRequest)WebRequest.Create((string)address);
            d.Method = WebRequestMethods.Ftp.ListDirectory;
            d.Credentials = cred;
            // Traverse list of files to pick candidates
            WebResponse WR = null;
            try
            {
                WR = d.GetResponse();
            }
            catch
            {
                return new List<string>();
            }
            
            StreamReader SR = new StreamReader(WR.GetResponseStream());

          
            List<String> Directories = new List<string>();
            String r = "";
            try
            {
                while ((r = SR.ReadLine()) != null)
                {
                    String fileName = r;
                    if (r != "." && r != "..")
                    {
                        Directories.Add(fileName);
                    }
                }
            }
            catch
            {
            }
            // Close the connections
            SR.Close();
            WR.Close();

            // Return the result
            return Directories;
        }
        /// <summary>
        /// Extracts the last string in an compound 
        /// </summary>
        /// <param name="d">the input string</param>
        /// <returns>the last occurance</returns>
        public static string GetReverse(string d)
        {
            string r = "";
            int i=d.Length-1;
            int pos = -1;
            char prevChar = '\0';
            while (i > 0)
            {
                char ch = d[i];
                if ((ch == '\n' || ch == '\r')  && prevChar != '\n' && prevChar != '\r')
                {
                    pos++;
                }
                if (pos == 0)
                {

                    r=r.Insert(0, d[i].ToString());
                }
                prevChar = ch;
                i--;
            }
            return r.Replace("\r","").Replace("\n","");


        }
        /// <summary>
        /// The host
        /// </summary>
        public string Host;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<EngineDescriptor> Descriptors = new List<EngineDescriptor>();
            List<string> directories = ListFilesFromFtp((string)e.Argument,cred);
            try
            {
                /**
                 * Browse all directories
                 * */
                foreach (string dir in directories)
                {
                    // If directory not contains . it's an directory
                    if (!dir.Contains("."))
                    {

                        // Download the information file
                        BasicFTPClient WC = new BasicFTPClient(cred.UserName, cred.Password, Host.Replace("ftp://",""));
                       
                        String XML =  System.Text.Encoding.UTF8.GetString(WC.DownloadData( dir + "/" + dir + ".xml"));
                        XmlDocument D = new XmlDocument();
                        D.LoadXml(XML);
                        // Create new object
                        EngineDescriptor Engine = new EngineDescriptor();
                        Engine.Title = D.GetElementsByTagName("title")[0].InnerText;
                        Engine.Description = D.GetElementsByTagName("description")[0].InnerText;
                        Engine.Author = D.GetElementsByTagName("author")[0].InnerText;
                       
                        Engine.Namespace = D.GetElementsByTagName("namespace")[0].InnerText;
                        Engine.Host = Host;
                        Engine.Address = dir;
                        Engine.Installed = File.Exists(DownloadDir + "\\" + Engine.Namespace+"\\"+ Engine.Namespace + ".dll");
                        Descriptors.Add(Engine);

                    }
                }

            }
            catch
            {
                
            }
            // Return result as an list of descriptors
            e.Result = Descriptors;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Unbox the descriptors and traverse through it
            List<EngineDescriptor> descriptors = (List<EngineDescriptor>)e.Result;
            foreach (EngineDescriptor r in descriptors)
            {
                // Create new engine object
                EngineEntry w = new EngineEntry(this.Host,cred,r.Title, r.Description, DownloadDir, r.Address, r.Namespace);
                
                this.panel2.Controls.Add(w);
                w.Dock = DockStyle.Top;
              

            }
            progressBar1.Hide();
            button3.Show();
        }
        NetworkCredential cred;
        private void button3_Click(object sender, EventArgs e)
        {
            this.panel2.Controls.Clear();
            // Use creditals provided with the textboxes
            cred = new NetworkCredential(textBox1.Text, textBox2.Text);
            Host = comboBox1.Text;
            // Only begin if the field starts with ftp:// 
            if(comboBox1.Text.StartsWith("ftp://"))
            {
                // Show progressbar
                progressBar1.Show();
                progressBar1.Style = ProgressBarStyle.Marquee;
                button3.Hide();
                backgroundWorker1.RunWorkerAsync(comboBox1.Text);
             
            }
       }
        public string DownloadDir = "C:\\MediaProviders";
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /**
             * Restart mediaChrome if plugins has been installed or removed
             * */
            if (ChangesApplied)
                Application.Restart();

        }

        private void AddOnManager_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void AddOnManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            /**
             * Restart mediaChrome if plugins has been installed or removed
             * */
            if (ChangesApplied)
                Application.Restart();
        }
    }
}
