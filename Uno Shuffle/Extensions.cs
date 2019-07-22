using System;
using System.Collections.Generic;
using System.Text;

namespace Uno_Shuffle
{
    static class Extensions
    {
        public static T PopAt<T>(this List<T> list, int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static T Pop<T>(this List<T> list, T item)
        {
            list.Remove(item);
            return item;
        }

        public static T? Pop<T>(this List<T> list, T? item)  where T : struct
        {
            if(item != null)
                list.Remove(item.Value);
            return item;
        }
    }
}
