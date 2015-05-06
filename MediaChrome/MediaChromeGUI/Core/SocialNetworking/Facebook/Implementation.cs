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
        public string Namespace
        {
            get
            {
                return "facebook";
            }
        }
 
        public string Name
        {
            get
            {
                return "Facebook";
            }
        }
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
                   JSONObject jobject = api.Get("/me/home");

                // Create an socialfeed object
                   SocialFeed a = new SocialFeed(this);
                   
                   /**
                    * Add all messages from the feed
                    * */
                   foreach (JSONObject post in jobject.Dictionary["data"].Array)
                   {
                       try
                       {



                           StatusMessage message = JSONToMessage(post,a);
                           a.Add(message);
                       }
                       catch
                       {
                       }
                   }
                   return a;
            }
        }
        private StatusMessage JSONToMessage(JSONObject post,SocialFeed a)
        {
            try
            {
                String msg = post.Dictionary["message"].ToDisplayableString(); // The message
                String time = post.Dictionary["created_time"].ToDisplayableString(); // The time
                StatusMessage message = new StatusMessage(msg, a, DateTime.Parse(time), Visibility.All);
                try
                {

                    message.Link = post.Dictionary["link"].ToDisplayableString();
                }
                catch
                {
                    message.Link = " ";
                } User d = new User();
                d.ID = post.Dictionary["from"].Dictionary["id"].ToDisplayableString();
                d.FirstName = post.Dictionary["from"].Dictionary["name"].ToDisplayableString();
                d.ImageUrl = (String.Format("https://graph.facebook.com/{0}/picture?access_token={1}", d.ID, this.token));
                message.User = d;
                return message;
            }
            catch { }
            return null;
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
        
            /**
             * Get the updates 
             * */
            JSONObject jobject = api.Get("/"+userName+"/feed");

            // Create an socialfeed object
            SocialFeed a = new SocialFeed(this);

            /**
             * Add all messages from the feed
             * */
            try
            {
                foreach (JSONObject post in jobject.Dictionary["data"].Array)
                {
               /*     String msg = post.Dictionary["message"].ToDisplayableString(); // The message
                    String time = post.Dictionary["created_time"].ToDisplayableString(); // The time
                    StatusMessage message = new StatusMessage(msg, a, DateTime.Parse(time), Visibility.All);
                    try
                    {

                        message.Link = post.Dictionary["link"].ToDisplayableString();
                    }
                    catch
                    {
                        message.Link = " ";
                    } User d = new User();
                    d.ID = post.Dictionary["from"].Dictionary["id"].ToDisplayableString();
                    d.ImageUrl = (String.Format("https://graph.facebook.com/{0}/picture?access_token={1}", d.ID, this.token));
                    message.User = d;
                    */
                    StatusMessage message = JSONToMessage(post,a);

                    if(message!=null)
                        a.Add(message);
                }
            }
            catch
            {
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
                JSONObject jobject = api.Get("/me/friends");
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


        public SocialFeed QueryFeedFromUser(string userName, string query)
        {
            // If the API is not initialized, return an empty feed.
            if (api == null)
                return new SocialFeed(this);

            /**
             * Get the updates 
             * */

            Dictionary<string,string> args = new Dictionary<string,string>();
            args.Add("q",query);
            JSONObject jobject = api.Get("/"+userName,args);

            // Create an socialfeed object
            SocialFeed a = new SocialFeed(this);
       
            foreach (JSONObject post in jobject.Dictionary["data"].Array)
            {
                StatusMessage message = JSONToMessage(post, a);
                 if(message!=null) a.Add(message);
            }
            return a;
        }

        public SocialFeed GetLastestUpdateFromQuery(string query)
        {
            // If the API is not initialized, return an empty feed.
            if (api == null)
                return new SocialFeed(this);

            /**
             * Get the updates 
             * */
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("q", query);
            JSONObject jobject = api.Get(String.Format("/me/home"),args);

            // Create an socialfeed object
            SocialFeed a = new SocialFeed(this);
            foreach (JSONObject post in jobject.Dictionary["data"].Array)
            {
                StatusMessage message = JSONToMessage(post, a);
                if (message != null) a.Add(message);
            }
            return a;
        }
    }
}
