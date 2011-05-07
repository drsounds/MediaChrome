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
            this.timrDownloadCheck = new System.Windows.Forms.Timer(this.components);
            this.tmrViewUpdate = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timrDownloadCheck
            // 
            this.timrDownloadCheck.Interval = 500;
            this.timrDownloadCheck.Tick += new System.EventHandler(this.timrDownloadCheck_Tick);
            // 
            // tmrViewUpdate
            // 
            this.tmrViewUpdate.Enabled = true;
            this.tmrViewUpdate.Interval = 10000;
            this.tmrViewUpdate.Tick += new System.EventHandler(this.tmrViewUpdate_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 15000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // DrawBoard
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DrawBoard";
            this.Size = new System.Drawing.Size(736, 539);
            this.Load += new System.EventHandler(this.Artist_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DrawBoard_Scroll);
            this.SizeChanged += new System.EventHandler(this.DrawBoard_SizeChanged);
            this.Click += new System.EventHandler(this.DrawBoard_Click);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DrawBoardDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DrawBoard_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.DrawBoard_DragOver);
            this.DragLeave += new System.EventHandler(this.DrawBoard_DragLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Artist_Paint);
            this.DoubleClick += new System.EventHandler(this.Artist_DoubleClick);
            this.Enter += new System.EventHandler(this.Artist_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DrawBoard_KeyDown);
            this.Leave += new System.EventHandler(this.Artist_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Artist_MouseDown);
            this.MouseLeave += new System.EventHandler(this.Artist_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Artist_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawBoardMouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timrDownloadCheck;
        private System.Windows.Forms.Timer tmrViewUpdate;
        private System.Windows.Forms.Timer timer2;
    }
}
