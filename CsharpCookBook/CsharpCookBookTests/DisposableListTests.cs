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
            Assert.Fail();
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