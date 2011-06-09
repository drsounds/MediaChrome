using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace webclassprototype
{
    public partial class ucPosBar : UserControl
    {
       
    
        public float XPart
        {
            get
            {
                return (float)(Width / Maximum);

            }
        }
        private float maximum=100;
        public float Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                maximum = value;
                //this.Refresh();
            }
        }
        private float value = 0;
        public float Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
               this.Refresh();
            }
        }
        public ucPosBar()
        {
            InitializeComponent();
            FillColor = Color.White;
            BorderColor = Color.Black;
        }

        private void ucPosBar_Load(object sender, EventArgs e)
        {

        }
        public Color FillColor
        {
            get;
            set;
        }
        public Image Thumb
        {
            get
            {
                return MediaChrome.Properties.Resources.playthumb;
            }
        }
        public Image Bar
        {
            get
            {
                return MediaChrome.Properties.Resources.position_bar;
            }
        }
        public Color BorderColor
        {
            get;
            set;
        }
        public void Draw(Graphics p)
        {
#if(nobug)
            e.DrawRectangle(new Pen(BorderColor), 0, 0, this.Width - 1, this.Height - 1);
            e.FillEllipse(new SolidBrush(FillColor), Value * XPart, 0, this.Height, this.Height);
#endif
            BufferedGraphicsContext Cont = new BufferedGraphicsContext();
            BufferedGraphics r = Cont.Allocate(p, new Rectangle(0, 0, this.Width, this.Height));
            Graphics e = r.Graphics;
            // Draw background
            e.DrawImage(Bar,new Rectangle(0,0,this.Height,this.Height),new Rectangle(0,0,16,16),GraphicsUnit.Pixel);
            e.DrawImage(Bar, new Rectangle(this.Height, 0, this.Width-this.Height, this.Height), new Rectangle(16, 0, 16, 16), GraphicsUnit.Pixel);
            e.DrawImage(Bar, new Rectangle(this.Width - this.Height, 0,this.Height , this.Height), new Rectangle(32, 0, 16, 16), GraphicsUnit.Pixel);
            float val = value > 0 ? value : 1;
            // Draw position bar
            if (!float.IsInfinity(XPart))
            {
                e.DrawImage(Thumb, (val / maximum)*Width, 0, this.Height, this.Height);
            }
            r.Render();
        }
        private void ucPosBar_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
           
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Draw(e.Graphics);
        }
        private void ucPosBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float x = 0;
                int i = 0;
                while (x < e.X)
                {
                    x += XPart;
     
                }
                Value = x/XPart;
                this.Refresh();
            }
        }

        private void ucPosBar_PaddingChanged(object sender, EventArgs e)
        {

        }
    }
}
