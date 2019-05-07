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
using System.Threading;

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
            m_settings.Load();
            foreach (SyncItem item in m_settings.SyncItems)
            {
                item.FileInfoEvent += new FileInfoHandler(item_FileInfoEvent);
            }

            m_updateThread = new Thread(new ThreadStart(UpdateThread));
            m_updateThread.IsBackground = true;

            Globals.StarterProcess.Exited += new EventHandler(StarterProcess_Exited);

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
        /// 
        bool isFirst = true;
		private void buttonStartService_Click(object sender, EventArgs e)
		{
			//Globals.StartService();
            UpdateActivityList("Started", DateTime.Now);
            //if (m_updateThread.ThreadState == System.Threading.ThreadState.Stopped)
            //{
            //m_updateThread = new Thread(new ThreadStart(UpdateThread));
            //m_updateThread.IsBackground = true;
            if (!isFirst)
            {
                m_updateThread.Abort();
                m_updateThread = new Thread(new ThreadStart(UpdateThread));
                m_updateThread.IsBackground = true;
            }
            //}
            m_updateThread.Start();
            isFirst = false;

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
                        UpdateActivityListInvoker method2 = new UpdateActivityListInvoker(UpdateActivityList);
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

        Thread m_updateThread = null;
        DatePartFlags m_equalityFlags = DatePartFlags.Minute | DatePartFlags.Second;
       

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>

            
            
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
                DateTime then = new DateTime(0);
                TimeSpan interval = new TimeSpan(0, 0, this.m_settings.SyncMinutes, 0, 0);
                bool waiting = true;
                while (true)
                {
                    temp = DateTime.Now;
                    now = new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, 0, 0);
                    if (!waiting)
                    {
                        int difference = (this.m_settings.NormalizeTime) ? now.Minute % m_settings.SyncMinutes : 0;
                        then = now.Add(interval.Subtract(new TimeSpan(0, 0, difference, 0, 0)));
                        Debug.WriteLine(string.Format("Next update time is {0}", then.ToString("dd MMM yyyy HH:mm")));
                        waiting = true;
                    }
                    if (now.Equal(then, m_equalityFlags) || then.Ticks == 0)
                    {
                        Debug.WriteLine("Checking for files");
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
                // we dn't care about abort exceptions, because we probably caused 
                // it intentionally.
                if (taex != null) { }
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
      
    }
}
