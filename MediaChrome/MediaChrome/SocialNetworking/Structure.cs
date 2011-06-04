using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MediaChrome.SocialNetworking
{
    /// <summary>
    /// Resembling an wall of feeds
    /// </summary>

    public class User
    {
        public User()
        {
        }
        /// <summary>
        /// Returns the network the user is residing on
        /// </summary>
        public ISocialNetwork Engine { get; set; }

        /// <summary>
        /// Returns the user's feed
        /// </summary>
        public SocialFeed Feed
        {
            get
            {
                try
                {
                    return Engine.GetFeedFromUser(UserName);
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// The user alias
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Image url
        /// </summary>
        public String ImageUrl { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The ID of the user
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The link to the user's page
        /// </summary>
        public String Link { get; set; }
    }

    /// <summary>
    /// Class for social feeds
    /// </summary>
    public class SocialFeed : IList<StatusMessage>
    {
        public User User { get; set; }
        
         List<StatusMessage> feed;
         /// <summary>
         /// Creates an new instance of Social Feed
         /// </summary>
         /// <param name="Service">The service used for the feed</param>
         public SocialFeed(ISocialNetwork Service)
         {
             engine = Service;
             feed = new List<StatusMessage>();

         }
        #region Implementation
        public int IndexOf(StatusMessage item)
        {
            return feed.IndexOf(item);
        }

        public void Insert(int index, StatusMessage item)
        {
            feed.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            feed.RemoveAt(index);
        }

        public StatusMessage this[int index]
        {
            get
            {
                return feed[index];
            }
            set
            {
                feed[index] = value;
            }
        }

        public void Add(StatusMessage item)
        {
            feed.Add(item);
        }

        public void Clear()
        {
            feed.Clear();
        }

        public bool Contains(StatusMessage item)
        {
            return feed.Contains(item);
        }

        public void CopyTo(StatusMessage[] array, int arrayIndex)
        {
            feed.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return feed.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(StatusMessage item)
        {
            return feed.Remove(item);
        }

        public IEnumerator<StatusMessage> GetEnumerator()
        {
            return feed.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return feed.GetEnumerator();
        }
        #endregion
        

        /// <summary>
        /// Gets the engine resembling the social network.
        /// </summary>
        public ISocialNetwork Engine { get; set; }
        private ISocialNetwork engine;

        
    }
    public enum Visibility { All, Friends, Custom }
    /// <summary>
    /// Interface for social networks. Assembles an social network.
    /// </summary>
    public interface ISocialNetwork
    {

        /// <summary>
        /// Login to an social network. UserName/Password is handled by an form
        /// provided by the extension.
        /// </summary>
        void Login();
        
        /// <summary>
        /// Logout from the network
        /// </summary>
        void Logout();

        /// <summary>
        /// Gets the last updates
        /// </summary>
        SocialFeed LastUpdates { get; }

        /// <summary>
        /// Post an status message
        /// </summary>
        /// <param name="str">The string for status</param>
        /// <returns>TRUE if sucess, FALSE if failed</returns>
        bool PostStatusMessage(string str, Visibility security);

        /// <summary>
        /// Share an link to the wall
        /// </summary>
        /// <param name="msg">The message attached to the link</param>
        /// <param name="link">The link to share</param>
        /// <param name="security">How the visibility for the announcement should be configured</param>
        /// <returns>TRUE if sucess, FALSE if failed</returns>
        bool PostLink(string msg, Uri link,Visibility security);

        /// <summary>
        /// Returns an feed for an particular user
        /// </summary>
        /// <param name="userName">The user name to locate</param>
        /// <returns>an instance to an SocialFeed encapsulating the user's feed data, or NULL if failed</returns>
        SocialFeed GetFeedFromUser(string userName);

        /// <summary>
        /// Returns an list of your friends on the particular network
        /// </summary>
        List<User> Friends { get; }
        
    }


    /// <summary>
    /// An status message
    /// </summary>
    public class StatusMessage
    {
        /// <summary>
        /// Security for the message. 
        /// </summary>
        public Visibility Security;

        /// <summary>
        /// Creates an new status message
        /// </summary>
        /// <param name="msg">The message</param>
        /// <param name="parent">The parent</param>
        /// <param name="time">The time</param>
        public StatusMessage(string msg, SocialFeed parent, DateTime time,Visibility security)
        {
            Message = msg;
            Parent = parent;
            Time = time;
            Security = security;
        }
        /// <summary>
        /// The message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the parent feed
        /// </summary>
        public SocialFeed Parent { get; set; }

        /// <summary>
        /// Returns the engine providing the feed (shortcut to the parent one)
        /// </summary>
        public ISocialNetwork Engine
        {
            get
            {
                return Parent.Engine;
            }
        }

        /// <summary>
        /// Returns if the message has an link
        /// </summary>
        public bool HasLink
        {
            get
            {
                return !String.IsNullOrEmpty(Link);
            }
        }
        /// <summary>
        /// The link to provide by the status message. Null if the post
        /// does not attach an link
        /// </summary>
        public String Link { get; set; }

        /// <summary>
        /// The time for the post
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// User behind the status messsage
        /// </summary>
        public User User { get; set; }
    }
}
