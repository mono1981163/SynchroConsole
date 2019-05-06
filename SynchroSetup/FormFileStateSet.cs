using SynchroLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SynchroSetup
{
    public partial class FormFileStateSet : Form
    {
        FileInfoList newList;
        List<FileInfoEx> newerList;
        public SyncItem SyncParent { get; set; }

        

        protected FileCompareFlags m_compareFlags = FileCompareFlags.UnrootedName |
                                                    FileCompareFlags.LastWrite |
                                                    FileCompareFlags.Length;
        public FormFileStateSet()
        {
            InitializeComponent();

            this.SuspendLayout();

            // Create data for combobox
            StringCollection grades = new StringCollection();
            grades.AddRange(new string[] { FileStates.Init.ToString(), FileStates.WorkInProcess.ToString(), FileStates.Release.ToString() });

            // Set the combobox
            this.listViewMain.AddComboBoxCell(-1, 1, grades);

            if (Globals.SourceDirectoryPath != null && Globals.TargetDirectoryPath != null)
            {
                newList = new FileInfoList(Globals.SourceDirectoryPath, Globals.TargetDirectoryPath);
                newList.GetFiles(Globals.SourceDirectoryPath, true);

                var row = 0;
                foreach (FileInfoEx file in newList)
                {
                    var lvItem = new ListViewItem(file.FileName);
                    lvItem.SubItems.Add(((FileStates)(file.FileState)).ToString());

                    this.listViewMain.Items.Add(lvItem);

                    row++;
                }

            }

            this.ResumeLayout();
        }

        private void FormFileStateSet_Load(object sender, EventArgs e)
        {
            

            

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
