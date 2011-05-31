using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace SpofityRuntime
{
    public class Pane : Panel
    {
        private bool dark;
        public bool Dark
        {
            get{return dark;}
            set { dark = value; }
        }
        BufferedGraphicsContext D;
        BufferedGraphics X;
        public Pane() 
        {
            D = new BufferedGraphicsContext();
            this.Paint += new PaintEventHandler(Pane_Paint);
            if (secondColor == null)
            {
                secondColor = Color.Gray;
            }
            this.MouseDoubleClick += new MouseEventHandler(Pane_MouseDoubleClick);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        	try{
            base.OnPaintBackground(e);
            X = D.Allocate(e.Graphics, this.Bounds);
            X.Graphics.FillRectangle(new LinearGradientBrush(new Point(1, 1), new Point(1, this.Height), this.BackColor, this.secondColor), new Rectangle(0, 0, this.Width, this.Height));
            X.Render();
        	}
        	catch{
        		
        	}
        }
        protected override void OnPaint(PaintEventArgs e)
        {
        	try
        	{
	        	X = D.Allocate(e.Graphics, this.Bounds);
	            X.Graphics.FillRectangle(new LinearGradientBrush(new Point(1, 1), new Point(1, this.Height), this.BackColor, this.secondColor), new Rectangle(1, 1, this.Width, this.Height));
	            X.Render();
        	}
        	catch
        	{
        	
        	}
        }
        void Pane_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        
        }
        private Color secondColor;
        public Color SecondColor
        {
            get
            {
                return secondColor;
            }
            set 
            {
                secondColor = value; 
            }
        }
        void Pane_Paint(object sender, PaintEventArgs e)
        {
            
      		X = D.Allocate(e.Graphics, this.Bounds);
            X.Graphics.FillRectangle(new LinearGradientBrush(new Point(1, 1), new Point(1, this.Height), this.BackColor, this.secondColor), new Rectangle(1, 1, this.Width, this.Height));
            X.Render();
           
        }
    }
}
