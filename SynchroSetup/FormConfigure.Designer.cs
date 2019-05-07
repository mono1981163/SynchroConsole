namespace SynchroSetup
{
	partial class FormConfigure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfigure));
            this.listViewSyncItems = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSyncTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSyncFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSyncSubs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnBackup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDelete = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSyncMinutes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxNormalize = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewSyncItems
            // 
            this.listViewSyncItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnSyncTo,
            this.columnSyncFrom,
            this.columnSyncSubs,
            this.columnBackup,
            this.columnDelete});
            this.listViewSyncItems.FullRowSelect = true;
            this.listViewSyncItems.GridLines = true;
            this.listViewSyncItems.HideSelection = false;
            this.listViewSyncItems.Location = new System.Drawing.Point(9, 62);
            this.listViewSyncItems.MultiSelect = false;
            this.listViewSyncItems.Name = "listViewSyncItems";
            this.listViewSyncItems.ShowGroups = false;
            this.listViewSyncItems.Size = new System.Drawing.Size(734, 159);
            this.listViewSyncItems.TabIndex = 9;
            this.listViewSyncItems.UseCompatibleStateImageBehavior = false;
            this.listViewSyncItems.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            this.columnName.Text = "Sync Name";
            this.columnName.Width = 100;
            // 
            // columnSyncTo
            // 
            this.columnSyncTo.Text = "SyncTo";
            this.columnSyncTo.Width = 225;
            // 
            // columnSyncFrom
            // 
            this.columnSyncFrom.Text = "Sync From";
            this.columnSyncFrom.Width = 225;
            // 
            // columnSyncSubs
            // 
            this.columnSyncSubs.Text = "+Subs";
            // 
            // columnBackup
            // 
            this.columnBackup.Text = "Backup";
            // 
            // columnDelete
            // 
            this.columnDelete.Text = "Delete";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sync Items";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(258, 227);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "Add...";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAddSync_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(339, 227);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 12;
            this.buttonEdit.Text = "Edit...";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEditSync_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(420, 227);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 13;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDeleteSync_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sync every";
            // 
            // textBoxSyncMinutes
            // 
            this.textBoxSyncMinutes.Location = new System.Drawing.Point(69, 6);
            this.textBoxSyncMinutes.Name = "textBoxSyncMinutes";
            this.textBoxSyncMinutes.Size = new System.Drawing.Size(25, 20);
            this.textBoxSyncMinutes.TabIndex = 1;
            this.textBoxSyncMinutes.TextChanged += new System.EventHandler(this.textBoxSyncMinutes_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "(5-60) minutes";
            // 
            // checkBoxNormalize
            // 
            this.checkBoxNormalize.AutoSize = true;
            this.checkBoxNormalize.Location = new System.Drawing.Point(212, 8);
            this.checkBoxNormalize.Name = "checkBoxNormalize";
            this.checkBoxNormalize.Size = new System.Drawing.Size(94, 17);
            this.checkBoxNormalize.TabIndex = 3;
            this.checkBoxNormalize.Text = "Normalize time";
            this.checkBoxNormalize.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(9, 227);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 16;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormConfigure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 256);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxNormalize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSyncMinutes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listViewSyncItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonDelete);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigure";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listViewSyncItems;
		private System.Windows.Forms.ColumnHeader columnName;
		private System.Windows.Forms.ColumnHeader columnSyncTo;
		private System.Windows.Forms.ColumnHeader columnSyncFrom;
		private System.Windows.Forms.ColumnHeader columnSyncSubs;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxSyncMinutes;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxNormalize;
		private System.Windows.Forms.ColumnHeader columnBackup;
		private System.Windows.Forms.ColumnHeader columnDelete;
		private System.Windows.Forms.Button buttonOK;
	}
}

