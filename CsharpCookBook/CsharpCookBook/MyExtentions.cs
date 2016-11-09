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
                    right += 1;
                    ret.Add(mylist[right]);
                }
            }
            return ret.ToArray();
        }

        public static int CountAl<T>(this List<T> mylist,T searchValue) => mylist.GetAll(searchValue).Count();

        public static int BinarySearchCountAll<T>(this List<T> mylist, T searchValue) =>
            mylist.BinarySearchGetAll(searchValue).Count();

        public static IEnumerable<T> EveryNthIthem<T>(this IEnumerable<T> enumerable,int step)
        {
            int current = 0;
            foreach (var item in enumerable)
            {
                ++current;
                if (current % step==0)
                {
                    yield return item;
                }
            }
        }
        /// <summary>
        /// Safely performing a narrowing numeric cast
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static int AddNarrowingCheked(this long lhs,long rhs)
        {
            return checked((int)(lhs + rhs));
        }
        public static void AddNarrowingChekedTest()
        {
            long lhs = 340000;
            var rhs = long.MaxValue;
            try
            {
                var result = lhs.AddNarrowingCheked(rhs);
            }
            catch (OverflowException)
            {

                throw;
            }

            var sourceValue = 34000;
            var destinationValue = 0;
            if (sourceValue<=short.MaxValue && sourceValue>=short.MaxValue)
            {
                destinationValue = (short)sourceValue;
            }
        }
        

    }
}
