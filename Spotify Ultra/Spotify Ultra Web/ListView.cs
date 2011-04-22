/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 2010-11-14
 * Time: 10:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SpofityRuntime
{
	public class DropEventArgs
		{
			public int OldPosition {get;set;}
			public int NewPosition {get;set;}
			public List<ListViewItem> Items {get;set;}
			public DropEventArgs()
			{
				Items = new List<ListViewItem>();
			}
		}
	/// <summary>
	/// Description of ListView.
	/// </summary>
	public class XListView : System.Windows.Forms.ListView
	{
		
		public delegate void ItemDrop(object sender,DropEventArgs e);
		public event ItemDrop ItemDropped;
		
		public XListView()
		{

			this.MouseMove+= new MouseEventHandler(LVMouseMove);
			this.MouseDown+=new MouseEventHandler(LVMouseDown);
			this.MouseUp+=new MouseEventHandler(LVMouseUp);
			this.BackColor = System.Drawing.Color.FromArgb(30,30,30);
			this.ForeColor=System.Drawing.Color.FromArgb(244,244,244);
			this.View = View.Details;
			this.Columns.Add("Title");
            this.Columns.Add("Artist");
            this.Columns.Add("Album");
		}
		private int diff(int x,int y)
      	{
      		return x > y ? x-y : y-x;
      	}
		public bool CanDrag
		{
			get;set;
		}
		private bool dragging;
		private void LVMouseMove(object sender,MouseEventArgs e)
      	{
      		if(list_mousedown)
      		{
      			if(diff(mx,e.X) > 3 || diff(my,e.Y) > 3)
      			{
      				if(this.SelectedItems.Count< 1)
      				{
      					return;
      				}
      				this.DoDragDrop(this.SelectedItems, DragDropEffects.Copy);
      				LSelection=this.SelectedItems;
      				list_mousedown=false;
      				mx=0;
      				my=0;
      				dragging=true;
      			}
      		}
      	}
      	private int oldPosition;
      	private ListView.SelectedListViewItemCollection LSelection;
      	private void LVMouseDown(object sender,MouseEventArgs e)
      	{
      		if(this.SelectedItems.Count >0&&CanDrag)
      		{
	      		mx=e.X;
	      		my=e.Y;
	      		list_mousedown=true;
      		}
      	}
      	private void LVMouseUp(object Sender,MouseEventArgs e)
      	{
      		try{
      			
      		
      		
      		if(dragging && this.SelectedItems.Count > 0)
      		{
      			DropEventArgs Args = new DropEventArgs();
      			
	      		ListViewItem d = this.GetItemAt(e.X,e.Y);
	      		Args.OldPosition=this.SelectedItems[0].Index;
	      		int dep=0;
	      		Args.NewPosition=d.Index;
	      		foreach(System.Windows.Forms.ListViewItem _Item in this.SelectedItems)
	      		{
	      		
	      			Args.Items.Add(_Item);
	      			
	      		}
	      		//this.SelectedItems.Clear();
	      		foreach(ListViewItem _Item in Args.Items)
	      		{
	      				this.Items.Remove(_Item);
	      				this.Items.Insert(Args.NewPosition+dep,_Item);
	      				
	      				dep++;
	      		}
	      		
	      		dragging=false;
	      		if(ItemDropped!=null)
	      			ItemDropped((Object)this,Args);
      		}
      		}catch{
      			
      		}
      	}
      	private bool list_mousedown;
      	private int mx,my;

        internal ListViewItem GetNodeAt(System.Drawing.Point point)
        {
            throw new NotImplementedException();
        }
    }
}
