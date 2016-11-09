using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    public class MyHelper
    {
        /// <summary>
        /// Serializer the object to and from a file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="dataFile"></param>
        public static void SerializeToFile<T>(T obj,string dataFile)
        {
            using(var fileStream = File.Create(dataFile))
            {
                BinaryFormatter binSerializer = new BinaryFormatter();
                binSerializer.Serialize(fileStream, obj);
            }
        }

        public static T DeserializeFromFile<T>(string dataFile)
        {
            T obj = default(T);
            using (var fileStream = File.OpenRead(dataFile))
            {
                var binSerializer = new BinaryFormatter();
                obj = (T)binSerializer.Deserialize(fileStream);
            }
            return obj;
        }

        /// <summary>
        /// Serializer the object to and from byte array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="dataFile"></param>
        public static byte[] SerializeToStream<T>(T obj)
        {
            using (var fileStream = new MemoryStream())
            {
                BinaryFormatter binSerializer = new BinaryFormatter();
                binSerializer.Serialize(fileStream, obj);
                return fileStream.ToArray();
            }
        }

        public static T DeserializeFromStream<T>(byte[] dataFile)
        {
            T obj = default(T);
            using (var fileStream = new MemoryStream(dataFile))
            {
                var binSerializer = new BinaryFormatter();
                obj = (T)binSerializer.Deserialize(fileStream);
            }
            return obj;
        }

        public static double RoundDown(double valueToRound)
        {
            var floorValue = Math.Round(valueToRound);
            if ((valueToRound - floorValue > .5))
            {
                return floorValue + 1;
            }
            else
                return floorValue;
        }
    }
}
