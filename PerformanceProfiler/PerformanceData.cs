using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PerformanceProfiler
{
    /// <summary>
    /// 一日分の特定端末のパフォーマンスデータを保持する
    /// </summary>
    public class PerformanceData
    {

        /// <summary>
        /// 端末名（コード）
        /// </summary>
        public string Machine { get; set; }
        public string App { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Date { get; set; }
        public double Score { get => _Score; }

        public string DateTimeString
        {
            get => $"{Year}/{string.Format("{0:00}", Month)}/{string.Format("{0:00}", Date)}";
        }

        public string StartToken { get; set; }
        public string EndToken { get; set; }
        public List<LogTimeSpan> TimeSpanList { set; get; }

        private double _Score;

        /// <summary>
        /// 同じ日の同じ端末のログかを判定する。
        /// log4netのログファイル分割対策
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsSameDateAndMachineLog(PerformanceData other)
        {
            if (other.Machine != this.Machine) { return false; }
            if (other.App != this.App) { return false; }
            if (other.Year != this.Year) { return false; }
            if (other.Month != this.Month) { return false; }
            if (other.Date != this.Date) { return false; }
            return true;
        }

        /// <summary>
        /// 測定データを追加する
        /// </summary>
        /// <param name="timespans">測定データ</param>
        public void Add(ICollection<LogTimeSpan> timespans)
        {
            TimeSpanList.AddRange(timespans);
            TimeSpanList = TimeSpanList.OrderBy(item => item.LogDateTime).ToList();
        }

        /// <summary>
        /// タブ区切りでファイルに出力する
        /// </summary>
        /// <param name="outputPath">出力パス</param>
        public void ExportTSV(string outputPath)
        {
            using(StreamWriter writer = new StreamWriter(outputPath))
            {
                foreach (var item in TimeSpanList)
                {
                    writer.WriteLine(item.ToTSV());
                }
            }
        }

        /// <summary>
        /// カンマ区切りでファイルに出力する
        /// </summary>
        /// <param name="outputPath">出力パス</param>
        public void ExportCSV(string outputPath)
        {
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                foreach (var item in TimeSpanList)
                {
                    writer.WriteLine(item.ToCSV());
                }
            }
        }

        /// <summary>
        /// 経過時間のスコアを計算する
        /// </summary>
        /// <returns>スコア</returns>
        /// <remarks>毎時読み込み処理の経過時間を合計し分で割る</remarks>
        public void CalculateScore()
        {
            // ログを含む分までの時刻の合計→総稼働時間(min)
            int totlaRunningMinute = TimeSpanList.Select(span => span.LogDateTimeString).Distinct().Count();
            double totalFreezeSecond = TimeSpanList.Sum(span => span.Seconds);
            _Score = totalFreezeSecond / totlaRunningMinute;
        }
    }
}
