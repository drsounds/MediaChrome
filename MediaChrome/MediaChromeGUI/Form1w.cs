using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace SpofityRuntime
{
    public partial class Form1 : Form
    {
        public string ParseURI(string URI)
        {
            if (URI != null)
            {
                if (URI.StartsWith("spotify:"))
                {
                    if (URI.StartsWith("spotify:view:"))
                    {
                        string application = URI.Split(':')[2];
                        if (File.Exists(Application.LocalUserAppDataPath + "\\views\\" + application + "\\main.view"))
                        {
                            back.Push(geckoWebBrowser1.Url.ToString());
                            geckoWebBrowser1.Navigate(Application.LocalUserAppDataPath + "\\views\\" + application + "\\main.view");

                        }
                    }
                    if (URI.StartsWith("spotify:artist:"))
                    {

                        // TODO: Call PHP handler and create custom artist view
                    }
                }
            }
            return "ZERO";
        }
        public Stack<String> back;
        public Stack<string> forward;
        public Form1()
        {
            back = new Stack<string>();
            forward = new Stack<string>();
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pane1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string uri = Application.LocalUserAppDataPath + "\\views\\Standard\\main.xul";
            Skybound.Gecko.GeckoPreferences.User["capability.policy.default.XMLHttpRequest.open"] = "allAccess";
         
            geckoWebBrowser1.Navigate( uri);
            DirectoryInfo D = new DirectoryInfo(Application.LocalUserAppDataPath + "\\views\\");
            foreach (DirectoryInfo R in D.GetDirectories())
            {
                if (File.Exists(R.FullName + "\\main.xul"))
                {
                    ListViewItem DF = listViewX1.Items.Add(R.Name);
                    DF.Tag = (object)("spotify:view:" + R.Name);
                }
            }
        }

        private void geckoWebBrowser1_Click(object sender, EventArgs e)
        {

        }

        private void listViewX1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ParseURI((string)listViewX1.SelectedItems[0].Tag);
            }
            catch
            {
            }
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
        bool finished = false;
        private void geckoWebBrowser1_Navigating(object sender, Skybound.Gecko.GeckoNavigatingEventArgs e)
        {
            string Uri = e.Uri.ToString();
            if (Uri.EndsWith(".view"))
            {
                
                

                Thread D = new Thread(new ParameterizedThreadStart(LoadPage));
                D.Start(Uri);
              
                e.Cancel = true;
                geckoWebBrowser1.Navigate("file://" + Program.GetAppString() + "\\wait.xul");
            }
            
            System.Windows.Forms.Timer N = new System.Windows.Forms.Timer();
            
        }
        void LoadPage(object d)
        {
            string Uri = (string)d[0];
            string[] f;
                if (Uri.Contains('?'))
                {
                    f = Uri.Split('?');
                }
                else
                {
                    f = new string[] { Uri, "" };
                }
            T = new Process();

            string parameters = f[1];
            T = new Process();
            T.StartInfo.FileName = (Program.GetAppString() + "\\php\\php.exe");
            T.StartInfo.Arguments = (string)f[0] + " " + (string)f[1];
            T.StartInfo.CreateNoWindow = true;
            T.StartInfo.RedirectStandardOutput = true;
            T.StartInfo.UseShellExecute = false;
            T.EnableRaisingEvents = true;

            T.Exited += new EventHandler(T_Exited);
            T.OutputDataReceived += new DataReceivedEventHandler(T_OutputDataReceived);
            T.Start();
            T.BeginOutputReadLine();
            StreamReader SR = T.StandardOutput;
            T.WaitForExit();
            string output = T.StandardOutput.ReadToEnd();
            using (StreamWriter SW = new StreamWriter("C:\\s_buffer.xul"))
            {
                SW.Write(output);
            } 
            finished = true;
        }
        StreamReader F;
        void N_Tick(object sender, EventArgs e)
        {
            
        }
        Process T;
        void T_Exited(object sender, EventArgs e)
        {
            
           
        }




        void T_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (finished)
            {
                geckoWebBrowser1.Navigate("C:\\s_buffer.xul");

                finished = false;
            }
        }
    }
}
