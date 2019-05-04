using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SynchroSetup
{
	//////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Borderless modeless form that contains a listbox that fills the entire form, and 
	/// contains a list of existing sync item names.
	/// </summary>
	public partial class FormExistingNames :Form
	{
		public string CurrentSelection  { get; private set; }
		public bool   HiddenBySelection { get; set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="names"></param>
		public FormExistingNames(List<string> names)
		{
			InitializeComponent();
			this.listBox1.Items.AddRange(names.ToArray());
			this.CurrentSelection  = "";
			this.HiddenBySelection = false;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the mouse leavse the listbox.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBox1_MouseLeave(object sender, EventArgs e)
		{
			this.CurrentSelection  = "";
			this.HiddenBySelection = false;
			this.Hide();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when an item is double-clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			this.CurrentSelection  = (string)this.listBox1.SelectedItem;
			this.HiddenBySelection = true;
			this.Hide();
		}
	}
}
