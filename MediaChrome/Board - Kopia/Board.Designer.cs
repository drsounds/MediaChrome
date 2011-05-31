namespace Board
{
    partial class DrawBoard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
       /* protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }*/

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	this.timer1 = new System.Windows.Forms.Timer(this.components);
        	this.SuspendLayout();
        	// 
        	// timer1
        	// 
        	this.timer1.Enabled = true;
        	this.timer1.Interval = 10;
        	this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        	// 
        	// DrawBoard
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.Name = "DrawBoard";
        	this.Size = new System.Drawing.Size(934, 779);
        	this.Load += new System.EventHandler(this.Artist_Load);
        	this.DoubleClick += new System.EventHandler(this.Artist_DoubleClick);
        	this.MouseLeave += new System.EventHandler(this.Artist_MouseLeave);
        	this.Paint += new System.Windows.Forms.PaintEventHandler(this.Artist_Paint);
        	this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Artist_MouseMove);
        	this.Leave += new System.EventHandler(this.Artist_Leave);
        	this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DrawBoardDragDrop);
        	this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Artist_MouseDown);
        	this.Enter += new System.EventHandler(this.Artist_Enter);
        	this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawBoardMouseUp);
        	this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
    }
}
