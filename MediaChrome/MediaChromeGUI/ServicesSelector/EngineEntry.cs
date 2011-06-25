using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;
using BasicFTPClientNamespace;

namespace MediaChromeGUI.ServicesSelector
{
    public partial class EngineEntry : UserControl
    {  
        NetworkCredential ftpCreditals;

        /// <summary>
        /// Host of the FTP
        /// </summary>
        public string Host;
        /// <summary>
        /// Creates an entry for an new engine
        /// </summary>
        /// <param name="title">Title of the engine</param>
        /// <param name="description">Description of the engine</param>
        /// <param name="downloadDirectory">Download directory of the engine</param>
        /// <param name="address">Address to the engine</param>
        /// <param name="nameSpace">Namespace of the engine</param>
        public EngineEntry (string host,NetworkCredential ftpCred, string title,string description, string downloadDirectory,string address,string nameSpace)
        {

            Host = host;
            ftpCreditals = ftpCred;
            Namespace = nameSpace;
            Address = address;
            DownloadDirectory = downloadDirectory;
            InitializeComponent();
            Title = title;
            Description = description;
            pictureBox1.WC.Credentials = ftpCreditals;
            try
            {
                pictureBox1.Url = new Uri(this.Host +"/" + Namespace + "/" + Namespace + ".png");
            }
            catch
            {
            }
        }
        /// <summary>
        /// Directory for downloading the engine
        /// </summary>
        public string DownloadDirectory { get; set; }
        [DefaultValue("")]
        /// <summary>
        /// Address to engine folder
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Namespace
        /// </summary>
        public string Namespace {get;set;}
        public EngineEntry()
        {
            InitializeComponent();
        }
        public void install_async(object path)
        {
           

        }
        public void Install()
        {
            /**
             *  If installed, ask for uninstallation
             *  
             * */
            if (Installed)
            {
                if (MessageBox.Show("Do you really want to uninstall the app", "Confirm uninstallation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!Directory.Exists(this.DownloadDirectory + "/" + this.Namespace))
                        return;
                    DirectoryInfo DI = new DirectoryInfo(this.DownloadDirectory + "/" + this.Namespace);
                    foreach (FileInfo FI in DI.GetFiles("*.*"))
                    {
                        File.Delete(FI.FullName);
                    }
                }
                Installed = false;
                return;
            }
            backgroundWorker1.RunWorkerAsync((DownloadDirectory + "/"));
            progressBar1.Show();
            progressBar1.Style = ProgressBarStyle.Marquee;

            // Set progressbar position as the button
            progressBar1.Left = button1.Left;
            progressBar1.Top = button1.Top;
            progressBar1.Width = button1.Width;
          
            progressBar1.Anchor = button1.Anchor;
            button1.Hide();
           
        }
        /// <summary>
        /// Gets or sets the title of the engine
        /// </summary>
        public string Title
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
        /// <summary>
        /// Gets or sets the description of the engine
        /// </summary>
        public string Description
        {
            get
            {
                return label3.Text;
            }
            set
            {
                label3.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the author of the engine
        /// </summary>
        public string Author
        {
            get
            {
                return linkLabel1.Text;
            }
            set
            {
                linkLabel1.Text = value;
            }
        }
        /// <summary>
        /// Gets or sets the logotype of the engine
        /// </summary>
        public Uri Logotype
        {
            get
            {
                return this.pictureBox1.Url;
            }
            set
            {
                this.pictureBox1.Url = value;
            }
        }

        /// <summary>
        /// Gets or sets the logotype of the engines author
        /// </summary>
        public Uri AuthorLogotype
        {
            get
            {
                return this.pictureBox2.Url;
            }
            set
            {
                this.pictureBox2.Url = value;
            }
        }
        private Uri link;

        /// <summary>
        /// Link to the author's website
        /// </summary>
        public Uri Link
        {
            get
            {

                return link;
            }
            set
            {
                link = value;
                this.linkLabel1.Links.Add(0, this.linkLabel1.Text.Length - 1, link);
            }
        }
        private void EngineEntry_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Install();
        }
        public List<String> Files = new List<string>();
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           
         
            string Path = (string)e.Argument;
            /***
             * Get an list of all files attached to an plugin and download files
             * */
            List<String> files = AddOnManager.ListFilesFromFtp(this.Host+"/"+Address, ftpCreditals);

            
            // Download all found files
            foreach (String DirName in files)
            {
                // If the file is not an directory (eg. not contains .) download it, 
                // but check and create dir if not exists
                String tPath = String.Format("{0}\\{1}", DownloadDirectory,this.Address.Replace("/",""));
                if (!Directory.Exists(tPath))
                {
                    Directory.CreateDirectory(tPath);
                }
                // Download the plugin
                BasicFTPClient WC = new BasicFTPClient(ftpCreditals.UserName,ftpCreditals.Password,this.Host.Replace("ftp://",""));
                
                //  WebClient WC = new WebClient();
                WC.DownloadFile((this.Address+"/"+DirName),tPath+"\\"+DirName );
                
            }
            
        }
        private bool installed;
        /// <summary>
        /// Gets or sets if the component is installed
        /// </summary>
        public bool Installed
        {
            get
            {
                return installed;
            }
            set
            {
                installed = value;
                this.button1.Text = installed ? "Uninstall" : "Install";
                this.label5.Visible = installed;
                this.BackColor = installed ? Color.FromArgb(241, 255, 241) : Color.White;
                
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.button1.Show();
            this.progressBar1.Hide();
            Installed = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
