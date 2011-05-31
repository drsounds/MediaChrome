using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Design;
namespace MediaChrome
{
    class ListViewX : ListView
    {
    
        public ListViewX()
        {
            
            this.OwnerDraw = false;
            this.DrawItem += new DrawListViewItemEventHandler(ListViewX_DrawItem);
            this.DrawSubItem += new DrawListViewSubItemEventHandler(ListViewX_DrawSubItem);
            this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(ListViewX_DrawColumnHeader);
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.ForeColor = Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SizeChanged += new EventHandler(ListViewX_SizeChanged);
            this.OwnerDraw = false;
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        	
        }
        void ListViewX_SizeChanged(object sender, EventArgs e)
        {
            if (this.Columns.Count > 0)
            {
                this.Columns[0].Width = this.Width;
            }
        }

        void ListViewX_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            int left = 0;
            if (e.Item.Text != "-")
            {
                if (e.ColumnIndex == 0)
                {
                    left = 18;
                    object key;
                    key = (e.Item.ImageKey != null ? e.Item.ImageKey : "");
                    if (key == "")
                    {
                        key = -1;
                    }
                    try
                    {
                        e.Graphics.DrawImage(this.SmallImageList.Images[key.ToString()], e.Bounds.Left, e.Bounds.Top);
                    }
                    catch
                    {
                    }
                }
                else
                {
                    left = 0;
                }
                if (e.Item.Selected)
                {
                    e.Graphics.DrawString(e.Item.Text, new Font("MS Sans Serif", 8), new SolidBrush(Color.Black), new Point(e.Bounds.Left+left, e.Bounds.Top));
                }
                else
                {
                    e.Graphics.DrawString(e.Item.Text, new Font("MS Sans Serif", 8), new SolidBrush(Color.White), new Point(e.Bounds.Left + left, e.Bounds.Top));
                }
            }
            
        }

        void ListViewX_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
         
        }

        void ListViewX_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item.Text == "-")
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50,50,50)), new Point(e.Bounds.Left, e.Bounds.Top + e.Bounds.Height / 2), new Point(e.Bounds.Width, e.Bounds.Top + e.Bounds.Height / 2));
            }
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(127, 200, 255)), e.Bounds);
            }
        }

    }
}
