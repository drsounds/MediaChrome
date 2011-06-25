using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaChrome;
namespace MediaChromeGUI
{
    public partial class MainForm
    {
        /// <summary>
        /// Download directory
        /// </summary>
        public static string DownloadDir = "C:\\Downloads";

        /// <summary>
        /// Extract content inside ()
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String findVersion(String text)
        {
            bool inVersion = false;
            String ver = "";
            foreach (Char d in text)
            {
                if (inVersion)
                    ver += d;
                if (d == ')')
                {
                    inVersion = false;
                    continue;
                }
                if (d == '(')
                {
                    inVersion = true;
                    continue;
                }
            }
            return ver;
        }
    
        /// <summary>
        /// Extract the content inside []
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String findCommit(String text)
        {
            bool inVersion = false;
            String ver = "";
            foreach (Char d in text)
            {

                if (d == ']')
                {
                    inVersion = false;
                    continue;
                }
                if (d == '[')
                {
                    inVersion = true;
                    continue;
                }
                if (inVersion)
                    ver += d;
            }
            return ver;
        }
        public static System.Data.SQLite.SQLiteConnection MakeConnection()
        {
            return new MediaChromeGUI.LocalLibrary().MakeConnection();
        }
        public static Song GetSongFromQuery(System.Data.SQLite.SQLiteDataReader DR)
        {

            // Attach all returned local files to the list
            while (DR.Read())
            {
                MediaChrome.Song LF = new MediaChrome.Song();
                LF.Title = (String)DR["name"];
                LF.Artist = (String)DR["artist"];
                LF.AlbumName = (String)DR["album"];
                LF.Path = (String)DR["path"];
                try
                {
                    LF.CoverArt = (String)DR["cover_art"];
                }
                catch
                {
                    LF.CoverArt = "";
                }

                return LF;
            }
            // If no song was found return null;
            return null;
        }
        public static String GetURIFromSong(Song _Song)
        {
            String Version = MainForm.findVersion(_Song.Title);
            String Commit = MainForm.findCommit(_Song.Title);

            String Path = "music://t/" + _Song.Artist + "/" + _Song.Title.Replace("(" + Version + ")", "").Replace("[" + Commit + "]", "") + "/" + _Song.Album + (Version != "" ? "/" + Version : " ") + (Commit != "" ? "/" + Commit : "") + "?a=b" + (_Song.Engine != null || _Song.Engine != null ? "&service=" + _Song.Engine : "") + (_Song.ID != null ? "&id=" + _Song.ID : "");
            Path = Path.Replace("'", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "");
            return Path;
        }
        public static Song GetSongFromURI(string song)
        {
            return new Song();
        }
    }
}
