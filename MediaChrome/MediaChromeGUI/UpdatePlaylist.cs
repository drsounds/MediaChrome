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
namespace MediaChromeGUI
{
    public partial class UpdatePlaylist : Form
    {
        public UpdatePlaylist()
        {
            InitializeComponent();
        }
     
                
        public UpdatePlaylist(string Desc,string uri)
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                byte[] bitmapData ;
                byte[] textData ;
                using (FileStream FS = new FileStream(textBox2.Text,FileMode.Open))
                {

                   bitmapData = new byte[FS.Length];
                    FS.Read(bitmapData, 0, (int)FS.Length);

                }
                StringConverter D = new StringConverter();
                textData = new byte[textBox1.Text.Length];
                Encoding.ASCII.GetBytes(textBox1.Text, 0, textBox1.Text.Length, textData, 0);
                WebClient X = new WebClient();
                
                using (StreamWriter SW = new StreamWriter("text.temp"))
                {
                    SW.Write(textBox1.Text);
                }
                X.UploadFile(new Uri("http://spotiapps.krakelin.com/playlists/uploadimage.php"), textBox2.Text);
                X.UploadFile(new Uri("http://spotiapps.krakelin.com/playlists/uploaddescription.php"), "text.temp");
                this.Close();

            }
        }

        private void UpdatePlaylist_Load(object sender, EventArgs e)
        {

        }
    }
}
