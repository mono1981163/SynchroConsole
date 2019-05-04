namespace SynchroConsole
{
	partial class FormMainConsole
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
			this.buttonClose = new System.Windows.Forms.Button();
			this.listBoxActivity = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelConnected = new System.Windows.Forms.Label();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClose.Location = new System.Drawing.Point(12, 247);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// listBoxActivity
			// 
			this.listBoxActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxActivity.FormattingEnabled = true;
			this.listBoxActivity.Location = new System.Drawing.Point(13, 19);
			this.listBoxActivity.Name = "listBoxActivity";
			this.listBoxActivity.Size = new System.Drawing.Size(599, 212);
			this.listBoxActivity.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Activity";
			// 
			// labelConnected
			// 
			this.labelConnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelConnected.Location = new System.Drawing.Point(479, 3);
			this.labelConnected.Name = "labelConnected";
			this.labelConnected.Size = new System.Drawing.Size(136, 13);
			this.labelConnected.TabIndex = 11;
			this.labelConnected.Text = "Connected";
			this.labelConnected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonStart
			// 
			this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStart.Location = new System.Drawing.Point(153, 247);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(75, 23);
			this.buttonStart.TabIndex = 12;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonSyncStart_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStop.Location = new System.Drawing.Point(235, 247);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(75, 23);
			this.buttonStop.TabIndex = 13;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonSyncStop_Click);
			// 
			// FormMainConsole
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 282);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.labelConnected);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBoxActivity);
			this.Controls.Add(this.buttonClose);
			this.MinimumSize = new System.Drawing.Size(640, 320);
			this.Name = "FormMainConsole";
			this.Text = "Synchronicity Test Console";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.ListBox listBoxActivity;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelConnected;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonStop;
	}
}

