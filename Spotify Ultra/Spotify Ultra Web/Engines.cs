﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpofityRuntime
{
    public partial class Engines : Form
    {
        public Engines()
        {
            InitializeComponent();
        }

        private void Engines_Load(object sender, EventArgs e)
        {
            int left = 10;
            int top = 10;
            // Create List of media Engines
            foreach (MediaChrome.IPlayEngine Engine in Program.MediaEngines.Values)
            {
                PictureBox pt = new PictureBox();
                this.Controls.Add(pt);
                pt.Left = left;
                pt.Top = top;
                pt.Click += new EventHandler(l_Click);
                pt.BackColor = Color.Transparent;
                if (Engine.Icon != null)
                {
                    pt.BackgroundImage = Engine.Icon;
                    pt.Width = Engine.Icon.Width;
                    pt.Height = Engine.Icon.Width;
                }
                Label l = new Label();
                l.Left = left + 30;
                l.Top = top;
                l.Font = new Font(l.Font.FontFamily, 12.0f, GraphicsUnit.Pixel);
                l.Click += new EventHandler(l_Click);
                l.Tag = Engine;
                pt.Tag = Engine;
                l.Cursor = Cursors.Hand;
                pt.Cursor = Cursors.Hand;
            }
        }

        void l_Click(object sender, EventArgs e)
        {
            // Get engine of option
            MediaChrome.IPlayEngine Engine = (MediaChrome.IPlayEngine)((Control)sender).Tag;
            Host.CurrentPlayer = Engine;

        }
        /// <summary>
        /// Parent form to host the menu
        /// </summary>
        public Form1 Host { get; set; }

        private void Engines_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Engines_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
