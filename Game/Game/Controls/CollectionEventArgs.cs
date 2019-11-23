using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Controls
{
    public class CollectionEventArgs<T>
    {
        private T item;
        private int index;

        public CollectionEventArgs(T item, int index)
        {
            this.item = item;
            this.index = index;
        }

        public T Item
        {
            get
            {
                return item;
            }
        }

        public int Index
        {
            get
            {
                return Index;
            }
        }

    }
}
