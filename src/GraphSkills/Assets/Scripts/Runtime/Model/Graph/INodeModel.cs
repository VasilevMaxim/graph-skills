using System.Collections;
using System.Collections.Generic;
using Kefir.Сommon.Model.Bindings;
using UnityEngine;

namespace Kefir.Model.Graph
{
    public interface INodeModel
    {
        IReadOnlyModelItem<bool> IsOpened { get; }
        IReadOnlyModelItem<int> Cost { get; }
    }
}