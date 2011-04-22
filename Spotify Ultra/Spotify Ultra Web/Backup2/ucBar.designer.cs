namespace webclassprototype
{
    partial class ucBar
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
            // ucBar
            // 
           
            this.BackColor = System.Drawing.Color.White;
            this.Name = "ucBar";
            this.Size = new System.Drawing.Size(713, 88);

            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucBar_Paint);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
