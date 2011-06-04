using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;

namespace MediaChrome.SocialNetworking
{
    public partial class FacebookForm : Form
    {
        public FacebookForm()
        {
            InitializeComponent();
        }

        private void FacebookForm_Load(object sender, EventArgs e)
        {
            // Navigate to the page
            webBrowser1.Navigate("https://www.facebook.com/dialog/oauth?client_id=127995343947903&redirect_uri=" + HttpUtility.UrlEncode("http://apps.facebook.com/mediachrome/") + "&scope=email,read_stream");
            
        }
        /// <summary>
        /// The result code of the query is here
        /// </summary>
        public String Result
        {
            get;
            set;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            /**
             * If the redirect URI starts with the fictive address mce.fc (mediachrome.fictive) 
             * pass the authorization token back to the application and close the form
             * */
            if (e.Url.ToString().StartsWith("http://apps.facebook.com/mediachrome/"))
            {
                // Get the code
                Result = e.Url.Query.Split('=')[1];
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
