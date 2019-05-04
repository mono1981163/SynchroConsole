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
using System.ServiceModel;

using SynchroLib;
using SynchroWCF;

namespace SynchroConsole
{
	public partial class FormMainConsole :Form
	{
		Thread             m_updateThread   = null;
		DatePartFlags      m_equalityFlags  = DatePartFlags.Minute | DatePartFlags.Second;
		SyncSettings       m_settings       = new SyncSettings(null);

		private delegate void UpdateActivityListInvoker(string text, DateTime datetime);

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		public FormMainConsole()
		{
			InitializeComponent();
			labelConnected.Text = "";

			m_settings.Load();
			foreach (SyncItem item in m_settings.SyncItems)
			{
				item.FileInfoEvent += new FileInfoHandler(item_FileInfoEvent);
			}

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
			string dateFormat = "dd MMM yyyy HH:mm";

			string errorTemplate = "{0} - Exception: {1}";
			string errorText = "";
			string connectStatus = "Server Not Found";

			text = string.Format("{0} - {1}", date.ToString(dateFormat), text);
			this.listBoxActivity.Items.Insert(0, text);

			try
			{
				if (SvcGlobals.CreateServiceClient())
				{
					SvcGlobals.SvcClient.SendStatusMessageEx(text, date);
					connectStatus = "Connected";
				}
			}
			catch (EndpointNotFoundException)
			{
				errorText = "No endpoint found.";
			}
			catch (CommunicationObjectFaultedException)
			{
				errorText = "WCF client object is faulted.";
			}
			catch (Exception ex)
			{
				errorText = ex.Message;
			}
			if (!string.IsNullOrEmpty(errorText))
			{
				this.listBoxActivity.Items.Insert(0, string.Format(errorTemplate, date.ToString(dateFormat), errorText));
			}
			labelConnected.Text = connectStatus;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when we receive a Fileinfo event, indciating that an attempt was made 
		/// at an update.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void item_FileInfoEvent(object sender, FileInfoArgs e)
		{
			Debug.WriteLine("Update completed");
			SyncItem item = sender as SyncItem;
			string text = string.Format("{0}, updated={1}, elapsed={2}", 
										item.Name,
										e.UpdateCount,
										e.Elapsed.ToString());
			if (this.listBoxActivity.InvokeRequired)
			{
				UpdateActivityListInvoker method = new UpdateActivityListInvoker(UpdateActivityList);
				Invoke(method, text, DateTime.Now);
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
		/// The thread delegate method. It sits/spinswiting for the next ttie to check 
		/// for file updates.
		/// </summary>
		private void UpdateThread()
		{
			try
			{
				DateTime temp;
				DateTime now;
				DateTime then     = new DateTime(0);
				TimeSpan interval = new TimeSpan(0, 0, this.m_settings.SyncMinutes, 0, 0);
				bool     waiting  = true;
				while (true)
				{
					temp = DateTime.Now;
					now  = new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, 0, 0);
					if (!waiting)
					{
						int difference = (this.m_settings.NormalizeTime) ? now.Minute % m_settings.SyncMinutes : 0;
						then           = now.Add(interval.Subtract(new TimeSpan(0, 0, difference, 0, 0)));
						Debug.WriteLine(string.Format("Next update time is {0}", then.ToString("dd MMM yyyy HH:mm")));
						waiting        = true;
					}
					if (now.Equal(then, m_equalityFlags) || then.Ticks == 0)
					{
						Debug.WriteLine("Checking for files");
						waiting       = false;
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
				// we dn't care about abort exceptions, because we probably caused 
				// it intentionally.
				if (taex != null) {}
			}
			catch (Exception ex)
			{
				// if this application is a windows service, log the error
				// otherwise, show a messagebox.
				MessageBox.Show(string.Format("Exception encountered:\n\n{0}", ex.Message));
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		private void UpdateListBox(string text)
		{
			if (this.listBoxActivity.InvokeRequired)
			{
				UpdateActivityListInvoker method = new UpdateActivityListInvoker(UpdateActivityList);
				Invoke(method, text, DateTime.Now);
			}
			else
			{
				UpdateActivityList(text, DateTime.Now);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		private void CheckForFiles()
		{
			Debug.WriteLine("Checking for files");
			string text = string.Format("Checking {0} sync item{1}", 
										this.m_settings.SyncItems.Count, 
										(this.m_settings.SyncItems.Count > 1) ? "s" : "");
			UpdateListBox(text);

			//foreach (SyncItem item in this.m_settings.SyncItems)
			//{
			//    item.Start();
			//}
			m_settings.SyncItems.StartUpdate();
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
		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		//--------------------------------------------------------------------------------
		private void FormMain_Load(object sender, EventArgs e)
		{
		}
	}

}

