using System.Collections.Generic;

namespace Kefir.Model.Graph
{
    internal interface IVertexModel<T>
    {
        IEnumerable<T> Neighbors { get; }
        
        void RemoveAllNeighbors();
        void RemoveNeighbour(T node);
        void AddNeighbour(T node);
    }
}