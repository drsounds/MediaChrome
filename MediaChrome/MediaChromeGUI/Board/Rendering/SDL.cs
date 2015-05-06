using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using System.Drawing;

namespace Board.Rendering
{
    class SDL : IBoardRender
    {

        public void Initialize()
        {
            /**
             * Initialize SDL components
             * */
            control = new SdlDotNet.Windows.SurfaceControl();
            surface = new Surface(9000, 9000, 24, false);
        }

        public void Destroy()
        {
            
        }
        private Surface surface;
        private SdlDotNet.Windows.SurfaceControl control;
        public System.Windows.Forms.Control Surface
        {
            get
            {
                return control;   
            }
          
        }

        public void FillRectangle(System.Drawing.Color border, System.Drawing.Rectangle Bounds)
        {
           
        }
        public void FillRectangle(System.Drawing.Pen border, System.Drawing.Rectangle Bounds)
        {

        }
        public void DrawImage(System.Drawing.Bitmap image, System.Drawing.Rectangle Bounds)
        {
            surface.Blit(new Surface(image), Bounds);
        }

        public void DrawLine(System.Drawing.Color color, System.Drawing.Point start, System.Drawing.Point end)
        {
            
        }

        public void DrawString(string str, System.Drawing.Font font,Color color, System.Drawing.Rectangle Bounds)
        {
            SdlDotNet.Graphics.Sprites.TextSprite text = new SdlDotNet.Graphics.Sprites.TextSprite(str, new SdlDotNet.Graphics.Font(font.Name,(int)font.SizeInPoints),color);
            surface.Blit(text, Bounds);
        }
        
        public void DrawString(string str, System.Drawing.Font font,Color color, System.Drawing.Point Bounds)
        {
            SdlDotNet.Graphics.Sprites.TextSprite text = new SdlDotNet.Graphics.Sprites.TextSprite(str, new SdlDotNet.Graphics.Font(font.Name, (int)font.SizeInPoints),color);
            surface.Blit(text, Bounds);
        }
        public void Clear(System.Drawing.Color color)
        {
            surface.Fill(color);
        }
        public Size MeasureString(String str,System.Drawing.Font font)
        {
            SdlDotNet.Graphics.Sprites.TextSprite r = new SdlDotNet.Graphics.Sprites.TextSprite(str, new SdlDotNet.Graphics.Font(font.Name, (int)font.SizeInPoints), Color.Black, false);
            return 
                new Size(r.TextWidth,r.Height);

        }
        public void Render()
        {
            control.Blit(surface);
        }


        public void DrawRectangle(Color border, Rectangle Bounds)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(Bitmap image, Point Bounds)
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(Brush bgColor, Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(Image image, Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(Image image, int left, int top, int width, int height)
        {
            throw new NotImplementedException();
        }
    }
}
