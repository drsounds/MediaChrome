using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using System.Windows.Forms;

namespace MediaChrome.SocialNetworking
{
    public class Facebook : ISocialNetwork
    {
        /// <summary>
        /// The underlying api
        /// </summary>
        FacebookAPI api;
        string apiKey = "";
        public Facebook()
        {
          

        }
        /// <summary>
        /// The secret API token
        /// </summary>
        string token = "";
        public void Login()
        {
           /***
            * Show the authorization form
            * */
            FacebookForm ff = new FacebookForm();
            if (ff.ShowDialog() == DialogResult.OK)
            {
                /**
                 * Now instantiate the API
                 * */
                api = new FacebookAPI(ff.Result);
                token = ff.Result;
            }
        }

        public void Logout()
        {
             
        }

        public SocialFeed LastUpdates
        {
            get
            {
                // If the API is not initialized, return an empty feed.
                if (api == null)
                    return new SocialFeed(this);

                /**
                 * Get the updates 
                 * */
                   JSONObject jobject = api.Get("https://graph.facebook.com/me/home?access_token="+token+"");

                // Create an socialfeed object
                   SocialFeed a = new SocialFeed(this);
                   
                   /**
                    * Add all messages from the feed
                    * */
                   foreach (JSONObject post in jobject.Dictionary["data"].Array)
                   {
                        String msg = post.Dictionary["message"].ToDisplayableString(); // The message
                        String time = post.Dictionary["time"].ToDisplayableString(); // The time
                        StatusMessage message = new StatusMessage(msg, a, DateTime.Parse(time), Visibility.All);
                        a.Add(message);
                   }
                   return a;
            }
        }

        public bool PostStatusMessage(string str, Visibility security)
        {
            return false;
        }

        public bool PostLink(string msg, Uri link, Visibility security)
        {
            return false;
        }

        public SocialFeed GetFeedFromUser(string userName)
        {
            // If the API is not initialized, return an empty feed.
            if (api == null)
                return new SocialFeed(this);
            return new SocialFeed(this);
            /**
             * Get the updates 
             * */
            JSONObject jobject = api.Get("https://graph.facebook.com/me/home?access_token="+token+"");

            // Create an socialfeed object
            SocialFeed a = new SocialFeed(this);

            /**
             * Add all messages from the feed
             * */
            foreach (JSONObject post in jobject.Dictionary["data"].Array)
            {
                String msg = post.Dictionary["message"].ToDisplayableString(); // The message
                String time = post.Dictionary["time"].ToDisplayableString(); // The time
                StatusMessage message = new StatusMessage(msg, a, DateTime.Parse(time), Visibility.All);
                a.Add(message);
            }
            return a;
        }

        public List<User> Friends
        {
            get 
            {
                List<User> users = new List<User>();
                // if the API is null return an empty list so it won't crasnh
                if (api == null)
                    return users;

                /**
                 * Return an friend list from the facebook.
                 * */
                JSONObject jobject = api.Get("https://graph.facebook.com/me/friends?access_token=" + token + "");
                foreach (JSONObject _friend in jobject.Dictionary["data"].Array)
                {
                    string name = _friend.Dictionary["name"].ToDisplayableString();
                    string id = _friend.Dictionary["id"].ToDisplayableString();
                    User d = new User();
                    d.ID = id;
                    d.FirstName = name;
                    d.Engine = this;
                    users.Add(d);
                    
                }

                return users;


            }
        }
    }
}
