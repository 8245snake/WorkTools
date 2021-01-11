using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using PerformanceProfiler;


namespace PerformanceViewer
{
    public class ScoreStatistic
    {
        private DataGridView _ColorGrid;
        private DataGridViewCellStyle colorCellStyle20 = new DataGridViewCellStyle();
        private DataGridViewCellStyle colorCellStyle40 = new DataGridViewCellStyle();
        private DataGridViewCellStyle colorCellStyle60 = new DataGridViewCellStyle();
        private DataGridViewCellStyle colorCellStyle80 = new DataGridViewCellStyle();

        private double levelLimit20;
        private double levelLimit40;
        private double levelLimit60;
        private double levelLimit80;

        public ScoreStatistic(DataGridView grid)
        {
            _ColorGrid = grid;
            DefineCellStyle();
        }

        private void  DefineCellStyle()
        {
            colorCellStyle20.BackColor = Color.FromArgb(0, 255, 255);
            colorCellStyle20.SelectionBackColor = Color.FromArgb(0, 255, 255);
            colorCellStyle20.SelectionForeColor = Color.Black;

            colorCellStyle40.BackColor = Color.FromArgb(0, 0, 255);
            colorCellStyle40.SelectionBackColor = Color.FromArgb(0, 0, 255);
            colorCellStyle40.SelectionForeColor = Color.Black;

            colorCellStyle60.BackColor = Color.FromArgb(255, 0, 255);
            colorCellStyle60.SelectionBackColor = Color.FromArgb(255, 0, 255);
            colorCellStyle60.SelectionForeColor = Color.Black;

            colorCellStyle80.BackColor = Color.FromArgb(255, 0, 0);
            colorCellStyle80.SelectionBackColor = Color.FromArgb(255, 0, 0);
            colorCellStyle80.SelectionForeColor = Color.Black;
        }

        /// <summary>
        /// 色の凡例グリッドの初期化
        /// </summary>
        public void InitColorGrid()
        {
            _ColorGrid.Rows.Clear();
            _ColorGrid.RowTemplate.Height = _ColorGrid.Height;
            _ColorGrid.Rows.Add();

            _ColorGrid.Rows[0].Cells[0].Style = colorCellStyle20;
            _ColorGrid.Rows[0].Cells[0].Value = "20%以下";
            _ColorGrid.Rows[0].Cells[1].Style = colorCellStyle40;
            _ColorGrid.Rows[0].Cells[1].Value = "40%以下";
            _ColorGrid.Rows[0].Cells[2].Style = colorCellStyle60;
            _ColorGrid.Rows[0].Cells[2].Value = "60%以下";
            _ColorGrid.Rows[0].Cells[3].Style = colorCellStyle80;
            _ColorGrid.Rows[0].Cells[3].Value = "80%以上";

        }

        /// <summary>
        /// レベル分けする基準値を決定する
        /// </summary>
        /// <param name="result"></param>
        public void Calculate(ProfilingResult result)
        {
            double min = result.GetScores().Min();
            double max = result.GetScores().Max();
            double delta = (max - min) / 100;

            levelLimit20 = min + delta * 20;
            levelLimit40 = min + delta * 40;
            levelLimit60 = min + delta * 60;
            levelLimit80 = min + delta * 80;

        }


        public void PaintColorGrid(DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    PaiantCell(cell);
                }
            }
        }

        private void PaiantCell(DataGridViewCell cell)
        {
            string val = cell.Value?.ToString();
            if (string.IsNullOrEmpty(val)){
                return;
            }

            if (!double.TryParse(val, out double score)) {
                return;
            }

            if (score < levelLimit20) {
                cell.Style = colorCellStyle20;
                return;
            }

            if (score < levelLimit60)
            {
                cell.Style = colorCellStyle40;
                return;
            }

            if (score < levelLimit80)
            {
                cell.Style = colorCellStyle60;
                return;
            }

            cell.Style = colorCellStyle80;

        }
    }
}
