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

        public static IEnumerable<T> GetAll<T>(this List<T> mylist, T searchValue) =>
            mylist.Where(t => t.Equals(searchValue));

        public static T[] BinarySearchGetAll<T>(this List<T> mylist,T searchValue)
        {
            List<T> ret = new List<T>();

            int center = mylist.BinarySearch(searchValue);
            if (center > 0)
            {
                ret.Add(mylist[center]);
                int left = center;
                while(left > 0&&mylist[left-1].Equals(searchValue))
                {
                    left -= 1;
                    ret.Add(mylist[left]);
                }
                var right = center;
                while (right < (mylist.Count-1) && mylist[right + 1].Equals(searchValue))
                {
                    left += 1;
                    ret.Add(mylist[right]);
                }
            }
            return ret.ToArray();
        }

        public static int CountAl<T>(this List<T> mylist,T searchValue) => mylist.GetAll(searchValue).Count();

        public static int BinarySearchCountAll<T>(this List<T> mylist, T searchValue) =>
            mylist.BinarySearchGetAll(searchValue).Count();
    }
}
