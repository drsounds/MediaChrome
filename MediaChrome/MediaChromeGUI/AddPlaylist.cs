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
    public partial class AddPlaylist : Form
    {
        public AddPlaylist()
        {
            InitializeComponent();
        }

        private void AddPlaylist_Load(object sender, EventArgs e)
        {

        }
        public String PlaylistName
        {
            get
            {
                return this.textBox1.Text;
            }
        }
    }
}
