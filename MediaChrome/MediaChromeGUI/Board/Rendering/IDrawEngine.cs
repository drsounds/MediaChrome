using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Board
{
    /**
     * Abstract drawing engine for the board
     * */
    public interface IBoardRender
    {
        /// <summary>
        /// Initializes the engine
        /// </summary>
        void Initialize();

        /// <summary>
        /// Clear the plate
        /// </summary>
        void Destroy();




        /// <summary>
        /// Gets or sets the surface which 
        /// returns the drawing cache
        /// </summary>
        Control Surface { get;  }
        /// <summary>
        /// Draws an rectangle
        /// </summary>
        /// <param name="left">x</param>
        /// <param name="top">y</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        void DrawRectangle(System.Drawing.Color border,Rectangle Bounds);

        /// <summary>
        /// Draws an image with the specified coordinates. Should be scaled
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="Bounds"></param>
        void DrawImage(System.Drawing.Bitmap image, Rectangle Bounds);
        /// <summary>
        /// Draws an image with the specified coordinates. Should be scaled
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="Bounds"></param>
        void DrawImage(System.Drawing.Bitmap image, Point Bounds);
        /// <summary>
        /// Draws an line.
        /// </summary>
        /// <remarks>Points are absolute, not relative to each other</remarks>
        /// <param name="color">Color of the line</param>
        /// <param name="start">Starting point</param>
        /// <param name="end">End point</param>
        void DrawLine(Color color, Point start, Point end);

        /// <summary>
        /// Draws an string. If the string exceeds the bounds,
        /// clip it.
        /// </summary>
        /// <param name="str">The string to draw</param>
        /// <param name="font">The font to use</param>
        /// <param name="Bounds">The bounds</param>
        /// <param name="shadow">Decides if the string should have an shadow</param>
        void DrawString(String str, Font font, Color color, Rectangle Bounds);
        /// <summary>
        /// Draws an string. If the string exceeds the bounds,
        /// clip it.
        /// </summary>
        /// <param name="str">The string to draw</param>
        /// <param name="font">The font to use</param>
        /// <param name="Bounds">The bounds</param>
        /// <param name="shadow">Decides if the string should have an shadow</param>
        void DrawString(String str, Font font,Color color, Point Bounds);
        /// <summary>
        /// Clear the drawing cache and fill the 
        /// output with the specified color.
         /// </summary>
         /// <param name="color">The color to fill</param>
        void Clear(Color color);

        /// <summary>
        /// Fills an rectangle
        /// </summary>
        /// <param name="bgColor"></param>
        void FillRectangle(Color bgColor,Rectangle rect);

        /// <summary>
        /// Fills an rectangle
        /// </summary>
        /// <param name="bgColor"></param>
        void FillRectangle(Brush bgColor, Rectangle rect);

        /// <summary>
        /// Render the graphics to the output
        /// </summary>
        void Render();


        void DrawImage(Image image, Rectangle rectangle);
        void DrawImage(Image image, int left,int top,int width,int height);
        Size MeasureString(string p, Font font);
    }
}
