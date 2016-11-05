using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    public class StringSet : IEnumerable<string>
    {
        private List<string> _item = new List<string>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()=> GetEnumerator();


        private IEnumerator<string> GetEnumerator()
        {
            try
            {
                for(int index = 0;index < _item.Count;index++)
                {
                    yield return (_item[index]);
                }
            }
            finally
            {
                Console.WriteLine("");

            }

            }
        }


}
