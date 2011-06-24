using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using MediaChrome.Views;
using System.Configuration;
namespace MediaChromeService.Controllers
{
    public class PlaylistController : Controller
    {
        /// <summary>
        /// Makes an connection
        /// </summary>
        /// <returns></returns>
        public SqlConnection MakeConnection()
        {
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);
            Conn.Open();
            return Conn;
        }
        /// <summary>
        /// Returns the dataset for the playlist
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            // Create objects
            DataSet r = new DataSet("Playlist");
            DataTable Playlist = r.Tables.Add("playlist");

            // specify columns
            Playlist.Columns.Add("id");             // The id column
            Playlist.Columns.Add("title");          // The title column
            
            Playlist.Columns.Add("userID");         // The USer ID column
            Playlist.Columns.Add("data");           // the data of the playlist
            Playlist.Columns.Add("annotation");     // The annotation of the playlist
            Playlist.Columns.Add("collaborative");  // integer specifying the mode of collaboration the playlist accepts

            return r;
        }
        /// <summary>
        /// Returns an IEnemurable[Playlist] query
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<Playlist> GetPlaylistsFromID(string ID)
        {
            

           DataSet set = GetDataSet();
           SqlConnection Connection = MakeConnection(); // Connect to the database
           
            // Request data
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM playlist WHERE userID=" + ID, Connection);
            
            // fill the datapter
            Adapter.Fill(set, "playlist");

            // Return the data
            var data = from datatable in set.Tables[0].AsEnumerable() where datatable["userID"] == ID orderby datatable["title"] select datatable;
            List<Playlist> Playlists = new List<Playlist>();
            foreach (DataRow Row in data)
            {
                /**
                 * Now set data to the mediaChrome playlist object
                 * */
                Playlist d = new Playlist();
                d.Title = (string)Row["title"];
                d.ID = ((int)Row["id"]).ToString();
                Playlists.Add(d);
            }
            return Playlists;

        }
        //
        // GET: /Playlist/

        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /Playlist/Details/5

        public ActionResult Details(int id)
        {

            return View();
        }

        //
        // GET: /Playlist/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Playlist/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Playlist/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Playlist/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Playlist/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Playlist/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
