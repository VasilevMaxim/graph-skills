using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kefir.Extensions
{
    public static class ExtensionsList
    {
        public static void ForEach<T>(this List<T> list, [NotNull] Action<T, int> action)
        {
            for(var i = 0; i < list.Count; i++)
                action?.Invoke(list[i], i);
        }
    }
}