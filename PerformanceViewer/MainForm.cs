using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PerformanceProfiler;
using RazorEngine;
using System.IO;

namespace PerformanceViewer
{
    public partial class frmMain : Form
    {
        private ProfilingResult _ProfilingResult;

        public frmMain()
        {
            InitializeComponent();
            txtStartToken.Text = "毎時読み込み処理開始";
            txtEndToken.Text = "毎時読み込み処理終了";
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            string start = txtStartToken.Text;
            string end = txtEndToken.Text;
            string path = txtFolder.Text.Trim('"');
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("フォルダを指定してください");
                return;
            }
            StartCalculate(start, end, path);
        }

        private void StartCalculate(string start, string end, string path)
        {
            Profiler p = new Profiler(start, end);
            _ProfilingResult = p.Calculate(path);

            // 先に行と列のヘッダだけ作る
            gridLogs.Columns.Clear();
            foreach (var date in _ProfilingResult.GetDateRange())
            {
                int colIndex = gridLogs.Columns.Add(date, date);
            }

            gridLogs.Rows.Clear();
            foreach (var machine in _ProfilingResult.GetMachines())
            {
                int rowIndex = gridLogs.Rows.Add();
                gridLogs.Rows[rowIndex].HeaderCell.Value = machine;
            }
            gridLogs.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);


            // セルにデータを入れる
            for (int rowIndex = 0; rowIndex < gridLogs.Rows.Count; rowIndex++)
            {
                string rowHeader = gridLogs.Rows[rowIndex].HeaderCell.Value?.ToString();
                for (int colIndex = 0; colIndex < gridLogs.Columns.Count; colIndex++)
                {
                    string colHeader = gridLogs.Columns[colIndex].HeaderCell.Value?.ToString();
                    PerformanceData data = GetPerformanceData(rowHeader, colHeader);
                    string dispData = "";
                    if (data != null) {
                        dispData = Math.Round(data.Score, 2).ToString();
                    }
                    gridLogs.Rows[rowIndex].Cells[colIndex].Value = dispData;
                }
            }


        }

        /// <summary>
        /// 指定したマシン名、日付のデータを取得する。
        /// なければnullを返す。
        /// </summary>
        /// <param name="machine">マシン名（行ヘッダに対応する）</param>
        /// <param name="datetimeString">日付（列ヘッダに対応する）</param>
        /// <returns>パフォーマンスデータ</returns>
        private PerformanceData GetPerformanceData(string machine, string datetimeString)
        {
            PerformanceData data = null;
            try
            {
                data = _ProfilingResult.EnumPerformanceData()
                    .Where(item => (item.Machine + "_" + item.App) == machine && item.DateTimeString == datetimeString)
                    .FirstOrDefault();
            }
            catch { }

            return data;
        }


        private void gridLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) { return; }
                string rowHeader = gridLogs.Rows[e.RowIndex].HeaderCell.Value?.ToString();
                string colHeader = gridLogs.Columns[e.ColumnIndex].HeaderCell.Value?.ToString();

                PerformanceData data = GetPerformanceData(rowHeader, colHeader);
                if (data == null) { return; }

                // Chromeを起動する（パスが通っていることが前提）
                System.Diagnostics.Process.Start("chrome.exe", CreateChartPage(data));
            }
            catch { }


        }

        /// <summary>
        /// チャートのHTMLを作成しURIを返す
        /// </summary>
        /// <param name="data">測定データセット</param>
        /// <returns>URI文字列</returns>
        private string CreateChartPage(PerformanceData data) {

            string templateDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "script");
            string templatePath = Path.Combine(templateDir, "chart.cshtml");
            string template = "";
            using (StreamReader reader = new StreamReader(templatePath)) {
                template = reader.ReadToEnd();
            }
            string resultPath = Path.Combine(templateDir, "chart.html");

            var viewBag = new RazorEngine.Templating.DynamicViewBag();
            viewBag.AddValue("Title", data.Machine);
            viewBag.AddValue("Items", ChartItem.GetChartItems(data));

            var result = Razor.Parse(template, null, viewBag, data.Machine);
            using (StreamWriter writer = new StreamWriter(resultPath))
            {
                writer.Write(result);
            }

            Uri uri = new Uri(resultPath);
            return uri.AbsoluteUri;
        }
    }
}
