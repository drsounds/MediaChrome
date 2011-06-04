using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;

namespace MediaChrome.SocialNetworking
{
    public partial class FacebookForm : Form
    {
        public FacebookForm()
        {
            InitializeComponent();
        }
        String clientID = "127995343947903";
        private void FacebookForm_Load(object sender, EventArgs e)
        {
            // Navigate to the page
            webBrowser1.Navigate("https://www.facebook.com/dialog/oauth?client_id="+clientID+"&redirect_uri=" + HttpUtility.UrlEncode("http://apps.facebook.com/mediachrome/") + "&scope=email,read_stream");
            
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
                string code = e.Url.Query.Split('=')[1];
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                // Pass the code to the server
                WebClient client = new WebClient();
                String request = String.Format("https://graph.facebook.com/oauth/access_token?redirect_uri={3}&client_id={0}&client_secret={1}&code={2}", clientID, "", code, "http://apps.facebook.com/mediachrome/"); // The request
                String res = client.DownloadString(request);
                // Set the secret code here
                this.Result = res.Split('=')[1].Split('&')[0];
                this.expires = int.Parse(res.Split('&')[1].Split('=')[1]);
               
            }
        }
        int expires = 0;

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
