using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            await ConcurrencyTask.ProcessTaskAsync2();
        }//
        
    }


}
