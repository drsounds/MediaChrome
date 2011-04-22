using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Board
{
    class Program
    {
        [STAThread]
        public static void Main(String[] args)
        {
           
            MakoParser D = new MakoParser();

            Application.Run(D);
        }
    }
}
