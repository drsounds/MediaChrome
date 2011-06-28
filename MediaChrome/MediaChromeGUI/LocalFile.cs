using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using MediaChrome;
namespace MediaChromeGUI
{
    public class LocalLibrary
    {
       
        /// <summary>
        /// Creates an new SQLiteConnection
        /// </summary>
        /// <returns>An sqlite connection</returns>
        public SQLiteConnection MakeConnection()
        {
            SQLiteConnection d = new SQLiteConnection("Data source=localFiles.sqlite;");
            d.Open();
            return d;
        }
        public Song GetSongFromQuery(string Query)
        {
            // Create connection
            SQLiteConnection Conn = MakeConnection();
            // Make query command
            SQLiteCommand Command = new SQLiteCommand(Query, Conn);
            SQLiteDataReader DR = Command.ExecuteReader();
            // Create output list of local files
            List<MediaChrome.Song> localFile = new List<MediaChrome.Song>();

            // Attach all returned local files to the list
            while (DR.Read())
            {
                MediaChrome.Song LF = new MediaChrome.Song();
                LF.Title = (String)DR["title"];
                LF.ArtistName = (String)DR["artist"];
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

            // Return null if no song was found
            return null;
        }
        /// <summary>
        /// Returns an array of local files from Query
        /// </summary>
        /// <param name="query">the SQL query</param>
        /// <returns>a list of LocalFiles</returns>
        public List<MediaChrome.Song> GetFilesFromQuery(string query)
        {
            // Create connection
            SQLiteConnection Conn = MakeConnection();
            // Make query command
            SQLiteCommand Command = new SQLiteCommand(query, Conn);
            SQLiteDataReader DR = Command.ExecuteReader();
            // Create output list of local files
            List<MediaChrome.Song> localFile = new List<MediaChrome.Song>();

            // Attach all returned local files to the list
            while (DR.Read())
            {
                MediaChrome.Song LF = new MediaChrome.Song();
              
                LF.Title = GetData("name",DR);
                LF.ArtistName = GetData("artist",DR);
                LF.AlbumName = GetData("album",DR);
                LF.Path = GetData("path",DR);
                    LF.CoverArt = GetData("cover_art",DR);
               
                localFile.Add(LF);
            }
            return localFile;
        }
        /// <summary>
        /// Return safe dat from an column frm an data reader
        /// </summary>
        /// <param name="Column">the name of the column</param>
        /// <param name="DR">the SQLite data reader</param>
        /// <returns></returns>
        public string GetData(String Column,SQLiteDataReader DR)
        {
            
            try
            {
                return (string)DR[Column];
            }
            catch
            {
                return null;
            }
        }
    }
    
}
