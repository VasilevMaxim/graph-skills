using System.Collections.Generic;

namespace Kefir.Model.Graph
{
    public interface IVertexModel
    {
        
    }
    
    public interface IVertexModel<T> : IVertexModel
    {
        IEnumerable<T> Neighbors { get; }
        
        void RemoveAllNeighbors();
        void RemoveNeighbour(T node);
        void AddNeighbour(T node);
    }
}