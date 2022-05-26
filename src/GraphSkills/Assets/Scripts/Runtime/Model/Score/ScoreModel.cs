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

        public void Inc()
        {
            _value.Value += 1;
        }

        public void Add(int value)
        {
            if (value < 0 || int.MaxValue -_value < value)
                throw new ArgumentException();
            
            _value.Value += value;
        }
        
        public void Remove(int value)
        {
            if (value < 0 || _value - value < 0)
                throw new ArgumentException();
            
            _value.Value -= value;
        }
    }
}