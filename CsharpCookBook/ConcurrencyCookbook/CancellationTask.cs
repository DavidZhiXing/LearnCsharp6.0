using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConcurrencyCookbook
{
    class CancellationTask
    {
        CancellationTokenSource _cts;
        private Mybtn startbtn;
        private Mybtn cancelbtn;

        public int CancelableMethod(CancellationToken token)
        {
            for (int i = 0; i != 100; i++)
            {
                Thread.Sleep(1000);
                token.ThrowIfCancellationRequested();
            }
            return 42;
        }

        public async Task<int> CancelableMethodAsync(CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(10), token);
            return 42;
        }

        static void RotateMaTrices(IEnumerable<Matrix> matrieces,
            float degrees, CancellationToken token)
        {
            Parallel.ForEach(matrieces,
                new ParallelOptions { CancellationToken = token },
                matrix => matrix.Rotate(degrees));

            Parallel.ForEach(matrieces,

                    matrix =>
                    {
                        matrix.Rotate(degrees);
                        token.ThrowIfCancellationRequested();
                    });
        }

        static IEnumerable<int> MultiplyBy2(IEnumerable<int> values,
            CancellationToken token)
        {
            return values.AsParallel().WithCancellation(token).
                Select(item=>item*2);
        }

        async Task IssueTimeoutAsync()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var token = cts.Token;
            //cts = new CancellationTokenSource();
            //token = cts.Token;
            //cts.CancelAfter(TimeSpan.FromSeconds(5))
            await Task.Delay(TimeSpan.FromSeconds(10), token);
        }

        async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            startbtn.Enable(false);
            cancelbtn.Enable(true);
            try
            {
                _cts = new CancellationTokenSource();
                var token = _cts.Token;
                await Task.Delay(5000, token);
                Trace.WriteLine("delay completed successfully.");
            }
            catch (OperationCanceledException)
            {
                Trace.WriteLine("delay was canceled.");

            }
            catch (Exception)
            {
                Trace.WriteLine("delay completed with error.");
                throw;
            }
            finally
            {
                startbtn.Enable(!false);
                cancelbtn.Enable(!true);
            }

        }
        void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _cts.Cancel();
        }
        void IssueCancelRequest()
        {
            var cts = new CancellationTokenSource();
            var task = CancelableMethodAsync1(cts.Token);
            cts.Cancel();
        }

        IPropagatorBlock<int,int> CreateMyCustomBlock(CancellationToken token)
        {
            ISourceBlock<int> divideBlock = null;
            ITargetBlock<int> multiplayBlock = null;
            return DataflowBlock.Encapsulate(multiplayBlock, divideBlock);
        }

        private Task CancelableMethodAsync1(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        async Task IssueCancelRequestAsync()
        {
            var cts = new CancellationTokenSource();
            var task = CancelableMethodAsync(cts.Token);
            cts.Cancel();
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    internal class Mybtn
    {
        internal void Enable(bool v)
        {
            throw new NotImplementedException();
        }
    }

    internal class RoutedEventArgs : EventArgs
    {
    }
}
