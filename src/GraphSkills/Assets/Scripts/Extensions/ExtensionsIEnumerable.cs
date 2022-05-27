using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Kefir.Extensions
{
    public static class ExtensionsIEnumerable
    {
        public static void ForEach<T1, T2>(this IEnumerable<T1> enumerable, 
                                           IEnumerable<T2> enumerable2, 
                                           [NotNull] Action<T1, T2> action)
        {
            var tuples = enumerable.Zip(enumerable2, (first, second) => (first, second));
            foreach (var tuple in tuples)
                action?.Invoke(tuple.first, tuple.second);
        }
    }
}