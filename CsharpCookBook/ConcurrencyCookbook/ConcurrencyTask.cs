using Nito.AsyncEx;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConcurrencyCookbook
{
    public  class ConcurrencyTask1
    {
        static async Task<T> DelayResult<T>(T result,TimeSpan delay)
        {
            await Task.Delay(delay);
            return result;
        }

        static async Task<string> DownloadStringWithRetries(string uri)
        {
            using (var client = new HttpClient())
            {
                var nextDelay = TimeSpan.FromSeconds(1);
                for (int i = 0; i != 3; i++)
                {
                    try
                    {
                        return await client.GetStringAsync(uri);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    await Task.Delay(nextDelay);
                    nextDelay = nextDelay + nextDelay;
                }
                return await client.GetStringAsync(uri);
            }
        }

        static async Task<string> DownloadStringWithTimeout(string uri)
        {
            using (var client = new HttpClient())
            {
                var downloadTask = client.GetStringAsync(uri);
                var timeoutTask = Task.Delay(3000);
                var completeTask = await Task.WhenAny(downloadTask, timeoutTask);
                if (completeTask == timeoutTask)
                {
                    return null;
                }
                return await downloadTask;
            }
        }

        static Task<T> NotImplementedAsync<T>()
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetException(new NotImplementedException());
            return tcs.Task;
        }

        private static readonly Task<int> zeroTask = Task.FromResult(0);
        static Task<int> GetValueAsync()
        {
            return zeroTask;
        }

        static async Task MyMethodAsync(IProgress<double> progress = null)
        {
            double percentComplete = 0;
            var done = false;
            while (!done)
            {
                if (progress != null)
                {
                    progress.Report(percentComplete);
                }
            }            
        }

        static async Task CallMyMethodAsync()
        {
            var progress = new Progress<double>();
            progress.ProgressChanged += (sender, args) =>
            {

            };
            await MyMethodAsync(progress);
        }

        static async Task<string> DownloadAllAsync(IEnumerable<string> urls)
        {
            var httpClient = new HttpClient();
            var downloads = urls.Select(url => httpClient.GetStringAsync(url));
            Task<string>[] downloadTasks = downloads.ToArray();
            string[] htmPages = await Task.WhenAll(downloadTasks);
            return string.Concat(htmPages);
        }


        static async Task<int>  FirstRespondingUrlAsync(string urlA,string urlB)
        {
            var httpClient = new HttpClient();
            Task<byte[]> downloadTaskA = httpClient.GetByteArrayAsync(urlA);
            Task<byte[]> downloadTaskB = httpClient.GetByteArrayAsync(urlB);

            Task<byte[]> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);
            byte[] data = await completedTask;
            return data.Length;

        }

        async Task DoSomethingAsync()
        {
            int val = 13;
            //asynchrounously wait 1 second
            await Task.Delay(TimeSpan.FromSeconds(1));
            val *= 2;
            await Task.Delay(TimeSpan.FromSeconds(1));

            Trace.WriteLine(val);
        }



        async Task DoSomethingAsync2()
        {
            int val = 13;
            //asynchrounously wait 1 second
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            val *= 2;
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

            Trace.WriteLine(val.ToString());
        }

        async Task TrySomeThingAsync()
        {
            Task task = PossibleExceptionAsync();
            try
            {
                await task;
            }
            catch (NotSupportedException ex)
            {
                LogException(ex);
                throw;
            }
        }

        private void LogException(NotSupportedException ex)
        {
            throw new NotImplementedException();
        }

        private Task PossibleExceptionAsync()
        {
            throw new NotImplementedException();
        }

        async Task WaitAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
        }

        void DeadLock()
        {
            //start the delay
            Task task = WaitAsync();
            // synchronously block ,waiting for the async method to complete
            task.Wait();
        }

        void RotateMatrices(IEnumerable<Matrix> matrices,float degrees)
        {
            Parallel.ForEach(matrices, matrix => matrix.Rotate(degrees));
        }

        IEnumerable<bool> PrimalityTest(IEnumerable<int> values)
        {
            return values.AsParallel().Select(val => IsPrime(val));
        }

        private bool IsPrime(int val)
        {
            throw new NotImplementedException();
        }

        void ProcessArray(double[] array)
        {
            Parallel.Invoke(
                () => ProcessPartialArray(array, 0, array.Length / 2),
                () => ProcessPartialArray(array, array.Length / 2, array.Length));

            try
            {
                Parallel.Invoke(() => { throw new Exception(); },
                    () => { throw new Exception(); });
            }
            catch (AggregateException ex)
            {
                ex.Handle(exception =>
                {
                    Trace.WriteLine(exception);
                    return true;
                });
            }
        }

        private void ProcessPartialArray(double[] array, int begin  , int end)
        {
            throw new NotImplementedException();
        }

        static async Task<int> DelayAndReturnAsync(int val)
        {
            await Task.Delay(TimeSpan.FromSeconds(val));
            return val;
        }

        public static async Task ProcessTaskAsync()
        {
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);

            Task<int> taskC = DelayAndReturnAsync(1);

            var tasks = new[] { taskA, taskB, taskC };

            foreach (var task in tasks)
            {
                var result = await task;
                Console.WriteLine(result);
            }

        }

        public static async Task ProcessTaskAsync2()
        {
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);

            Task<int> taskC = DelayAndReturnAsync(1);

            var tasks = new[] { taskA, taskB, taskC };

            var processingTasks = (from t in tasks
                                   select AwaitAndProcessAsync(t)).ToArray();
            await Task.WhenAll(processingTasks);

        }

        static async Task AwaitAndProcessAsync(Task<int> task)
        {
            var result = await task;
            Console.WriteLine(result);
        }

        public static async Task UseOrderByCompletionAsync()
        {
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);

            Task<int> taskC = DelayAndReturnAsync(1);

            var tasks = new[] { taskA, taskB, taskC };
            // OrderByCompletion=>Nito.AsyncEx Nuget Package
            //foreach (var task in tasks.OrderByCompletion())
            //{
            //    var result = await task;
            //    Console.WriteLine(result);
            //}
        }

        async Task ResumeOnContextAsync()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException("test");
        }

        async Task ResumeWithoutContextAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            try
            {
                await ResumeOnContextAsync();
            }
            catch (InvalidOperationException)
            {

            }
        }


    }

    internal class Matrix
    {
        internal void Rotate(float degrees)
        {
            throw new NotImplementedException();
        }

        readonly BlockingCollection<int> _blcokingQuene =
            new BlockingCollection<int>();
        BlockingCollection<int> _bQuene = new BlockingCollection<int>(boundedCapacity: 1);
        BlockingCollection<int> _blockingStack = new BlockingCollection<int>(
            new ConcurrentStack<int>());
        BlockingCollection<int> _blockingBag = new BlockingCollection<int>(
            new ConcurrentBag<int>()); 

        

        public Matrix()
        {
            _blcokingQuene.Add(7);
            _blcokingQuene.Add(13);
            _blcokingQuene.CompleteAdding();
            foreach (var item in _blcokingQuene.GetConsumingEnumerable())
            {
                Trace.WriteLine(item);
            }


            _blockingStack.Add(7);
            _blockingStack.Add(13);
            _blockingStack.CompleteAdding();
            foreach (var item in _blockingStack.GetConsumingEnumerable())
            {
                Trace.WriteLine(item);
            }



            _blockingBag.Add(7);
            _blockingBag.Add(13);
            _blockingBag.CompleteAdding();
            foreach (var item in _blockingBag.GetConsumingEnumerable())
            {
                Trace.WriteLine(item);
            }
            var newValue = dic.AddOrUpdate(0,
                key => "zero",
                (key, oldvalue) => "zero");
            dic[0] = "zero";
            string currentValue;
            bool keyExists = dic.TryGetValue(0, out currentValue);
            string removeValue;
            bool keyExisted = dic.TryRemove(0, out removeValue);
        }

        BufferBlock<int> _asyncQueue = new BufferBlock<int>();
        AsyncProducerConsumerQueue<int> _asyncPQueue =
            new AsyncProducerConsumerQueue<int>();
        AsyncCollection<int> _asyncStack = new AsyncCollection<int>(
            new ConcurrentStack<int>());
        AsyncCollection<int> _asyncBags = new AsyncCollection<int>(
             new ConcurrentBag<int>());
        ConcurrentDictionary<int, string> dic = new ConcurrentDictionary<int, string>();
        ActionBlock<int> quen = new ActionBlock<int>(item => Trace.WriteLine(item));
        async Task test()
        {
            await _asyncQueue.SendAsync(7);
            await _asyncQueue.SendAsync(13);
            _asyncQueue.Complete();
            while (await _asyncQueue.OutputAvailableAsync())
            {
                Trace.WriteLine(await _asyncQueue.ReceiveAsync());
            }
            while (true)
            {
                int item;
                try
                {
                    item = await _asyncQueue.ReceiveAsync();
                }
                catch (InvalidOperationException)
                {

                    break;
                }
                Trace.WriteLine(item);
            }

            _asyncQueue.Post(7);
            _asyncQueue.Post(13);
            _asyncQueue.Complete();
            while (true)
            {
                int item;
                try
                {
                    item = await _asyncQueue.ReceiveAsync();
                }
                catch (InvalidOperationException)
                {

                    break;
                }
                Trace.WriteLine(item);
            }

            await quen.SendAsync(7);
            await quen.SendAsync(13);
            quen.Post(7);
            quen.Post(13);
            quen.Complete();



            await _asyncPQueue.EnqueueAsync(7);
            await _asyncPQueue.EnqueueAsync(13);
            _asyncPQueue.CompleteAdding();
            while (await _asyncPQueue.OutputAvailableAsync())
            {
                Trace.WriteLine(await _asyncPQueue.DequeueAsync());
            }

            await _asyncStack.AddAsync(7);
            await _asyncStack.AddAsync(13);
            _asyncStack.CompleteAdding();
            while (await _asyncStack.OutputAvailableAsync())
            {
                Trace.WriteLine(await _asyncStack.TakeAsync());
            }
            _asyncStack = new AsyncCollection<int>(
                new ConcurrentStack<int>(), maxCount: 1);
            await _asyncStack.AddAsync(7);
            await _asyncStack.AddAsync(13);
            await _asyncStack.CompleteAddingAsync();
            while (true)
            {
                var taleResult = await _asyncStack.TryTakeAsync();
                if (!taleResult.Success)
                {
                    break;
                }
                Trace.WriteLine(taleResult.Item);
            }
        }
    }

    public interface IMyAsyncHttpService
    {
        void DownloadString(Uri address, Action<string, Exception> callback);
    }

    internal static class WrapAsync

    {

        public static Task<string> DownloadStringAsync(this WebClient client, Uri address)
        {
            var tcs = new TaskCompletionSource<string>();
            DownloadStringCompletedEventHandler handler = null;
            handler = (_, e) =>
            {
                client.DownloadStringCompleted -= handler;
                if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else
                    tcs.TrySetResult(e.Result);



            };

            client.DownloadStringCompleted += handler;
            client.DownloadStringAsync(address);
            return tcs.Task;
        }

        public static Task<WebResponse> GetRspAsync(this WebRequest client)
        {
            return Task<WebResponse>.Factory.FromAsync(client.BeginGetResponse, client.EndGetResponse, null);
        }

        public static Task<string> DownloadStringAsync(this IMyAsyncHttpService httpService, Uri address)
        {
            var tcs = new TaskCompletionSource<string>();
            httpService.DownloadString(address, (result, ex) =>
            {
                if (ex != null)
                    tcs.TrySetException(ex);
                else
                    tcs.TrySetResult(result);



            });

            return tcs.Task;
        }

    }

    interface IObserver<in T>
    {
        void OnNext(T item);
        void OnCompleted();

        void OnError(Exception error);
    }

    interface IObservable<out T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }

    interface IMyAsyncInterface
    {
        Task<int> GetvluesAsync();
    }
    class MySynchronousImlementation : IMyAsyncInterface
    {
        public Task<int> GetvluesAsync()
        {
            return Task.FromResult(13);
        }
    }
    public class Test
    {
        void testDx()
        {

            //IObservable<DateTimeOffset> timestamps = 
            //    Obervable.Interval(TimeSpan.FromSeconds(1))
            //    .Timestamp()
            //    .Where(x => x.Value % 2 == 0)
            //    .Select(x => x.Timestamp)
            //    .Subsribe(x => Trace.WriteLine(x));

            //Obervable.Interval(TimeSpan.FromSeconds(1))
            //    .Timestamp()
            //    .Where(x => x.Value % 2 == 0)
            //    .Select(x => x.Timestamp)
            //    .Subsribe(x => Trace.WriteLine(x));
        }

        private class Obervable : IObservable<Obervable>
        {
            internal static Object Interval(TimeSpan timeSpan)
            {
                throw new NotImplementedException();
            }

            public IDisposable Subscribe(IObserver<Obervable> observer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
