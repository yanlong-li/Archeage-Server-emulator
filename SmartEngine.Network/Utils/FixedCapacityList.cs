using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Utils
{
    /// <summary>
    /// 容量固定的列表类，用于快速进行索引，并且无需动态改变容量的列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedCapacityList<T> : IList<T>
    {
        T[] array;
        int capacity;

        public int Capacity { get { return capacity; } }
        public FixedCapacityList(int capacity)
        {
            this.capacity = capacity;
            array = new T[capacity];
        }
        public void ExtendCapacity(int newCap)
        {
            if (newCap > array.Length)
            {
                T[] newArry = new T[newCap];
                array.CopyTo(newArry, 0);
                array = newArry;
                capacity = newCap;
            }
        }
        #region IList<T> 成员

        public int IndexOf(T item)
        {
            int res = -1;
            for (int i = 0; i < capacity; i++)
            {
                if (array[i].Equals(item))
                    res = i;
            }
            return res;
        }

        /// <summary>
        /// 在指定index出放入item,若原index不为空，则出错
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="item">对象</param>
        public void Insert(int index, T item)
        {
            if (EqualityComparer<T>.Default.Equals(array[index], default(T)))
            {
                array[index] = item;
            }
            else
                throw new ArgumentException("原index处不为null");

        }

        public void RemoveAt(int index)
        {
            array[index] = default(T);
        }

        public T this[int index]
        {
            get
            {
                return array[index];
            }
            set
            {
                array[index] = value;
            }
        }

        #endregion

        #region ICollection<T> 成员

        /// <summary>
        /// 添加一个项目并返回其Index
        /// </summary>
        /// <param name="item">项目</param>
        /// <returns>该项目的Index</returns>
        public int Add2(T item)
        {
            int index = -1;
            for (int i = 0; i < capacity; i++)
            {
                if (EqualityComparer<T>.Default.Equals(array[i], default(T)))
                    index = i;
            }
            if (index >= 0)
            {
                array[index] = item;
                return index;
            }
            else
                throw new OverflowException("已经没有空余空间!");
        }

        public void Add(T item)
        {
            Add2(item);
        }

        public void Clear()
        {
            for (int i = 0; i < capacity; i++)
                array[i] = default(T);
        }

        public bool Contains(T item)
        {
            bool res = false;
            for (int i = 0; i < capacity; i++)
            {
                if (EqualityComparer<T>.Default.Equals(array[i], item))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            List<T> list = new List<T>();
            foreach (T i in this.array)
            {
                if (!EqualityComparer<T>.Default.Equals(i, default(T)))
                    list.Add(i);
            }
            if (array.Length < list.Count)
                throw new ArgumentOutOfRangeException();
            for (int i = 0; i < list.Count; i++)
                array[arrayIndex + i] = list[i];
        }

        public int Count
        {
            get
            {
                int count = 0;
                for (int i = 0; i < capacity; i++)
                {
                    if (!EqualityComparer<T>.Default.Equals(array[i], default(T)))
                        count++;
                }
                return count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            bool res = false;
            for (int i = 0; i < capacity; i++)
            {
                if (EqualityComparer<T>.Default.Equals(array[i], item))
                {
                    array[i] = default(T);
                    res = true;
                    break;
                }
            }
            return res;
        }

        #endregion

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            return array.AsEnumerable().GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return array.GetEnumerator();
        }

        #endregion
    }
}
