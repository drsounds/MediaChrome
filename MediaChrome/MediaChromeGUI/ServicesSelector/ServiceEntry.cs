using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaChrome.ServicesSelector
{
    public partial class ServiceEntry : UserControl
    {
        /// <summary>
        /// Engine associated with this entry
        /// </summary>
        public MediaChrome.IPlayEngine Engine
        {
            get;
            set;
        }
        public ServiceEntry(MediaChrome.IPlayEngine engine)
        {
            this.Engine = engine;
            InitializeComponent();
        }
        public ServiceEntry()
        {
            InitializeComponent();
        }
        private bool loggedIn;

        /// <summary>
        /// Decides wheter you're logged in
        /// </summary>
        public bool LoggedIn
        {
            get
            {
                return loggedIn;
            }
            set
            {
                this.switch1.Active = loggedIn;
            }
        }
        bool hover = false;
        private void ServiceEntry_MouseEnter(object sender, EventArgs e)
        {
            hover = true;
            this.Refresh();
        }

        private void ServiceEntry_MouseLeave(object sender, EventArgs e)
        {
            hover = false;
            this.Refresh();
        }
        
        public String Title
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
        public String Description
        {
            get
            {
                return label2.Text;
            }
            set
            {
                label2.Text = value;
            }
        }
        /// <summary>
        /// Gets and sets the icon for the entry
        /// </summary>
        public Image Icon
        {
            get
            {
                return this.pictureBox1.BackgroundImage;
            }
            set
            {
                this.pictureBox1.BackgroundImage = value;
            }
        }
        private void ServiceEntry_Load(object sender, EventArgs e)
        {

        }

        private void ServiceEntry_Paint(object sender, PaintEventArgs e)
        {
            // If the mouse is hovered draw selection rectangle
            if (hover)
            {
                e.Graphics.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), Color.FromArgb(255, 211, 0), Color.FromArgb(255, 211, 0)), new Rectangle(0, 0, this.Width, this.Height));
                
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!loggedIn)
                this.Engine.Login();
            else
                this.Engine.Logout();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Engine.ShowOptions();
        }
    }
}
