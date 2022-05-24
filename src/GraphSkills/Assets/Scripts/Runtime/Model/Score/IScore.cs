using System.Collections;
using System.Collections.Generic;
using Kefir.Сommon.Model.Bindings;
using UnityEngine;

public interface IScore
{
    public IReadOnlyModelItem<int> Value { get; }
}
