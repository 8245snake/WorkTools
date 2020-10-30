using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using IniUtils;
using System.Windows.Forms;

namespace TestFormApp
{
    public partial class Form1 : Form
    {
        const int COL_FILENAME = 0;
        const int COL_SECTION = 1;
        const int COL_KEY = 2;
        const int COL_VALUE = 3;
        const int COL_COMMENT = 4;

        private DataGridViewCellStyle defaultCellStyle;
        private DataGridViewCellStyle selectedCellStyle;

        public Form1()
        {
            InitializeComponent();
            //デフォルトのセルスタイルの設定
            this.defaultCellStyle = new DataGridViewCellStyle();
            //現在のセルのセルスタイルの設定
            this.selectedCellStyle = new DataGridViewCellStyle();
            this.selectedCellStyle.BackColor = gridIniLeft.DefaultCellStyle.SelectionBackColor;
            this.selectedCellStyle.SelectionBackColor = gridIniLeft.DefaultCellStyle.SelectionBackColor;
        }

        private void gridIniLeft_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            parse(filePaths[0]);
        }

        private void gridIniLeft_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void parse(string path)
        {
            IniFile ini = IniUtils.IniFileParser.ParseIniFile(path);
            foreach (var section in ini.GetIniSections())
            {
                foreach (var data in section.GetIniValues())
                {
                    gridIniLeft.Rows.Add(data.FileName, data.SectionName, data.KeyName, data.Value, data.Comment);
                }
            }
        }

        /// <summary>
        /// 指定したセルと1つ上のセルの値を比較
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        bool IsTheSameCellValue(int column, int row, bool upforword = true)
        {
            DataGridViewCell cell1 = null, cell2 = null;

            if (upforword)
            {
                if (column < 0 || row < 1) { return false; }
                cell1 = gridIniLeft[column, row];
                cell2 = gridIniLeft[column, row - 1];
            }
            else
            {
                if (column < 0 || row > gridIniLeft.Rows.Count) { return false; }
                cell1 = gridIniLeft[column, row];
                cell2 = gridIniLeft[column, row + 1];
            }

            if (cell1.Value == null || cell2.Value == null) { return false; }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }


        // DataGridViewのCellFormattingイベント・ハンドラ
        void gridIniLeft_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // iniファイル名とセクション名列以外はマージしない
            if (e.ColumnIndex != COL_FILENAME && e.ColumnIndex != COL_SECTION) { return; }
            // 1行目については何もしない
            if (e.RowIndex < 1){ return; }

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true; // 以降の書式設定は不要
            }
        }

        // DataGridViewのCellPaintingイベント・ハンドラ
        void gridIniLeft_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // iniファイル名とセクション名列以外はマージしない
            if (e.ColumnIndex != COL_FILENAME && e.ColumnIndex != COL_SECTION) { return; }
            // ヘッダ行は無視
            if (e.RowIndex < 0) { return; }

            // セルの下側の境界線を「境界線なし」に設定
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                // セルの上側の境界線を「境界線なし」に設定
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                // セルの上側の境界線を既定の境界線に設定
                e.AdvancedBorderStyle.Top = gridIniLeft.AdvancedCellBorderStyle.Top;
            }
        }

        private void gridIniLeft_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            DataGridViewCell cell = grid.CurrentCell;
            int targetCol = cell.ColumnIndex;
            int targetRow = cell.RowIndex;
            // iniファイル名とセクション名列以外はマージしない
            if (targetCol != COL_FILENAME && targetCol != COL_SECTION) { return; }

            string targetString = cell?.Value + ""; 

            for (int i = targetRow; i < grid.Rows.Count; i++)
            {
                if ((string)grid[targetCol, i].Value == targetString)
                {
                    grid[targetCol, i].Style = selectedCellStyle;
                }
                else
                {
                    break;
                }
            }

            for (int i = targetRow; i > 0; i--)
            {
                if ((string)grid[targetCol, i].Value == targetString)
                {
                    grid[targetCol, i].Style = selectedCellStyle;
                }
                else
                {
                    break;
                }
            }

        }

        private void gridIniLeft_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            DataGridViewCell cell = grid.CurrentCell;
            int targetCol = cell.ColumnIndex;
            // iniファイル名とセクション名列以外はマージしない
            if (targetCol != COL_FILENAME && targetCol != COL_SECTION) { return; }

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                grid[targetCol, i].Style = null;
            }
        }
    }
}
