namespace SynchroSetup
{
	partial class FormAddSyncItem
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSyncFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBrowseFrom = new System.Windows.Forms.Button();
            this.textBoxSyncTo = new System.Windows.Forms.TextBox();
            this.buttonBrowseTo = new System.Windows.Forms.Button();
            this.checkBoxIncludeSubs = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxRemoveAfterSync = new System.Windows.Forms.CheckBox();
            this.checkBoxBackupBeforeSync = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBackupFolder = new System.Windows.Forms.TextBox();
            this.buttonBrowseBackup = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.labelHoverList = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxMakeFilesWritable = new System.Windows.Forms.CheckBox();
            this.checkBoxLck = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDeletedDirOrFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxExcludeFile = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxRunFile = new System.Windows.Forms.TextBox();
            this.checkBoxForceDownload = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPattern = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxFolderMapping = new System.Windows.Forms.TextBox();
            this.checkBoxMirror = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.textBoxInvalidState = new System.Windows.Forms.TextBox();
            this.btnChangeFileState = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sync FROM this folder:";
            // 
            // textBoxSyncFrom
            // 
            this.textBoxSyncFrom.Location = new System.Drawing.Point(136, 40);
            this.textBoxSyncFrom.Name = "textBoxSyncFrom";
            this.textBoxSyncFrom.Size = new System.Drawing.Size(341, 21);
            this.textBoxSyncFrom.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sync TO this folder:";
            // 
            // buttonBrowseFrom
            // 
            this.buttonBrowseFrom.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowseFrom.Location = new System.Drawing.Point(484, 40);
            this.buttonBrowseFrom.Name = "buttonBrowseFrom";
            this.buttonBrowseFrom.Size = new System.Drawing.Size(32, 18);
            this.buttonBrowseFrom.TabIndex = 5;
            this.buttonBrowseFrom.Text = "...";
            this.buttonBrowseFrom.UseVisualStyleBackColor = true;
            this.buttonBrowseFrom.Click += new System.EventHandler(this.buttonBrowseFrom_Click);
            // 
            // textBoxSyncTo
            // 
            this.textBoxSyncTo.Location = new System.Drawing.Point(136, 65);
            this.textBoxSyncTo.Name = "textBoxSyncTo";
            this.textBoxSyncTo.Size = new System.Drawing.Size(341, 21);
            this.textBoxSyncTo.TabIndex = 7;
            // 
            // buttonBrowseTo
            // 
            this.buttonBrowseTo.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowseTo.Location = new System.Drawing.Point(484, 65);
            this.buttonBrowseTo.Name = "buttonBrowseTo";
            this.buttonBrowseTo.Size = new System.Drawing.Size(32, 18);
            this.buttonBrowseTo.TabIndex = 8;
            this.buttonBrowseTo.Text = "...";
            this.buttonBrowseTo.UseVisualStyleBackColor = true;
            this.buttonBrowseTo.Click += new System.EventHandler(this.buttonBrowseTo_Click);
            // 
            // checkBoxIncludeSubs
            // 
            this.checkBoxIncludeSubs.AutoSize = true;
            this.checkBoxIncludeSubs.Checked = true;
            this.checkBoxIncludeSubs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIncludeSubs.Location = new System.Drawing.Point(16, 138);
            this.checkBoxIncludeSubs.Name = "checkBoxIncludeSubs";
            this.checkBoxIncludeSubs.Size = new System.Drawing.Size(138, 16);
            this.checkBoxIncludeSubs.TabIndex = 13;
            this.checkBoxIncludeSubs.Text = "Include sub-folders";
            this.checkBoxIncludeSubs.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(184, 487);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 21);
            this.buttonOK.TabIndex = 16;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(266, 487);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 21);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxRemoveAfterSync
            // 
            this.checkBoxRemoveAfterSync.AutoSize = true;
            this.checkBoxRemoveAfterSync.Location = new System.Drawing.Point(16, 180);
            this.checkBoxRemoveAfterSync.Name = "checkBoxRemoveAfterSync";
            this.checkBoxRemoveAfterSync.Size = new System.Drawing.Size(228, 16);
            this.checkBoxRemoveAfterSync.TabIndex = 15;
            this.checkBoxRemoveAfterSync.Text = "Remove \"FROM\" files after synching";
            this.checkBoxRemoveAfterSync.UseVisualStyleBackColor = true;
            this.checkBoxRemoveAfterSync.Visible = false;
            // 
            // checkBoxBackupBeforeSync
            // 
            this.checkBoxBackupBeforeSync.AutoSize = true;
            this.checkBoxBackupBeforeSync.Location = new System.Drawing.Point(16, 160);
            this.checkBoxBackupBeforeSync.Name = "checkBoxBackupBeforeSync";
            this.checkBoxBackupBeforeSync.Size = new System.Drawing.Size(228, 16);
            this.checkBoxBackupBeforeSync.TabIndex = 14;
            this.checkBoxBackupBeforeSync.Text = "Backup \"TO\" files before replacing";
            this.checkBoxBackupBeforeSync.UseVisualStyleBackColor = true;
            this.checkBoxBackupBeforeSync.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Backup folder";
            // 
            // textBoxBackupFolder
            // 
            this.textBoxBackupFolder.Location = new System.Drawing.Point(136, 90);
            this.textBoxBackupFolder.Name = "textBoxBackupFolder";
            this.textBoxBackupFolder.Size = new System.Drawing.Size(341, 21);
            this.textBoxBackupFolder.TabIndex = 10;
            // 
            // buttonBrowseBackup
            // 
            this.buttonBrowseBackup.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowseBackup.Location = new System.Drawing.Point(484, 90);
            this.buttonBrowseBackup.Name = "buttonBrowseBackup";
            this.buttonBrowseBackup.Size = new System.Drawing.Size(32, 18);
            this.buttonBrowseBackup.TabIndex = 11;
            this.buttonBrowseBackup.Text = "...";
            this.buttonBrowseBackup.UseVisualStyleBackColor = true;
            this.buttonBrowseBackup.Click += new System.EventHandler(this.buttonBrowseBackup_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sync item name";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(136, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(180, 21);
            this.textBoxName.TabIndex = 1;
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Checked = true;
            this.checkBoxEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnable.Location = new System.Drawing.Point(16, 117);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(60, 16);
            this.checkBoxEnable.TabIndex = 12;
            this.checkBoxEnable.Text = "Enable";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            // 
            // labelHoverList
            // 
            this.labelHoverList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelHoverList.Cursor = System.Windows.Forms.Cursors.Cross;
            this.labelHoverList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHoverList.Location = new System.Drawing.Point(323, 12);
            this.labelHoverList.Name = "labelHoverList";
            this.labelHoverList.Size = new System.Drawing.Size(154, 18);
            this.labelHoverList.TabIndex = 2;
            this.labelHoverList.Text = "Hover Here For Current List";
            this.labelHoverList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelHoverList.MouseEnter += new System.EventHandler(this.labelHoverList_MouseEnter);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(280, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(236, 37);
            this.label5.TabIndex = 18;
            this.label5.Text = "All folder specified on this form MUST already  exist before attempting to run th" +
    "e service.";
            // 
            // checkBoxMakeFilesWritable
            // 
            this.checkBoxMakeFilesWritable.AutoSize = true;
            this.checkBoxMakeFilesWritable.Location = new System.Drawing.Point(16, 202);
            this.checkBoxMakeFilesWritable.Name = "checkBoxMakeFilesWritable";
            this.checkBoxMakeFilesWritable.Size = new System.Drawing.Size(162, 16);
            this.checkBoxMakeFilesWritable.TabIndex = 15;
            this.checkBoxMakeFilesWritable.Text = "Make the files writable";
            this.checkBoxMakeFilesWritable.UseVisualStyleBackColor = true;
            // 
            // checkBoxLck
            // 
            this.checkBoxLck.AutoSize = true;
            this.checkBoxLck.Location = new System.Drawing.Point(15, 321);
            this.checkBoxLck.Name = "checkBoxLck";
            this.checkBoxLck.Size = new System.Drawing.Size(282, 16);
            this.checkBoxLck.TabIndex = 15;
            this.checkBoxLck.Text = "Special treatment for files locked by vault";
            this.checkBoxLck.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Delete these";
            // 
            // textBoxDeletedDirOrFile
            // 
            this.textBoxDeletedDirOrFile.Location = new System.Drawing.Point(136, 222);
            this.textBoxDeletedDirOrFile.Name = "textBoxDeletedDirOrFile";
            this.textBoxDeletedDirOrFile.Size = new System.Drawing.Size(341, 21);
            this.textBoxDeletedDirOrFile.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(167, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "Exclude sub path from input";
            // 
            // textBoxExcludeFile
            // 
            this.textBoxExcludeFile.Location = new System.Drawing.Point(172, 247);
            this.textBoxExcludeFile.Name = "textBoxExcludeFile";
            this.textBoxExcludeFile.Size = new System.Drawing.Size(305, 21);
            this.textBoxExcludeFile.TabIndex = 20;
            this.textBoxExcludeFile.Text = "Yucheng Printer System.docx";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 273);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "Run these file patterns";
            // 
            // textBoxRunFile
            // 
            this.textBoxRunFile.Location = new System.Drawing.Point(136, 270);
            this.textBoxRunFile.Name = "textBoxRunFile";
            this.textBoxRunFile.Size = new System.Drawing.Size(341, 21);
            this.textBoxRunFile.TabIndex = 22;
            // 
            // checkBoxForceDownload
            // 
            this.checkBoxForceDownload.AutoSize = true;
            this.checkBoxForceDownload.Location = new System.Drawing.Point(16, 298);
            this.checkBoxForceDownload.Name = "checkBoxForceDownload";
            this.checkBoxForceDownload.Size = new System.Drawing.Size(108, 16);
            this.checkBoxForceDownload.TabIndex = 23;
            this.checkBoxForceDownload.Text = "Force Download";
            this.checkBoxForceDownload.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 346);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "Pattern";
            // 
            // textBoxPattern
            // 
            this.textBoxPattern.Location = new System.Drawing.Point(136, 343);
            this.textBoxPattern.Name = "textBoxPattern";
            this.textBoxPattern.Size = new System.Drawing.Size(341, 21);
            this.textBoxPattern.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 391);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "Folder Mapping";
            // 
            // textBoxFolderMapping
            // 
            this.textBoxFolderMapping.Location = new System.Drawing.Point(137, 389);
            this.textBoxFolderMapping.Name = "textBoxFolderMapping";
            this.textBoxFolderMapping.Size = new System.Drawing.Size(341, 21);
            this.textBoxFolderMapping.TabIndex = 10;
            // 
            // checkBoxMirror
            // 
            this.checkBoxMirror.AutoSize = true;
            this.checkBoxMirror.Location = new System.Drawing.Point(15, 367);
            this.checkBoxMirror.Name = "checkBoxMirror";
            this.checkBoxMirror.Size = new System.Drawing.Size(60, 16);
            this.checkBoxMirror.TabIndex = 23;
            this.checkBoxMirror.Text = "Mirror";
            this.checkBoxMirror.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 416);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "States to Download";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 440);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 12);
            this.label12.TabIndex = 25;
            this.label12.Text = "Invalid States";
            // 
            // textBoxState
            // 
            this.textBoxState.Location = new System.Drawing.Point(137, 414);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Size = new System.Drawing.Size(340, 21);
            this.textBoxState.TabIndex = 26;
            // 
            // textBoxInvalidState
            // 
            this.textBoxInvalidState.Location = new System.Drawing.Point(137, 438);
            this.textBoxInvalidState.Name = "textBoxInvalidState";
            this.textBoxInvalidState.Size = new System.Drawing.Size(340, 21);
            this.textBoxInvalidState.TabIndex = 27;
            // 
            // btnChangeFileState
            // 
            this.btnChangeFileState.Location = new System.Drawing.Point(38, 487);
            this.btnChangeFileState.Name = "btnChangeFileState";
            this.btnChangeFileState.Size = new System.Drawing.Size(93, 21);
            this.btnChangeFileState.TabIndex = 28;
            this.btnChangeFileState.Text = "Change state";
            this.btnChangeFileState.UseVisualStyleBackColor = true;
            this.btnChangeFileState.Click += new System.EventHandler(this.btnChangeFileState_Click);
            // 
            // FormAddSyncItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 520);
            this.Controls.Add(this.btnChangeFileState);
            this.Controls.Add(this.textBoxInvalidState);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.checkBoxMirror);
            this.Controls.Add(this.checkBoxForceDownload);
            this.Controls.Add(this.textBoxRunFile);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxExcludeFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelHoverList);
            this.Controls.Add(this.checkBoxEnable);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonBrowseBackup);
            this.Controls.Add(this.textBoxFolderMapping);
            this.Controls.Add(this.textBoxPattern);
            this.Controls.Add(this.textBoxDeletedDirOrFile);
            this.Controls.Add(this.textBoxBackupFolder);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxBackupBeforeSync);
            this.Controls.Add(this.checkBoxLck);
            this.Controls.Add(this.checkBoxMakeFilesWritable);
            this.Controls.Add(this.checkBoxRemoveAfterSync);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxIncludeSubs);
            this.Controls.Add(this.buttonBrowseTo);
            this.Controls.Add(this.textBoxSyncTo);
            this.Controls.Add(this.buttonBrowseFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSyncFrom);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddSyncItem";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FormAddSyncItem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddSyncItem_FormClosing);
            this.Load += new System.EventHandler(this.FormAddSyncItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxSyncFrom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonBrowseFrom;
		private System.Windows.Forms.TextBox textBoxSyncTo;
		private System.Windows.Forms.Button buttonBrowseTo;
		private System.Windows.Forms.CheckBox checkBoxIncludeSubs;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.CheckBox checkBoxRemoveAfterSync;
		private System.Windows.Forms.CheckBox checkBoxBackupBeforeSync;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxBackupFolder;
		private System.Windows.Forms.Button buttonBrowseBackup;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.CheckBox checkBoxEnable;
		private System.Windows.Forms.Label labelHoverList;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxMakeFilesWritable;
        private System.Windows.Forms.CheckBox checkBoxLck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDeletedDirOrFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxExcludeFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxRunFile;
        private System.Windows.Forms.CheckBox checkBoxForceDownload;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPattern;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxFolderMapping;
        private System.Windows.Forms.CheckBox checkBoxMirror;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.TextBox textBoxInvalidState;
        private System.Windows.Forms.Button btnChangeFileState;
    }
}