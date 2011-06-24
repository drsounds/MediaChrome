using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MediaChrome.ServicesSelector;

namespace MediaChrome
{
    public partial class Engines : Form
    {
        public Engines()
        {
            InitializeComponent();
            this.SetStyle((ControlStyles)CS_DROPSHADOW,true);
        }  
        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        private void Engines_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Host.Skin.Components["Menu#BG"].BackgroundImage;
            this.BackColor = Host.Skin.Components["Menu#BG"].BackColor;
            int left = 10;
            int top = 80;
            int increase=60;
            // Create List of media Engines
            foreach (MediaChrome.IPlayEngine Engine in Program.MediaEngines.Values)
            {
                ServiceEntry entry = new ServiceEntry(Engine);
                entry.Title = Engine.Title;
                entry.Description = "";
                entry.LoggedIn = entry.LoggedIn;
                entry.Tag = Engine;
                entry.Click += new EventHandler(entry_Click);
                this.Controls.Add(entry);
                entry.Top = top;
                entry.Left = left;
                entry.Width = 300;
                entry.Height = 50;
                entry.Icon = Engine.Icon;
                top += increase;
                if (top >= this.Height - 20)
                {
                    this.Height += increase + 20;
                }
#if (nobug)
                PictureBox pt = new PictureBox();
                this.Controls.Add(pt);
                pt.Left = left;
                pt.Top = top;
                pt.Click += new EventHandler(l_Click);
                pt.BackColor = Color.Transparent;
                pt.BackgroundImageLayout = ImageLayout.Zoom;
                
                if (Engine.Icon != null)
                {
                    pt.BackgroundImage = Engine.Icon;
                    pt.Width = 32;
                    pt.Height = 32;
                }
                // Create name label
                Label l = new Label();
                l.Text = Engine.Title;
                this.Controls.Add(l);
                l.Left = left + 50;
                l.BackColor = Color.Transparent;
               
                l.Top = top;
                l.Font = new Font(l.Font.FontFamily, 15.0f,FontStyle.Bold, GraphicsUnit.Pixel);
                l.Click += new EventHandler(l_Click);
                l.Tag = Engine;
                pt.Tag = Engine;
                l.Cursor = Cursors.Hand;
                pt.Cursor = Cursors.Hand;

                // Create login button or logged in
                LinkLabel Login = new LinkLabel();
                Login.Text = Engine.LoggedIn ? "Log out" : "Log in";
                Login.Width = 50;
                Login.Height = 20;
                Login.Left = this.Width - Login.Width - 10;
                Login.Anchor = AnchorStyles.Right;
                Login.BackColor = Color.Transparent;
                Login.Top = top;
                Login.Click += new EventHandler(Login_Click);
                Login.Tag = Engine;
                this.Controls.Add(Login);
                top += 60;
#endif
            }
        }

        void entry_Click(object sender, EventArgs e)
        {
            // Get engine of option
            MediaChrome.IPlayEngine Engine = (MediaChrome.IPlayEngine)((Control)sender).Tag;
            Host.CurrentPlayer = Engine;
            this.Close();
        }

        void Login_Click(object sender, EventArgs e)
        {
            MediaChrome.IPlayEngine Engine = (MediaChrome.IPlayEngine)((Control)sender).Tag;
            if (Engine.LoggedIn)
                Engine.Logout();
            else
                Engine.Login();
        }

        void l_Click(object sender, EventArgs e)
        {
            // Get engine of option
            MediaChrome.IPlayEngine Engine = (MediaChrome.IPlayEngine)((Control)sender).Tag;
            Host.CurrentPlayer = Engine;

        }
        /// <summary>
        /// Parent form to host the menu
        /// </summary>
        public Form1 Host { get; set; }

        private void Engines_Leave(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Engines_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Preferences prefs = new Preferences(this.Host);
            prefs.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EngineManager d = new EngineManager();
            d.Show();
        }
    }
}
