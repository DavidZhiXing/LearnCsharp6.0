using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    // GenericType

    public class FixedSizeCollection
    {
        public FixedSizeCollection(int maxItems)
        {
            FixedSizeCollection.InstanceCount++;
            this.Items = new object[maxItems];
        }

        public int AddItem(object item)
        {
            if (this.ItemCount < this.Items.Length)
            {
                this.Items[this.ItemCount] = item;
                return this.ItemCount++;
            }
            else
                throw new Exception("item queue is full");
        }

        public object GetItem(int index)
        {
            if (index >= this.Items.Length && index >= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            return this.Items[index];
        }
        public static int InstanceCount { get; private set; }
        public object[] Items { get; private set; }
        public int ItemCount { get; private set; }

        public override string ToString() =>
            $"there are {FixedSizeCollection.InstanceCount.ToString()}";

    }

    public class FixedSizeCollection<T>
    {
        public FixedSizeCollection(int items)
        {
            FixedSizeCollection<T>.InstanceCount++;
            this.Items = new T[items];
        }

        public int AddItem(T item)
        {
            if (this.ItemCount < this.Items.Length)
            {
                this.Items[this.ItemCount] = item;
                return this.ItemCount++;
            }
            else
                throw new Exception("item queue is full");
        }

        public T GetItem(int index)
        {
            if (index >= this.Items.Length && index >= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            return this.Items[index];
        }

        public static int InstanceCount { get; private set; }
        public T[] Items { get; private set; }
        public int ItemCount { get; private set; }

        public override string ToString() =>
        $"there are {FixedSizeCollection<T>.InstanceCount.ToString()}";

        public void reverseList()
        {
            SortedList<int, string> data = new SortedList<int, string>()
            { [2] = "tow", [5] = "five", [3] = "three", [1] = "one" };
            foreach (var item in data)
            {
                Console.WriteLine($"\t {item.Key}\t{item.Value}");
            }

            var query = from d in data
                        orderby d.Key descending
                        select d;
            foreach (var item in query)
            {
                Console.WriteLine($"\t {item.Key}\t{item.Value}");
            }
            data.Add(4, "four");

            query = from d in data
                    orderby d.Key descending
                    select d;
            foreach (var item in query)
            {
                Console.WriteLine($"\t {item.Key}\t{item.Value}");
            }
            foreach (var item in data)
            {
                Console.WriteLine($"\t {item.Key}\t{item.Value}");
            }

        }

    }

    //Constaining Type

    public class DisposableList<T> : IList<T>
        where T : class, IDisposable
    {
        private List<T> _item = new List<T>();
        public T this[int index]
        {
            get
            {
                return (_item[index]);
            }

            set
            {
                _item[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _item.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(T item)
        {
            _item.Add(item);
        }

        public void Clear()
        {

            for (int index = 0; index < _item.Count; index++)
            {
                Delete(_item[index]);
            }
            _item.Clear();
        }

        public bool Contains(T item)
        {
            return _item.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _item.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _item.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _item.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _item[index] = item;
        }

        public bool Remove(T item)
        {
            int index = _item.IndexOf(item);
            if (index >= 0)
            {
                Delete(_item[index]);
                _item.RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            Delete(this[index]);
            _item.RemoveAt(index);
        }

        private void Delete(T t)
        {
            t.Dispose();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _item.GetEnumerator();
        }
    }

    public class Test
    {
        public static void TestDisposibleListCls()
        {
            DisposableList<StreamReader> dl = new DisposableList<StreamReader>();
            StreamReader sr1 = new StreamReader("c:\\Windows\\system.ini");
            StreamReader sr2 = new StreamReader("c:\\Windows\\iis7.log");

            StreamReader sr3 = new StreamReader("c:\\Windows\\Starter.xml");

            dl.Add(sr1);
            //dl.Insert(0, sr2);
            dl.Add(sr2);
            dl.Add(sr3);

            foreach (var item in dl)
            {
                Console.WriteLine($"sr.ReadLine=={item.ReadLine()}");
            }

            dl.RemoveAt(0);
            dl.Remove(sr1);
            dl.Clear();
        }

        public static void ShowSettingFieldsToDefaults()
        {
            DefaultValueExample<int> dv = new DefaultValueExample<int>();
            var isDefault = dv.IsDefaultData();
            Console.WriteLine($"Initial data:{isDefault}");
            dv.SetData(100);
            isDefault = dv.IsDefaultData();
            Console.WriteLine($"Set data:{isDefault}");
        }
    }

    public class DefaultValueExample<T>
    {
        T data = default(T);

        public bool IsDefaultData()
        {
            T temp = default(T);
            if (temp.Equals(data))
            {
                return true;
            }
            else
                return false;
        }

        public void SetData(T val) => data = val;
    }

    public partial class GrneratedEntity
    {
        public GrneratedEntity(string entityName)
        {
            this.EntityName = entityName;
        }

        partial void ChangingProperty(string name, string originalValue, string newValue);

        public string EntityName { get; private set; }
    }

}
