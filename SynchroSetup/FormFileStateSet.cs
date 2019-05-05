using SynchroLib;
using System;
using System.Collections.Generic;
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
        }

        private void FormFileStateSet_Load(object sender, EventArgs e)
        {
            if(Globals.SourceDirectoryPath != null && Globals.TargetDirectoryPath != null)
            {
                newList = new FileInfoList(Globals.SourceDirectoryPath, Globals.TargetDirectoryPath);
                newList.GetFiles(Globals.SourceDirectoryPath, true);

                foreach(FileInfoEx file in newList)
                {
                    CheckBox ckBox = new CheckBox();
                    List<CheckBox> checkBoxFileState = new List<CheckBox>();
                    //for (int i = 0; i < 3; i++)
                    //    if (i == file.FileState)
                    //        checkBoxFileState[i].Checked = true;
                    dataGridView1.Rows.Add(file.FileInfoObj.FullName, ckBox, ckBox, ckBox);

                    //DataGridViewCheckBoxColumn filePath = new DataGridViewCheckBoxColumn();
                    //filePath.HeaderText = file.FileName;
                    //dataGridView1.Columns.Add(filePath);

                    //DataGridViewCheckBoxColumn checkRelease = new DataGridViewCheckBoxColumn();
                    //checkRelease.ValueType = typeof(bool);
                    //checkRelease.Name = "CheckRelease";
                    //checkRelease.HeaderText = "Release";
                    //dataGridView1.Columns.Add(checkRelease);

                    //DataGridViewCheckBoxColumn checkWork = new DataGridViewCheckBoxColumn();
                    //checkWork.ValueType = typeof(bool);
                    //checkWork.Name = "CheckWork";
                    //checkWork.HeaderText = "Work";
                    //dataGridView1.Columns.Add(checkWork);

                    //DataGridViewCheckBoxColumn checkInit = new DataGridViewCheckBoxColumn();
                    //checkInit.ValueType = typeof(bool);
                    //checkInit.Name = "CheckInit";
                    //checkInit.HeaderText = "Init";
                    //dataGridView1.Columns.Add(checkInit);

                    //ListViewItem fileStateItem = new ListViewItem();
                    //fileStateItem.Text = file.FileInfoObj.FullName;
                    ////fileStateItem.SubItems.Add(file.FileInfoObj.FullName);
                    //fileStateItem.SubItems.Add("R");
                    //fileStateItem.Checked = true;
                    //fileStateItem.SubItems.Add("W");
                    //fileStateItem.SubItems.Add("I");
                    //listViewFileState.Items.Add(fileStateItem);
                }
                
            }
           
        }
    }
}
