namespace PerformanceViewer
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
            this.gridLogs = new System.Windows.Forms.DataGridView();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEndToken = new System.Windows.Forms.TextBox();
            this.btnExec = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLogs
            // 
            this.gridLogs.AllowUserToAddRows = false;
            this.gridLogs.AllowUserToDeleteRows = false;
            this.gridLogs.AllowUserToResizeRows = false;
            this.gridLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.gridLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLogs.Location = new System.Drawing.Point(14, 173);
            this.gridLogs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridLogs.MultiSelect = false;
            this.gridLogs.Name = "gridLogs";
            this.gridLogs.ReadOnly = true;
            this.gridLogs.RowTemplate.Height = 21;
            this.gridLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridLogs.ShowEditingIcon = false;
            this.gridLogs.Size = new System.Drawing.Size(676, 374);
            this.gridLogs.TabIndex = 0;
            this.gridLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLogs_CellDoubleClick);
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Font = new System.Drawing.Font("游ゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFolder.Location = new System.Drawing.Point(85, 12);
            this.txtFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(599, 29);
            this.txtFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "フォルダ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "開始文字列";
            // 
            // txtStartToken
            // 
            this.txtStartToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartToken.Font = new System.Drawing.Font("游ゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtStartToken.Location = new System.Drawing.Point(85, 68);
            this.txtStartToken.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStartToken.Name = "txtStartToken";
            this.txtStartToken.Size = new System.Drawing.Size(599, 29);
            this.txtStartToken.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "終了文字列";
            // 
            // txtEndToken
            // 
            this.txtEndToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndToken.Font = new System.Drawing.Font("游ゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtEndToken.Location = new System.Drawing.Point(85, 105);
            this.txtEndToken.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEndToken.Name = "txtEndToken";
            this.txtEndToken.Size = new System.Drawing.Size(599, 29);
            this.txtEndToken.TabIndex = 5;
            // 
            // btnExec
            // 
            this.btnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExec.Location = new System.Drawing.Point(604, 141);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(80, 25);
            this.btnExec.TabIndex = 7;
            this.btnExec.Text = "解析開始";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 562);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEndToken);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStartToken);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.gridLogs);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.Text = "パフォーマンス測定";
            ((System.ComponentModel.ISupportInitialize)(this.gridLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridLogs;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStartToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEndToken;
        private System.Windows.Forms.Button btnExec;
    }
}

