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
        	System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Home");
        	System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Playlists");
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        	this.imageList1 = new System.Windows.Forms.ImageList(this.components);
        	this.timer1 = new System.Windows.Forms.Timer(this.components);
        	this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        	this.splitContainer2 = new System.Windows.Forms.SplitContainer();
        	this.listViewX1 = new System.Windows.Forms.TreeView();
        	this.pane3 = new SpofityRuntime.Pane();
        	this.panel2 = new System.Windows.Forms.Panel();
        	this.pictureBox1 = new System.Windows.Forms.PictureBox();
        	this.pane7 = new SpofityRuntime.Pane();
        	this.lAlbum = new System.Windows.Forms.LinkLabel();
        	this.lArtist = new System.Windows.Forms.LinkLabel();
        	this.listView1 = new System.Windows.Forms.ListView();
        	this.splitContainer3 = new System.Windows.Forms.SplitContainer();
        	this.geckoWebBrowser1 = new Skybound.Gecko.GeckoWebBrowser();
        	this.panel1 = new System.Windows.Forms.Panel();
        	this.listView2 = new System.Windows.Forms.ListView();
        	this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        	this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        	this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
        	this.pane4 = new SpofityRuntime.Pane();
        	this.label1 = new System.Windows.Forms.Label();
        	this.pane5 = new SpofityRuntime.Pane();
        	this.pane8 = new SpofityRuntime.Pane();
        	this.button1 = new System.Windows.Forms.Button();
        	this.label2 = new System.Windows.Forms.Label();
        	this.pane1 = new SpofityRuntime.Pane();
        	this.cBtn5 = new SpofityRuntime.cBtn();
        	this.cBtn4 = new SpofityRuntime.cBtn();
        	this.cBtn3 = new SpofityRuntime.cBtn();
        	this.textBox1 = new SpofityRuntime.ucSearch();
        	this.pane2 = new SpofityRuntime.Pane();
        	this.pane6 = new SpofityRuntime.Pane();
        	this.cBtn2 = new SpofityRuntime.cBtn();
        	this.cBtn1 = new SpofityRuntime.cBtn();
        	this.timer2 = new System.Windows.Forms.Timer(this.components);
        	this.splitContainer1.Panel1.SuspendLayout();
        	this.splitContainer1.Panel2.SuspendLayout();
        	this.splitContainer1.SuspendLayout();
        	this.splitContainer2.Panel1.SuspendLayout();
        	this.splitContainer2.Panel2.SuspendLayout();
        	this.splitContainer2.SuspendLayout();
        	this.panel2.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.pane7.SuspendLayout();
        	this.splitContainer3.Panel1.SuspendLayout();
        	this.splitContainer3.Panel2.SuspendLayout();
        	this.splitContainer3.SuspendLayout();
        	this.panel1.SuspendLayout();
        	this.pane4.SuspendLayout();
        	this.pane5.SuspendLayout();
        	this.pane8.SuspendLayout();
        	this.pane1.SuspendLayout();
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
        	// splitContainer1
        	// 
        	this.splitContainer1.BackColor = System.Drawing.Color.Black;
        	this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
        	this.splitContainer1.Location = new System.Drawing.Point(0, 93);
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
        	this.splitContainer1.Size = new System.Drawing.Size(904, 435);
        	this.splitContainer1.SplitterDistance = 220;
        	this.splitContainer1.SplitterWidth = 1;
        	this.splitContainer1.TabIndex = 11;
        	this.splitContainer1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SplitContainer1Scroll);
        	// 
        	// splitContainer2
        	// 
        	this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.splitContainer2.Location = new System.Drawing.Point(0, 0);
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
        	this.splitContainer2.Panel2.Controls.Add(this.panel2);
        	this.splitContainer2.Size = new System.Drawing.Size(220, 435);
        	this.splitContainer2.SplitterDistance = 197;
        	this.splitContainer2.SplitterWidth = 1;
        	this.splitContainer2.TabIndex = 9;
        	// 
        	// listViewX1
        	// 
        	this.listViewX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.listViewX1.BorderStyle = System.Windows.Forms.BorderStyle.None;
        	this.listViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.listViewX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        	this.listViewX1.Location = new System.Drawing.Point(0, 25);
        	this.listViewX1.Name = "listViewX1";
        	treeNode1.Name = "Nod0";
        	treeNode1.Text = "Home";
        	treeNode2.Name = "Playlists";
        	treeNode2.Text = "Playlists";
        	this.listViewX1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
        	        	        	treeNode1,
        	        	        	treeNode2});
        	this.listViewX1.Size = new System.Drawing.Size(220, 172);
        	this.listViewX1.TabIndex = 15;
        	this.listViewX1.Layout += new System.Windows.Forms.LayoutEventHandler(this.ListViewX1Layout);
        	this.listViewX1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListViewX1MouseUp);
        	this.listViewX1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
        	// 
        	// pane3
        	// 
        	this.pane3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
        	this.pane3.Dark = false;
        	this.pane3.Dock = System.Windows.Forms.DockStyle.Top;
        	this.pane3.Location = new System.Drawing.Point(0, 0);
        	this.pane3.Name = "pane3";
        	this.pane3.SecondColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
        	this.pane3.Size = new System.Drawing.Size(220, 25);
        	this.pane3.TabIndex = 14;
        	// 
        	// panel2
        	// 
        	this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.panel2.Controls.Add(this.pictureBox1);
        	this.panel2.Controls.Add(this.pane7);
        	this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.panel2.Location = new System.Drawing.Point(0, 0);
        	this.panel2.Name = "panel2";
        	this.panel2.Size = new System.Drawing.Size(220, 237);
        	this.panel2.TabIndex = 11;
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.pictureBox1.Location = new System.Drawing.Point(0, 62);
        	this.pictureBox1.Name = "pictureBox1";
        	this.pictureBox1.Size = new System.Drawing.Size(220, 175);
        	this.pictureBox1.TabIndex = 17;
        	this.pictureBox1.TabStop = false;
        	// 
        	// pane7
        	// 
        	this.pane7.BackColor = System.Drawing.Color.Silver;
        	this.pane7.Controls.Add(this.lAlbum);
        	this.pane7.Controls.Add(this.lArtist);
        	this.pane7.Dark = false;
        	this.pane7.Dock = System.Windows.Forms.DockStyle.Top;
        	this.pane7.Location = new System.Drawing.Point(0, 0);
        	this.pane7.Name = "pane7";
        	this.pane7.SecondColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
        	this.pane7.Size = new System.Drawing.Size(220, 62);
        	this.pane7.TabIndex = 16;
        	// 
        	// lAlbum
        	// 
        	this.lAlbum.BackColor = System.Drawing.Color.Transparent;
        	this.lAlbum.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
        	this.lAlbum.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.lAlbum.Location = new System.Drawing.Point(3, 26);
        	this.lAlbum.Name = "lAlbum";
        	this.lAlbum.Size = new System.Drawing.Size(100, 13);
        	this.lAlbum.TabIndex = 0;
        	this.lAlbum.TabStop = true;
        	this.lAlbum.Text = "Album";
        	// 
        	// lArtist
        	// 
        	this.lArtist.BackColor = System.Drawing.Color.Transparent;
        	this.lArtist.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
        	this.lArtist.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.lArtist.Location = new System.Drawing.Point(3, 13);
        	this.lArtist.Name = "lArtist";
        	this.lArtist.Size = new System.Drawing.Size(100, 13);
        	this.lArtist.TabIndex = 0;
        	this.lArtist.TabStop = true;
        	this.lArtist.Text = "Artist";
        	// 
        	// listView1
        	// 
        	this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        	this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
        	this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.listView1.Location = new System.Drawing.Point(0, 0);
        	this.listView1.Name = "listView1";
        	this.listView1.OwnerDraw = true;
        	this.listView1.Size = new System.Drawing.Size(220, 435);
        	this.listView1.TabIndex = 7;
        	this.listView1.UseCompatibleStateImageBehavior = false;
        	// 
        	// splitContainer3
        	// 
        	this.splitContainer3.BackColor = System.Drawing.Color.Black;
        	this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
        	this.splitContainer3.Location = new System.Drawing.Point(0, 0);
        	this.splitContainer3.Name = "splitContainer3";
        	// 
        	// splitContainer3.Panel1
        	// 
        	this.splitContainer3.Panel1.Controls.Add(this.geckoWebBrowser1);
        	// 
        	// splitContainer3.Panel2
        	// 
        	this.splitContainer3.Panel2.Controls.Add(this.panel1);
        	this.splitContainer3.Panel2Collapsed = true;
        	this.splitContainer3.Size = new System.Drawing.Size(683, 435);
        	this.splitContainer3.SplitterDistance = 469;
        	this.splitContainer3.SplitterWidth = 1;
        	this.splitContainer3.TabIndex = 0;
        	this.splitContainer3.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer3_SplitterMoved);
        	// 
        	// geckoWebBrowser1
        	// 
        	this.geckoWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.geckoWebBrowser1.Location = new System.Drawing.Point(0, 0);
        	this.geckoWebBrowser1.Name = "geckoWebBrowser1";
        	this.geckoWebBrowser1.NoDefaultContextMenu = true;
        	this.geckoWebBrowser1.Size = new System.Drawing.Size(683, 435);
        	this.geckoWebBrowser1.TabIndex = 4;
        	this.geckoWebBrowser1.Click += new System.EventHandler(this.geckoWebBrowser1_Click_2);
        	this.geckoWebBrowser1.DocumentTitleChanged += new System.EventHandler(this.geckoWebBrowser1_DocumentTitleChanged);
        	this.geckoWebBrowser1.DomMouseDown += new Skybound.Gecko.GeckoDomMouseEventHandler(this.GeckoWebBrowser1DomMouseDown);
        	this.geckoWebBrowser1.Navigating += new Skybound.Gecko.GeckoNavigatingEventHandler(this.geckoWebBrowser1_Navigating);
        	this.geckoWebBrowser1.DomClick += new Skybound.Gecko.GeckoDomEventHandler(this.GeckoWebBrowser1DomClick);
        	this.geckoWebBrowser1.Navigated += new Skybound.Gecko.GeckoNavigatedEventHandler(this.geckoWebBrowser1_Navigated);
        	// 
        	// panel1
        	// 
        	this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
        	this.panel1.Controls.Add(this.listView2);
        	this.panel1.Controls.Add(this.pane4);
        	this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.panel1.Location = new System.Drawing.Point(0, 0);
        	this.panel1.Name = "panel1";
        	this.panel1.Size = new System.Drawing.Size(96, 100);
        	this.panel1.TabIndex = 4;
        	this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
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
        	// pane4
        	// 
        	this.pane4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
        	this.pane4.Controls.Add(this.label1);
        	this.pane4.Dark = false;
        	this.pane4.Dock = System.Windows.Forms.DockStyle.Top;
        	this.pane4.Location = new System.Drawing.Point(0, 0);
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
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(47, 17);
        	this.label1.TabIndex = 0;
        	this.label1.Text = "Playlist";
        	// 
        	// pane5
        	// 
        	this.pane5.BackColor = System.Drawing.Color.BlanchedAlmond;
        	this.pane5.Controls.Add(this.pane8);
        	this.pane5.Dark = false;
        	this.pane5.Dock = System.Windows.Forms.DockStyle.Top;
        	this.pane5.Location = new System.Drawing.Point(0, 62);
        	this.pane5.Name = "pane5";
        	this.pane5.SecondColor = System.Drawing.Color.Tan;
        	this.pane5.Size = new System.Drawing.Size(904, 31);
        	this.pane5.TabIndex = 10;
        	this.pane5.Visible = false;
        	// 
        	// pane8
        	// 
        	this.pane8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        	this.pane8.Controls.Add(this.button1);
        	this.pane8.Controls.Add(this.label2);
        	this.pane8.Dark = false;
        	this.pane8.Dock = System.Windows.Forms.DockStyle.Top;
        	this.pane8.Location = new System.Drawing.Point(0, 0);
        	this.pane8.Name = "pane8";
        	this.pane8.SecondColor = System.Drawing.Color.Gold;
        	this.pane8.Size = new System.Drawing.Size(904, 31);
        	this.pane8.TabIndex = 11;
        	// 
        	// button1
        	// 
        	this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.button1.BackColor = System.Drawing.Color.Transparent;
        	this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.button1.Location = new System.Drawing.Point(867, 6);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(25, 23);
        	this.button1.TabIndex = 7;
        	this.button1.Text = "X";
        	this.button1.UseVisualStyleBackColor = false;
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.BackColor = System.Drawing.Color.Transparent;
        	this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.label2.Location = new System.Drawing.Point(41, 3);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(99, 19);
        	this.label2.TabIndex = 6;
        	this.label2.Text = "Error Message";
        	// 
        	// pane1
        	// 
        	this.pane1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
        	this.pane1.Controls.Add(this.cBtn5);
        	this.pane1.Controls.Add(this.cBtn4);
        	this.pane1.Controls.Add(this.cBtn3);
        	this.pane1.Controls.Add(this.textBox1);
        	this.pane1.Dark = false;
        	this.pane1.Dock = System.Windows.Forms.DockStyle.Top;
        	this.pane1.Location = new System.Drawing.Point(0, 0);
        	this.pane1.Name = "pane1";
        	this.pane1.SecondColor = System.Drawing.Color.Gray;
        	this.pane1.Size = new System.Drawing.Size(904, 62);
        	this.pane1.TabIndex = 5;
        	this.pane1.DoubleClick += new System.EventHandler(this.pane1_DoubleClick);
        	this.pane1.Paint += new System.Windows.Forms.PaintEventHandler(this.pane1_Paint);
        	// 
        	// cBtn5
        	// 
        	this.cBtn5.BackColor = System.Drawing.Color.Transparent;
        	this.cBtn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn5.BackgroundImage")));
        	this.cBtn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.cBtn5.Img = global::SpofityRuntime.Properties.Resources.update;
        	this.cBtn5.Location = new System.Drawing.Point(324, 12);
        	this.cBtn5.Name = "cBtn5";
        	this.cBtn5.Size = new System.Drawing.Size(38, 37);
        	this.cBtn5.TabIndex = 8;
        	this.cBtn5.Click += new System.EventHandler(this.CBtn5Click);
        	// 
        	// cBtn4
        	// 
        	this.cBtn4.BackColor = System.Drawing.Color.Transparent;
        	this.cBtn4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn4.BackgroundImage")));
        	this.cBtn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.cBtn4.Img = global::SpofityRuntime.Properties.Resources.forward;
        	this.cBtn4.Location = new System.Drawing.Point(59, 12);
        	this.cBtn4.Name = "cBtn4";
        	this.cBtn4.Size = new System.Drawing.Size(32, 32);
        	this.cBtn4.TabIndex = 7;
        	this.cBtn4.Click += new System.EventHandler(this.CBtn4Click);
        	// 
        	// cBtn3
        	// 
        	this.cBtn3.BackColor = System.Drawing.Color.Transparent;
        	this.cBtn3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn3.BackgroundImage")));
        	this.cBtn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.cBtn3.Img = global::SpofityRuntime.Properties.Resources.back;
        	this.cBtn3.Location = new System.Drawing.Point(12, 12);
        	this.cBtn3.Name = "cBtn3";
        	this.cBtn3.Size = new System.Drawing.Size(31, 32);
        	this.cBtn3.TabIndex = 6;
        	this.cBtn3.Click += new System.EventHandler(this.CBtn3Click);
        	// 
        	// textBox1
        	// 
        	this.textBox1.BackColor = System.Drawing.Color.Transparent;
        	this.textBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("textBox1.BackgroundImage")));
        	this.textBox1.Location = new System.Drawing.Point(97, 9);
        	this.textBox1.Name = "textBox1";
        	this.textBox1.Size = new System.Drawing.Size(221, 40);
        	this.textBox1.TabIndex = 4;
        	this.textBox1.SearchClicked += new System.EventHandler(this.textBox1_SearchClicked);
        	// 
        	// pane2
        	// 
        	this.pane2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.pane2.Controls.Add(this.pane6);
        	this.pane2.Dark = true;
        	this.pane2.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.pane2.Location = new System.Drawing.Point(0, 528);
        	this.pane2.Name = "pane2";
        	this.pane2.SecondColor = System.Drawing.Color.Silver;
        	this.pane2.Size = new System.Drawing.Size(904, 60);
        	this.pane2.TabIndex = 8;
        	// 
        	// pane6
        	// 
        	this.pane6.BackColor = System.Drawing.Color.Gray;
        	this.pane6.Controls.Add(this.cBtn2);
        	this.pane6.Controls.Add(this.cBtn1);
        	this.pane6.Dark = false;
        	this.pane6.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.pane6.ForeColor = System.Drawing.Color.Black;
        	this.pane6.Location = new System.Drawing.Point(0, 0);
        	this.pane6.Name = "pane6";
        	this.pane6.SecondColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.pane6.Size = new System.Drawing.Size(904, 60);
        	this.pane6.TabIndex = 12;
        	// 
        	// cBtn2
        	// 
        	this.cBtn2.BackColor = System.Drawing.Color.Transparent;
        	this.cBtn2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn2.BackgroundImage")));
        	this.cBtn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.cBtn2.Img = global::SpofityRuntime.Properties.Resources.stop;
        	this.cBtn2.Location = new System.Drawing.Point(81, 12);
        	this.cBtn2.Name = "cBtn2";
        	this.cBtn2.Size = new System.Drawing.Size(34, 36);
        	this.cBtn2.TabIndex = 1;
        	// 
        	// cBtn1
        	// 
        	this.cBtn1.BackColor = System.Drawing.Color.Transparent;
        	this.cBtn1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cBtn1.BackgroundImage")));
        	this.cBtn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.cBtn1.Img = global::SpofityRuntime.Properties.Resources.play;
        	this.cBtn1.Location = new System.Drawing.Point(32, 8);
        	this.cBtn1.Name = "cBtn1";
        	this.cBtn1.Size = new System.Drawing.Size(43, 42);
        	this.cBtn1.TabIndex = 1;
        	this.cBtn1.Load += new System.EventHandler(this.CBtn1Load);
        	// 
        	// timer2
        	// 
        	this.timer2.Enabled = true;
        	this.timer2.Tick += new System.EventHandler(this.Timer2Tick);
        	// 
        	// Form1
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.ClientSize = new System.Drawing.Size(904, 588);
        	this.Controls.Add(this.splitContainer1);
        	this.Controls.Add(this.pane5);
        	this.Controls.Add(this.pane2);
        	this.Controls.Add(this.pane1);
        	this.DoubleBuffered = true;
        	this.Name = "Form1";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "ERS";
        	this.TransparencyKey = System.Drawing.Color.Fuchsia;
        	this.Load += new System.EventHandler(this.Form1_Load);
        	this.Activated += new System.EventHandler(this.Form1Activated);
        	this.Enter += new System.EventHandler(this.Form1Enter);
        	this.splitContainer1.Panel1.ResumeLayout(false);
        	this.splitContainer1.Panel2.ResumeLayout(false);
        	this.splitContainer1.ResumeLayout(false);
        	this.splitContainer2.Panel1.ResumeLayout(false);
        	this.splitContainer2.Panel2.ResumeLayout(false);
        	this.splitContainer2.ResumeLayout(false);
        	this.panel2.ResumeLayout(false);
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.pane7.ResumeLayout(false);
        	this.splitContainer3.Panel1.ResumeLayout(false);
        	this.splitContainer3.Panel2.ResumeLayout(false);
        	this.splitContainer3.ResumeLayout(false);
        	this.panel1.ResumeLayout(false);
        	this.pane4.ResumeLayout(false);
        	this.pane5.ResumeLayout(false);
        	this.pane8.ResumeLayout(false);
        	this.pane8.PerformLayout();
        	this.pane1.ResumeLayout(false);
        	this.pane2.ResumeLayout(false);
        	this.pane6.ResumeLayout(false);
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.TreeView listViewX1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.LinkLabel lAlbum;
        private System.Windows.Forms.LinkLabel lArtist;
        private SpofityRuntime.cBtn cBtn3;
        private SpofityRuntime.cBtn cBtn4;
        private SpofityRuntime.cBtn cBtn5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private SpofityRuntime.Pane pane8;
        private SpofityRuntime.cBtn cBtn2;
        private SpofityRuntime.cBtn cBtn1;
        private SpofityRuntime.Pane pane6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView listView2;

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private Pane pane1;
        private ucSearch textBox1;
        private Pane pane5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Pane pane3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Skybound.Gecko.GeckoWebBrowser geckoWebBrowser1;
        private System.Windows.Forms.Panel panel1;
        private Pane pane4;
        private System.Windows.Forms.Button button1;
        private Pane pane7;
        
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Timer timer1;
        private Pane pane2;

    }
}