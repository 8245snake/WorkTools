using System;
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

namespace Specificker
{
    public partial class frmMain : Form
    {
        private Configuration _config;

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

        private void btnRef1_Click(object sender, EventArgs e)
        {
            string input = "";
            if (opbDirectory.Checked)
            {
                input = getFolderFromDialog();
            }
            else
            {
                input = getFilePathFromDialog();
            }
            if (input != "")
            {
                txtInput1.Text = input;
            }
            btnExec.Enabled = isTextValidatedOK();
        }

        private void btnRef2_Click(object sender, EventArgs e)
        {
            string input = "";
            if (opbDirectory.Checked)
            {
                input = getFolderFromDialog();
            }
            else
            {
                input = getFilePathFromDialog();
            }
            if (input != "")
            {
                txtInput2.Text = input;
            }
            btnExec.Enabled = isTextValidatedOK();
        }

        private void btnOutputRef_Click(object sender, EventArgs e)
        {
            string output = getFolderFromDialog();
            if (output == "")
            {
                return;
            }
            txtOutput.Text = output;
            btnExec.Enabled = isTextValidatedOK();
        }

        private string getFilePathFromDialog()
        {
            string filename = "";
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = @"C:\";
                dialog.Filter = "iniファイル(*.ini)|*.ini|すべてのファイル(*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Title = "開くファイルを選択してください";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filename = dialog.FileName;
                }
            }
            return filename;
        }

        private string getFolderFromDialog()
        {
            string selectedPath = "";
            using (FolderBrowserDialog fbDialog = new FolderBrowserDialog())
            {
                fbDialog.Description = "開くフォルダを選択してください";
                fbDialog.SelectedPath = @"C:";
                // 「新しいフォルダーの作成する」ボタンを表示する
                fbDialog.ShowNewFolderButton = true;
                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = fbDialog.SelectedPath;
                }
            }
            return selectedPath;
        }

        private void optionButton_CheckedChanged(object sender, EventArgs e)
        {
            txtInput1.Text = "";
            txtInput2.Text = "";
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

            bgworkerMain.RunWorkerAsync(txtOutput.Text);

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
            string saveFolder = (string)e.Argument;
            Extracter extracter = new Extracter(txtInput1.Text, txtInput2.Text, saveFolder);
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

        private void txtInput1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
