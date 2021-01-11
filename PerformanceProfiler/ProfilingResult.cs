using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceProfiler
{
    /// <summary>
    /// パフォーマンスデータのコレクションクラス
    /// </summary>
    public class ProfilingResult
    {
        private List<PerformanceData> _list;

        public PerformanceData this[int index]{
            get => _list[index];
        }

        public ProfilingResult()
        {
            _list = new List<PerformanceData>();
            
        }

        public void Add(PerformanceData addend)
        {
            PerformanceData mine = _list.Where(item => item.IsSameDateAndMachineLog(addend)).FirstOrDefault();
            if (mine == default)
            {
                _list.Add(addend);
                return;
            }

            // すでにパース済みのログなら秒数データのみ追加
            mine.Add(addend.TimeSpanList);
        }

        public void CalculateAllScore() {
            foreach (var item in _list)
            {
                item.CalculateScore();
            }
        }

        public IEnumerable<PerformanceData> EnumPerformanceData()
        {
            foreach (var item in _list)
            {
                yield return item;
            }
        }

        public IEnumerable<string> GetMachines()
        {
            return _list.Select(item => item.Machine + "_" + item.App).Distinct();
        }

        public IEnumerable<string> GetDateRange()
        {
            return _list.OrderBy(item => item.Year)
                .ThenBy(item => item.Month)
                .ThenBy(item => item.Date)
                .Select(item => item.DateTimeString)
                .Distinct();
        }

        public IEnumerable<double> GetScores()
        {
            return _list.Select(item => item.Score);
        }
    }
}
