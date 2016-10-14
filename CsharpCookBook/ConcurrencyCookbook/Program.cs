using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    class Program
    {
        static  void Main(string[] args)
        {
            var taskA = ConcurrencyTask.ProcessTaskAsync2();
            var taskB = ConcurrencyTask.ProcessTaskAsync2();
            taskA.Wait();
            taskB.Wait();

            Console.ReadLine();

        }
    }
}
