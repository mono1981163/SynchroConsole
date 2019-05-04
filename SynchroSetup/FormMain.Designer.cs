namespace SynchroSetup
{
	partial class FormMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.listBoxActivity = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonConfigure = new System.Windows.Forms.Button();
			this.buttonMinimize = new System.Windows.Forms.Button();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.SuspendLayout();
			// 
			// listBoxActivity
			// 
			this.listBoxActivity.FormattingEnabled = true;
			this.listBoxActivity.Location = new System.Drawing.Point(13, 27);
			this.listBoxActivity.Name = "listBoxActivity";
			this.listBoxActivity.Size = new System.Drawing.Size(646, 329);
			this.listBoxActivity.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Activity for last 24 Hours";
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(12, 363);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonConfigure
			// 
			this.buttonConfigure.Location = new System.Drawing.Point(341, 363);
			this.buttonConfigure.Name = "buttonConfigure";
			this.buttonConfigure.Size = new System.Drawing.Size(75, 23);
			this.buttonConfigure.TabIndex = 3;
			this.buttonConfigure.Text = "Configure";
			this.buttonConfigure.UseVisualStyleBackColor = true;
			this.buttonConfigure.Click += new System.EventHandler(this.buttonConfigure_Click);
			// 
			// buttonMinimize
			// 
			this.buttonMinimize.Location = new System.Drawing.Point(255, 363);
			this.buttonMinimize.Name = "buttonMinimize";
			this.buttonMinimize.Size = new System.Drawing.Size(75, 23);
			this.buttonMinimize.TabIndex = 4;
			this.buttonMinimize.Text = "Minimize";
			this.buttonMinimize.UseVisualStyleBackColor = true;
			this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(671, 392);
			this.Controls.Add(this.buttonMinimize);
			this.Controls.Add(this.buttonConfigure);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBoxActivity);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.ShowInTaskbar = false;
			this.Text = "FormMain";
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxActivity;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonConfigure;
		private System.Windows.Forms.Button buttonMinimize;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
	}
}