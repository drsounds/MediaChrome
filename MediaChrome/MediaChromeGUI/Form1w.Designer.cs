namespace SpofityRuntime
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Home");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("-");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.geckoWebBrowser1 = new Skybound.Gecko.GeckoWebBrowser();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listViewX1 = new SpofityRuntime.ListViewX();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pane3 = new SpofityRuntime.Pane();
            this.pane2 = new SpofityRuntime.Pane();
            this.pane1 = new SpofityRuntime.Pane();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewX1);
            this.splitContainer1.Panel1.Controls.Add(this.pane3);
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer1.Panel2.Controls.Add(this.geckoWebBrowser1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1026, 374);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 9;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(10, 10);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(220, 374);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // geckoWebBrowser1
            // 
            this.geckoWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geckoWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.geckoWebBrowser1.Name = "geckoWebBrowser1";
            this.geckoWebBrowser1.NoDefaultContextMenu = true;
            this.geckoWebBrowser1.Size = new System.Drawing.Size(805, 374);
            this.geckoWebBrowser1.TabIndex = 0;
            this.geckoWebBrowser1.DocumentTitleChanged += new System.EventHandler(this.geckoWebBrowser1_DocumentTitleChanged);
            this.geckoWebBrowser1.Navigating += new Skybound.Gecko.GeckoNavigatingEventHandler(this.geckoWebBrowser1_Navigating);
            this.geckoWebBrowser1.DomClick += new Skybound.Gecko.GeckoDomEventHandler(this.geckoWebBrowser1_DomClick);
            this.geckoWebBrowser1.Click += new System.EventHandler(this.geckoWebBrowser1_Click);
            this.geckoWebBrowser1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.geckoWebBrowser1_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listViewX1
            // 
            this.listViewX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listViewX1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewX1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewX1.ForeColor = System.Drawing.Color.White;
            this.listViewX1.FullRowSelect = true;
            this.listViewX1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.Tag = "spotify:view:Standard";
            this.listViewX1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewX1.Location = new System.Drawing.Point(0, 25);
            this.listViewX1.MultiSelect = false;
            this.listViewX1.Name = "listViewX1";
            this.listViewX1.OwnerDraw = true;
            this.listViewX1.Size = new System.Drawing.Size(220, 349);
            this.listViewX1.SmallImageList = this.imageList1;
            this.listViewX1.TabIndex = 9;
            this.listViewX1.UseCompatibleStateImageBehavior = false;
            this.listViewX1.View = System.Windows.Forms.View.Details;
            this.listViewX1.SelectedIndexChanged += new System.EventHandler(this.listViewX1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 220;
            // 
            // pane3
            // 
            this.pane3.Dark = false;
            this.pane3.Dock = System.Windows.Forms.DockStyle.Top;
            this.pane3.Location = new System.Drawing.Point(0, 0);
            this.pane3.Name = "pane3";
            this.pane3.Size = new System.Drawing.Size(220, 25);
            this.pane3.TabIndex = 8;
            // 
            // pane2
            // 
            this.pane2.Dark = true;
            this.pane2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pane2.Location = new System.Drawing.Point(0, 423);
            this.pane2.Name = "pane2";
            this.pane2.Size = new System.Drawing.Size(1026, 71);
            this.pane2.TabIndex = 8;
            // 
            // pane1
            // 
            this.pane1.Dark = false;
            this.pane1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pane1.Location = new System.Drawing.Point(0, 0);
            this.pane1.Name = "pane1";
            this.pane1.Size = new System.Drawing.Size(1026, 49);
            this.pane1.TabIndex = 5;
            this.pane1.Paint += new System.Windows.Forms.PaintEventHandler(this.pane1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1026, 494);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pane2);
            this.Controls.Add(this.pane1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private Skybound.Gecko.GeckoWebBrowser geckoWebBrowser1;
        private Pane pane1;
        private ListViewX listViewX1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private Pane pane3;
        private System.Windows.Forms.ImageList imageList1;
        private Pane pane2;
        private System.Windows.Forms.Timer timer1;

    }
}