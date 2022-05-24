using System;
using System.Collections;
using System.Collections.Generic;
using Kefir.Сommon.Model.Bindings;
using UnityEngine;

namespace Kefir.Model.Graph
{
    internal sealed class NodeModel : INodeModelInternalVertex
    {
        private readonly ModelItem<bool> _isOpened;
        public IReadOnlyModelItem<bool> IsOpened => _isOpened;
        
        private readonly ModelItem<int> _cost;
        public IReadOnlyModelItem<int> Cost => _cost;

        private List<INodeModelInternalVertex> _neighbors;
        public IEnumerable<INodeModelInternalVertex> Neighbors => _neighbors;
        
        internal NodeModel()
        {
            _isOpened = new ModelItem<bool>();
            _cost = new ModelItem<int>();
        }
        
        internal NodeModel(int cost) : this()
        {
            _cost.Value = cost;
        }

        public void SetOpened(bool state)
        {
            _isOpened.Value = state;
        }
        
        public void SetCost(int cost)
        {
            _cost.Value = cost;
        }
        
        public void RemoveAllNeighbors()
        {
            _neighbors = new List<INodeModelInternalVertex>();
        }

        public void RemoveNeighbour(INodeModelInternalVertex node)
        {
            if (node == null || _neighbors.Contains(node) == false)
                throw new ArgumentException();
            
            _neighbors.Remove(node);
        }
        
        public void AddNeighbour(INodeModelInternalVertex node)
        {
            if (node == null || _neighbors.Contains(node) == true)
                throw new ArgumentException();
            
            _neighbors.Add(node);
        }
    }
}
