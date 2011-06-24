using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spotify;
namespace SpotifyPlayer
{
    public partial class Player : UserControl
    {
        /// <summary>
        /// Gets or sets the session of the control
        /// </summary>
        public Spotify.Session Session { get; set; }
        public Player()
        {
            InitializeComponent();
        }
        public Player(Spotify.Session Session)
        {
            this.session = Session;
        }
        
        /// <summary>
        /// Current playlist for the player
        /// </summary>
        public Playlist CurrentPlaylist { get; set; }
        private void Player_Load(object sender, EventArgs e)
        {

        }
    }
}
