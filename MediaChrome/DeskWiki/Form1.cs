using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Board;
namespace DeskWiki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Board = new Board.DrawBoard();
        }
        Board.DrawBoard Board { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            Board = new DrawBoard();
            this.Controls.Add(Board);
            Board.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.StartsWith("desktop:"))
            {
                Board.Navigate(textBox1.Text, "desktop", "views");
            }
            else
            {
                Board.Navigate("desktop:wiki:"+textBox1.Text, "desktop", "views");
            }
        }
    }
}
