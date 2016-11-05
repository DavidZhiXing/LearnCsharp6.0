using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    public class Container<T> : IEnumerable<T>
    {
        public Container()
        {

        }
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<int> GetValues()
        {
            yield return 10;
            yield return 20;
            yield return 30;
            yield return 300;
            yield return 1000;
        }

        
    }
}
