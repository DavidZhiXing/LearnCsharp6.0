using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    [Serializable]
    public class MinMaxDictionary<T,U> where U:IComparable<U>
    {
        private Dictionary<T, U> internalDictionary;

        public MinMaxDictionary(U min,U max)
        {
            this.MinValue = min;
            this.MaxValue = max;
            this.internalDictionary = new Dictionary<T, U>();
        }

        public U MaxValue { get; private set; } = default(U);
        public U MinValue { get; private set; } = default(U);

        public int Count => (internalDictionary.Count);
        public Dictionary<T, U>.KeyCollection keys => (internalDictionary.Keys);
        public Dictionary<T, U>.ValueCollection values => (internalDictionary.Values);

        public U this[T key]
        {
            get { return (internalDictionary[key]); }
            set
            {
                if (value.CompareTo(MinValue) >= 0 &&
                    value.CompareTo(MaxValue) <= 0)
                    internalDictionary[key] = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        $"value must be within the range {MinValue} to {MaxValue}");
                {

                }
            }
        }

        public bool ContainsKey(T key) => (internalDictionary.ContainsKey(key));
        public bool ContainsValue(U value) => (internalDictionary.ContainsValue(value));
        public override bool Equals(object obj) => (internalDictionary.Equals(obj));

        public IEnumerator GetEnumerator() => (internalDictionary.GetEnumerator());

        public override int GetHashCode()
        {
            return (internalDictionary.GetHashCode());
        }
        public void GetobjectData(SerializationInfo info,StreamingContext context)
        {
            internalDictionary.GetObjectData(info, context);
        }

        public void Ondeserialization(object sender)
        {
            internalDictionary.OnDeserialization(sender);
        }

        public override string ToString()
        {
            return internalDictionary.ToString();
        }

        public bool TryGetValue(T key, out U value) =>
            internalDictionary.TryGetValue(key, out value);
        public void Remove(T key)
        {
            internalDictionary.Remove(key);
        }

        public void Clear()
        {
            internalDictionary.Clear();
        }
    }
}
