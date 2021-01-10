using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceProfiler;

namespace PerformanceViewer
{
    public class ChartItem
    {
        public string X { set; get; }
        public string Y { set; get; }

        public ChartItem(string x, string y)
        {
            X = x;
            Y = y;
        }

        public static List<ChartItem> GetChartItems(PerformanceData data)
        {
            List<ChartItem> list = new List<ChartItem>();

            foreach (var span in data.TimeSpanList)
            {
                string x = span.LogDateTime.ToString("yyyy-MM-ddThh:mm:ss");
                string y = Math.Round(span.Seconds, 2).ToString();
                list.Add(new ChartItem(x, y));
            }

            return list;
        }
    }
}
