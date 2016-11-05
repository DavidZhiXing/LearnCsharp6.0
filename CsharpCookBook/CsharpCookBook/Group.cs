using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    public class Group<T> : IEnumerable<T>
    {
        public Group(string name)
        {
            this.Name = name;
        }
        public List<T> _groupList = new List<T>();
        public string Name { get; private set; }

        public int Count => _groupList.Count;

        public void Add(T item)
        {
            _groupList.Add(item);
        }
        public IEnumerator<T> GetEnumerator() => _groupList.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public class Item
    {
        public Item(string name, int location)
        {
            this.Name = name;
            this.Location = location;
        }

        public int Location { get; private set; }
        public string Name { get; private set; }
    }

    public class GroupEnumerator<T> : IEnumerator
    {
        public T[] _items;
        int position = -1;
        public GroupEnumerator(T[] list)
        {
            _items = list;
        }
        public bool MoveNext()
        {
            position++;
            return (position < _items.Length);
        }

        public void Reset()
        {
            position = -1;
        }


        object IEnumerator.Current
        {
            get
            {
                try
                {
                    return _items[position];
                }
                catch (IndexOutOfRangeException)
                {

                    throw new InvalidOperationException();
                }
            }
        }


        public static void CreatNestedObjects()
        {
            Group<Group<Item>> hierarchy =
                new Group<Group<Item>>("root")
                {
                    new Group<Item>("subbGroup1")
                    {
                        new Item("item1",100),
                        new Item("item2",100),
                    },
                    new Group<Item>("subGroup2")
                    {
                        new Item("item1",100),
                        new Item("item2",100),
                    }
                };
            IEnumerator enumerator = ((IEnumerable)hierarchy).GetEnumerator();
            while(enumerator.MoveNext())
            {
                Console.WriteLine(((Group<Item>)enumerator.Current).Name);
                foreach (var item in (Group<Item>)enumerator.Current)
                {
                    Console.WriteLine(item.Name);
                }
            }
            DisplayNestedobjects(hierarchy);
        }

        private static void DisplayNestedobjects(Group<Group<Item>> hierarchy)
        {
            Console.WriteLine($"toplevelGroup:{hierarchy.Count}");
            Console.WriteLine($"toplevelGroupName:{hierarchy.Name}");
            foreach (var item in hierarchy)
            {
                Console.WriteLine($"\tsubGroup.SubGroupName:{item.Name}");
                Console.WriteLine($"\tsubGroup.SubGroupCount:{item.Count}");
                foreach (var i in item)
                {
                    Console.WriteLine($"\t\titem.Name:{i.Name}");
                    Console.WriteLine($"\t\titem.Location:{i.Location}");
                }
            }
        }
    }
}
