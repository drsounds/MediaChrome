using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaChrome
{
    public partial class PlaybackNotifier : Form
    {
        /// <summary>
        /// The engine used for the new song
        /// </summary>
        public IPlayEngine Engine { get; set; }

        /// <summary>
        /// Song that are going to be playing
        /// </summary>
        public Song Song { get; set; }
        public PlaybackNotifier()
        {
            InitializeComponent();
        }
        public PlaybackNotifier(IPlayEngine engine,Song song)
        {
            Song = song;
            // Initialize form controls
            InitializeComponent();

            // Set position of the form
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 10;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 10;
            // Get current engine
            this.Engine = engine;
            
            // Set image icon
            if(this.Engine.Icon != null)
            this.pictureBox1.BackgroundImage = this.Engine.Icon;

            this.label1.Text = Song.Name;
            this.label2.Text = Song.Artist;
           

        }

        private void PlaybackNotifier_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Lower opacity
            if (this.Opacity > 0)
                this.Opacity -= 0.01;
            else // Close when ready
                this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // Begin fade out
            timer1.Start();
        }
    }
}
