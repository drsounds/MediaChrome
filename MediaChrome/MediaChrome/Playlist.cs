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
            public List<Song> Songs { get; set; }
            public void Remove(int id)
            {
                Engine.RemoveFromPlaylist(ID, id);
            }
            public void Add(Song _Song, int pos)
            {
                Engine.AddToPlaylist(ID, _Song, pos);
                this.Songs.Insert(pos, _Song);
                RetrieveData();
            }
            public void Rorder(Song _Song, int spos, int epos)
            {
                Engine.MoveSongPlaylist(ID, _Song, spos, epos);
            }
            public Playlist(IPlayEngine Engine, string Name, String ID, System.Windows.Forms.Form host)
            {
                Songs = new List<Song>();
                this.Engine = Engine;
                this.Title = Name;
                this.ID = ID;

                this.Host = host;
                Thread ds = new Thread(RetrieveData);
                ds.Start();
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
