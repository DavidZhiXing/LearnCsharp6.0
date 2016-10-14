using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsharpCookBook.SortableType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook.SortableType.Tests
{
    [TestClass()]
    public class SquareTests
    {
        [TestMethod()]
        public void SquareTest()
        {
            List<Square> listOfSquare = new List<Square>()
            {
                new Square(1,3),
                new Square(4,3),
                new Square(2,1),
                new Square(6,1)
            };

            Console.WriteLine("List<String>");
           // Console.WriteLine("Original list");

            foreach (var item in listOfSquare)
            {
                Console.WriteLine(item.ToString());
            }
            //Assert.Fail();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("CompareHeight ");
           
            var heightCompare = new CompareHeight();
            listOfSquare.Sort(heightCompare);
            foreach (var item in listOfSquare)
            {
                Console.WriteLine(item.ToString());
            }

            int found = listOfSquare.BinarySearch(new Square(1, 3), heightCompare);
            Console.WriteLine($"Found(1,3):{found}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Sort ");
            listOfSquare.Sort();
            foreach (var item in listOfSquare)
            {
                Console.WriteLine(item.ToString());
            }


            found = listOfSquare.BinarySearch(new Square(6, 1));
            Console.WriteLine($"Found(6,1):{found}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("SortedList<int,Square>()");
            var sortedListOfSquare = new SortedList<int, Square>()
            {
                {0,new Square(1,3) },
                {2,new Square(3,3) },
                {3,new Square(2,1) },
                {1,new Square(6,1) },
            };

            foreach (var item in sortedListOfSquare)
            {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }

            var foundItem = sortedListOfSquare.ContainsKey(2);
            Console.WriteLine($"sortedListOfSquare.ContainsKey(2):{foundItem}");

        }
    }
}