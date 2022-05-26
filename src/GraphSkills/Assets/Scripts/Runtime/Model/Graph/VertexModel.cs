using System;
using System.Collections.Generic;

namespace Kefir.Model.Graph
{
    internal class VertexModel<T> : IVertexModel<T> where T : class, IVertexModel<T>, new()
    {
        private List<T> _neighbors;
        public IEnumerable<T> Neighbors => _neighbors;

        internal VertexModel()
        {
            _neighbors = new List<T>();
        }
        
        public void RemoveAllNeighbors() => _neighbors = new List<T>();

        public void RemoveNeighbour(T node)
        {
            if (node == null || _neighbors.Contains(node) == false)
                throw new ArgumentException();
            
            _neighbors.Remove(node);
        }
        
        public void AddNeighbour(T node)
        {
            if (node == null || _neighbors.Contains(node) == true)
                throw new ArgumentException();
            
            _neighbors.Add(node);
        }
    }
}