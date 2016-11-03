using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
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
    public partial class GeneratedEntity
    {
        public GeneratedEntity(string entityName)
        {
            this.EntityName = entityName;
        }

        partial void ChangingProperty(string name, string originalValue,
            string newValue);



        public string EntityName { get; private set; }

        public string FirstName
        {
            get
            {
                return _FirstName;
            }

            set
            {
                ChangingProperty("FirstName", _FirstName, value);
                _FirstName = value;
            }
        }

        public string State
        {
            get
            {
                return _State;
            }

            set
            {
                ChangingProperty("State", _FirstName, value);
                _State = value;
            }
        }

        private string _FirstName;

        private string _State;



    }

    public partial class GeneratedEntity
    {
        partial void ChangingProperty(string name, string originalValue, string newValue)
        {
            Console.WriteLine($"Changed property {name} for entity" +
                $"{this.EntityName} from" +
                $"{originalValue} to {newValue}");
        }

        public static void InvokeInReverse()
        {
            Func<int> d1 = TestInvokeIntReturn.Method1;
            Func<int> d2 = TestInvokeIntReturn.Method2;
            Func<int> d3 = TestInvokeIntReturn.Method3;

            Func<int> allInstances = d1 + d2 + d3;
            Console.WriteLine("Fire delegates in reverse");
            Delegate[] delegateList = allInstances.GetInvocationList();
            foreach (Func<int> item in delegateList.Reverse())
            {
                item();
            }
        }


        public static void InvokeEveryOtherOpration()
        {
            Func<int> d1 = TestInvokeIntReturn.Method1;
            Func<int> d2 = TestInvokeIntReturn.Method2;
            Func<int> d3 = TestInvokeIntReturn.Method3;
            Func<int> allInstances = d1 + d2 + d3;
            Delegate[] delegateList = allInstances.GetInvocationList();

            Console.WriteLine("Invoke every other delegate");
            foreach (Func<int> instance in delegateList.EveryOther())
            {
                int retVal = instance();
                Console.WriteLine($"\tOutput:{retVal}");
            }


        }

        public static void InvokeExceptions()
        {
            Func<int> d1 = TestInvokeIntReturn.Method1;
            Func<int> d2 = TestInvokeIntReturn.Method2;
            Func<int> d3 = TestInvokeIntReturn.Method3;
            Func<int> allInstances = d1 + d2 + d3;
            Delegate[] delegateList = allInstances.GetInvocationList();
            List<Exception> invocationEx = new List<Exception>();
            Console.WriteLine("Invoke every other delegate");
            foreach (Func<int> instance in delegateList)
            {
                try
                {
                    int retVal = instance();
                    Console.WriteLine($"\tOutput:{retVal}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    EventLog myLog = new EventLog();
                    myLog.Source = "C";
                    myLog.WriteEntry("");
                    invocationEx.Add(ex);
                }
            }
            if (invocationEx.Count > 0)
            {
                throw new MulticastInvocationEx(invocationEx);
            }


        }

    }

    [Serializable]
    internal class MulticastInvocationEx : Exception
    {
        private List<Exception> invocationEx;

        public MulticastInvocationEx() : base()
        {
        }

        public MulticastInvocationEx(string message) : base(message)
        {
        }

        public MulticastInvocationEx(IEnumerable<Exception> invocationEx)
        {
            this.invocationEx = new List<Exception>(invocationEx);
        }

        public MulticastInvocationEx(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MulticastInvocationEx(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.invocationEx = (List<Exception>)info.GetValue("InvocationExceptions", typeof(List<Exception>));
        }
        //[SecurityPermissionAttribute(SecurityAction.Demand,
        //    SerializationFormatter = true)]
        //public override void GetObjectData(SerializationInfo info,StreamingContext contex)
    }

    public static class TestInvokeIntReturn
    {
        internal static int Method1()
        {
            Console.WriteLine("Method1");
            return 1;
        }

        internal static int Method2()
        {
            Console.WriteLine("Method1");
            return 1;
        }

        internal static int Method3()
        {
            Console.WriteLine("Method1");
            return 1;
        }

        public static IEnumerable<T> EveryOther<T>(this IEnumerable<T> list)
        {
            bool reNext = true;
            foreach (T t in list)
            {
                if (reNext) yield return t;
                reNext = !reNext;
            }
        }

        class SalesPerson
        {
            private decimal _commission;

            public SalesPerson()
            {

            }
            public SalesPerson(string name, decimal annualQuota,
                                decimal commissionRate)
            {
                this.Name = name;
                this.AnnualQuota = annualQuota;
                this.CommissionRate = commissionRate;
            }

            public decimal AnnualQuota { get; private set; }
            public decimal CommissionRate { get; private set; }
            public string Name { get; private set; }

            public decimal Commission
            {
                get { return _commission; }
                set
                {
                    _commission = value;
                    this.TotalCommission += _commission;
                }
            }

            public decimal TotalCommission { get; private set; }
            delegate void CalculateEarning(SalesPerson sp);
            static Action<SalesPerson> GetEarningsCalc(decimal quarterlySales,
                                                decimal bonusRate)
            {
                //SalesPerson salesPerson = new SalesPerson();
                return salesPerson =>
                {
                    decimal quota = (salesPerson.AnnualQuota / 4);
                    if (quarterlySales < quota)
                    {
                        salesPerson.Commission = 0;
                    }
                    else if (quarterlySales > (quota * 2.0m))
                    {
                        decimal baseComission = quota * salesPerson.Commission;
                        salesPerson.Commission = (baseComission + ((quarterlySales - quota)
                        * (salesPerson.Commission * (1 + bonusRate))));
                    }
                    else
                    {
                        salesPerson.Commission = salesPerson.Commission * quarterlySales;
                    }
                };
            }

            void TestSalePerson()
            {
                SalesPerson[] sps =
                {
                    new SalesPerson {Name="Chas",AnnualQuota=10000m,Commission = 0.10m },
                    new SalesPerson {Name="Cas",AnnualQuota=20000m,Commission = 0.010m },
                    new SalesPerson {Name="has",AnnualQuota=50000m,Commission = 0.020m }
                };

                QuarterlyEarning[] earnings =
                {
                    new QuarterlyEarning {Name="Q1",AnnualQuota=10000m,Commission = 0.10m },
                    new QuarterlyEarning {Name="Q2",AnnualQuota=20000m,Commission = 0.010m },
                    new QuarterlyEarning {Name="Q3",AnnualQuota=50000m,Commission = 0.020m }
                };

                var caculators = from e in earnings
                                 select new
                                 {
                                     Caculator = GetEarningsCalc(e.AnnualQuota, e.Commission),
                                     QuarterlyEarning = e
                                 };
            }

            static void WriteQuarterlyReport(string quarter,decimal quarterSales,
                CalculateEarning eCalc,SalesPerson[] salePerson)
            {
                var s0 = $"{quarter} sales earnings on quarterly sales of {quarterSales}";
                foreach (var sp in salePerson)
                {
                    eCalc(sp);
                    var s = $"salesperson {sp.Name} made a commision of:{sp.Commission}";
                    Console.WriteLine("", sp.Name, sp.Commission);
                }
            }

            static void WriteCommissionReport(decimal annualEarnings,
                    SalesPerson[] salePeople)
            {
                decimal revenueProduced = (annualEarnings) / salePeople.Length;
                var whoToCan = from sp in salePeople
                               select new
                               {
                                   CanThem = (revenueProduced * 0.2m) < sp.TotalCommission,
                                   sp.Name,
                                   sp.TotalCommission

                               };
                foreach (var si in whoToCan)
                {
                    var s = $"paid+{si.Name} to  {si.TotalCommission} produce {revenueProduced}";
                    if (si.CanThem)
                    {
                        var bb =$"Fire {si.Name}";
                    }
                }
            }
        }
    }

    internal class QuarterlyEarning
    {
        public decimal AnnualQuota { get; internal set; }
        public decimal Commission { get; internal set; }
        public string Name { get; internal set; }
    }
}
