using System;
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
    public partial class Appify : Form
    {
        public Appify()
        {
            InitializeComponent();
        }
        public Appify(string URI)
        {
            InitializeComponent();
            this.Show();
            Skybound.Gecko.Xpcom.Initialize(Application.ExecutablePath.Replace("SpofityRuntime.exe", "")+"\\xulrunner");
            Browser = new Skybound.Gecko.GeckoWebBrowser();
            this.Controls.Add(Browser);
            Browser.Show();
            Browser.Dock = DockStyle.Fill;
            Browser.Navigate(URI);
           

        }
        Skybound.Gecko.GeckoWebBrowser Browser;
        private void Appify_Load(object sender, EventArgs e)
        {

        }
    }
}
