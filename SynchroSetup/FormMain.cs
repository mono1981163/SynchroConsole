using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SynchroWCF;
using SynchroLib;

namespace SynchroSetup
{
	public partial class FormMain :Form
	{
		//--------------------------------------------------------------------------------
		public FormMain()
		{
			InitializeComponent();

			try
			{
				if (SvcGlobals.CreateServiceHost())
				{
					//SvcGlobals.SynchroHostEvent += new SynchroHostEventHandler(SvcGlobals_SynchroHostEvent);
					//SynchroService host = SvcGlobals.SvcHost.SingletonInstance as SynchroService;
					(SvcGlobals.SvcHost.SingletonInstance as SynchroService).SynchroHostEvent += new SynchroHostEventHandler(Form1_SynchroHostEvent);
				}
			}
			catch (Exception ex)
			{
				if (ex != null) {}
			}
		}

		//--------------------------------------------------------------------------------
		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
			Show();
			this.WindowState = FormWindowState.Normal;
		}

		//--------------------------------------------------------------------------------
		private void FormMain_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				Hide();
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the WCF service host receives data. The newest message is added to 
		/// the listbox, and any data older than 24 hours is removed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form1_SynchroHostEvent(object sender, SynchroHostEventArgs e)
		{
			e.Date            = e.Date.ClearSeconds();
			string dateFormat = "MMM/dd/yyyy HH:mm";

			// remove anything older than 24 hours (this should only result in one thing 
			// being removed)
			bool deleted      = false;
			do
			{
				deleted          = false;
				int    lastItem  = this.listBoxActivity.Items.Count - 1;
				string oldestMsg = this.listBoxActivity.Items[lastItem].ToString();
				if (!string.IsNullOrEmpty(oldestMsg) && oldestMsg.Length > 12)
				{
					oldestMsg = oldestMsg.Substring(0, 12); 
					DateTime datetime;
					if (DateTime.TryParse(dateFormat, out datetime))
					{
						TimeSpan elapsed = datetime - e.Date;
						if (elapsed.Days > 0)
						{
							this.listBoxActivity.Items.RemoveAt(lastItem);
							deleted = true;
						}
					}
				}
				else
				{
					this.listBoxActivity.Items.RemoveAt(lastItem);
					deleted = true;
				}
			} while (deleted);

			// insert the newest item at the top of the list
			string msg        = string.Format("{0} - {1}", e.Date.ToString(dateFormat), e.Message);
			this.listBoxActivity.Items.Insert(0, msg);
		}

		//--------------------------------------------------------------------------------
		private void buttonConfigure_Click(object sender, EventArgs e)
		{
			FormConfigure form = new FormConfigure();
			form.ShowDialog();
		}

		//--------------------------------------------------------------------------------
		private void buttonMinimize_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		//--------------------------------------------------------------------------------
		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		////--------------------------------------------------------------------------------
		//void SvcGlobals_SynchroHostEvent(object sender, SynchroHostEventArgs e)
		//{
		//}

	}
}
