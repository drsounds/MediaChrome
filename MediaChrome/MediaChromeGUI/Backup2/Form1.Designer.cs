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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Home");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Playlists");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Shortcuts");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("History");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
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
            this.pane5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listViewX1 = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.geckoWebBrowser1 = new Skybound.Gecko.GeckoWebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView2 = new XListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pane3 = new SpofityRuntime.Pane();
            this.pane7 = new SpofityRuntime.Pane();
            this.lArtist = new System.Windows.Forms.Label();
            this.lAlbum = new System.Windows.Forms.Label();
            this.pane4 = new SpofityRuntime.Pane();
            this.label1 = new System.Windows.Forms.Label();
            this.cBtn5 = new SpofityRuntime.cBtn();
            this.cBtn4 = new SpofityRuntime.cBtn();
            this.cBtn3 = new SpofityRuntime.cBtn();
            this.textBox1 = new SpofityRuntime.ucSearch();
            this.pane2 = new SpofityRuntime.Pane();
            this.pane6 = new SpofityRuntime.Pane();
            this.pane9 = new SpofityRuntime.Pane();
            this.pane8 = new SpofityRuntime.Pane();
            this.ucPosBar2 = new webclassprototype.ucPosBar();
            this.ucPosBar1 = new webclassprototype.ucPosBar();
            this.cBtn6 = new SpofityRuntime.cBtn();
            this.cBtn2 = new SpofityRuntime.cBtn();
            this.cBtn1 = new SpofityRuntime.cBtn();
            this.contextMenuStrip1.SuspendLayout();
            this.pane5.SuspendLayout();
    //        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
    //        ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
    //        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
    //        ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pane7.SuspendLayout();
            this.pane4.SuspendLayout();
            this.pane2.SuspendLayout();
            this.pane6.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.Timer2Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Enabled = true;
            this.timer4.Interval = 1050;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
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
            // pane5
            // 
            this.pane5.BackColor = System.Drawing.Color.LightGray;
            this.pane5.Controls.Add(this.label2);
            this.pane5.Dock = System.Windows.Forms.DockStyle.Top;
            this.pane5.Location = new System.Drawing.Point(0, 47);
            this.pane5.Name = "pane5";
            this.pane5.Size = new System.Drawing.Size(898, 33);
            this.pane5.TabIndex = 14;
             this.pane5.BackgroundImage = (System.Drawing.Image)resources.GetObject("top_bar_blue");
             this.pane5.Visible=false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Error";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 80);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(898, 448);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 15;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listViewX1);
            this.splitContainer2.Panel1.Controls.Add(this.pane3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer2.Panel2.Controls.Add(this.pane7);
            this.splitContainer2.Size = new System.Drawing.Size(220, 448);
            this.splitContainer2.SplitterDistance = 193;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 9;
            // 
            // listViewX1
            // 
            this.listViewX1.AllowDrop = true;
            this.listViewX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listViewX1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listViewX1.Location = new System.Drawing.Point(0, 24);
            this.listViewX1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listViewX1.Name = "listViewX1";
            treeNode5.Name = "Nod0";
            treeNode5.Text = "Home";
            treeNode6.Name = "Playlists";
            treeNode6.Text = "Playlists";
            treeNode7.Name = "Custom";
            treeNode7.Text = "Shortcuts";
            treeNode8.Name = "History";
            treeNode8.Text = "History";
            this.listViewX1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.listViewX1.Size = new System.Drawing.Size(220, 169);
            this.listViewX1.TabIndex = 15;
     //       this.listViewX1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
            this.listViewX1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewX1_KeyUp);
       //     this.listViewX1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListViewX1MouseUp);
       this.listViewX1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ListViewX1_AfterSelect);
       	this.listView2.ItemDropped += new XListView.ItemDrop(Form1_ItemDropped);
            this.listView2.DoubleClick+= new System.EventHandler(LVDB);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 207);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(220, 448);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.Color.Black;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer3.Name = "splitContainer3";
            
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.geckoWebBrowser1);
             this.splitContainer3.Panel1.Controls.Add(this.listView2);
            this.listView2.Visible=false;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panel1);
            this.splitContainer3.Panel2Collapsed =false;
            this.splitContainer3.Size = new System.Drawing.Size(677, 448);
            this.splitContainer3.SplitterDistance = 469;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            // 
            // geckoWebBrowser1
            // 
            this.geckoWebBrowser1.AllowDrop = true;
            this.geckoWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geckoWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.geckoWebBrowser1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.geckoWebBrowser1.Name = "geckoWebBrowser1";
            this.geckoWebBrowser1.Size = new System.Drawing.Size(677, 448);
            this.geckoWebBrowser1.TabIndex = 4;
            this.geckoWebBrowser1.Navigating += new Skybound.Gecko.GeckoNavigatingEventHandler(this.geckoWebBrowser1_Navigating);
            // 
            // panel1
            // 
           this.panel1.Controls.Add(this.pane4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(96, 100);
            this.panel1.BackgroundImage = (System.Drawing.Image)resources.GetObject("top_wall");
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            
            this.panel1.TabIndex = 4;
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.Location = new System.Drawing.Point(0, 25);
            this.listView2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(96, 75);
            this.listView2.TabIndex = 10;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.cBtn5);
            this.panel2.Controls.Add(this.cBtn4);
            this.panel2.Controls.Add(this.cBtn3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(898, 47);
            this.panel2.TabIndex = 12;
         
            this.panel2.BackgroundImage = (System.Drawing.Image)resources.GetObject("top_wall");
            
            // 
            // pane3
            // 
            this.pane3.BackColor = System.Drawing.Color.DarkGray;
            this.pane3.BackgroundImage = global::SpofityRuntime.Properties.Resources.top_toolbar;
            this.pane3.Dark = false;
            this.pane3.Dock = System.Windows.Forms.DockStyle.Top;
            this.pane3.Location = new System.Drawing.Point(0, 0);
            this.pane3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pane3.Name = "pane3";
            this.pane3.SecondColor = System.Drawing.SystemColors.ControlDark;
            this.pane3.Size = new System.Drawing.Size(220, 24);
            this.pane3.TabIndex = 14;
            this.pane3.BackgroundImage = (System.Drawing.Image)resources.GetObject("top_bar_blue");
            this.pane3.Visible=false;
            
            // 
            // pane7
            // 
            this.pane7.BackColor = System.Drawing.Color.Gainsboro;
            this.pane7.Controls.Add(this.lArtist);
            this.pane7.Controls.Add(this.lAlbum);
            this.pane7.Dark = false;
            this.pane7.Dock = System.Windows.Forms.DockStyle.Top;
            this.pane7.Location = new System.Drawing.Point(0, 0);
            this.pane7.Name = "pane7";
            this.pane7.SecondColor = System.Drawing.Color.Gray;
            this.pane7.Size = new System.Drawing.Size(220, 47);
            this.pane7.TabIndex = 0;
            // 
            // lArtist
            // 
            this.lArtist.AutoSize = true;
            this.lArtist.BackColor = System.Drawing.Color.Transparent;
            this.lArtist.Location = new System.Drawing.Point(4, 19);
            this.lArtist.Name = "lArtist";
            this.lArtist.Size = new System.Drawing.Size(35, 13);
            this.lArtist.TabIndex = 1;
            this.lArtist.Text = "label4";
            // 
            // lAlbum
            // 
            this.lAlbum.AutoSize = true;
            this.lAlbum.BackColor = System.Drawing.Color.Transparent;
            this.lAlbum.Location = new System.Drawing.Point(4, 6);
            this.lAlbum.Name = "lAlbum";
            this.lAlbum.Size = new System.Drawing.Size(35, 13);
            this.lAlbum.TabIndex = 0;
            this.lAlbum.Text = "label3";
            // 
            // pane4
            // 
            this.pane4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.pane4.Controls.Add(this.label1);
            this.pane4.Dark = false;
            this.pane4.Dock = System.Windows.Forms.DockStyle.Top;
            this.pane4.Location = new System.Drawing.Point(0, 0);
            this.pane4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pane4.Name = "pane4";
            this.pane4.SecondColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.pane4.Size = new System.Drawing.Size(96, 25);
            this.pane4.TabIndex = 9;
           
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(13, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Playlist";
            // 
            // cBtn5
            // 
            this.cBtn5.BackColor = System.Drawing.Color.Transparent;
            this.cBtn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn5.BackgroundImage")));
            this.cBtn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn5.Img = global::SpofityRuntime.Properties.Resources.update;
            this.cBtn5.Location = new System.Drawing.Point(70, 8);
            this.cBtn5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn5.Name = "cBtn5";
            this.cBtn5.Size = new System.Drawing.Size(23, 22);
            this.cBtn5.TabIndex = 12;
            this.cBtn5.Click += new System.EventHandler(this.CBtn5Click);
            // 
            // cBtn4
            // 
            this.cBtn4.BackColor = System.Drawing.Color.Transparent;
            this.cBtn4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn4.BackgroundImage")));
            this.cBtn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn4.Img = global::SpofityRuntime.Properties.Resources.forward;
            this.cBtn4.Location = new System.Drawing.Point(39, 8);
            this.cBtn4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn4.Name = "cBtn4";
            this.cBtn4.Size = new System.Drawing.Size(25, 22);
            this.cBtn4.TabIndex = 11;
            this.cBtn4.Click += new System.EventHandler(this.CBtn4Click);
            // 
            // cBtn3
            // 
           
            this.cBtn3.BackColor = System.Drawing.Color.Transparent;
            this.cBtn3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn3.BackgroundImage")));
            this.cBtn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn3.Img = global::SpofityRuntime.Properties.Resources.back;
            this.cBtn3.Location = new System.Drawing.Point(14, 8);
            this.cBtn3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn3.Name = "cBtn3";
            this.cBtn3.Size = new System.Drawing.Size(23, 22);
            this.cBtn3.TabIndex = 10;
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
            // pane2
            // 
            this.pane2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pane2.Controls.Add(this.pane6);
            this.pane2.Dark = true;
            this.pane2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pane2.Location = new System.Drawing.Point(0, 528);
            this.pane2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pane2.Name = "pane2";
            this.pane2.SecondColor = System.Drawing.Color.Silver;
            this.pane3.BackgroundImage = (System.Drawing.Image)resources.GetObject("rbottom");
            this.pane2.Size = new System.Drawing.Size(898, 60);
            this.pane2.TabIndex = 8;
            // 
            // pane6
            // 
            this.pane6.BackColor = System.Drawing.Color.Gray;
            this.pane6.Controls.Add(this.pane9);
            this.pane6.Controls.Add(this.pane8);
            this.pane6.Controls.Add(this.ucPosBar2);
            this.pane6.Controls.Add(this.ucPosBar1);
            this.pane6.Controls.Add(this.cBtn6);
            this.pane6.Controls.Add(this.cBtn2);
            this.pane6.Controls.Add(this.cBtn1);
            this.pane6.BackgroundImage = (System.Drawing.Image)resources.GetObject("rbottom");
            this.pane6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pane6.Height = 42;
            this.pane6.Dark = false;
            this.pane6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pane6.ForeColor = System.Drawing.Color.Black;
            this.pane6.Location = new System.Drawing.Point(0, 0);
            this.pane6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pane6.Name = "pane6";
            this.pane6.SecondColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pane6.Size = new System.Drawing.Size(898, 60);
            this.pane6.TabIndex = 12;
            this.pane6.Paint += new System.Windows.Forms.PaintEventHandler(this.pane6_Paint_1);
            // 
            // pane9
            // 
            this.pane9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pane9.BackColor = System.Drawing.Color.Black;
            this.pane9.Dark = false;
            this.pane9.Location = new System.Drawing.Point(789, 0);
            this.pane9.Name = "pane9";
            this.pane9.SecondColor = System.Drawing.Color.Empty;
            this.pane9.Size = new System.Drawing.Size(1, 63);
            this.pane9.TabIndex = 8;
            // 
            // pane8
            // 
            this.pane8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pane8.BackColor = System.Drawing.Color.Black;
            this.pane8.Dark = false;
            this.pane8.Location = new System.Drawing.Point(191, -2);
            this.pane8.Name = "pane8";
            this.pane8.SecondColor = System.Drawing.Color.Empty;
            this.pane8.Size = new System.Drawing.Size(1, 63);
            this.pane8.TabIndex = 4;
            // 
            // ucPosBar2
            // 
            this.ucPosBar2.BackColor = System.Drawing.Color.Black;
            this.ucPosBar2.BorderColor = System.Drawing.Color.Black;
            this.ucPosBar2.FillColor = System.Drawing.Color.White;
            this.ucPosBar2.ForeColor = System.Drawing.Color.LightGray;
            this.ucPosBar2.Location = new System.Drawing.Point(111, 23);
            this.ucPosBar2.Maximum = 120F;
            this.ucPosBar2.Name = "ucPosBar2";
            this.ucPosBar2.Size = new System.Drawing.Size(60, 10);
            this.ucPosBar2.TabIndex = 3;
            this.ucPosBar2.Value = 80F;
            // 
            // ucPosBar1
            // 
            this.ucPosBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPosBar1.BackColor = System.Drawing.Color.Black;
            this.ucPosBar1.BorderColor = System.Drawing.Color.Black;
            this.ucPosBar1.FillColor = System.Drawing.Color.White;
            this.ucPosBar1.ForeColor = System.Drawing.Color.LightGray;
            this.ucPosBar1.Location = new System.Drawing.Point(206, 23);
            this.ucPosBar1.Maximum = 100F;
            this.ucPosBar1.Name = "ucPosBar1";
            this.ucPosBar1.Size = new System.Drawing.Size(577, 10);
            this.ucPosBar1.TabIndex = 3;
            this.ucPosBar1.Value = 25F;
            this.ucPosBar1.Visible = false;
            this.ucPosBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ucPosBar1_MouseUp);
            this.ucPosBar1.Move += new System.EventHandler(this.ucPosBar1_Move);
            // 
            // cBtn6
            // 
            this.cBtn6.BackColor = System.Drawing.Color.Transparent;
            this.cBtn6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn6.BackgroundImage")));
            this.cBtn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn6.Img = global::SpofityRuntime.Properties.Resources.stop;
            this.cBtn6.Location = new System.Drawing.Point(9, 16);
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
            this.cBtn2.Location = new System.Drawing.Point(71, 16);
            this.cBtn2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn2.Name = "cBtn2";
            this.cBtn2.Size = new System.Drawing.Size(23, 24);
            this.cBtn2.TabIndex = 1;
            this.cBtn2.Load += new System.EventHandler(this.cBtn2_Load);
            this.cBtn2.Click += new System.EventHandler(this.cBtn2_Click);
            // 
            // cBtn1
            // 
            this.cBtn1.BackColor = System.Drawing.Color.Transparent;
            this.cBtn1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn1.BackgroundImage")));
            this.cBtn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cBtn1.Img = global::SpofityRuntime.Properties.Resources.play;
            this.cBtn1.Location = new System.Drawing.Point(36, 14);
            this.cBtn1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cBtn1.Name = "cBtn1";
            this.cBtn1.Size = new System.Drawing.Size(31, 28);
            this.cBtn1.TabIndex = 1;
            this.cBtn1.Load += new System.EventHandler(this.CBtn1Load);
            this.cBtn1.Click += new System.EventHandler(this.cBtn1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 588);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pane5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pane2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spotify Ultra";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Activated += new System.EventHandler(this.Form1Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Enter += new System.EventHandler(this.Form1Enter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pane5.ResumeLayout(false);
            this.pane5.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
          //  ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
        //    ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
        //    ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pane7.ResumeLayout(false);
            this.pane7.PerformLayout();
            this.pane4.ResumeLayout(false);
            this.pane2.ResumeLayout(false);
            this.pane6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      
        private System.Windows.Forms.Timer timer2;
        private SpofityRuntime.cBtn cBtn2;
        private SpofityRuntime.cBtn cBtn1;
        private SpofityRuntime.Pane pane6;

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Timer timer1;
        private Pane pane2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer4;
        private webclassprototype.ucPosBar ucPosBar1;
        private webclassprototype.ucPosBar ucPosBar2;
        private cBtn cBtn6;
        private Pane pane9;
        private Pane pane8;
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
        private System.Windows.Forms.Panel pane5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView listViewX1;
        private Pane pane3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Pane pane7;
        private System.Windows.Forms.Label lArtist;
        private System.Windows.Forms.Label lAlbum;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Skybound.Gecko.GeckoWebBrowser geckoWebBrowser1;
        private System.Windows.Forms.Panel panel1;
        private XListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private Pane pane4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ucSearch textBox1;
        private cBtn cBtn3;
        private cBtn cBtn4;
        private cBtn cBtn5;
        private System.Windows.Forms.Panel panel2;

    }
}