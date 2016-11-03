using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    public static class MyExtentions
    {
        public static void DisplayStocks(this IEnumerable<Stock> stocks)
        {
            var s = string.Empty; 
            foreach (Stock item in stocks)
            {
                s = item.GainLoss < 0 ? "lost":"gain";
                Console.WriteLine($"\t({item.Ticker}) {s} {item.GainLoss}%");
            }
        }
    }
}
