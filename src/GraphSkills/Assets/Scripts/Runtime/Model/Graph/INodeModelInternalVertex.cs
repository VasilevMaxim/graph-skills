using System.Collections.Generic;

namespace Kefir.Model.Graph
{
    internal interface INodeModelInternalVertex : INodeModelInternal
    {
        IEnumerable<INodeModelInternalVertex> Neighbors { get; }
        
        void RemoveAllNeighbors();
        void RemoveNeighbour(INodeModelInternalVertex node);
        void AddNeighbour(INodeModelInternalVertex node);
        
        
        bool TryOpen();
        bool TryForget();
        bool IsCanBeForget();
    }
}