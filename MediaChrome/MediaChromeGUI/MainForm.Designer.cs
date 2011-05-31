/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 2010-11-07
 * Time: 10:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SpofityRuntime
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listView1 = new System.Windows.Forms.ListView();
			this.panel3 = new System.Windows.Forms.Panel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(949, 68);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 555);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(949, 68);
			this.panel2.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 68);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.listView1);
			this.splitContainer1.Panel1.Controls.Add(this.panel3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(949, 487);
			this.splitContainer1.SplitterDistance = 316;
			this.splitContainer1.TabIndex = 2;
			// 
			// listView1
			// 
			this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(316, 317);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// panel3
			// 
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 317);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(316, 170);
			this.panel3.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Size = new System.Drawing.Size(629, 487);
			this.splitContainer2.SplitterDistance = 392;
			this.splitContainer2.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(949, 623);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
	}
}
