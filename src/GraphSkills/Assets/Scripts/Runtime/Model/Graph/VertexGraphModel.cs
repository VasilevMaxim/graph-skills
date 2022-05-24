using System;
using System.Collections.Generic;
using System.Linq;

namespace Kefir.Model.Graph
{
    internal class VertexGraphModel : IVertexGraph
    {
        private List<IVertexGraph> _neighbors;
        public IEnumerable<IVertexGraph> Neighbors => _neighbors;

        public VertexGraphModel()
        {
            _neighbors = new List<IVertexGraph>();
        }
        
        public VertexGraphModel(IEnumerable<IVertexGraph> vertex) : this()
        {
            _neighbors = vertex.ToList();
        }
        
        public void RemoveAllNeighbors()
        {
            _neighbors = new List<IVertexGraph>();
        }

        public void RemoveNeighbour(IVertexGraph node)
        {
            if (node == null || _neighbors.Contains(node) == false)
                throw new ArgumentException();
            
            _neighbors.Remove(node);
        }
        
        public void AddNeighbour(IVertexGraph node)
        {
            if (node == null || _neighbors.Contains(node) == true)
                throw new ArgumentException();
            
            _neighbors.Add(node);
        }
    }
}