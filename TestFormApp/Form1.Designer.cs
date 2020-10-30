namespace TestFormApp
{
    partial class Form1
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
            this.gridIniLeft = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridIniLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // gridIniLeft
            // 
            this.gridIniLeft.AllowDrop = true;
            this.gridIniLeft.AllowUserToAddRows = false;
            this.gridIniLeft.AllowUserToDeleteRows = false;
            this.gridIniLeft.AllowUserToResizeRows = false;
            this.gridIniLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridIniLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridIniLeft.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.SectionName,
            this.KeyName,
            this.ValueData,
            this.Comment});
            this.gridIniLeft.Location = new System.Drawing.Point(33, 33);
            this.gridIniLeft.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gridIniLeft.Name = "gridIniLeft";
            this.gridIniLeft.ReadOnly = true;
            this.gridIniLeft.RowHeadersVisible = false;
            this.gridIniLeft.RowTemplate.Height = 21;
            this.gridIniLeft.Size = new System.Drawing.Size(995, 513);
            this.gridIniLeft.TabIndex = 0;
            this.gridIniLeft.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridIniLeft_CellFormatting);
            this.gridIniLeft.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridIniLeft_CellLeave);
            this.gridIniLeft.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridIniLeft_CellPainting);
            this.gridIniLeft.CurrentCellChanged += new System.EventHandler(this.gridIniLeft_CurrentCellChanged);
            this.gridIniLeft.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridIniLeft_DragDrop);
            this.gridIniLeft.DragEnter += new System.Windows.Forms.DragEventHandler(this.gridIniLeft_DragEnter);
            // 
            // FileName
            // 
            this.FileName.HeaderText = "ファイル";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 200;
            // 
            // SectionName
            // 
            this.SectionName.HeaderText = "セクション";
            this.SectionName.Name = "SectionName";
            this.SectionName.ReadOnly = true;
            this.SectionName.Width = 200;
            // 
            // KeyName
            // 
            this.KeyName.HeaderText = "キー";
            this.KeyName.Name = "KeyName";
            this.KeyName.ReadOnly = true;
            this.KeyName.Width = 200;
            // 
            // ValueData
            // 
            this.ValueData.HeaderText = "値";
            this.ValueData.Name = "ValueData";
            this.ValueData.ReadOnly = true;
            this.ValueData.Width = 200;
            // 
            // Comment
            // 
            this.Comment.HeaderText = "コメント";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Width = 500;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 586);
            this.Controls.Add(this.gridIniLeft);
            this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Form1";
            this.Text = "iniパーサーテスト用アプリ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.gridIniLeft)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridIniLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
    }
}

