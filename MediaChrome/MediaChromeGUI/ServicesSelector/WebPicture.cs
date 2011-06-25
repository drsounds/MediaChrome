using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Policy;
using System.Net;
using System.IO;
namespace MediaChromeGUI.ServicesSelector
{
    class WebPicture : PictureBox
    {
        public  WebClient WC = new WebClient();
        private Uri uri;

       
        
        public Uri Url
        {
            get
            {
                return uri;
            }
            set
            {
                this.uri = value;
                /**
                 * Download image asynchronisly
                 * */
               
                WC.DownloadDataCompleted+=new DownloadDataCompletedEventHandler(WC_DownloadDataCompleted);
                if(uri != null)
                WC.DownloadDataAsync(uri);


            }
        }


        void WC_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                MemoryStream MS = new MemoryStream(e.Result);


                Image By = Image.FromStream(MS);
                this.BackgroundImage = By;
            }
            catch { }
        }
    }
}
