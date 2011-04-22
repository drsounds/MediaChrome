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
               // this.Refresh();
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
        public Color BorderColor
        {
            get;
            set;
        }
        private void ucPosBar_Paint(object sender, PaintEventArgs e)
        {
            
            e.Graphics.DrawRectangle(new Pen(BorderColor), 0, 0, this.Width-1, this.Height-1);
            e.Graphics.FillEllipse(new SolidBrush(FillColor), Value * XPart, 0, this.Height, this.Height);
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
