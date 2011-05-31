using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpofityRuntime
{
    public partial class ListViewX1 : UserControl
    {
        private Skybound.Gecko.GeckoWebBrowser geckoWebBrowser1;
        public ListViewX1()
        {
            InitializeComponent();
            
        }
        public ListViewX1(bool init)
        {
            if (init)
            {
                geckoWebBrowser1 = new Skybound.Gecko.GeckoWebBrowser();
                this.Controls.Add(geckoWebBrowser1);
                geckoWebBrowser1.Dock = DockStyle.Fill;
                geckoWebBrowser1.Navigating += new Skybound.Gecko.GeckoNavigatingEventHandler(geckoWebBrowser1_Navigating);
            }
        }
        public void Reload()
        {
            geckoWebBrowser1.Reload();
        }
        public ListView.ListViewItemCollection Items { get { return this.treeView1.Items; } }
        public event NodeLabelEditEventHandler AfterLabelEdit;
        public delegate void QueryEventHandler(object sender,string uri);
        public event QueryEventHandler Browse;
        public ListViewItem GetNodeAt(Point Pt)
        {
            return this.treeView1.GetItemAt(Pt.X, Pt.Y);
        }
        public bool LabelEdit
        {
            get
            {
                return this.treeView1.LabelEdit;
            }
            set
            {
                this.treeView1.LabelEdit = value;
            }
        }
        public ListView.SelectedListViewItemCollection SelectedItems
        {
            get
            {
              
                return treeView1.SelectedItems;
            }
        }
        private void ListViewX1_Load(object sender, EventArgs e)
        {
         

        }

        private void geckoWebBrowser1_Click(object sender, EventArgs e)
        {

        }

  

        private void geckoWebBrowser2_Click(object sender, EventArgs e)
        {

        }

        private void listViewX2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
