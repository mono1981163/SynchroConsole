using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace SynchroSetup
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		//--------------------------------------------------------------------------------
		private void buttonAddSync_Click(object sender, EventArgs e)
		{
			List<string> strings = new List<string>();
			strings.Add("Sync Item 1");
			strings.Add("Sync Item 2");
			strings.Add("Sync Item 3");
			FormAddSyncItem form = new FormAddSyncItem(strings);
			form.ShowDialog();
		}

		//--------------------------------------------------------------------------------
		private void buttonEditSync_Click(object sender, EventArgs e)
		{
		}

		//--------------------------------------------------------------------------------
		private void buttonDeleteSync_Click(object sender, EventArgs e)
		{
		}


	}
}
