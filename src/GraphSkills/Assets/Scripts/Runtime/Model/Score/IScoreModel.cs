using System.Collections;
using System.Collections.Generic;
using Kefir.Сommon.Model.Bindings;
using UnityEngine;

public interface IScoreModel
{
    public IReadOnlyModelItem<int> Value { get; }
    void Inc();
    void Add(int value);
    void Subtract(int value);
}
