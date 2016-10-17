using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConcurrencyCookbook
{
    class Parallel1
    {
        /// <summary>
        /// this is not the most efficient implementation.
        /// this is just an example of using a lock to protect shared state
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        static int ParallelSum(IEnumerable<int> values)
        {
            object mutex = new object();
            int result = 0;
            Parallel.ForEach(source: values,
                localInit: () => 0,
                body: (item, state, localValuie) => localValuie + item,
                localFinally: localVaue =>
                 {
                     lock (mutex)
                     {
                         result += localVaue;
                     }
                 });
            return result;
        }

        static int ParallelSum2(IEnumerable<int> values)
        {
            return values.AsParallel().Sum();
        }

        static int ParallelSum3(IEnumerable<int> values)
        {
            return values.AsParallel().Aggregate(
                seed: 0,
                func: (sum, item) => sum + item
                );
        }

        static void ProcessArray(double[] array)
        {
            Parallel.Invoke(

                () => ProcessPartialArray(array, 0, array.Length / 2),
                () => ProcessPartialArray(array, array.Length / 2, array.Length);
        }

        private static void ProcessPartialArray(double[] array, int v1, int v2)
        {
        }

        static void DoAction20Times(Action action)
        {
            Action[] actions = Enumerable.Repeat(action, 20).ToArray();
            Parallel.Invoke(actions);
        }

        static void DoAction20Times(Action action,CancellationToken token)
        {
            Action[] actions = Enumerable.Repeat(action, 20).ToArray();
            Parallel.Invoke(new ParallelOptions { CancellationToken = token },actions);
        }

        void Travese(Node current)
        {
            DoExPensiveActionOnNode(current);
            if (current.Left != null)
            {
                Task.Factory.StartNew(() => Travese(current.Left),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Default);
                //Task task = Task.Factory.StartNew(
                //    ()=>Thread.Sleep(2000),
                //    CancellationToken.None,
                //    TaskCreationOptions.AttachedToParent,
                //    TaskScheduler.Default);
                //Task continuation = task.ContinueWith(
                //    t=>Trace.WriteLine("task is done"),
                //    CancellationToken.None,
                //    TaskContinuationOptions.None,
                //    TaskScheduler.Default);
            }
            if (current.Right != null)
            {
                Task.Factory.StartNew(() => Travese(current.Right),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Default);
            }
        }

        private void DoExPensiveActionOnNode(Node current)
        {
            throw new NotImplementedException();
        }
    }

    internal class Node
    {
        public Node Left { get; internal set; }
        public Node Right { get; internal set; }
    }
}
