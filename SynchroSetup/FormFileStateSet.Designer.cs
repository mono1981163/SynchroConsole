namespace SynchroSetup
{
    partial class FormFileStateSet
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
            this.listViewFileState = new System.Windows.Forms.ListView();
            this.lvFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvReleaseState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvWorkState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvInitState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.filePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.releaseState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.initState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewFileState
            // 
            this.listViewFileState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFileState.CheckBoxes = true;
            this.listViewFileState.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvFilePath,
            this.lvReleaseState,
            this.lvWorkState,
            this.lvInitState});
            this.listViewFileState.Location = new System.Drawing.Point(12, 271);
            this.listViewFileState.Name = "listViewFileState";
            this.listViewFileState.Size = new System.Drawing.Size(424, 165);
            this.listViewFileState.TabIndex = 0;
            this.listViewFileState.UseCompatibleStateImageBehavior = false;
            this.listViewFileState.View = System.Windows.Forms.View.Details;
            // 
            // lvFilePath
            // 
            this.lvFilePath.Text = "File Path";
            this.lvFilePath.Width = 240;
            // 
            // lvReleaseState
            // 
            this.lvReleaseState.Text = "Release";
            this.lvReleaseState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lvWorkState
            // 
            this.lvWorkState.Text = "Work";
            this.lvWorkState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lvInitState
            // 
            this.lvInitState.Text = "Init";
            this.lvInitState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(326, 451);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filePath,
            this.releaseState,
            this.workState,
            this.initState});
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(423, 233);
            this.dataGridView1.TabIndex = 2;
            // 
            // filePath
            // 
            this.filePath.HeaderText = "Column1";
            this.filePath.Name = "filePath";
            // 
            // releaseState
            // 
            this.releaseState.HeaderText = "Column2";
            this.releaseState.Name = "releaseState";
            // 
            // workState
            // 
            this.workState.HeaderText = "Column3";
            this.workState.Name = "workState";
            // 
            // initState
            // 
            this.initState.HeaderText = "Column4";
            this.initState.Name = "initState";
            // 
            // FormFileStateSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 486);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.listViewFileState);
            this.Name = "FormFileStateSet";
            this.Text = "FormFileStateSet";
            this.Load += new System.EventHandler(this.FormFileStateSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewFileState;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.ColumnHeader lvFilePath;
        public System.Windows.Forms.ColumnHeader lvReleaseState;
        private System.Windows.Forms.ColumnHeader lvWorkState;
        private System.Windows.Forms.ColumnHeader lvInitState;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn releaseState;
        private System.Windows.Forms.DataGridViewTextBoxColumn workState;
        private System.Windows.Forms.DataGridViewTextBoxColumn initState;
    }
}