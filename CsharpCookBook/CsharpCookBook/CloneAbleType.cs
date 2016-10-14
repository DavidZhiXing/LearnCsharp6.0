using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    //CloneAbleType
    public interface IShallowCopy<T>
    {
        T ShallowCopy();
    }

    public interface IDeepCopy<T>
    {
        T DeepCopy();
    }

    public class ShowllowClone : IShallowCopy<ShowllowClone>
    {
        public int Data = 1;
        public List<string> ListData = new List<string>();
        public Object ObjData = new Object();
        public ShowllowClone ShallowCopy()
        {
            return (ShowllowClone)this.MemberwiseClone();
        }
    }

    [Serializable]
    public class MultiClone : IShallowCopy<MultiClone>, IDeepCopy<MultiClone>
    {
        public int data = 1;
        public List<string> ListData = new List<string>();
        public object objData = new object();
        public MultiClone DeepCopy()
        {
            BinaryFormatter bf = new BinaryFormatter();
            var memStream = new MemoryStream();
            bf.Serialize(memStream, this);
            memStream.Flush();
            memStream.Position = 0;
            return (MultiClone)bf.Deserialize(memStream);
        }

        public MultiClone ShallowCopy()
        {
            return (MultiClone)this.MemberwiseClone();
        }

        public void ObjectDispose()
        {
            using (FileStream fs = new FileStream("Test.txt", FileMode.Create))
            {
                fs.WriteByte((byte)1);
                fs.WriteByte((byte)2);
                 
                fs.WriteByte((byte)3);
                using (var SW = new StreamWriter(fs))
                {
                    SW.WriteLine("some text.");
                }

            }
        }
    }

}
