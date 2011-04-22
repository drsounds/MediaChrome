/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 2010-11-04
 * Time: 09:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SpofityRuntime
{
	partial class View
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.geckoWebBrowser1 = new Skybound.Gecko.GeckoWebBrowser();
			this.SuspendLayout();
			// 
			// geckoWebBrowser1
			// 
			this.geckoWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.geckoWebBrowser1.Location = new System.Drawing.Point(0, 0);
			this.geckoWebBrowser1.Name = "geckoWebBrowser1";
			this.geckoWebBrowser1.Size = new System.Drawing.Size(495, 343);
			this.geckoWebBrowser1.TabIndex = 0;
			this.geckoWebBrowser1.Click += new System.EventHandler(this.GeckoWebBrowser1Click);
			// 
			// View
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.geckoWebBrowser1);
			this.Name = "View";
			this.Size = new System.Drawing.Size(495, 343);
			this.ResumeLayout(false);
		}
		private Skybound.Gecko.GeckoWebBrowser geckoWebBrowser1;
	}
}
