using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Controls
{
    public class ChooseCollection<T> : List<T>
    {
        public event EventHandler<CollectionEventArgs<T>> ItemAdded;
        public event EventHandler<CollectionEventArgs<T>> ItemRemoved;

        public ChooseCollection() : base()
        {

        }

        public new void Add(T item)
        {
            base.Add(item);
            OnItemAdded(new CollectionEventArgs<T>(item, Count - 1));
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            OnItemAdded(new CollectionEventArgs<T>(item, index));
        }

        public new void AddRange(IEnumerable<T> items)
        {
            int count = Count;
            base.AddRange(items);
            for (int i = count; i < Count; i++)
            {
                OnItemAdded(new CollectionEventArgs<T>(this[i], i));
            }
        }

        public void AddRange(T[] items)
        {
            int count = Count;
            base.AddRange(items);
            for (int i = count; i < Count; i++)
            {
                OnItemAdded(new CollectionEventArgs<T>(this[i], i));
            }
        }

        public new void InsertRange(int index, IEnumerable<T> items)
        {
            int count = Count;
            base.InsertRange(index, items);
            count = Count - count;
            for (int i = index; i < count + index; i++)
            {
                OnItemAdded(new CollectionEventArgs<T>(this[i], i));
            }
        }

        public new bool Remove(T item)
        {
            int index = IndexOf(item);
            bool re = base.Remove(item);
            OnItemRemoved(new CollectionEventArgs<T>(item, index));
            return re;
        }

        public new void RemoveAt(int index)
        {
            T item = this[index];
            base.RemoveAt(index);
            OnItemRemoved(new CollectionEventArgs<T>(item, index));
        }

        public new int RemoveAll(Predicate<T> match)
        {
            List<CollectionEventArgs<T>> colorEList = new List<CollectionEventArgs<T>>();
            int index = FindLastIndex(match);
            int removeCnt = 0;
            while (index >= 0)
            {
                RemoveAt(index);
                removeCnt++;
                index = FindLastIndex(index - 1, match);
            }
            return removeCnt;
        }

        public new void RemoveRange(int index, int count)
        {
            for (; index < count; index++)
            {
                RemoveAt(index);
            }
        }

        protected virtual void OnItemAdded(CollectionEventArgs<T> e)
        {
            ItemAdded?.Invoke(this, e);
        }

        protected virtual void OnItemRemoved(CollectionEventArgs<T> e)
        {
            ItemRemoved?.Invoke(this, e);
        }

    }
}
