using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MediaChrome
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
        }
        public Preferences(Form1 Host)
        {
            this.Host = Host;
            InitializeComponent();
            Skins = new List<Entry>();
        }
        public class Entry
        {
            public string Label;
            public string Value;
            public Entry(string label, string value)
            {
                Label = label;
                Value = value;
            }
        }
        /// <summary>
        /// The mainform host
        /// </summary>
        public MediaChrome.Form1 Host { get; set; }
        public List<Entry> Skins { get; set; }
        string baseFolder = "skins\\";
        private void Preferences_Load(object sender, EventArgs e)
        {
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Label";
            // Initiate combobox1
            
            DirectoryInfo di = new DirectoryInfo(baseFolder);
            int i = 0;
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                comboBox1.Items.Add(dir.Name);
                if (dir.Name == Settings.Default.Skin)
                {
                    comboBox1.SelectedIndex = i;
                }
                i++;
                
            
            }
           
          
          
        }
        private void ApplySettings()
        {
            Host.Skin = new Board.Skin(String.Format(baseFolder + "{0}\\{0}.xml", (string)comboBox1.Items[comboBox1.SelectedIndex]));
            Settings.Default.Skin = (string)comboBox1.Items[comboBox1.SelectedIndex];
            Settings.Default.Save();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ApplySettings();
        }
    }
}
