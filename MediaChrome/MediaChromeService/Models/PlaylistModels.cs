using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaChromeService.Models
{
    public class Playlist
    {
        /// <summary>
        /// ID of playlist
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// User ID created the playlist
        /// </summary>
        public string UserID { get; set; }

  
        
    }
    public class PlaylistModels : Controller
    {
       
        
        //
        // GET: /PlaylistModels/

        public ActionResult Index()
        {
            return View();
        }

    }
}
