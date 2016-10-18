using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConcurrencyCookbook
{
    [TestClass]
    public class LinkingBlocks
    {
        [TestMethod]
        public async Task MyMethodAsync_ReturnFalse()
        {
            await ConcurrencyTask1.ProcessTaskAsync2();
        }

        [TestMethod]
        public void MyMethodAsync_void()
        {
            AsyncContext.Run(() =>
            ConcurrencyTask1.ProcessTaskAsync());
        }

    }


}
