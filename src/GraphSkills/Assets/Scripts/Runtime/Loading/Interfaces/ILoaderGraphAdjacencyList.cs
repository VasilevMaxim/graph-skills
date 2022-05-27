using System.Collections.Generic;

namespace Kefir.Loading
{
    public interface ILoaderGraphAdjacencyList
    {
        IDictionary<int, IEnumerable<int>> Load();
    }
}