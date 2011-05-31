namespace webclassprototype
{
    partial class ucPosBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ucPosBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucPosBar";
            this.Load += new System.EventHandler(this.ucPosBar_Load);
            this.PaddingChanged += new System.EventHandler(this.ucPosBar_PaddingChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucPosBar_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ucPosBar_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
