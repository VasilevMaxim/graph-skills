using System.Collections.Generic;

namespace Kefir.Loading
{
    public interface ILoaderCosts
    {
        IEnumerable<int> LoadCosts();
    }
}