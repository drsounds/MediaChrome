using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;
using System.Data;
namespace ws.spotify.com
{
    /// <summary>
    /// A reader for the ws.spotify.com
    /// </summary>
    public class Reader
    {
        public DataSet GetStatistics(String query)
        {
            ws.spotify.com.Reader reader = new ws.spotify.com.Reader(query);
            reader.Read();
            DataSet R = new DataSet("Result");
            DataTable r = R.Tables.Add();
            r.Columns.Add("Title");
            r.Columns.Add("Artist");
            r.Columns.Add("Album");
            r.Columns.Add("Popularity");
            r.Columns.Add("PlaysPerDay");
            r.Columns.Add("PlaysPerWeek");
            r.Columns.Add("TotalPlays");
            r.Columns.Add("LinkString");
            foreach (Track rt in reader.Result.Tracks)
            {
               DataRow row = r.Rows.Add(rt.Name,rt.Artist.Name,"",rt.Popularity,rt.PlaysPerDay,rt.PlaysPerWeek,rt.TotalPlays);
              
            }
            return R;
        }
        public string Query {get;set;}
        public Reader(String query)
        {
            
            Query = query;
        }
        public Reader()
        {
        }
        public static Album GetAlbum(String URI)
        {
            System.Xml.Serialization.XmlSerializer w = new XmlSerializer(typeof(Album));
            string baseQuery = String.Format("http://ws.spotify.com/lookup/1?uri={0}",URI);
            XmlDocument xmlDoc = new XmlDocument();
            WebClient WC = new WebClient();
            string xml = WC.DownloadString(baseQuery);
            xml = xml.Replace("xmlns:opensearch=", "xmlns_opensearch=");
            xml = xml.Replace("opensearch:", "opensearch_");
            xmlDoc.LoadXml(xml);

            return  (Album)w.Deserialize(new XmlNodeReader(xmlDoc));

        }
        /// <summary>
        /// The client used for web query
        /// </summary>
        private WebClient client;
        public void Read()
        {
            /**
             * Connect to the server 
             * */
            System.Xml.Serialization.XmlSerializer w = new XmlSerializer(typeof(Result));
            XmlDocument xmlDoc = new XmlDocument();
             String baseQuery = String.Format("http://ws.spotify.com/search/1/track?q={0}",Query.Replace(" ","%20").Replace(".",""));


            /**
             * Get data
             * */
             WebClient WC = new WebClient();
             string xml = WC.DownloadString(baseQuery);
             xml = xml.Replace("xmlns:opensearch=", "xmlns_opensearch=");
             xml = xml.Replace("opensearch:", "opensearch_");
             xmlDoc.LoadXml(xml);
            
            Result = (Result)w.Deserialize(new  XmlNodeReader(xmlDoc));

            // Generate all stats for the songs
            foreach (Track d in Result.Tracks)
            {
                d.GenerateStats();
            }


        }
        public Result Result;
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.spotify.com/ns/music/1")]
    [System.Xml.Serialization.XmlRootAttribute("tracks", Namespace = "http://www.spotify.com/ns/music/1")]
    public class Result
    {
        [XmlElement("track")]
        public List<Track> Tracks;
        public Result()
        {
            Tracks = new List<Track>();
        }
    }
   
    /// <summary>
    /// A base class with all common properties for each spotify object
    /// </summary>
    [Serializable]
    public class SPObject
    {
        [XmlElement("popularity")]
        public float Popularity;
        [XmlAttribute("href")]
        public string Link;


        [XmlElement("name")]
        public string Name;

        [XmlElement("territories")]
        public string Territories;

    }

    public class Album : SPObject
    {
        [XmlElement("track")]
        public List<Track> Tracks;
        public Album()
        {
            Tracks = new List<Track>();
        }
    }
    [Serializable] 
    public class Artist : SPObject
    {
        

    }
    [Serializable]
    public class ID
    {
        [XmlAttribute("name")]
        public string Type;
        [XmlText]
        public string Value;
    }
    [Serializable]
    public class Track : SPObject
    {
        /// <summary>
        /// The popularity of the song
        /// </summary>
       

      
        /// <summary>
        /// The artist of the song
        /// </summary>
        /// 
        [XmlElement("artist")]
        public Artist Artist;

        /// <summary>
        /// The interval of update
        /// </summary>
        public static int INTERVAL = 31;

        /// <summary>
        /// Days per week
        /// </summary>
        public const Double WEEKDAYS = 7;

        /// <summary>
        /// Rate of update
        /// </summary>
        public  const Double RATE = 0.006f;


        /// <summary>
        /// Total songs on Spotify
        /// </summary>
        public int COUNT_SONGS = 11358958;

        /// <summary>
        /// Total plays per month on spotify for all media
        /// </summary>
        public  Double TOTAL_PLAYS_PER_MONTH = (double)92000000 * 10;

        /// <summary>
        /// The count plays compared to the total plays per month of all songs
        /// </summary>
        public Double NormalCount
        {
            get
            {
                return Math.Floor((TOTAL_PLAYS_PER_MONTH * 2) / COUNT_SONGS);
            }
        }
        public Double OldPopularity { get; set; }
   
        public Double PlaysPerDay { get; set; }
        public Double TotalPlays { get; set; }

        public Double NeatRevenue { get; set; }
       
        public int ID = 0;
        public void GenerateStats()
        {
            Double differences = Popularity / OldPopularity;
            double cf = this.Popularity * (NormalCount);
            TotalPlays = cf + Math.Pow(cf-cf/2,2);

            // Decode amount of plays according to the popularity in the stats pulse
            PlaysPerDay = (Double)Math.Floor(TotalPlays / INTERVAL);
            PlaysPerWeek = PlaysPerDay * WEEKDAYS;
            TotalPlays = PlaysPerWeek * 4;
            NeatRevenue = TotalPlays * RATE;

        }
        
        public String LinkString;
        public Double CountPlays
        {
            get
            {
                return 0;
            }
        }
        public Double PlaysPerWeek
        {
            get;
            set;
        }
    }
    
    
}