using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaChrome.SocialNetworking
{
    /// <summary>
    /// Scrobble to an network
    /// </summary>
    public interface IScrobbler
    {
        /// <summary>
        /// Called on initialization
        /// </summary>
        void Initialize();
        /// <summary>
        /// Scrobble the current song
        /// </summary>
        /// <param name="song"></param>
        void Scrobble(Song song,string User,string Password);

       
        

    }

    /// <summary>
    /// LastFM scrobbler
    /// </summary>
    public class LastFM : IScrobbler
    {
        public void Initialize()
        {

        }
        public void Scrobble(Song song, string User, string Password)
        {

        }
    }

}
