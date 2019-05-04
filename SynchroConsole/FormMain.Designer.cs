namespace SynchroConsole
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
			this.buttonDone = new System.Windows.Forms.Button();
			this.listBoxActivity = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonDone
			// 
			this.buttonDone.Location = new System.Drawing.Point(349, 428);
			this.buttonDone.Name = "buttonDone";
			this.buttonDone.Size = new System.Drawing.Size(75, 23);
			this.buttonDone.TabIndex = 5;
			this.buttonDone.Text = "Done";
			this.buttonDone.UseVisualStyleBackColor = true;
			this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
			// 
			// listBoxActivity
			// 
			this.listBoxActivity.FormattingEnabled = true;
			this.listBoxActivity.Location = new System.Drawing.Point(13, 19);
			this.listBoxActivity.Name = "listBoxActivity";
			this.listBoxActivity.Size = new System.Drawing.Size(755, 394);
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
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(786, 463);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBoxActivity);
			this.Controls.Add(this.buttonDone);
			this.Name = "FormMain";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonDone;
		private System.Windows.Forms.ListBox listBoxActivity;
		private System.Windows.Forms.Label label2;
	}
}

