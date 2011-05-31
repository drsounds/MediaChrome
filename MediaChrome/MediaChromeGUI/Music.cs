using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicEngine
{
	public interface ISearchResult
	{
        ITrack[] Tracks { get; set; }
        IArtist[] Artists { get; set; }
        IAlbum[] Albums { get; set; }
        ISession Session { get; set; }
        string Query { get; set; }


	}

    public interface ISession
    {
        MediaChrome.Form1 Host
        {
            get;
            set;
        }
        void LogIn(string userName,string Password);
        void LogOut();
        void Load(ITrack Track);
        void Play();
        void Pause();
        string Namespace
        {
            get;
            
        }
        void Stop();
    }
    public interface ITrack
    {
        ISession Session
        {
            get;
            set;
        }
        IAlbum Album
        {
            get;set;
        }
        IArtist[] Artist
        {
            get;
        }
        string LinkString
        {
            get;
        }
        bool IsStarred
        {
            get;set;
        }
        bool IsAvailable
        {
            get;
        }
        string Provider
        {
            get;
        }
    }
    public interface IAlbumBrowse
    {
        
        ITrack Tracks{get;set;}
        IAlbum Album{get;set;}
    }

    public interface IPlaylist
    {
        ITrack[] Tracks {get;set;}
    }
    public interface IAlbum
    {
        IArtist Artist
        {
            get;
            set;
        }
        
    }
    public interface ILink
    {
    
    }
    public interface IEntity
    {
        
        IArtist[]  Artist
        {
            get;set;
        }
        IAlbum Album
        {
            get;
       
        }
        bool IsStarred
        {
            get;set;
        }
        bool IsAvailable
        {
            get;set;
        }
        string Name
        {
            get;
        }
        
    }

    public interface IArtist
    {
        string Name
        {
            get;
     
        }
    }
}
