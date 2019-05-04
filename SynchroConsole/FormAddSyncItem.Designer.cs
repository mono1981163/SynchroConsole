namespace SynchroConsole
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
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonBrowseBackup = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sync FROM this folder:";
			// 
			// textBoxSyncFrom
			// 
			this.textBoxSyncFrom.Location = new System.Drawing.Point(136, 8);
			this.textBoxSyncFrom.Name = "textBoxSyncFrom";
			this.textBoxSyncFrom.Size = new System.Drawing.Size(341, 20);
			this.textBoxSyncFrom.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Sync TO this folder:";
			// 
			// buttonBrowseFrom
			// 
			this.buttonBrowseFrom.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonBrowseFrom.Location = new System.Drawing.Point(484, 8);
			this.buttonBrowseFrom.Name = "buttonBrowseFrom";
			this.buttonBrowseFrom.Size = new System.Drawing.Size(32, 20);
			this.buttonBrowseFrom.TabIndex = 3;
			this.buttonBrowseFrom.Text = "...";
			this.buttonBrowseFrom.UseVisualStyleBackColor = true;
			this.buttonBrowseFrom.Click += new System.EventHandler(this.buttonBrowseFrom_Click);
			// 
			// textBoxSyncTo
			// 
			this.textBoxSyncTo.Location = new System.Drawing.Point(136, 35);
			this.textBoxSyncTo.Name = "textBoxSyncTo";
			this.textBoxSyncTo.Size = new System.Drawing.Size(341, 20);
			this.textBoxSyncTo.TabIndex = 4;
			// 
			// buttonBrowseTo
			// 
			this.buttonBrowseTo.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonBrowseTo.Location = new System.Drawing.Point(484, 35);
			this.buttonBrowseTo.Name = "buttonBrowseTo";
			this.buttonBrowseTo.Size = new System.Drawing.Size(32, 20);
			this.buttonBrowseTo.TabIndex = 5;
			this.buttonBrowseTo.Text = "...";
			this.buttonBrowseTo.UseVisualStyleBackColor = true;
			this.buttonBrowseTo.Click += new System.EventHandler(this.buttonBrowseTo_Click);
			// 
			// checkBoxIncludeSubs
			// 
			this.checkBoxIncludeSubs.AutoSize = true;
			this.checkBoxIncludeSubs.Checked = true;
			this.checkBoxIncludeSubs.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxIncludeSubs.Location = new System.Drawing.Point(16, 72);
			this.checkBoxIncludeSubs.Name = "checkBoxIncludeSubs";
			this.checkBoxIncludeSubs.Size = new System.Drawing.Size(115, 17);
			this.checkBoxIncludeSubs.TabIndex = 6;
			this.checkBoxIncludeSubs.Text = "Include sub-folders";
			this.checkBoxIncludeSubs.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(186, 181);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(268, 181);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// checkBoxRemoveAfterSync
			// 
			this.checkBoxRemoveAfterSync.AutoSize = true;
			this.checkBoxRemoveAfterSync.Location = new System.Drawing.Point(16, 137);
			this.checkBoxRemoveAfterSync.Name = "checkBoxRemoveAfterSync";
			this.checkBoxRemoveAfterSync.Size = new System.Drawing.Size(200, 17);
			this.checkBoxRemoveAfterSync.TabIndex = 9;
			this.checkBoxRemoveAfterSync.Text = "Remove \"FROM\" files after synching";
			this.checkBoxRemoveAfterSync.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(16, 95);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(191, 17);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "Backup \"TO\" files before replacing";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(38, 117);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Backup folder";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(136, 114);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(341, 20);
			this.textBox1.TabIndex = 12;
			// 
			// buttonBrowseBackup
			// 
			this.buttonBrowseBackup.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonBrowseBackup.Location = new System.Drawing.Point(484, 114);
			this.buttonBrowseBackup.Name = "buttonBrowseBackup";
			this.buttonBrowseBackup.Size = new System.Drawing.Size(32, 20);
			this.buttonBrowseBackup.TabIndex = 13;
			this.buttonBrowseBackup.Text = "...";
			this.buttonBrowseBackup.UseVisualStyleBackColor = true;
			this.buttonBrowseBackup.Click += new System.EventHandler(this.buttonBrowseBackup_Click);
			// 
			// FormAddSyncItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 211);
			this.Controls.Add(this.buttonBrowseBackup);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkBox1);
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
			this.Name = "FormAddSyncItem";
			this.Text = "FormAddSyncItem";
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
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button buttonBrowseBackup;
	}
}