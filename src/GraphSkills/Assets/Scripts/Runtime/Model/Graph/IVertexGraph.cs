using System.Collections.Generic;

namespace Kefir.Model.Graph
{
    internal interface IVertexGraph
    {
        IEnumerable<IVertexGraph> Neighbors { get; }
        
        void RemoveAllNeighbors();
        void RemoveNeighbour(IVertexGraph node);
        void AddNeighbour(IVertexGraph node);
    }
}