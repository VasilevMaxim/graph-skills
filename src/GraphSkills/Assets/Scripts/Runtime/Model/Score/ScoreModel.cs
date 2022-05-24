using System;
using System.Collections;
using System.Collections.Generic;
using Kefir.Сommon.Model.Bindings;
using UnityEngine;

namespace Kefir.Model.Score
{
    internal sealed class ScoreModel : IScore
    {
        private readonly ModelItem<int> _value;
        public IReadOnlyModelItem<int> Value => _value;

        internal ScoreModel()
        {
            _value = new ModelItem<int>();
        }

        public void Add()
        {
            _value.Value += 1;
        }

        public void Remove(int count)
        {
            if (count < 0 || _value - count < 0)
                throw new ArgumentException();
            
            _value.Value = count;
        }
    }
}