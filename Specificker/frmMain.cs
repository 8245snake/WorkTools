﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using CustomControls;

namespace Specificker
{
    public partial class frmMain : Form
    {
        

        private Configuration _config;

        private string _Input1 = "";
        private string _Input2 = "";
        private string _OutputDir = "";
        private Extracter.ExecOperationMode _Mode;


        public frmMain()
        {
            InitializeComponent();
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        private void setDefaultCheck()
        {
            string[] allKeys = _config.AppSettings.Settings.AllKeys;
            string key = "DEFAULT_SELECTED_INDEX";
            if (allKeys.Contains(key))
            {
                string index = _config.AppSettings.Settings[key].Value;
                if (index == "1")
                {
                    opbDirectory.Checked = true;
                }
                else
                {
                    opbFile.Checked = true;
                }
            }
            else
            {
                opbFile.Checked = true;
            }
        }


        private void txtInput1_DragDrop(object sender, DragEventArgs e)
        {
            txtInput1.Text = getFilePathFromDragDrop(e);
            btnExec.Enabled = isTextValidatedOK();
        }

        private void txtInput2_DragDrop(object sender, DragEventArgs e)
        {
            txtInput2.Text = getFilePathFromDragDrop(e);
            btnExec.Enabled = isTextValidatedOK();
        }

        private void txtOutput_DragDrop(object sender, DragEventArgs e)
        {
            txtOutput.Text = getFilePathFromDragDrop(e);
            btnExec.Enabled = isTextValidatedOK();
        }

        private void enableAllControl(bool enabled)
        {
            enableChildControls(this, enabled);
        }

        /// <summary>
        /// 指定されたコントロールの子要素を再帰的に全てenabledを設定する
        /// </summary>
        /// <param name="parent">コントロール</param>
        /// <param name="enabled">有効/無効</param>
        private void enableChildControls(Control parent, bool enabled)
        {
            if (parent.Controls.Count > 0)
            {
                foreach (Control control in parent.Controls)
                {
                    enableChildControls(control, enabled);
                }
            }
            else
            {
                switch (parent.GetType().Name)
                {
                    case "Button":
                    case "RadioButton":
                    case "TextBox":
                        parent.Enabled = enabled;
                        break;
                    default:
                        break;
                }
            }
        }

        private string getFilePathFromDragDrop(DragEventArgs e)
        {
            // ファイルが渡されていない場合
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return "";

            // ファイルは複数来る
            foreach (string filePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                // 先頭の１つで終わり
                return filePath + "";
            }
            return "";
        }

        private void txtInput_DragEnter(object sender, DragEventArgs e)
        {
            if (checkDropFileTyep(e))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void txtOutput_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private bool checkDropFileTyep(DragEventArgs e)
        {
            string path = getFilePathFromDragDrop(e);
            bool isDirectory = File.GetAttributes(path).HasFlag(FileAttributes.Directory);
            if (opbDirectory.Checked && isDirectory)
            {
                return true;
            }
            if (opbFile.Checked && !isDirectory)
            {
                return true;
            }
            return false;
        }

        private void optionButton_CheckedChanged(object sender, EventArgs e)
        {
            txtInput1.Text = "";
            txtInput2.Text = "";

            IODialogButton.PathType pathType = IODialogButton.PathType.FileInput;
            if (opbDirectory.Checked)
            {
                pathType = IODialogButton.PathType.FolderInput;
            }
            btnRef1.BindTextBox(txtInput1, pathType);
            btnRef2.BindTextBox(txtInput2, pathType);
            btnOutputRef.BindTextBox(txtOutput, pathType);

        }

        private bool isTextValidatedOK()
        {
            return (txtInput1.Text != "" && txtInput2.Text != "" && txtOutput.Text != "");
        }

        private void btnExec_Click(object sender, EventArgs e)
        {

            if (!isTextValidatedOK())
            {
                MessageBox.Show("入力値が足りません");
                return;
            }
            // 初期化
            pgbMain.Value = 0;
            lblProgress.Text = "";
            // 画面ロック
            enableAllControl(false);

            _Input1 = txtInput1.Text;
            _Input2 = txtInput2.Text;
            _OutputDir = txtOutput.Text;
            if (optAdd.Checked)
            {
                _Mode = Extracter.ExecOperationMode.Addition;
            }
            else if (optSub.Checked)
            {
                _Mode = Extracter.ExecOperationMode.Subtraction;
            }
            else if (optExclusive.Checked)
            {
                _Mode = Extracter.ExecOperationMode.Exclusion;
            }
            else
            {
                MessageBox.Show("入力値が足りません");
                return;
            }

            bgworkerMain.RunWorkerAsync();

        }

        private void txtInput_Validated(object sender, EventArgs e)
        {
            btnExec.Enabled = isTextValidatedOK();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            // 実行ボタン制御
            btnExec.Enabled = isTextValidatedOK();
            // デフォルトチェック
            setDefaultCheck();
        }

        private void bgworkerMain_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            bw.ReportProgress(0);

            Extracter extracter = new Extracter(_Input1, _Input2, _OutputDir, _Mode);
            if (opbDirectory.Checked)
            {
                extracter.ExtractFolder(bw);
            }
            else
            {
                extracter.ExtractFile(bw);
            }
        }

        private void bgworkerMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgbMain.Value = e.ProgressPercentage;
            switch (pgbMain.Value)
            {
                case 0:
                    lblProgress.Text = "①を解析中";
                    break;
                case 25:
                    lblProgress.Text = "②を解析中";
                    break;
                case 50:
                    lblProgress.Text = "①と②の差分を抽出中";
                    break;
                case 75:
                    lblProgress.Text = "iniを出力中";
                    break;
                default:
                    break;
            }
        }

        private void bgworkerMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            enableAllControl(true);
            lblProgress.Text = "完了";
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            string tmp = txtInput1.Text;
            txtInput1.Text = txtInput2.Text;
            txtInput2.Text = tmp;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            this.Text = string.Format("Specificker (ver. {0})", ver.FileVersion);
            btnRef1.BindTextBox(txtInput1, IODialogButton.PathType.FolderInput);
            btnRef2.BindTextBox(txtInput2, IODialogButton.PathType.FolderInput);
            btnOutputRef.BindTextBox(txtOutput, IODialogButton.PathType.FolderInput);
        }
    }
}
