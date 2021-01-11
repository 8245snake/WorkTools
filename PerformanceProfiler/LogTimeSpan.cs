using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceProfiler
{
    /// <summary>
    /// 経過時間を保持するクラス
    /// </summary>
    public class LogTimeSpan
    {
        /// <summary>
        /// 測定日時
        /// </summary>
        public DateTime LogDateTime { set; get; }

        /// <summary>
        /// 経過時間
        /// </summary>
        public TimeSpan LogSpan { set; get; }

        /// <summary>
        /// 秒数
        /// </summary>
        public double Seconds { get => LogSpan.TotalSeconds; }


        public string LogDateTimeString { get => LogDateTime.ToString("yyyy/MM/dd HH:mm"); }


        public string ToTSV() {
            string delim = "\t";
            return $"{LogDateTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}{delim}{Seconds}";
        }

        public string ToCSV()
        {
            string delim = ",";
            return $"{LogDateTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}{delim}{Seconds}";
        }
    }
}
