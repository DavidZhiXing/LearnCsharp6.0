using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{

    /// <summary>
    /// SortableType
    /// </summary>
    public class Square : IComparable<Square>
    {
        public Square()
        {

        }

        public Square(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }

        public int Height { get; private set; }
        public int Width { get; private set; }

        public int CompareTo(object other)
        {
            var square = other as Square;
            if (square != null)
            {
                return CompareTo(square);
            }
            throw
                new ArgumentException(
                    "Both objects being compared must be of type squre");
        }

        public override string ToString() => ($"Height:{this.Height} Width:{this.Width}");

        public int CompareTo(Square other)
        {
            long area1 = this.Height * this.Width;
            long area2 = other.Height * other.Width;

            if (area1 == area2)
            {
                return 0;
            }
            else if (area1 > area2)
            {
                return 1;
            }
            else if (area1 < area2)
            {
                return -1;
            }
            else return -1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var square = obj as Square;
            if (square != null)
            {
                return this.Height == square.Height;
            }
            return false;
            //return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Height.GetHashCode() | this.Width.GetHashCode();
        }

        public static bool operator ==(Square x, Square y) => x.Equals(y);
        public static bool operator !=(Square x, Square y) => !(x == y);
        public static bool operator <(Square x, Square y) => (x.CompareTo(y) < 0);
        public static bool operator >(Square x, Square y) => (x.CompareTo(y) > 0);


        public void ReturnDimensions(int inputShape,out int height,out int width,out int depth)
        {
            height = 0;
            width = 0;
            depth = 0;
        }

        public Tuple<int,int,int> ReturnDimensions(int inputShape)
        {
            var objDim = Tuple.Create<int, int, int>(5, 10, 5);
            return (objDim);
        }

        public readonly int age;


    }
    public class CompareHeight : IComparer<Square>
    {
        public int Compare(object firstSquare, object secondSquare)
        {
            var square1 = firstSquare as Square;
            var square2 = secondSquare as Square;
            if (square1 == null || square2 == null)
            {
                throw (new ArgumentException(""));
            }
            else
                return Compare(firstSquare, secondSquare);
        }

        public int Compare(Square x, Square y)
        {
            if (x.Height == y.Height)
            {
                return 0;
            }

            else if (x.Height > y.Height)
            {
                return 1;
            }
            else
                return -1;
        }

    }

    public class SortedList<T>:List<T>
    {
        public new void Add(T item)
        {
            int pos = this.BinarySearch(item);
            if (pos < 0)
            {
                pos = ~pos;
            }
            this.Insert(pos, item);
        }

        public void modifySorted(T item, int index)
        {
            this.RemoveAt(index); 

            int pos = this.BinarySearch(item);
            if (pos<0)
            {
                pos = ~pos;
            }
            this.Insert(pos, item);
        }
    }
}
