using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Security.Principal;

using System.Threading;

using AppSettingsLib;
using SynchroLib;
using SynchroWCF;

namespace SynchroService
{
	public partial class SynchroService :ServiceBase
	{
		Thread                       m_updateThread   = null;
		DatePartFlags                m_equalityFlags  = DatePartFlags.Minute | DatePartFlags.Second;
		//SyncItemCollection           m_syncItems      = new SyncItemCollection();
		SyncSettings                 m_settings       = new SyncSettings(null);
		System.Threading.ThreadState m_threadState    = System.Threading.ThreadState.Unstarted;
		AppLog                       m_log            = new AppLog("SynchroService", AppLog.LogLevel.Verbose);

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		public SynchroService()
		{
			InitializeComponent();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Starts the service
		/// </summary>
		/// <param name="args"></param>
		protected override void OnStart(string[] args)
		{
			m_log.SendToLog("Starting SynchroService...", AppLog.LogLevel.Verbose);

			this.m_settings.Load();
			foreach(SyncItem item in this.m_settings.SyncItems)
			{
				item.FileInfoEvent += new FileInfoHandler(item_FileInfoEvent);
			}

			this.m_updateThread              = new Thread(new ThreadStart(UpdateThread));
			this.m_updateThread.IsBackground = true;
			this.m_updateThread.Start();
		}

		#region Other Commands
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Stops the service
		/// </summary>
		protected override void OnStop()
		{
			foreach(SyncItem item in this.m_settings.SyncItems)
			{
				item.FileInfoEvent -= new FileInfoHandler(item_FileInfoEvent);
			}

			m_updateThread.Abort();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Continue a suspended service
		/// </summary>
		protected override void OnContinue()
		{
			base.OnContinue();
			m_threadState = System.Threading.ThreadState.Suspended;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Pauses a running service
		/// </summary>
		protected override void OnPause()
		{
			m_threadState = System.Threading.ThreadState.Running;
			base.OnPause();
		}
		#endregion Other Commands


		//////////////////////////////////////////////////////////////////////////////////
		// Other methods
		//////////////////////////////////////////////////////////////////////////////////

		//--------------------------------------------------------------------------------
		private void LoadSettings()
		{
			this.m_settings.Load();
		}


		//////////////////////////////////////////////////////////////////////////////////
		// Thread stuff
		//////////////////////////////////////////////////////////////////////////////////

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired by a SyncItem when it's done trying to update itself
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void item_FileInfoEvent(object sender, FileInfoArgs e)
		{
			SyncItem item = sender as SyncItem;
			string text = string.Format("{0}, updated={1}, elapsed={2}", 
										item.Name,
										e.UpdateCount,
										e.Elapsed.ToString());

			UpdateActivityList(text, DateTime.Now);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// The sit/spin thread delegate method
		/// </summary>
		private void UpdateThread()
		{
			m_threadState = System.Threading.ThreadState.Running;
			try
			{
				DateTime temp;
				DateTime now;
				DateTime then     = new DateTime(0);
				TimeSpan interval = new TimeSpan(0, 0, this.m_settings.SyncMinutes, 0, 0);
				bool     waiting  = true;
				while (true)
				{
					if (m_threadState == System.Threading.ThreadState.Running)
					{
						temp = DateTime.Now;
						now  = new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, 0, 0);
						if (!waiting)
						{
							int difference = (this.m_settings.NormalizeTime) ? now.Minute % m_settings.SyncMinutes : 0;
							then           = now.Add(interval.Subtract(new TimeSpan(0, 0, difference, 0, 0)));
							waiting        = true;
						}
						if (now.Equal(then, m_equalityFlags) || then.Ticks == 0)
						{
							waiting        = false;
							CheckForFiles();
						}
						else
						{
							Thread.Sleep(1000);
						}
					}
					else
					{
						Thread.Sleep(1000);
					}
				}
			}
			catch (ThreadAbortException)
			{
				// we don't care if the thread aborted because we probably aborted it.
			}
			catch (Exception ex)
			{
				m_log.SendErrorToLog(ex.Message);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Starts the update process for all of the sync items (called from the thread 
		/// delegate method)
		/// </summary>
		private void CheckForFiles()
		{
			string text = string.Format("Checking {0} sync item{1}", 
										this.m_settings.SyncItems.Count, 
										(this.m_settings.SyncItems.Count > 1) ? "s" : "");

			UpdateActivityList(text, DateTime.Now);
	
			m_settings.SyncItems.StartUpdate();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Updates the activity list box
		/// </summary>
		/// <param name="text"></param>
		private void UpdateActivityList(string text, DateTime date)
		{
			//string dateFormat = "dd MMM yyyy HH:mm";

			//string errorText = "";

			//DateTime now = DateTime.Now;
			//text = string.Format("{0} - {1}", date.ToString(dateFormat), text);

			string errorText = "";
			DateTime now = DateTime.Now;

			try
			{
				if (SvcGlobals.CreateServiceClient())
				{
					SvcGlobals.SvcClient.SendStatusMessageEx(text, date);
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
				m_log.SendErrorToLog(errorText);
				m_log.SendToLog(text, AppLog.LogLevel.Noise);
			}
		}
	}

}
