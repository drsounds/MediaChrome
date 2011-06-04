using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Board.Rendering
{
    class GDI : IBoardRender
    {

        public void Initialize()
        {
           
        }

        public void Destroy()
        {
           
        }
        private Control surface;
        public System.Windows.Forms.Control Surface
        {
            get { return surface; }
        }

        public void DrawRectangle(System.Drawing.Color border, System.Drawing.Rectangle Bounds)
        {
         
        }

        public void DrawImage(System.Drawing.Bitmap image, System.Drawing.Rectangle Bounds)
        {
          
        }

        public void DrawImage(System.Drawing.Bitmap image, System.Drawing.Point Bounds)
        {
          
        }

        public void DrawLine(System.Drawing.Color color, System.Drawing.Point start, System.Drawing.Point end)
        {
           
        }

        public void DrawString(string str, System.Drawing.Font font, System.Drawing.Color color, System.Drawing.Rectangle Bounds)
        {
            
        }

        public void DrawString(string str, System.Drawing.Font font, System.Drawing.Color color, System.Drawing.Point Bounds)
        {
           
        }

        public void Clear(System.Drawing.Color color)
        {
           
        }

        public void FillRectangle(System.Drawing.Color bgColor, System.Drawing.Rectangle rect)
        {
            
        }

        public void FillRectangle(System.Drawing.Brush bgColor, System.Drawing.Rectangle rect)
        {
          
        }

        public void Render()
        {
            
        }

        public void DrawImage(System.Drawing.Image image, System.Drawing.Rectangle rectangle)
        {
        }

        public void DrawImage(System.Drawing.Image image, int left, int top, int width, int height)
        {
           
        }

        public System.Drawing.Size MeasureString(string p, System.Drawing.Font font)
        {
            return new Size(0,0);
        }   
    }
}
