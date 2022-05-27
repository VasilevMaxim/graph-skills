using System.Collections.Generic;
using Kefir.Сommon.Model.Bindings;

namespace Kefir.Model.Graph
{
    public interface ISkill
    {
        IReadOnlyModelItem<bool> IsOpened { get; }
        IReadOnlyModelItem<int> Cost { get; }
    }
}