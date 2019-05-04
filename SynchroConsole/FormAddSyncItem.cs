using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SynchroConsole
{
	public partial class FormAddSyncItem :Form
	{
		//--------------------------------------------------------------------------------
		public FormAddSyncItem()
		{
			InitializeComponent();
		}

		//--------------------------------------------------------------------------------
		private void buttonBrowseFrom_Click(object sender, EventArgs e)
		{

		}

		//--------------------------------------------------------------------------------
		private void buttonBrowseTo_Click(object sender, EventArgs e)
		{

		}

		//--------------------------------------------------------------------------------
		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		//--------------------------------------------------------------------------------
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		//--------------------------------------------------------------------------------
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}

		//--------------------------------------------------------------------------------
		private void buttonBrowseBackup_Click(object sender, EventArgs e)
		{

		}
	}
}
