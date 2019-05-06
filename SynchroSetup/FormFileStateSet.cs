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
                    GroupBox groupBox = new GroupBox();
                    groupBox.FlatStyle = FlatStyle.Standard;

                    Label labelFilePath = new Label();
                    labelFilePath.Width = 200;
                    labelFilePath.Text = file.FileInfoObj.FullName;

                    RadioButton rbRelease = new RadioButton();
                    rbRelease.Text = "Release";
                    RadioButton rbWork = new RadioButton();
                    rbWork.Text = "Work";
                    RadioButton rbInit = new RadioButton();
                    rbInit.Text = "Init";
                 
                    groupBox.Width = 500;
                    rbRelease.Margin = new Padding(250, 30, 0, 0);
                    rbWork.Margin = new Padding(320, 30, 0, 0);
                    rbInit.Margin = new Padding(350, 30, 0, 0);
                    groupBox.Controls.Add(labelFilePath);
                    groupBox.Controls.Add(rbRelease);
                    groupBox.Controls.Add(rbWork);
                    groupBox.Controls.Add(rbInit);
                    
                    flowLayoutPanel.Controls.Add(groupBox);
                }
                
            }
           
        }
    }
}
