/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 2010-10-28
 * Time: 09:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MediaChrome
{
	/// <summary>
	/// Description of ImportLibrary.
	/// </summary>
	public partial class ImportLibrary : Form
	{
		/// <summary>
		/// Change this in future
		/// </summary>
		public IPlayEngine Importer;
		
	
		public ImportLibrary()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Importer = null;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
		}	
		public int completed=0;
		bool ready=true;
		void ImportLibraryLoad(object sender, EventArgs e)
		{
			comboBox1.DisplayMember="Title";
			comboBox1.ValueMember="Namespace";
			List<IPlayEngine> Engines = new List<IPlayEngine>();
            foreach (IPlayEngine Engine in SpofityRuntime.Program.MediaEngines.Values)
			{
				Engines.Add(Engine);
			}
			comboBox1.DataSource = Engines;
			//Importer = (IPlayEngine)comboBox1.SelectedValue;
			
		}
		public void ImportFiles(){
			
			
			//ImportEx(textBox1.Text);
			Import(textBox1.Text);
		}
		public int max=0;
		public int countFiles=110;
		
		public void Import(string RootDir)
        {
            List<Song> songs =    Importer.Import( RootDir);
        
            SQLiteConnection Conn = new SQLiteConnection("Data Source=sqlite.db;");
            try
            {
                Conn.Open();
            }
            catch
            {
            
			
				foreach(Song Ds in songs)
				{
					SQLiteCommand C = new SQLiteCommand("SELECT count(*) FROM song WHERE path='"+Ds.Path+"'",(SQLiteConnection)Conn);
					SQLiteDataReader SQDR = C.ExecuteReader();
					
					if(SQDR.HasRows)
					{
						SQDR.Read();
						if(SQDR.GetInt32(0)==0)
						{
							try
							{
							SQLiteCommand Df = new SQLiteCommand("INSERT INTO song (name,artist,album,engine,path,genre,store) VALUES(\""+Ds.Name+"\",\""+Ds.Artists[0].Name+"\",\""+Ds.Album.Name+"\",\"sp\",\"sp:"+Ds.Path+"\",\"pop\",\"Spotify\")",Conn);
							Df.ExecuteNonQuery();
							}
							catch
							{
								
							}
						}
					}
				}
			}
			Conn.Close();
        
        }
		
		void Button1Click(object sender, EventArgs e)
		{
			if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text=folderBrowserDialog1.SelectedPath;
			}
			
			
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			Importer = (IPlayEngine)SpofityRuntime.Program.MediaEngines[(String)comboBox1.SelectedValue];
			button2.Enabled=false;
			button1.Enabled=false;
			
			Thread XCM = new Thread(ImportFiles);
			XCM.Start();
			timer1.Start();
			label3.Show();
			button3.Enabled=false;
			ready=false;
		}
		
		void Timer1Tick(object sender, EventArgs e)
		{
			if(Importer.Ready)
			{
				button1.Enabled=true;
				button2.Enabled=true;
				button3.Enabled=true;
				ready=true;
			
			}
			else
			{
				try
				{
					progressBar1.Maximum = Importer.TotalFiles;
					progressBar1.Value= Importer.FilesCompleted;
				}catch{}
			}
		}
		
		void ImportLibraryFormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel=!ready;
		}
	}
}
