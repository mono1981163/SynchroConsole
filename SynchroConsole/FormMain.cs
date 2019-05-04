using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.ServiceProcess;

using SynchroLib;
using SynchroWCF;

namespace SynchroConsole
{
	public partial class FormMain :Form
	{
		TimeSpan           m_updateInterval = new TimeSpan(0,0,5,0,0);
		Thread             m_updateThread   = null;
		DatePartFlags      m_equalityFlags  = DatePartFlags.Minute | DatePartFlags.Second;
		SyncItemCollection m_syncItems      = new SyncItemCollection();

		private delegate void UpdateActivityListInvoker(string text, DateTime datetime);

		//--------------------------------------------------------------------------------
		public FormMain()
		{
			InitializeComponent();

			//m_toFiles = new FileInfoList();
			//m_toFiles.GetFiles(Globals.SyncToFolder, true);
			SyncItem item = new SyncItem(@"d:\asyncfrom\", @"d:\asyncto\");
			item.Name = "Test Sync";
			item.FileInfoEvent += new FileInfoHandler(item_FileInfoEvent);
			m_syncItems.Add(item);

			m_updateThread = new Thread(new ThreadStart(UpdateThread));
			m_updateThread.IsBackground = true;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Updates the activity list box
		/// </summary>
		/// <param name="text"></param>
		private void UpdateActivityList(string text, DateTime date)
		{
			text = string.Format("{0} - {1}", DateTime.Now.ToString("dd MMM yyyy HH:mm"), text);
			this.listBoxActivity.Items.Insert(0, text);
			if (SvcGlobals.CreateServiceClient())
			{
				SvcGlobals.SvcClient.SendStatusMessageEx(text, date);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void item_FileInfoEvent(object sender, FileInfoArgs e)
		{
			Debug.WriteLine("Update completed");
			SyncItem item = sender as SyncItem;
			//string text = string.Format("{0} - {1}, updated={2}, elapsed={3}", 
			//                            DateTime.Now.ToString("dd MMM yyyy HH:mm"),
			//                            item.Name,
			//                            e.UpdateCount,
			//                            e.Elapsed.ToString());
			string text = string.Format("{1}, updated={2}, elapsed={3}", 
										item.Name,
										e.UpdateCount,
										e.Elapsed.ToString());
			if (this.listBoxActivity.InvokeRequired)
			{
				UpdateActivityListInvoker method = new UpdateActivityListInvoker(UpdateActivityList);
				Invoke(method, text);
			}
			else
			{
				UpdateActivityList(text, DateTime.Now);
			}
		}


		//////////////////////////////////////////////////////////////////////////////////
		// Thread stuff
		//////////////////////////////////////////////////////////////////////////////////

		//--------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		private void UpdateThread()
		{
			try
			{
				DateTime temp;
				DateTime now;
				DateTime then = new DateTime(0);
				bool waiting = true;
				while (true)
				{
					temp = DateTime.Now;
					now = new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, 0, 0);
					if (!waiting)
					{
						int difference = now.Minute % 5;
						then = now.Add(m_updateInterval.Subtract(new TimeSpan(0, 0, difference, 0, 0)));
						waiting = true;
					}
					if (now.Equal(then, m_equalityFlags) || then.Ticks == 0)
					{
						waiting = false;
						CheckForFiles();
					}
					else
					{
						Thread.Sleep(1000);
					}
				}
			}
			catch (ThreadAbortException taex)
			{
				if (taex != null) {}
			}
			catch (Exception ex)
			{
				if (ex != null) {}
				// if this application is a windows service, log the error
				// otherwise, show a messagebox.
				MessageBox.Show(string.Format("Exception encountered:\n\n{0}", ex.Message));
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		private void CheckForFiles()
		{
			Debug.WriteLine("Checking for files");
			string text = string.Format("Checking {1} sync item{2}", 
										this.m_syncItems.Count, 
										(this.m_syncItems.Count > 1) ? "s" : "");
			if (this.listBoxActivity.InvokeRequired)
			{
				UpdateActivityListInvoker method = new UpdateActivityListInvoker(UpdateActivityList);
				Invoke(method, text);
			}
			else
			{
				UpdateActivityList(text, DateTime.Now);
			}

			foreach (SyncItem item in m_syncItems)
			{
				item.Start();
			}
		}
	
		//////////////////////////////////////////////////////////////////////////////////
		// Form stuff
		//////////////////////////////////////////////////////////////////////////////////

		//--------------------------------------------------------------------------------
		private void buttonSyncStart_Click(object sender, EventArgs e)
		{
			Debug.WriteLine("Starting main thread");
			UpdateActivityList("Started", DateTime.Now);
			if (m_updateThread.ThreadState == System.Threading.ThreadState.Stopped)
			{
				m_updateThread = new Thread(new ThreadStart(UpdateThread));
				m_updateThread.IsBackground = true;
			}
			m_updateThread.Start();
		}

		//--------------------------------------------------------------------------------
		private void buttonSyncStop_Click(object sender, EventArgs e)
		{
			this.m_updateThread.Abort();
		}

		//--------------------------------------------------------------------------------
		private void buttonDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		//--------------------------------------------------------------------------------
		private void FormMain_Load(object sender, EventArgs e)
		{
		}
	}

}

