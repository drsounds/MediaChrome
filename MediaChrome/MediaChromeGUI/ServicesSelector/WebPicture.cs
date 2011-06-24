using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Policy;
using System.Net;
using System.IO;
namespace MediaChrome.ServicesSelector
{
    class WebPicture : PictureBox
    {
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
                WebClient WC = new WebClient();
                WC.DownloadDataCompleted+=new DownloadDataCompletedEventHandler(WC_DownloadDataCompleted);
                if(uri != null)
                WC.DownloadDataAsync(uri);


            }
        }


        void WC_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            MemoryStream MS =new MemoryStream(e.Result);


            Image By = Image.FromStream(MS);
            this.BackgroundImage = By;
        }
    }
}
