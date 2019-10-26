using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace MediaChrome.Engines.Spotify
{
    public partial class AuthorizeSpotifyForm : Form
    {
        public AuthorizeSpotifyForm()
        {
            InitializeComponent();
            webBrowser1.Navigated += WebBrowser1_Navigated;
        }

        private void AuthorizeSpotifyForm_Load(object sender, EventArgs e)
        {
            string[] scopes = new string[]
            {
                "playlist-read-private",
                "playlist-modify-public",
                "playlist-modify-private",
                "user-read-private",
                "user-follow-read",
                "user-follow-modify",
                "playlist-read-collaborative",
                "user-read-recently-played",
                "user-top-read",
                "user-modify-playback-state",
                "user-read-currently-playing",
                "user-read-playback-state",
                "user-library-modify",
                "user-library-read",
                "app-remote-control"
            };
            webBrowser1.Navigate("https://accounts.spotify.com/authorize?response_type=code&client_id=" + Credentials.CLIENT_ID + "&scope=" + String.Join(" ", scopes) + "&redirect_uri=" + Credentials.REDIRECT_URI);
            webBrowser1.Navigated += WebBrowser1_Navigated1;
        }

        private void WebBrowser1_Navigated1(object sender, WebBrowserNavigatedEventArgs e)
        {
            Console.WriteLine(e.Url);
        }

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString().StartsWith("https://graph.buddhalow.app/callback/spotify"))
            {
                if (e.Url.ToString().StartsWith("https://graph.buddhalow.app/callback/spotify?code="))
                {
                    string code = e.Url.ToString().Split('=')[1];
                    var client = new RestClient("https://accounts.spotify.com");
                    var request = new RestRequest("api/token", Method.POST);
                    request.AddParameter("client_id", Credentials.CLIENT_ID);
                    request.AddParameter("client_secret", Credentials.CLIENT_SECRET);
                    request.AddParameter("grant_type", "authorization_code");
                    request.AddParameter("code", code);
                    request.AddParameter("redirect_uri", Credentials.REDIRECT_URI);
                    IRestResponse<SpotifySession> result = client.Execute<SpotifySession>(request);
                    SpotifySession session = result.Data;
                    session.issued = DateTime.Now.Ticks;
                    Properties.Settings.Default["spotify_session"] = new JavaScriptSerializer().Serialize(session);
                    Properties.Settings.Default.Save();
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
