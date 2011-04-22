using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace webclassprototype
{
    public partial class ucBar : Panel
    {
        public ucBar()
        {
            InitializeComponent();
        }

        private void ucBar_Load(object sender, EventArgs e)
        {

        }

        private void ucBar_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush LGB = new LinearGradientBrush(new Point(1,1),new Point(1,this.Height),Color.White,this.BackColor);
            e.Graphics.FillRectangle(LGB, 0, 0, this.Width, this.Height);

        }
    }
}
