using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaChromeGUI
{
    public partial class AskDialog : Form
    {
        public AskDialog()
        {
            InitializeComponent();
        }
        public AskDialog(Image glyph, string message, string title)
        {
            InitializeComponent();
            this.Text = title;
            this.Glyph = glyph;
            this.Message = message;
        }
        /// <summary>
        /// Message
        /// </summary>
        public string Message
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }
        /// <summary>
        /// Icon
        /// </summary>
        public Image Glyph
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
        private void AddPlaylist_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Value
        /// </summary>
        public String Value
        {
            get
            {
                return this.textBox1.Text;
            }
        }
    }
}
