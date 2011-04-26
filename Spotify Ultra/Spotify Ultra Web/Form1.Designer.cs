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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copySpotifyURIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHTTPLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.shareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facebookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.listViewX1 = new SpofityRuntime.XListView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pane5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.ucSearch1 = new SpofityRuntime.ucSearch();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ucSearch2 = new SpofityRuntime.ucSearch();
            this.lAlbum = new System.Windows.Forms.Label();
            this.lArtist = new System.Windows.Forms.Label();
            this.cBtn7 = new SpofityRuntime.cBtn();
            this.cBtn5 = new SpofityRuntime.cBtn();
            this.ucPosBar2 = new webclassprototype.ucPosBar();
            this.cBtn8 = new SpofityRuntime.cBtn();
            this.cBtn4 = new SpofityRuntime.cBtn();
            this.cBtn6 = new SpofityRuntime.cBtn();
            this.cBtn2 = new SpofityRuntime.cBtn();
            this.ucPosBar1 = new webclassprototype.ucPosBar();
            this.cBtn1 = new SpofityRuntime.cBtn();
            this.cBtn3 = new SpofityRuntime.cBtn();
            this.textBox1 = new SpofityRuntime.ucSearch();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.pane5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copySpotifyURIToolStripMenuItem,
            this.copyHTTPLinkToolStripMenuItem,
            this.toolStripMenuItem2,
            this.shareToolStripMenuItem,
            this.toolStripMenuItem3});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(164, 110);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.playToolStripMenuItem.Text = "Play / Browse";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 6);
            // 
            // copySpotifyURIToolStripMenuItem
            // 
            this.copySpotifyURIToolStripMenuItem.Name = "copySpotifyURIToolStripMenuItem";
            this.copySpotifyURIToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.copySpotifyURIToolStripMenuItem.Text = "Copy Spotify URI";
            // 
            // copyHTTPLinkToolStripMenuItem
            // 
            this.copyHTTPLinkToolStripMenuItem.Name = "copyHTTPLinkToolStripMenuItem";
            this.copyHTTPLinkToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.copyHTTPLinkToolStripMenuItem.Text = "Copy HTTP Link";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 6);
            // 
            // shareToolStripMenuItem
            // 
            this.shareToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.facebookToolStripMenuItem,
            this.twitterToolStripMenuItem});
            this.shareToolStripMenuItem.Name = "shareToolStripMenuItem";
            this.shareToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.shareToolStripMenuItem.Text = "Share";
            // 
            // facebookToolStripMenuItem
            // 
            this.facebookToolStripMenuItem.Name = "facebookToolStripMenuItem";
            this.facebookToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.facebookToolStripMenuItem.Text = "Facebook";
            // 
            // twitterToolStripMenuItem
            // 
            this.twitterToolStripMenuItem.Name = "twitterToolStripMenuItem";
            this.twitterToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.twitterToolStripMenuItem.Text = "Twitter";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(160, 6);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Title";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Artist";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Album";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 314);
            this.panel1.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.axWindowsMediaPlayer1);
            this.panel3.Controls.Add(this.listViewX1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 86);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1062, 314);
            this.panel3.TabIndex = 22;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(413, 143);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(380, 111);
            this.axWindowsMediaPlayer1.TabIndex = 23;
            this.axWindowsMediaPlayer1.Visible = false;
            // 
            // listViewX1
            // 
            this.listViewX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listViewX1.CanDrag = false;
            this.listViewX1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.listViewX1.Location = new System.Drawing.Point(0, 0);
            this.listViewX1.Name = "listViewX1";
            this.listViewX1.Size = new System.Drawing.Size(211, 314);
            this.listViewX1.TabIndex = 22;
            this.listViewX1.UseCompatibleStateImageBehavior = false;
            this.listViewX1.View = System.Windows.Forms.View.Details;
            this.listViewX1.Visible = false;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(81, 26);
            // 
            // eToolStripMenuItem
            // 
            this.eToolStripMenuItem.Name = "eToolStripMenuItem";
            this.eToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.eToolStripMenuItem.Text = "e";
            this.eToolStripMenuItem.Click += new System.EventHandler(this.eToolStripMenuItem_Click);
            // 
            // pane5
            // 
            this.pane5.BackColor = System.Drawing.Color.LightGray;
            this.pane5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pane5.BackgroundImage")));
            this.pane5.Controls.Add(this.label2);
            this.pane5.Controls.Add(this.ucSearch1);
            this.pane5.Dock = System.Windows.Forms.DockStyle.Top;
            this.pane5.Location = new System.Drawing.Point(0, 53);
            this.pane5.Name = "pane5";
            this.pane5.Size = new System.Drawing.Size(1064, 33);
            this.pane5.TabIndex = 14;
            this.pane5.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Error";
            // 
            // ucSearch1
            // 
            this.ucSearch1.BackColor = System.Drawing.Color.Transparent;
            this.ucSearch1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucSearch1.BackgroundImage")));
            this.ucSearch1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ucSearch1.Location = new System.Drawing.Point(722, 0);
            this.ucSearch1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ucSearch1.Name = "ucSearch1";
            this.ucSearch1.Size = new System.Drawing.Size(180, 33);
            this.ucSearch1.TabIndex = 9;
            this.ucSearch1.SearchClicked += new System.EventHandler(this.textBox1_SearchClicked);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.BackgroundImage = global::SpofityRuntime.Properties.Resources.top_wall1;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.ucSearch2);
            this.panel2.Controls.Add(this.lAlbum);
            this.panel2.Controls.Add(this.lArtist);
            this.panel2.Controls.Add(this.cBtn7);
            this.panel2.Controls.Add(this.cBtn5);
            this.panel2.Controls.Add(this.ucPosBar2);
            this.panel2.Controls.Add(this.cBtn8);
            this.panel2.Controls.Add(this.cBtn4);
            this.panel2.Controls.Add(this.cBtn6);
            this.panel2.Controls.Add(this.cBtn2);
            this.panel2.Controls.Add(this.ucPosBar1);
            this.panel2.Controls.Add(this.cBtn1);
            this.panel2.Controls.Add(this.cBtn3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.menuStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1064, 53);
            this.panel2.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(846, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Filter";
            // 
            // ucSearch2
            // 
            this.ucSearch2.BackColor = System.Drawing.Color.Transparent;
            this.ucSearch2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucSearch2.BackgroundImage")));
            this.ucSearch2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ucSearch2.Location = new System.Drawing.Point(882, 0);
            this.ucSearch2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ucSearch2.Name = "ucSearch2";
            this.ucSearch2.Size = new System.Drawing.Size(180, 33);
            this.ucSearch2.TabIndex = 16;
            this.ucSearch2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ucSearch2_KeyUp);
            // 
            // lAlbum
            // 
            this.lAlbum.AutoSize = true;
            this.lAlbum.Location = new System.Drawing.Point(551, 9);
            this.lAlbum.Name = "lAlbum";
            this.lAlbum.Size = new System.Drawing.Size(35, 13);
            this.lAlbum.TabIndex = 14;
            this.lAlbum.Text = "label1";
            // 
            // lArtist
            // 
            this.lArtist.AutoSize = true;
            this.lArtist.Location = new System.Drawing.Point(479, 9);
            this.lArtist.Name = "lArtist";
            this.lArtist.Size = new System.Drawing.Size(35, 13);
            this.lArtist.TabIndex = 14;
            this.lArtist.Text = "label1";
            // 
            // cBtn7
            // 
            this.cBtn7.BackColor = System.Drawing.Color.Transparent;
            this.cBtn7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn7.BackgroundImage")));
            this.cBtn7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn7.Img = global::SpofityRuntime.Properties.Resources.update;
            this.cBtn7.Location = new System.Drawing.Point(283, 8);
            this.cBtn7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn7.Name = "cBtn7";
            this.cBtn7.Size = new System.Drawing.Size(23, 22);
            this.cBtn7.TabIndex = 12;
            this.cBtn7.Click += new System.EventHandler(this.CBtn6Click);
            // 
            // cBtn5
            // 
            this.cBtn5.BackColor = System.Drawing.Color.Transparent;
            this.cBtn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn5.BackgroundImage")));
            this.cBtn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn5.Img = global::SpofityRuntime.Properties.Resources.update;
            this.cBtn5.Location = new System.Drawing.Point(68, 8);
            this.cBtn5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn5.Name = "cBtn5";
            this.cBtn5.Size = new System.Drawing.Size(23, 22);
            this.cBtn5.TabIndex = 12;
            this.cBtn5.Click += new System.EventHandler(this.CBtn5Click);
            // 
            // ucPosBar2
            // 
            this.ucPosBar2.BackColor = System.Drawing.Color.Black;
            this.ucPosBar2.BorderColor = System.Drawing.Color.Black;
            this.ucPosBar2.FillColor = System.Drawing.Color.White;
            this.ucPosBar2.ForeColor = System.Drawing.Color.LightGray;
            this.ucPosBar2.Location = new System.Drawing.Point(454, 25);
            this.ucPosBar2.Maximum = 120F;
            this.ucPosBar2.Name = "ucPosBar2";
            this.ucPosBar2.Size = new System.Drawing.Size(60, 10);
            this.ucPosBar2.TabIndex = 3;
            this.ucPosBar2.Value = 80F;
            // 
            // cBtn8
            // 
            this.cBtn8.BackColor = System.Drawing.Color.Transparent;
            this.cBtn8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn8.BackgroundImage")));
            this.cBtn8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn8.Img = global::SpofityRuntime.Properties.Resources.forward;
            this.cBtn8.Location = new System.Drawing.Point(283, 36);
            this.cBtn8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn8.Name = "cBtn8";
            this.cBtn8.Size = new System.Drawing.Size(23, 22);
            this.cBtn8.TabIndex = 11;
            this.cBtn8.Load += new System.EventHandler(this.cBtn8_Load);
            this.cBtn8.Click += new System.EventHandler(this.cBtn8_Click_1);
            // 
            // cBtn4
            // 
            this.cBtn4.BackColor = System.Drawing.Color.Transparent;
            this.cBtn4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn4.BackgroundImage")));
            this.cBtn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn4.Img = global::SpofityRuntime.Properties.Resources.forward;
            this.cBtn4.Location = new System.Drawing.Point(41, 9);
            this.cBtn4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn4.Name = "cBtn4";
            this.cBtn4.Size = new System.Drawing.Size(23, 22);
            this.cBtn4.TabIndex = 11;
            this.cBtn4.Click += new System.EventHandler(this.CBtn4Click);
            // 
            // cBtn6
            // 
            this.cBtn6.BackColor = System.Drawing.Color.Transparent;
            this.cBtn6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn6.BackgroundImage")));
            this.cBtn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn6.Img = global::SpofityRuntime.Properties.Resources.stop;
            this.cBtn6.Location = new System.Drawing.Point(329, 15);
            this.cBtn6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn6.Name = "cBtn6";
            this.cBtn6.Size = new System.Drawing.Size(23, 24);
            this.cBtn6.TabIndex = 1;
            this.cBtn6.Load += new System.EventHandler(this.cBtn6_Load);
            this.cBtn6.Click += new System.EventHandler(this.cBtn6_Click);
            // 
            // cBtn2
            // 
            this.cBtn2.BackColor = System.Drawing.Color.Transparent;
            this.cBtn2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn2.BackgroundImage")));
            this.cBtn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn2.Img = global::SpofityRuntime.Properties.Resources.stop;
            this.cBtn2.Location = new System.Drawing.Point(391, 15);
            this.cBtn2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn2.Name = "cBtn2";
            this.cBtn2.Size = new System.Drawing.Size(23, 24);
            this.cBtn2.TabIndex = 1;
            this.cBtn2.Load += new System.EventHandler(this.cBtn2_Load);
            this.cBtn2.Click += new System.EventHandler(this.cBtn2_Click);
            // 
            // ucPosBar1
            // 
            this.ucPosBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPosBar1.BackColor = System.Drawing.Color.Black;
            this.ucPosBar1.BorderColor = System.Drawing.Color.Black;
            this.ucPosBar1.FillColor = System.Drawing.Color.White;
            this.ucPosBar1.ForeColor = System.Drawing.Color.LightGray;
            this.ucPosBar1.Location = new System.Drawing.Point(532, 25);
            this.ucPosBar1.Maximum = 100F;
            this.ucPosBar1.Name = "ucPosBar1";
            this.ucPosBar1.Size = new System.Drawing.Size(348, 11);
            this.ucPosBar1.TabIndex = 3;
            this.ucPosBar1.Value = 25F;
            this.ucPosBar1.Visible = false;
            this.ucPosBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ucPosBar1_MouseUp);
            this.ucPosBar1.Move += new System.EventHandler(this.ucPosBar1_Move);
            // 
            // cBtn1
            // 
            this.cBtn1.BackColor = System.Drawing.Color.Transparent;
            this.cBtn1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn1.BackgroundImage")));
            this.cBtn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn1.Img = global::SpofityRuntime.Properties.Resources.play;
            this.cBtn1.Location = new System.Drawing.Point(356, 13);
            this.cBtn1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn1.Name = "cBtn1";
            this.cBtn1.Size = new System.Drawing.Size(31, 28);
            this.cBtn1.TabIndex = 1;
            this.cBtn1.Load += new System.EventHandler(this.CBtn1Load);
            this.cBtn1.Click += new System.EventHandler(this.cBtn1_Click);
            // 
            // cBtn3
            // 
            this.cBtn3.BackColor = System.Drawing.Color.Transparent;
            this.cBtn3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn3.BackgroundImage")));
            this.cBtn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn3.Img = global::SpofityRuntime.Properties.Resources.back;
            this.cBtn3.Location = new System.Drawing.Point(2, 8);
            this.cBtn3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn3.Name = "cBtn3";
            this.cBtn3.Size = new System.Drawing.Size(38, 39);
            this.cBtn3.TabIndex = 10;
            this.cBtn3.Load += new System.EventHandler(this.cBtn3_Load);
            this.cBtn3.Click += new System.EventHandler(this.CBtn3Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Transparent;
            this.textBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox1.BackgroundImage")));
            this.textBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.textBox1.Location = new System.Drawing.Point(99, 3);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 33);
            this.textBox1.TabIndex = 9;
            this.textBox1.SearchClicked += new System.EventHandler(this.textBox1_SearchClicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(898, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.testToolStripMenuItem.Text = "test";
            this.testToolStripMenuItem.Visible = false;
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 400);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pane5);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MediaChrome (Spotify Ultra)";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.Enter += new System.EventHandler(this.Form1Enter);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.pane5.ResumeLayout(false);
            this.pane5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;

        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copySpotifyURIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHTTPLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem shareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facebookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        //private XListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private ucSearch textBox1;
        private cBtn cBtn3;
        private cBtn cBtn1;
        private webclassprototype.ucPosBar ucPosBar1;
        private cBtn cBtn2;
        private cBtn cBtn6;
        private cBtn cBtn4;
        private webclassprototype.ucPosBar ucPosBar2;
        private cBtn cBtn5;
        private cBtn cBtn7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pane5;
        private System.Windows.Forms.Label lAlbum;
        private System.Windows.Forms.Label lArtist;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private XListView listViewX1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItem;
        private ucSearch ucSearch1;
        private System.Windows.Forms.Label label1;
        private ucSearch ucSearch2;
        private cBtn cBtn8;

    }
}