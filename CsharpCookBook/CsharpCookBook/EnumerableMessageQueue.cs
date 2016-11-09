using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    public class EnumerableMessageQueue : MessageQueue, IEnumerable<Message>
    {
        public EnumerableMessageQueue() : base()
        {

        }
        public EnumerableMessageQueue(string paht) : base(paht)
        {

        }
        public EnumerableMessageQueue(string path, bool shareModeDenyReceive) :
            base(path, shareModeDenyReceive)
        {

        }
        public EnumerableMessageQueue(string path, bool shareModeDenyReceive, bool enableCache) :
                base(path, shareModeDenyReceive, enableCache)
        {

        }
        public EnumerableMessageQueue(string path, bool shareModeDenyReceive,
                            bool enableCache, QueueAccessMode accessMode) :
            base(path, shareModeDenyReceive, enableCache, accessMode)
        {

        }

        public static new EnumerableMessageQueue Create(string path) =>
            Create(path, false);

        public static new EnumerableMessageQueue Create(string path, bool transactional)
        {
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path, transactional);
            }
            return new EnumerableMessageQueue(path);
        }

        public new MessageEnumerator GetMessageEnumerator()
        {
            throw new NotSupportedException("Please use xxxx");
        }

        public new MessageEnumerator GetMessageEnumerator2()
        {
            throw new NotSupportedException("Please use xxxx");
        }
        IEnumerator<Message> IEnumerable<Message>.GetEnumerator()
        {
            var messageEnumator = base.GetMessageEnumerator2();
            while (messageEnumator.MoveNext())
            {
                yield return messageEnumator.Current;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            var messageEnumator = base.GetMessageEnumerator2();
            while (messageEnumator.MoveNext())
            {
                yield return messageEnumator.Current;
            }
        }
        
    }

    public class EnumerableMessageQueueTest
    {
        string queuePath = @".\private$\LINQMQ";
        EnumerableMessageQueue messageQueue = null;
        public  void test()
        {
            if (!EnumerableMessageQueue.Exists(queuePath))
            {
                messageQueue = EnumerableMessageQueue.Create(queuePath);
            }
            else
                messageQueue = new EnumerableMessageQueue(queuePath);

            using (messageQueue)
            {
                var messageFormatter = new BinaryMessageFormatter();
                var query = from Message msg in messageQueue
                            where ((msg.Formatter = messageFormatter) == messageFormatter) &&
                                int.Parse(msg.Label) < 5 &&
                                msg.Body.ToString().Contains("CSharpRecipes.D")
                            orderby msg.Body.ToString() descending
                            select msg;
                foreach (var item in query)
                {
                    Console.WriteLine($"item.Label:{item.Label}"+
                        $"item.Body:{item.Body}");
                }
            }
        }
        
    }
}
