using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceProcess;

using AppSettingsLib;
using SynchroLib;

namespace SynchroSetup
{
	public partial class FormConfigure : Form
	{
		private SyncSettings m_settings = null;
		private string       m_stdOutput = "";

		private delegate void InstallerButtonInvoker(string text);


		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		public FormConfigure()
		{
			InitializeComponent();

			//this.buttonInstall.Text = (Globals.IsServiceInstalled()) ? "Uninstall Service" : "Install Service";

			PopulateSyncItemListView();
		}

		#region Non-event methods
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Loads the settings and populates the listview
		/// </summary>
		private void PopulateSyncItemListView()
		{
			// get our settings
			this.m_settings = new SyncSettings(null);
			this.m_settings.Load();
			this.checkBoxNormalize.Checked    = this.m_settings.NormalizeTime;
			this.textBoxSyncMinutes.Text      = this.m_settings.SyncMinutes.ToString();
			//this.buttonInstall.Enabled        = true;

			// and then our sync items
			if (this.m_settings.SyncItems != null)
			{
				foreach (SyncItem item in this.m_settings.SyncItems)
				{
					AddSyncItem(item);
				}
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Adds a single sync item to the listview
		/// </summary>
		/// <param name="item"></param>
		private void AddSyncItem(SyncItem item)
		{
			ListViewItem lvi = new ListViewItem(item.Name);
			lvi.SubItems.Add(item.SyncToPath);
			lvi.SubItems.Add(item.SyncFromPath);
			lvi.SubItems.Add(item.SyncSubfolders.ToString());
			lvi.SubItems.Add(item.BackupBeforeSync.ToString());
			lvi.SubItems.Add(item.DeleteAfterSync.ToString());
			lvi.Tag = item;
			this.listViewSyncItems.Items.Add(lvi);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Verifies that the specified textbox only contains a valid integer value.
		/// </summary>
		/// <param name="box"></param>
		private void VerifyAsInteger(TextBox box)
		{
			int temp;
			box.Text = box.Text.Trim();
			if (!Int32.TryParse(box.Text, out temp))
			{
				if (box.Text.Length > 0)
				{
					box.Text = box.Text.Substring(0,box.Text.Length - 1);
				}
			}
			box.SelectionStart = box.Text.Length;
		}
		#endregion Non-event methods

		#region Event handlers
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Add button.  Displays the Add/Edit Sync Item 
		/// form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddSync_Click(object sender, EventArgs e)
		{
			List<string> strings = new List<string>();
			foreach (SyncItem item in this.m_settings.SyncItems)
			{
				strings.Add(item.ToString());
			}
			FormAddSyncItem form = new FormAddSyncItem(null, strings);
			if (form.ShowDialog() == DialogResult.OK && form.SyncItem != null)
			{
				this.m_settings.SyncItems.Add(form.SyncItem);
				AddSyncItem(form.SyncItem);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Edit button.  Displays the Add/Edit Sync Item 
		/// form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEditSync_Click(object sender, EventArgs e)
		{
			List<string> strings = new List<string>();
			foreach (SyncItem item in this.m_settings.SyncItems)
			{
				strings.Add(item.ToString());
			}
			if (this.listViewSyncItems.SelectedItems.Count > 0)
			{
				SyncItem syncItem = (SyncItem)(this.listViewSyncItems.SelectedItems[0].Tag); 
				FormAddSyncItem form = new FormAddSyncItem(syncItem, strings);
				if (form.ShowDialog() == DialogResult.OK && form.SyncItem != null)
				{
					this.listViewSyncItems.SelectedItems[0].SubItems[0].Text = form.SyncItem.Name;
					this.listViewSyncItems.SelectedItems[0].SubItems[1].Text = form.SyncItem.SyncToPath;
					this.listViewSyncItems.SelectedItems[0].SubItems[2].Text = form.SyncItem.SyncFromPath;
					this.listViewSyncItems.SelectedItems[0].SubItems[3].Text = form.SyncItem.SyncSubfolders.ToString();
					this.listViewSyncItems.SelectedItems[0].SubItems[4].Text = form.SyncItem.BackupBeforeSync.ToString();
					this.listViewSyncItems.SelectedItems[0].SubItems[5].Text = form.SyncItem.DeleteAfterSync.ToString();
				}
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Delete button.  Deletes the selected Sync Item 
		/// from the data file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonDeleteSync_Click(object sender, EventArgs e)
		{
			if (this.listViewSyncItems.SelectedItems.Count > 0)
			{
				SyncItem syncItem = (SyncItem)(this.listViewSyncItems.SelectedItems[0].Tag);
				this.m_settings.SyncItems.Remove(syncItem);
				this.listViewSyncItems.Items.Remove(this.listViewSyncItems.SelectedItems[0]);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the use clicks the OK button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			m_settings.NormalizeTime   = this.checkBoxNormalize.Checked;
			m_settings.SyncMinutes     = Convert.ToInt32(this.textBoxSyncMinutes.Text);
			m_settings.Save();
			this.Close();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Install/Uninstall Service button. It attempts 
		/// to run InstallUtil.exe to either install or uninstall the service, whichever 
		/// is appropriate.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInstall_Click(object sender, EventArgs e)
		{
			Process process = null;
			try
			{
				string appToRun = Application.ExecutablePath;
				appToRun = appToRun.Replace(System.IO.Path.GetFileName(appToRun), "SynchroService.exe");
				ProcessStartInfo info       = new ProcessStartInfo();
				info.FileName               = appToRun;
				info.WorkingDirectory       = System.IO.Path.GetDirectoryName(Environment.CurrentDirectory);
				info.CreateNoWindow         = false;
				info.UseShellExecute        = true;
				info.Verb                   = "runas";
				if (!Globals.IsServiceInstalled())
				{
				    info.Arguments = "-i";
				}
				else
				{
				    info.Arguments = "-u";
				}
				process                     = new Process();
				process.StartInfo           = info;
				process.EnableRaisingEvents = true;
				process.Exited             += new EventHandler(process_Exited);
				process.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Exception Encountered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the InstallUtil process has exited.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void process_Exited(object sender, EventArgs e)
		{
			string buttonText = (Globals.IsServiceInstalled()) ? "Uninstall Service" : "Install Service";

			InstallerButtonInvoker method = new InstallerButtonInvoker(UpdateInstallButton);
			Invoke(method, buttonText);

		}

		//--------------------------------------------------------------------------------
		private void UpdateInstallButton(string text)
		{
			//this.buttonInstall.Text = text;
			if (text == "Uninstall Service")
			{
				MessageBox.Show("Service installed but not started. Make sure you \nclick the Start/Restart Service button on \nthe main form.", "Service Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user enters a characer in the SyncMinutes textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxSyncMinutes_TextChanged(object sender, EventArgs e)
		{
			VerifyAsInteger(sender as TextBox);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user enters a characer in the HeuristicMinutes textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxHeuristicMinutes_TextChanged(object sender, EventArgs e)
		{
			VerifyAsInteger(sender as TextBox);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user enters a characer in the HeuristicMinutes textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxHeuristicEvents_TextChanged(object sender, EventArgs e)
		{
			VerifyAsInteger(sender as TextBox);
		}
		#endregion Event handlers

	}
}
