namespace MediaChrome
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
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new MediaChrome.ExPanel();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.listViewX1 = new MediaChrome.XListView();
            this.panel4 = new MediaChrome.ExPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ucPosBar1 = new webclassprototype.ucPosBar();
            this.cBtn2 = new MediaChrome.cBtn();
            this.cBtn1 = new MediaChrome.cBtn();
            this.cBtn8 = new MediaChrome.cBtn();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new MediaChrome.ExPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pane5 = new MediaChrome.ExPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new MediaChrome.ExPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cBtn4 = new MediaChrome.cBtn();
            this.cBtn5 = new MediaChrome.cBtn();
            this.cBtn3 = new MediaChrome.cBtn();
            this.textBox1 = new MediaChrome.ucSearch();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.panel4.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pane5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 110);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.playToolStripMenuItem.Text = "Play / Browse";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // copySpotifyURIToolStripMenuItem
            // 
            this.copySpotifyURIToolStripMenuItem.Name = "copySpotifyURIToolStripMenuItem";
            this.copySpotifyURIToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copySpotifyURIToolStripMenuItem.Text = "Copy Spotify URI";
            // 
            // copyHTTPLinkToolStripMenuItem
            // 
            this.copyHTTPLinkToolStripMenuItem.Name = "copyHTTPLinkToolStripMenuItem";
            this.copyHTTPLinkToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copyHTTPLinkToolStripMenuItem.Text = "Copy HTTP Link";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 6);
            // 
            // shareToolStripMenuItem
            // 
            this.shareToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.facebookToolStripMenuItem,
            this.twitterToolStripMenuItem});
            this.shareToolStripMenuItem.Name = "shareToolStripMenuItem";
            this.shareToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.shareToolStripMenuItem.Text = "Share";
            // 
            // facebookToolStripMenuItem
            // 
            this.facebookToolStripMenuItem.Name = "facebookToolStripMenuItem";
            this.facebookToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.facebookToolStripMenuItem.Text = "Facebook";
            // 
            // twitterToolStripMenuItem
            // 
            this.twitterToolStripMenuItem.Name = "twitterToolStripMenuItem";
            this.twitterToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.twitterToolStripMenuItem.Text = "Twitter";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(154, 6);
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
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick_1);
            // 
            // timer4
            // 
            this.timer4.Enabled = true;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick_1);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(346, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_3);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.axWindowsMediaPlayer1);
            this.panel3.Controls.Add(this.listViewX1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 86);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1076, 261);
            this.panel3.TabIndex = 24;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint_1);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(589, 113);
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
            this.listViewX1.Size = new System.Drawing.Size(211, 261);
            this.listViewX1.TabIndex = 22;
            this.listViewX1.UseCompatibleStateImageBehavior = false;
            this.listViewX1.View = System.Windows.Forms.View.Details;
            this.listViewX1.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.BackgroundImage = global::MediaChrome.Properties.Resources.rbottom1;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.ucPosBar1);
            this.panel4.Controls.Add(this.cBtn2);
            this.panel4.Controls.Add(this.cBtn1);
            this.panel4.Controls.Add(this.cBtn8);
            this.panel4.Controls.Add(this.menuStrip2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(2, 347);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1076, 53);
            this.panel4.TabIndex = 23;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.panel8.Location = new System.Drawing.Point(1033, 10);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(43, 43);
            this.panel8.TabIndex = 23;
            this.panel8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel8_MouseDown);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.panel6.Location = new System.Drawing.Point(10, 41);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1027, 12);
            this.panel6.TabIndex = 22;
            this.panel6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel6_MouseDown);
            // 
            // ucPosBar1
            // 
            this.ucPosBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPosBar1.BackColor = System.Drawing.Color.Transparent;
            this.ucPosBar1.BorderColor = System.Drawing.Color.Black;
            this.ucPosBar1.FillColor = System.Drawing.Color.White;
            this.ucPosBar1.Location = new System.Drawing.Point(141, 22);
            this.ucPosBar1.Maximum = 100F;
            this.ucPosBar1.Name = "ucPosBar1";
            this.ucPosBar1.Size = new System.Drawing.Size(739, 13);
            this.ucPosBar1.TabIndex = 14;
            this.ucPosBar1.Value = 0F;
            // 
            // cBtn2
            // 
            this.cBtn2.BackColor = System.Drawing.Color.Transparent;
            this.cBtn2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn2.BackgroundImage")));
            this.cBtn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn2.Img = global::MediaChrome.Properties.Resources.back;
            this.cBtn2.Location = new System.Drawing.Point(20, 10);
            this.cBtn2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn2.Name = "cBtn2";
            this.cBtn2.Size = new System.Drawing.Size(32, 32);
            this.cBtn2.TabIndex = 10;
            this.cBtn2.Load += new System.EventHandler(this.cBtn8_Load_1);
            this.cBtn2.Click += new System.EventHandler(this.cBtn2_Click_1);
            this.cBtn2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cBtn2_MouseClick);
            // 
            // cBtn1
            // 
            this.cBtn1.BackColor = System.Drawing.Color.Transparent;
            this.cBtn1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn1.BackgroundImage")));
            this.cBtn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn1.Img = global::MediaChrome.Properties.Resources.forward;
            this.cBtn1.Location = new System.Drawing.Point(92, 10);
            this.cBtn1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn1.Name = "cBtn1";
            this.cBtn1.Size = new System.Drawing.Size(32, 32);
            this.cBtn1.TabIndex = 10;
            this.cBtn1.Load += new System.EventHandler(this.cBtn8_Load_1);
            this.cBtn1.Click += new System.EventHandler(this.cBtn1_Click_1);
            this.cBtn1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cBtn1_MouseClick);
            // 
            // cBtn8
            // 
            this.cBtn8.BackColor = System.Drawing.Color.Transparent;
            this.cBtn8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn8.BackgroundImage")));
            this.cBtn8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn8.Img = global::MediaChrome.Properties.Resources.play;
            this.cBtn8.Location = new System.Drawing.Point(56, 10);
            this.cBtn8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn8.Name = "cBtn8";
            this.cBtn8.Size = new System.Drawing.Size(32, 32);
            this.cBtn8.TabIndex = 10;
            this.cBtn8.Load += new System.EventHandler(this.cBtn8_Load_1);
            this.cBtn8.Click += new System.EventHandler(this.cBtn8_Click_2);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(898, 24);
            this.menuStrip2.TabIndex = 13;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.Visible = false;
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(38, 20);
            this.toolStripMenuItem4.Text = "test";
            this.toolStripMenuItem4.Visible = false;
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.toolStripMenuItem5.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem5.Text = "Filter";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 314);
            this.panel1.TabIndex = 20;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this.panel7.Location = new System.Drawing.Point(0, 302);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(13, 12);
            this.panel7.TabIndex = 22;
            this.panel7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseDown);
            // 
            // pane5
            // 
            this.pane5.BackColor = System.Drawing.Color.LightGray;
            this.pane5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pane5.Controls.Add(this.label5);
            this.pane5.Controls.Add(this.label2);
            this.pane5.Dock = System.Windows.Forms.DockStyle.Top;
            this.pane5.Location = new System.Drawing.Point(0, 53);
            this.pane5.Name = "pane5";
            this.pane5.Size = new System.Drawing.Size(1078, 33);
            this.pane5.TabIndex = 14;
            this.pane5.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(1045, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 30);
            this.label5.TabIndex = 0;
            this.label5.Text = "X";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Error";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.BackgroundImage = global::MediaChrome.Properties.Resources.top_wall;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.cBtn4);
            this.panel2.Controls.Add(this.cBtn5);
            this.panel2.Controls.Add(this.cBtn3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.menuStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1078, 53);
            this.panel2.TabIndex = 12;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.DoubleClick += new System.EventHandler(this.panel2_DoubleClick);
            this.panel2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.panel2.MouseEnter += new System.EventHandler(this.panel2_MouseEnter);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.panel5.Location = new System.Drawing.Point(9, -4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1060, 16);
            this.panel5.TabIndex = 22;
            this.panel5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel5_MouseDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(469, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Mediachrome";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::MediaChrome.Properties.Resources.kan21;
            this.pictureBox3.Location = new System.Drawing.Point(1068, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(10, 10);
            this.pictureBox3.TabIndex = 21;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::MediaChrome.Properties.Resources.kan11;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(10, 10);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::MediaChrome.Properties.Resources.icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(9, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 39);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // cBtn4
            // 
            this.cBtn4.BackColor = System.Drawing.Color.Transparent;
            this.cBtn4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn4.BackgroundImage")));
            this.cBtn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn4.Img = global::MediaChrome.Properties.Resources.forward;
            this.cBtn4.Location = new System.Drawing.Point(88, 14);
            this.cBtn4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn4.Name = "cBtn4";
            this.cBtn4.Size = new System.Drawing.Size(28, 28);
            this.cBtn4.TabIndex = 11;
            this.cBtn4.Load += new System.EventHandler(this.cBtn4_Load);
            this.cBtn4.Click += new System.EventHandler(this.CBtn4Click);
            // 
            // cBtn5
            // 
            this.cBtn5.AllowDrop = true;
            this.cBtn5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cBtn5.BackColor = System.Drawing.Color.Transparent;
            this.cBtn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn5.BackgroundImage")));
            this.cBtn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn5.Img = null;
            this.cBtn5.Location = new System.Drawing.Point(304, 14);
            this.cBtn5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn5.Name = "cBtn5";
            this.cBtn5.Size = new System.Drawing.Size(27, 28);
            this.cBtn5.TabIndex = 10;
            this.cBtn5.Load += new System.EventHandler(this.cBtn3_Load);
            this.cBtn5.Click += new System.EventHandler(this.CBtn3Click);
            this.cBtn5.DragDrop += new System.Windows.Forms.DragEventHandler(this.cBtn5_DragDrop);
            this.cBtn5.DragOver += new System.Windows.Forms.DragEventHandler(this.cBtn5_DragOver);
            // 
            // cBtn3
            // 
            this.cBtn3.BackColor = System.Drawing.Color.Transparent;
            this.cBtn3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn3.BackgroundImage")));
            this.cBtn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn3.Img = global::MediaChrome.Properties.Resources.back;
            this.cBtn3.Location = new System.Drawing.Point(58, 14);
            this.cBtn3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn3.Name = "cBtn3";
            this.cBtn3.Size = new System.Drawing.Size(26, 28);
            this.cBtn3.TabIndex = 10;
            this.cBtn3.Load += new System.EventHandler(this.cBtn3_Load);
            this.cBtn3.Click += new System.EventHandler(this.CBtn3Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Transparent;
            this.textBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox1.BackgroundImage")));
            this.textBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.textBox1.Location = new System.Drawing.Point(120, 12);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 33);
            this.textBox1.TabIndex = 9;
            this.textBox1.SearchClicked += new System.EventHandler(this.textBox1_SearchClicked);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
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
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 400);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
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
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.Enter += new System.EventHandler(this.Form1Enter);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pane5.ResumeLayout(false);
            this.pane5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private cBtn cBtn4;
        private ExPanel panel2;
        private System.Windows.Forms.Label label2;
        private ExPanel pane5;
        private ExPanel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private ExPanel panel4;
        private cBtn cBtn8;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private ExPanel panel3;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private cBtn cBtn2;
        private cBtn cBtn1;
        private System.Windows.Forms.Label label3;
        private webclassprototype.ucPosBar ucPosBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private XListView listViewX1;
        private cBtn cBtn5;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;

    }
}