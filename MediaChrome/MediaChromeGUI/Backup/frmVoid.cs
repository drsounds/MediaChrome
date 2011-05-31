using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpofityRuntime
{
    public partial class frmVoid : Form
    {
        public frmVoid()
        {
            InitializeComponent();
        }

        private void frmVoid_Load(object sender, EventArgs e)
        {
      
        }

        private void frmVoid_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
