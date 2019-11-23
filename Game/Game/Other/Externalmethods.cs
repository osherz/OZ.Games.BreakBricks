using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Other
{
    public static class ExternalMethods
    {
        public static List<T> CopyList<T>(this List<T> listToCopy)
            where T : ICloneable
        {
            List<T> list = new List<T>();
            foreach(T item in listToCopy)
            {
                list.Add((T)item.Clone());
            }
            return list;
        }
    }
}
