using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    class Parallel1
    {
        static int ParallelSum(IEnumerable<int> values)
        {
            object mutex = new object();
            int result = 0;
            Parallel.ForEach(source: values,
                localInit: () => 0,
                body: (item, state, localValuie) => localValuie + item,
                localFinally: localVaue =>
                 {
                     lock (mutex)
                     {
                         result += localVaue;
                     }
                 });
            return result;
        }
    }
}
