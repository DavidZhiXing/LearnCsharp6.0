using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsharpCookBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CsharpCookBook.Tests
{
    [TestClass()]
    public class DisposableListTests
    {




        [TestMethod()]
        public void dddTest()
        {
            Test.TestDisposibleListCls();
        }

        [TestMethod()]
        public void AddTest()
        {
            Test.ShowSettingFieldsToDefaults();
        }

        [TestMethod()]
        public void ClearTest()
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
        public void ContainsTest()
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
            Assert.Fail();
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveAtTest()
        {
            Assert.Fail();
        }
    }
}