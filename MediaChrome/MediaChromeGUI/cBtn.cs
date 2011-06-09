/*
 * Created by SharpDevelop.
 * User: Alexander
 * Date: 2010-08-19
 * Time: 14:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MediaChrome
{
	/// <summary>
	/// Description of cBtn.
	/// </summary>
	/// 
	   
	public partial class cBtn : UserControl
	{
		public cBtn()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void CBtnMouseDown(object sender, MouseEventArgs e)
		{
            this.BackgroundImage = Properties.Resources.button_down;
		}
		
		void CBtnLoad(object sender, EventArgs e)
		{
			
		}
		
		void CBtnMouseUp(object sender, MouseEventArgs e)
		{
			this.BackgroundImage = Properties.Resources.button3;
		}
		public Image Img
		{
			get{return this.pictureBox1.BackgroundImage;}
            set { this.pictureBox1.BackgroundImage = value; }
		}
		
		void PictureBox1MouseEnter(object sender, EventArgs e)
		{
			
		}
		
		void PictureBox1MouseDown(object sender, MouseEventArgs e)
		{
			CBtnMouseDown(sender,e);
		}
		
		void PictureBox1MouseUp(object sender, MouseEventArgs e)
		{
			CBtnMouseUp(sender,e);
		}
		
		void PictureBox1MouseClick(object sender, MouseEventArgs e)
		{
			this.OnClick(new EventArgs());
		}

        private void cBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(new EventArgs());
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = Properties.Resources.button3;
        }
	}
}
