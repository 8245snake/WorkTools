namespace Specificker
{
    partial class frmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.opbDirectory = new System.Windows.Forms.RadioButton();
            this.opbFile = new System.Windows.Forms.RadioButton();
            this.bgworkerMain = new System.ComponentModel.BackgroundWorker();
            this.txtInput1 = new System.Windows.Forms.TextBox();
            this.btnRef1 = new System.Windows.Forms.Button();
            this.btnRef2 = new System.Windows.Forms.Button();
            this.txtInput2 = new System.Windows.Forms.TextBox();
            this.btnExec = new System.Windows.Forms.Button();
            this.pgbMain = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOutputRef = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.opbDirectory);
            this.groupBox1.Controls.Add(this.opbFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(164, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // opbDirectory
            // 
            this.opbDirectory.AutoSize = true;
            this.opbDirectory.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opbDirectory.Location = new System.Drawing.Point(6, 55);
            this.opbDirectory.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.opbDirectory.Name = "opbDirectory";
            this.opbDirectory.Size = new System.Drawing.Size(118, 24);
            this.opbDirectory.TabIndex = 1;
            this.opbDirectory.TabStop = true;
            this.opbDirectory.Text = "フォルダを比較";
            this.opbDirectory.UseVisualStyleBackColor = true;
            this.opbDirectory.CheckedChanged += new System.EventHandler(this.optionButton_CheckedChanged);
            // 
            // opbFile
            // 
            this.opbFile.AutoSize = true;
            this.opbFile.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.opbFile.Location = new System.Drawing.Point(6, 18);
            this.opbFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.opbFile.Name = "opbFile";
            this.opbFile.Size = new System.Drawing.Size(116, 24);
            this.opbFile.TabIndex = 0;
            this.opbFile.TabStop = true;
            this.opbFile.Text = "ファイルを比較";
            this.opbFile.UseVisualStyleBackColor = true;
            this.opbFile.CheckedChanged += new System.EventHandler(this.optionButton_CheckedChanged);
            // 
            // bgworkerMain
            // 
            this.bgworkerMain.WorkerReportsProgress = true;
            this.bgworkerMain.WorkerSupportsCancellation = true;
            this.bgworkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworkerMain_DoWork);
            this.bgworkerMain.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworkerMain_ProgressChanged);
            this.bgworkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgworkerMain_RunWorkerCompleted);
            // 
            // txtInput1
            // 
            this.txtInput1.AllowDrop = true;
            this.txtInput1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput1.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this.txtInput1.Location = new System.Drawing.Point(12, 136);
            this.txtInput1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtInput1.Name = "txtInput1";
            this.txtInput1.Size = new System.Drawing.Size(293, 31);
            this.txtInput1.TabIndex = 1;
            this.txtInput1.TextChanged += new System.EventHandler(this.txtInput1_TextChanged);
            this.txtInput1.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtInput1_DragDrop);
            this.txtInput1.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtInput_DragEnter);
            this.txtInput1.Validated += new System.EventHandler(this.txtInput_Validated);
            // 
            // btnRef1
            // 
            this.btnRef1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRef1.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this.btnRef1.Location = new System.Drawing.Point(310, 137);
            this.btnRef1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRef1.Name = "btnRef1";
            this.btnRef1.Size = new System.Drawing.Size(79, 31);
            this.btnRef1.TabIndex = 2;
            this.btnRef1.Text = "参照";
            this.btnRef1.UseVisualStyleBackColor = true;
            this.btnRef1.Click += new System.EventHandler(this.btnRef1_Click);
            // 
            // btnRef2
            // 
            this.btnRef2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRef2.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this.btnRef2.Location = new System.Drawing.Point(310, 196);
            this.btnRef2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRef2.Name = "btnRef2";
            this.btnRef2.Size = new System.Drawing.Size(79, 31);
            this.btnRef2.TabIndex = 4;
            this.btnRef2.Text = "参照";
            this.btnRef2.UseVisualStyleBackColor = true;
            this.btnRef2.Click += new System.EventHandler(this.btnRef2_Click);
            // 
            // txtInput2
            // 
            this.txtInput2.AllowDrop = true;
            this.txtInput2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput2.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this.txtInput2.Location = new System.Drawing.Point(12, 195);
            this.txtInput2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtInput2.Name = "txtInput2";
            this.txtInput2.Size = new System.Drawing.Size(293, 31);
            this.txtInput2.TabIndex = 3;
            this.txtInput2.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtInput2_DragDrop);
            this.txtInput2.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtInput_DragEnter);
            this.txtInput2.Validated += new System.EventHandler(this.txtInput_Validated);
            // 
            // btnExec
            // 
            this.btnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExec.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this.btnExec.Location = new System.Drawing.Point(218, 300);
            this.btnExec.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(170, 36);
            this.btnExec.TabIndex = 7;
            this.btnExec.Text = "実行";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // pgbMain
            // 
            this.pgbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbMain.Location = new System.Drawing.Point(194, 84);
            this.pgbMain.Name = "pgbMain";
            this.pgbMain.Size = new System.Drawing.Size(195, 23);
            this.pgbMain.TabIndex = 8;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProgress.Location = new System.Drawing.Point(191, 66);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 15);
            this.lblProgress.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(15, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "① specificのini";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(15, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "② commonのini";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(14, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "①-② 差分の出力先フォルダ";
            // 
            // btnOutputRef
            // 
            this.btnOutputRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputRef.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this.btnOutputRef.Location = new System.Drawing.Point(309, 254);
            this.btnOutputRef.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOutputRef.Name = "btnOutputRef";
            this.btnOutputRef.Size = new System.Drawing.Size(79, 31);
            this.btnOutputRef.TabIndex = 13;
            this.btnOutputRef.Text = "参照";
            this.btnOutputRef.UseVisualStyleBackColor = true;
            this.btnOutputRef.Click += new System.EventHandler(this.btnOutputRef_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.AllowDrop = true;
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this.txtOutput.Location = new System.Drawing.Point(11, 253);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(293, 31);
            this.txtOutput.TabIndex = 12;
            this.txtOutput.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtOutput_DragDrop);
            this.txtOutput.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtOutput_DragEnter);
            this.txtOutput.Validated += new System.EventHandler(this.txtInput_Validated);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 348);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOutputRef);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pgbMain);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.btnRef2);
            this.Controls.Add(this.txtInput2);
            this.Controls.Add(this.btnRef1);
            this.Controls.Add(this.txtInput1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmMain";
            this.Text = "Specificker";
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton opbDirectory;
        private System.Windows.Forms.RadioButton opbFile;
        private System.ComponentModel.BackgroundWorker bgworkerMain;
        private System.Windows.Forms.TextBox txtInput1;
        private System.Windows.Forms.Button btnRef1;
        private System.Windows.Forms.Button btnRef2;
        private System.Windows.Forms.TextBox txtInput2;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.ProgressBar pgbMain;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOutputRef;
        private System.Windows.Forms.TextBox txtOutput;
    }
}

