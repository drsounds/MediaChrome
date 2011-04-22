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
    public partial class ucSearch : UserControl
    {
        public ucSearch()
        {
            InitializeComponent();
        }
        public event EventHandler SearchClicked;
        private void label1_Click(object sender, EventArgs e)
        {
            SearchClicked(this, new EventArgs());
        }

        private void ucSearch_Load(object sender, EventArgs e)
        {

        }
        public string Text
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.panel1.Visible = textBox1.Text != "";
        }
    }
}
