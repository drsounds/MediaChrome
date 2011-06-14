using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaChrome;
using System.Threading;
namespace MediaChrome
{
    namespace Views
    {
        public class Playlist
        {
            public Playlist() 
            {
            }

            public string Owner { get; set; }
            public bool CanModify { get; set; }
            public String Title { get; set; }
            public IPlayEngine Engine { get; set; }
            public String ID { get; set; }
            public System.Windows.Forms.Form Host;
            public List<Song> Songs
            {
                get;set;
            }
            public void Remove(int id)
            {
                Engine.RemoveFromPlaylist(ID, id);
            }
            
            public void Add(Song _Song, int pos)
            {
                try
                {
                    Engine.AddToPlaylist(ID, _Song, pos);
                    this.Songs.Insert(pos, _Song);
                    RetrieveData();
                }
                catch { }
            }
            public void Rorder(Song _Song, int spos, int epos)
            {
                Engine.MoveSongPlaylist(ID, _Song, spos, epos);
            }
            /// <summary>
            /// Creates a new playlist without an buffer
            /// </summary>
            /// <param name="Engine">The engine to work with</param>
            /// <param name="Name">Name of the playlist</param>
            /// <param name="ID">The system id of the playlist (specific for the extension)</param>
            /// <param name="host">The host of the playlist (the media provider)</param>
            public Playlist(IPlayEngine Engine, string Name, String ID, System.Windows.Forms.Form host)
            {
                Songs = new List<Song>();
                this.Engine = Engine;
                this.Title = Name;
                this.ID = ID;

                this.Host = host;
                Playlist s = this;
              //  Thread ds = new Thread(RetrieveData);
                Songs = Engine.LoadPlaylist(this.ID,ref s) ;
                
            }

            /// <summary>
            /// Creates a new playlist without an buffer
            /// </summary>
            /// <param name="Engine">The engine to work with</param>
            /// <param name="Name">Name of the playlist</param>
            /// <param name="ID">The system id of the playlist (specific for the extension)</param>
            /// <param name="host">The host of the playlist (the media provider)</param>
            /// <param name="cache">The cache to use instead of loading an new one</param>
            public Playlist(IPlayEngine Engine, string Name, String ID, System.Windows.Forms.Form host,List<Song> cache)
            {
                Songs = new List<Song>();
                this.Engine = Engine;
                this.Title = Name;
                this.ID = ID;

                this.Host = host;
                this.Songs = cache;
         //       Thread ds = new Thread(RetrieveData);
           //     ds.Start();
            }
            public bool Loaded { get; set; }
            public void RetrieveData()
            {
                try
                {
                    Playlist X =  this;
                  Songs= Engine.LoadPlaylist(this.ID,ref X);
                }
                catch
                {
                }
                Loaded = true;
            }
        }
    }
}
