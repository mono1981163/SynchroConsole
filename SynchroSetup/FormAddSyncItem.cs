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
using System.Runtime.InteropServices;

using AppSettingsLib;
using SynchroLib;

namespace SynchroSetup
{

	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Allows the user to add a new sync itme, or edit an existing one.
	/// </summary>
	public partial class FormAddSyncItem :Form
	{
		List<string>      m_existingNames     = null;
		FormExistingNames m_formExistingNames = null;
		bool              m_adding            = false;
		SyncItem          m_syncItem          = null;

		public SyncItem   SyncItem 
		{
			get
			{
				if (m_syncItem != null)
				{
					m_syncItem.BackupBeforeSync = this.checkBoxBackupBeforeSync.Checked;
					m_syncItem.DeleteAfterSync  = this.checkBoxRemoveAfterSync.Checked;
					m_syncItem.Name             = this.textBoxName.Text;
					m_syncItem.SyncFromPath     = this.textBoxSyncFrom.Text;
					m_syncItem.SyncSubfolders   = this.checkBoxIncludeSubs.Checked;
					m_syncItem.SyncToPath       = this.textBoxSyncTo.Text;
					m_syncItem.Enabled          = this.checkBoxEnable.Checked;
                    // Rui_Add
                    m_syncItem.Writable         = this.checkBoxMakeFilesWritable.Checked;
                    m_syncItem.DeletedDirOrFile = this.textBoxDeletedDirOrFile.Text;
                    m_syncItem.ExcludeDirOrFile = this.textBoxExcludeFile.Text;
                    m_syncItem.RunFile          = this.textBoxRunFile.Text;
                    m_syncItem.ForceDownlaod    = this.checkBoxForceDownload.Checked;
                }
                return m_syncItem;
			}
			private set
			{
				m_syncItem = value;
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="item">Sync item to be edited</param>
		/// <param name="names">List of existing sync item names (so we can avoid duplicates)</param>
		public FormAddSyncItem(SyncItem item, List<string> names)
		{
			m_existingNames = names;
			m_adding        = (item == null);
			this.SyncItem   = item;

			InitializeComponent();

			// If we're adding a new item, come up with a unique name to avoid forcing 
			// the user to type one in himself
			if (m_adding)
			{
				string tempName = "Sync Item {0}";
				int    count    = 0;
				do
				{
					this.textBoxName.Text = string.Format(tempName, ++count);
				} while (NameIsDuplicate());
			}
			else
			{
				this.textBoxName.Text                 = item.Name;
				this.textBoxSyncFrom.Text             = item.SyncFromPath;
				this.textBoxSyncTo.Text               = item.SyncToPath;
				this.textBoxBackupFolder.Text         = item.BackupPath;
				this.checkBoxBackupBeforeSync.Checked = item.BackupBeforeSync;
				this.checkBoxEnable.Checked           = item.Enabled;
				this.checkBoxIncludeSubs.Checked      = item.SyncSubfolders;
				this.checkBoxRemoveAfterSync.Checked  = item.DeleteAfterSync;
                // Rui_Add
                this.checkBoxMakeFilesWritable.Checked= item.Writable;
                this.textBoxDeletedDirOrFile.Text            = item.DeletedDirOrFile;
                this.textBoxExcludeFile.Text                 = item.ExcludeDirOrFile;
                this.textBoxRunFile.Text                     = item.RunFile;
                this.checkBoxForceDownload.Checked    = item.ForceDownlaod;
            }
        }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Checks to see if the name in the textbox is a duplicate (case insensitive 
		/// comparison). 
		/// </summary>
		/// <returns></returns>
		private bool NameIsDuplicate()
		{
			int count = (from name in m_existingNames 
						 where this.textBoxName.Text.ToLower() == name.ToLower()
						 select name).Count();
			return (count > 0);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Ensures that the "newPath" is not the same as the other two paths that can 
		/// be specified.
		/// </summary>
		/// <param name="path1">One of the other two paths</param>
		/// <param name="path2">The other of the other two paths</param>
		/// <param name="newPath">The new path being specified</param>
		/// <returns></returns>
		private bool UniquePathName(string path1, string path2, string newPath)
		{
			path1   = path1.ToLower();
			path2   = path2.ToLower();
			newPath = newPath.ToLower();
			bool isUnique = false;
			isUnique = (newPath != path1 && newPath != path2);
			return isUnique;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Browse button for the Sync From path.  
		/// Displays the FolderBrowerDialog form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBrowseFrom_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.Description         = "Select 'Sync From' Folder";
			dlg.RootFolder          = Environment.SpecialFolder.MyComputer;
			dlg.ShowNewFolderButton = true;
			if (dlg.ShowDialog() == DialogResult.OK && UniquePathName(this.textBoxBackupFolder.Text, this.textBoxSyncTo.Text, dlg.SelectedPath))
			{
				this.textBoxSyncFrom.Text = dlg.SelectedPath;
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Browse button for the Sync To path.
		/// Displays the FolderBrowerDialog form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBrowseTo_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.Description         = "Select 'Sync To' Folder";
			dlg.RootFolder          = Environment.SpecialFolder.MyComputer;
			dlg.ShowNewFolderButton = true;
			if (dlg.ShowDialog() == DialogResult.OK && UniquePathName(this.textBoxBackupFolder.Text, this.textBoxSyncFrom.Text, dlg.SelectedPath))
			{
				this.textBoxSyncTo.Text = dlg.SelectedPath;
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Browse button for the Backup path.
		/// Displays the FolderBrowerDialog form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBrowseBackup_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.Description         = "Select 'Backup' Folder";
			dlg.RootFolder          = Environment.SpecialFolder.MyComputer;
			dlg.ShowNewFolderButton = true;
			if (dlg.ShowDialog() == DialogResult.OK && UniquePathName(this.textBoxSyncFrom.Text, this.textBoxSyncTo.Text, dlg.SelectedPath))
			{
				this.textBoxBackupFolder.Text = dlg.SelectedPath;
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the OK button.  Makes sure the data is valid, and 
		/// closes the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (DataIsValid())
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Closes the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.SyncItem     = null;
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Validates the various textbox fields.
		/// </summary>
		/// <returns></returns>
		private bool DataIsValid()
		{
			bool valid = true;
			Control invalidCtrl = null;
			string errorTxt = "";

			// The name cannot be a duplicate of names that already exist in the sync 
			// item collection
			if (NameIsDuplicate() && m_adding)
			{
				invalidCtrl = this.textBoxName;
				errorTxt = "The specified sync item name already exists.";
			}
			// The SyncFrom path must exist
			else if (!Directory.Exists(this.textBoxSyncFrom.Text))
			{
				invalidCtrl = this.textBoxSyncFrom;
				errorTxt = (string.IsNullOrEmpty(this.textBoxSyncFrom.Text)) ? "The 'Sync From' folder was not specified" : "The specified 'Sync From' folder does not exist.";
			}
			// The SyncTO path must exist
			else if (!Directory.Exists(this.textBoxSyncTo.Text))
			{
				invalidCtrl = this.textBoxSyncTo;
				errorTxt = (string.IsNullOrEmpty(this.textBoxSyncTo.Text)) ? "The 'Sync To' folder was not specified" : "The specified 'Sync To' folder does not exist.";
			}
			// If backups are to be performed, the backup path must exist
			else if (this.checkBoxBackupBeforeSync.Checked && !Directory.Exists(this.textBoxBackupFolder.Text))
			{
				invalidCtrl = this.textBoxBackupFolder;
				errorTxt = (string.IsNullOrEmpty(this.textBoxBackupFolder.Text)) ? "The 'Backup' folder was not specified" : "The specified 'Backup' folder does not exist.";
			}
			// If we have invalid data
			if (invalidCtrl != null)
			{
				// show a message and focus the errant control.
				MessageBox.Show(errorTxt);
				invalidCtrl.Focus();
				valid = false;
			}
			else
			{
				// otherwise, if we're adding, create a syncitem object
				if (m_adding)
				{
					SyncItem = new SyncItem();
				}
			}
			return valid;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user hovers the mouse pointer over the existing names label
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void labelHoverList_MouseEnter(object sender, EventArgs e)
		{
			if (this.m_existingNames.Count > 0)
			{
				PositionModeless();
				m_formExistingNames.Show();
				m_formExistingNames.Focus();
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when this form loads
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormAddSyncItem_Load(object sender, EventArgs e)
		{
			PositionModeless();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Moves the modeless form to an appropriate position relative to the existin 
		/// names label.
		/// </summary>
		private void PositionModeless()
		{
			if (m_formExistingNames == null)
			{
				m_formExistingNames                 = new FormExistingNames(this.m_existingNames);
				m_formExistingNames.VisibleChanged += new EventHandler(formExistingNames_VisibleChanged);
				m_formExistingNames.StartPosition   = FormStartPosition.Manual;
				m_formExistingNames.Visible         = false;
			}
			m_formExistingNames.Location = this.PointToScreen(this.labelHoverList.Location);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the modelss form's visibility property changes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void formExistingNames_VisibleChanged(object sender, EventArgs e)
		{
			FormExistingNames form = sender as FormExistingNames;
			if (form.HiddenBySelection && string.IsNullOrEmpty(this.textBoxName.Text))
			{
				this.textBoxName.Text = form.CurrentSelection + " (B)";
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when this form is closing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormAddSyncItem_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_formExistingNames.Hide();
			m_formExistingNames.Close();
		}

	}
}
