using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaChrome
{
    public partial class Switch : UserControl
    {
        public Switch()
        {
            InitializeComponent();
            if (this.BackgroundImage == null)
                this.BackgroundImage = MediaChrome.Properties.Resources._switch;
        }
        /// <summary>
        /// The switch value
        /// </summary>
        public bool Active { get; set; }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Draw(e.Graphics);
        }
        
        /// <summary>
        /// Paints the graphics
        /// </summary>
        /// <param name="d"></param>
        private void Draw(Graphics d)
        {
            if (this.BackgroundImage != null)
            {
                int ytop = 0;
                if (this.Active)
                    ytop += BackgroundImage.Height / 2;
                d.DrawImage(this.BackgroundImage, new Rectangle(0, 0, this.Width, this.Height), new Rectangle(0, ytop, BackgroundImage.Width, BackgroundImage.Height / 2), GraphicsUnit.Pixel);

            }
        }

        private void Switch_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }
        /// <summary>
        /// Toggle event handler
        /// </summary>
        public class ToggleEventArgs
        {
            /// <summary>
            /// TRUE if the operation should be cancel
            /// </summary>
            public bool Cancel { get; set; }
        }
        /// <summary>
        /// Occurs on toggling
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">Toggle event args</param>
        public delegate void ToggleEventHandler(object sender,ToggleEventArgs e);
        /// <summary>
        /// Occurs when toggling
        /// </summary>
        public event ToggleEventHandler Toggling;

        /// <summary>
        /// Occurs after toggle has been finished
        /// </summary>
        public event ToggleEventHandler Toggled;
        /// <summary>
        /// Switch
        /// </summary>
        public void Toggle()
        {
            // Initiate toggle event and check if canceled
            ToggleEventArgs args = new ToggleEventArgs();
            if (Toggling != null)
            {
                Toggling(this, args);
                if (args.Cancel)
                    return;
            }
            this.Active = !this.Active;
            
            // Raise toggled event handler
            if (Toggled != null)
                Toggled(this, args);

           

        }
        private void Switch_Click(object sender, EventArgs e)
        {
            
        }
    }
}
