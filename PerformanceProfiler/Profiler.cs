using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PerformanceProfiler
{
    public class Profiler
    {
        string _StartToken;
        string _EndToken;

        private Encoding _Encoding = Encoding.GetEncoding("shift_jis");
        private Regex regexDebuglog = new Regex(@"^OR_DBG_(?<code>[A-Za-z0-9]+)?_(?<machine>[A-Za-z0-9\-]+)_(?<app>([A-Za-z0-9\-]+)|(PRESCIENT.+))_(?<year>\d{4})(?<month>\d{2})(?<date>\d{2}).*", RegexOptions.Compiled);

        public Profiler(string startToken , string endToken)
        {
            _StartToken = startToken;
            _EndToken = endToken;
        }

        /// <summary>
        /// パフォーマンス計算処理
        /// </summary>
        /// <param name="directory">ログが格納されているフォルダ</param>
        /// <returns>パフォーマンス結果</returns>
        public ProfilingResult Calculate(string directory) {

            ProfilingResult result = new ProfilingResult();
            string[] files = Directory.GetFiles(directory, "*log*", SearchOption.AllDirectories);
            foreach (string path in files)
            {
                PerformanceData data = CreatePerformanceData(path);
                if (data != null) {
                    result.Add(data);
                }
            }
            result.CalculateAllScore();
            return result;
        }

        /// <summary>
        /// ログファイルを解析しパフォーマンスデータを作成する
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>パフォーマンスデータ</returns>
        private PerformanceData CreatePerformanceData(string path)
        {
            // ファイル名から情報を取得
            string filename = Path.GetFileNameWithoutExtension(path);
            var match = regexDebuglog.Match(filename);
            // 測定対象のファイル名規則に合致しなければ処理しない
            if (!match.Success) { return null; }

            PerformanceData data = new PerformanceData();
            data.StartToken = _StartToken;
            data.EndToken = _EndToken;
            data.Machine = match.Groups["code"].Value + "_" + match.Groups["machine"].Value;
            data.App = match.Groups["app"].Value;
            data.Year = int.Parse(match.Groups["year"].Value);
            data.Month = int.Parse(match.Groups["month"].Value);
            data.Date = int.Parse(match.Groups["date"].Value);

            IEnumerable<string> lines = EnumLogLines(path, _StartToken, _EndToken);
            IEnumerable<LogTimeSpan> spans = CreateTimeSpanData(lines, _StartToken, _EndToken);
            data.TimeSpanList = spans.ToList();
            return data;
        }

        /// <summary>
        /// 経過時間を順次返す
        /// </summary>
        /// <param name="lines">ログ１行</param>
        /// <param name="startToken">開始文字列</param>
        /// <param name="endToken">終了文字列</param>
        /// <returns>ログ経過時間データ</returns>
        private IEnumerable<LogTimeSpan> CreateTimeSpanData(IEnumerable<string> lines, string startToken, string endToken)
        {
            DateTime startDatetime = default;

            foreach (string line in lines)
            {
                if (line.Contains(startToken))
                {
                    GetDateTimeFromLogMessage(line, out startDatetime);
                    continue;
                }

                if (line.Contains(endToken) && startDatetime != default)
                {
                    DateTime endDatetime;
                    GetDateTimeFromLogMessage(line, out endDatetime);
 
                    LogTimeSpan timespan = new LogTimeSpan();
                    timespan.LogDateTime = endDatetime;
                    timespan.LogSpan = endDatetime - startDatetime;
                    startDatetime = default;
                    yield return timespan;
                }

            }
        }


        /// <summary>
        /// ログから日時を取得する
        /// </summary>
        /// <param name="line">ログ１行</param>
        /// <param name="datetime">日時</param>
        /// <returns>パースに成功したか</returns>
        private bool GetDateTimeFromLogMessage(string line, out DateTime datetime)
        {
            datetime = default;
            if (!int.TryParse(line.Substring(0, 4), out int year)) { return false; }
            if (!int.TryParse(line.Substring(5, 2), out int month)) { return false; }
            if (!int.TryParse(line.Substring(8, 2), out int day)) { return false; }
            if (!int.TryParse(line.Substring(11, 2), out int hour)) { return false; }
            if (!int.TryParse(line.Substring(14, 2), out int minute)) { return false; }
            if (!int.TryParse(line.Substring(17, 2), out int second)) { return false; }
            if (!int.TryParse(line.Substring(21, 3), out int millisecond)) { return false; }
            // すべてパースできたらDateTime型に変換して返す
            datetime = new DateTime(year, month, day, hour, minute, second, millisecond);
            return true;
        }

        /// <summary>
        /// ファイルを読み込み指定した文字列があったら返す
        /// </summary>
        /// <param name="path"></param>
        /// <param name="startToken"></param>
        /// <param name="endToken"></param>
        /// <returns></returns>
        private IEnumerable<string> EnumLogLines(string path, string startToken, string endToken)
        {
            using (StreamReader reader = new StreamReader(path, _Encoding))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(startToken) || line.Contains(endToken))
                    {
                        yield return line;
                    }
                }
            }
        }

    }



}
