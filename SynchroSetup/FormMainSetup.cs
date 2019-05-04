using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceProcess;
using System.Runtime.InteropServices;

using SynchroWCF;
using SynchroLib;
using Common;

namespace SynchroSetup
{
	public partial class FormMainSetup :Form
	{
		private SyncSettings m_settings = new SyncSettings(null);

		private delegate void StartStopButtonInvoker(bool start, bool stop);
		private delegate void UpdateActivityListInvoker(string text, DateTime datetime);

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		public FormMainSetup()
		{
			InitializeComponent();

			Globals.StarterProcess.Exited += new EventHandler(StarterProcess_Exited);
			SynchCommon.StartStopServiceEvent += new StartStopServiceHandler(SynchCommon_StartStopServiceEvent);

			bool start = (Globals.IsServiceInstalled());
			bool stop  = (Globals.IsServiceInstalledWithStatus(ServiceControllerStatus.Running));
			UpdateStartStopButtons(start, stop);

			try
			{
				if (SvcGlobals.CreateServiceHost())
				{
					this.labelListening.Text = "Listening";
					(SvcGlobals.SvcHost.SingletonInstance as SynchroService).SynchroHostEvent += new SynchroHostEventHandler(Form1_SynchroHostEvent);
				}
			}
			catch (Exception ex)
			{
				if (ex != null) {}
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user double-clicks the system tray icon.  It displays the 
		/// forum on the desktop.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
			Show();
			this.WindowState = FormWindowState.Normal;
			this.ShowInTaskbar = true;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the form is resized. It adds support for minimizing the form to 
		/// the system tray.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormMain_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				this.ShowInTaskbar = false;
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
			string dateFormat = "dd MMM yyyy HH:mm";

			// remove anything older than 24 hours (this should only result in one thing 
			// being removed)
			if (this.listBoxActivity.Items.Count > 0)
			{
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
			}

			// insert the newest item at the top of the list
			string msg        = string.Format("{0} - {1}", e.Date.ToString(dateFormat), e.Message);
			this.listBoxActivity.Items.Insert(0, msg);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Configure button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonConfigure_Click(object sender, EventArgs e)
		{
			FormConfigure form = new FormConfigure();
			form.ShowDialog();
			bool start = (Globals.IsServiceInstalled());
			bool stop  = (Globals.IsServiceInstalledWithStatus(ServiceControllerStatus.Running));
			UpdateStartStopButtons(start, stop);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Minimize button, and minimizes the application 
		/// to the system tray.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonMinimize_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Close button, and closes the application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Start/Restart Service button, and attempts to 
		/// start/restart the synchro windows service.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStartService_Click(object sender, EventArgs e)
		{
			Globals.StartService();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Stop Service button, and attempts to stop the 
		/// synchro windows service.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStopService_Click(object sender, EventArgs e)
		{
			Globals.StopService();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Updates the start/stop service buttons based on the specified parameters.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="stop"></param>
		private void UpdateStartStopButtons(bool start, bool stop)
		{
			this.buttonStartService.Enabled = start;
			this.buttonStopService.Enabled  = stop;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Updates the activity list box
		/// </summary>
		/// <param name="text"></param>
		private void UpdateActivityList(string text, DateTime date)
		{
			string dateFormat = "dd MMM yyyy HH:mm";
			text = string.Format("{0} - {1}", date.ToString(dateFormat), text);
			this.listBoxActivity.Items.Insert(0, text);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the service is started/stoped INTERNALLY (without using the 
		/// SynchServiceStarter app).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SynchCommon_StartStopServiceEvent(object sender, StartStopServiceEventArgs e)
		{
			bool start = (Globals.IsServiceInstalled());
			bool stop  = (Globals.IsServiceInstalledWithStatus(ServiceControllerStatus.Running));
			UpdateStartStopButtons(start, stop);

			string text = (stop) ? "Started/restarted SynchroService" : "Stopped SynchroService";
			UpdateActivityList(text, DateTime.Now);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the servicestarter application exits, and manages the various 
		/// application exit codes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void StarterProcess_Exited(object sender, EventArgs e)
		{
			Process process     = sender as Process;
			string  exitCodeMsg = "";
			switch (Common.SynchCommon.IntToEnum(process.ExitCode, SSSExitCodes.Success))
			{
				case SSSExitCodes.Success :
					{
						bool start = (Globals.IsServiceInstalled());
						bool stop  = (Globals.IsServiceInstalledWithStatus(ServiceControllerStatus.Running));
						StartStopButtonInvoker method = new StartStopButtonInvoker(UpdateStartStopButtons);
						Invoke(method, start, stop);

						string text = (stop) ? "Started/restarted SynchroService" : "Stopped SynchroService";
						UpdateActivityListInvoker method2 = new UpdateActivityListInvoker(UpdateActivityList);
						Invoke(method2, text, DateTime.Now);
					}
					break;

				case SSSExitCodes.NotAdminMode :
					exitCodeMsg = "SynchroServiceStarter.EXE not run as administrator.";
					break;

				case SSSExitCodes.ServiceNotFound :
					exitCodeMsg = "Service not installed.";
					break;

				case SSSExitCodes.Unexpected :
				default:
					exitCodeMsg = "Unexpected error ocurred.";
					break;
			}
			if (!string.IsNullOrEmpty(exitCodeMsg))
			{
				MessageBox.Show(exitCodeMsg, "SynchroService Starter App Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
}
