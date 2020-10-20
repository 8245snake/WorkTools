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
            this.btnSwitch = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optExclusive = new System.Windows.Forms.RadioButton();
            this.optSub = new System.Windows.Forms.RadioButton();
            this.optAdd = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.opbDirectory);
            this.groupBox1.Controls.Add(this.opbFile);
            this.groupBox1.Location = new System.Drawing.Point(14, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.groupBox1.Size = new System.Drawing.Size(159, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // opbDirectory
            // 
            this.opbDirectory.AutoSize = true;
            this.opbDirectory.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opbDirectory.Location = new System.Drawing.Point(7, 69);
            this.opbDirectory.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
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
            this.opbFile.Location = new System.Drawing.Point(7, 22);
            this.opbFile.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
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
            this.txtInput1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtInput1.Location = new System.Drawing.Point(14, 170);
            this.txtInput1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtInput1.Name = "txtInput1";
            this.txtInput1.Size = new System.Drawing.Size(635, 23);
            this.txtInput1.TabIndex = 1;
            this.txtInput1.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtInput1_DragDrop);
            this.txtInput1.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtInput_DragEnter);
            this.txtInput1.Validated += new System.EventHandler(this.txtInput_Validated);
            // 
            // btnRef1
            // 
            this.btnRef1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRef1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRef1.Location = new System.Drawing.Point(653, 171);
            this.btnRef1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnRef1.Name = "btnRef1";
            this.btnRef1.Size = new System.Drawing.Size(71, 22);
            this.btnRef1.TabIndex = 2;
            this.btnRef1.Text = "参照";
            this.btnRef1.UseVisualStyleBackColor = true;
            this.btnRef1.Click += new System.EventHandler(this.btnRef1_Click);
            // 
            // btnRef2
            // 
            this.btnRef2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRef2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRef2.Location = new System.Drawing.Point(653, 229);
            this.btnRef2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnRef2.Name = "btnRef2";
            this.btnRef2.Size = new System.Drawing.Size(71, 22);
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
            this.txtInput2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtInput2.Location = new System.Drawing.Point(13, 229);
            this.txtInput2.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtInput2.Name = "txtInput2";
            this.txtInput2.Size = new System.Drawing.Size(636, 23);
            this.txtInput2.TabIndex = 3;
            this.txtInput2.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtInput2_DragDrop);
            this.txtInput2.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtInput_DragEnter);
            this.txtInput2.Validated += new System.EventHandler(this.txtInput_Validated);
            // 
            // btnExec
            // 
            this.btnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExec.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this.btnExec.Location = new System.Drawing.Point(526, 332);
            this.btnExec.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(198, 45);
            this.btnExec.TabIndex = 7;
            this.btnExec.Text = "実行";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // pgbMain
            // 
            this.pgbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbMain.Location = new System.Drawing.Point(20, 348);
            this.pgbMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgbMain.Name = "pgbMain";
            this.pgbMain.Size = new System.Drawing.Size(483, 29);
            this.pgbMain.TabIndex = 8;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProgress.Location = new System.Drawing.Point(223, 82);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 15);
            this.lblProgress.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(17, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "① specificのini";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(17, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "② commonのini";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(17, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "出力先フォルダ";
            // 
            // btnOutputRef
            // 
            this.btnOutputRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputRef.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOutputRef.Location = new System.Drawing.Point(653, 289);
            this.btnOutputRef.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnOutputRef.Name = "btnOutputRef";
            this.btnOutputRef.Size = new System.Drawing.Size(71, 22);
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
            this.txtOutput.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOutput.Location = new System.Drawing.Point(13, 288);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(636, 23);
            this.txtOutput.TabIndex = 12;
            this.txtOutput.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtOutput_DragDrop);
            this.txtOutput.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtOutput_DragEnter);
            this.txtOutput.Validated += new System.EventHandler(this.txtInput_Validated);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(333, 200);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(114, 23);
            this.btnSwitch.TabIndex = 15;
            this.btnSwitch.Text = "⇅入れ替え";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optExclusive);
            this.groupBox2.Controls.Add(this.optSub);
            this.groupBox2.Controls.Add(this.optAdd);
            this.groupBox2.Location = new System.Drawing.Point(192, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(546, 119);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "演算";
            // 
            // optExclusive
            // 
            this.optExclusive.AutoSize = true;
            this.optExclusive.Location = new System.Drawing.Point(18, 56);
            this.optExclusive.Name = "optExclusive";
            this.optExclusive.Size = new System.Drawing.Size(243, 19);
            this.optExclusive.TabIndex = 2;
            this.optExclusive.TabStop = true;
            this.optExclusive.Text = "排他（①のみに存在する要素を抽出します）";
            this.optExclusive.UseVisualStyleBackColor = true;
            // 
            // optSub
            // 
            this.optSub.AutoSize = true;
            this.optSub.Checked = true;
            this.optSub.Location = new System.Drawing.Point(18, 27);
            this.optSub.Name = "optSub";
            this.optSub.Size = new System.Drawing.Size(508, 19);
            this.optSub.TabIndex = 1;
            this.optSub.TabStop = true;
            this.optSub.Text = "差分（①のみに存在する要素と、①と②の両方に存在するが値が異なる要素の①の値を抽出します）";
            this.optSub.UseVisualStyleBackColor = true;
            // 
            // optAdd
            // 
            this.optAdd.AutoSize = true;
            this.optAdd.Location = new System.Drawing.Point(18, 85);
            this.optAdd.Name = "optAdd";
            this.optAdd.Size = new System.Drawing.Size(395, 19);
            this.optAdd.TabIndex = 0;
            this.optAdd.TabStop = true;
            this.optAdd.Text = "合算（①と②の和集合です。両方に存在する場合は①の値が優先されます）";
            this.optAdd.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 412);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSwitch);
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
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "frmMain";
            this.Text = "Specificker";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optExclusive;
        private System.Windows.Forms.RadioButton optSub;
        private System.Windows.Forms.RadioButton optAdd;
    }
}

