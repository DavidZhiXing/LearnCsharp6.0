using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsharpCookBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;

namespace CsharpCookBook.Tests
{
    [TestClass()]
    public class DisposableListTests
    {




        [TestMethod()]
        public void TestDisposibleListClsTest()
        {
            Test.TestDisposibleListCls();
        }

        [TestMethod()]
        public void ShowSettingFieldsToDefaultsTest()
        {
            Test.ShowSettingFieldsToDefaults();
        }

        [TestMethod()]
        public void GeneratedEntityTest()
        {
            Console.WriteLine("start entity work");
            GeneratedEntity entity = new GeneratedEntity("FirstEntity");
            entity.FirstName = "Bob";
            entity.State = "NH";
            GeneratedEntity sencondEntity = new GeneratedEntity("sencondEntity");
            entity.FirstName = "Jay";
            sencondEntity.FirstName = "Steve";
            sencondEntity.State = "MA";
            entity.FirstName = "Barry";
            sencondEntity.State = "WA";
            sencondEntity.FirstName = "Matt";
            Console.WriteLine("End work");
        }

        [TestMethod()]
        public void StockPortfolioTest()
        {
            StockPortfolio tech = new StockPortfolio()
            {
                {"abc",-10.5 },
                {"ddd",2.0 },
                {"eee",12.3 },
                {"ggg",0.5 },
                {"ttt",15.7 },
                {"PCLS",9.16 }
            };
            tech.PrintPortfolio("Starting portfolio");
            var worst = tech.GetWorstPerformers(2);
            Console.WriteLine("selling worst");
            worst.DisplayStocks();
            tech.SellStocks(worst);
            tech.PrintPortfolio("After selling worst 3 performers");
        }

        [TestMethod()]
        public void CopyToTest()
        {
            var sample = new List<int> { 1, 2, 2, 2, 2, 3, 5,6, 7, 7, 7,8, 9 };
            Console.WriteLine("GetAll");
            var items = sample.GetAll(2);
            foreach (var item in items)
            {
                Console.WriteLine($"item2:{item}");
            }

            items = sample.GetAll(5);
            foreach (var item in items)
            {
                Console.WriteLine($"item5:{item}");
            }

            Console.WriteLine($"count2:{sample.CountAl(2)}");
            Console.WriteLine($"count3:{sample.CountAl(3)}");
            Console.WriteLine($"count5:{sample.CountAl(5)}");
            Console.WriteLine("BinarySearchGetAll:");
            var items2 = sample.BinarySearchGetAll(7);
            foreach (var item in items2)
            {
                Console.WriteLine($"item7:{item}");
            }
            items2 = sample.BinarySearchGetAll(2);
            foreach (var item in items2)
            {
                Console.WriteLine($"item2:{item}");
            }

            Console.WriteLine($"count2:{sample.BinarySearchCountAll(2)}");
            Console.WriteLine($"count3:{sample.BinarySearchCountAll(3)}");
            Console.WriteLine($"count5:{sample.BinarySearchCountAll(5)}");
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            var temp = new SortedList<int>();
            temp.Add(200);
            temp.Add(100);
            temp.Add(2);
            temp.Add(13);
            temp.Add(7);
            temp.Add(90);
            temp.Add(20);
            temp.Add(15);
            temp.Add(20);
            temp.Add(15);
            foreach (var item in temp)
            {
                Console.WriteLine(item);
            }
            temp.modifySorted(100, 3);
            temp.modifySorted(8, 5);
            Console.WriteLine("....");
            foreach (var item in temp)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            Dictionary<string, string> hash = new Dictionary<string, string>()
            {
                ["2"] = "two",
                ["1"] = "one",
                ["3"] = "three",
                ["5"] = "five",
                ["4"] = "four"
            };
            var x = from k in hash.Keys orderby k ascending select k;
            foreach (var s in x)
                Console.WriteLine($"key:{s} value:{hash[s]}");
            x = from k in hash.Keys orderby k descending select k;
            foreach (var s in x)
                Console.WriteLine($"key:{s} value:{hash[s]}");


        }

        [TestMethod()]
        public void InsertTest()
        {
            SortedDictionary<string, string> hashed = new SortedDictionary<string, string>()
            {
                ["2"] = "two",
                ["1"] = "one",
                ["3"] = "three",
                ["5"] = "five",
                ["4"] = "four"
            };

            foreach (var item in hashed.OrderByDescending(item => item.Key))
            {
                Console.WriteLine($"key:{item.Key} value:{item.Value}");
            }
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Dictionary<string, string> hash = new Dictionary<string, string>()
            {
                ["2"] = "two",
                ["1"] = "one",
                ["3"] = "three",
                ["5"] = "five",
                ["4"] = "four"
            };
            var x = from k in hash.Keys orderby k descending select k;
            foreach (var s in x)
                Console.WriteLine($"key:{s} value:{hash[s]}");
        }

        [TestMethod()]
        public void RemoveAtTest()
        {
            var ht = new ArrayList() { "zero", "one", "two" };
            foreach (var item in ht)
            {
                Console.WriteLine(item);
            }
            MyHelper.SerializeToFile<ArrayList>(ht, "ht.data");

            var htNew = new ArrayList();
            htNew = MyHelper.DeserializeFromFile<ArrayList>("ht.data");
            foreach (var item in htNew)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod()]
        public void ListTest()
        {
            var ht = new List<string>() { "zero", "one", "two" };
            var str = ht.TrueForAll(delegate (string val)
            {
                if (val == null)
                {
                    return false;
                }
                else
                    return true;
            }).ToString();
            Console.WriteLine(str?.Length);
        }

        [TestMethod()]
        public void ForeachTest()
        {
            //GroupEnumerator<string>.CreatNestedObjects();
            new FanTest().FanTest2();
        }


    }
}