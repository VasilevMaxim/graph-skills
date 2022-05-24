using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kefir.Model.Graph;

namespace Kefir.ViewModel
{
    public class PathfindingViewModel
    {
        private INodeModelInternalVertex _root;
        private List<INodeModelInternalVertex> _nodes;
        
        private List<INodeModelInternalVertex> _visited = new();
        
        internal bool TryOpen(INodeModelInternalVertex node)
        {
            if (node.Neighbors.All(neighbour => neighbour.IsOpened.Value == false)) return false;
            node.SetOpened(true);
            return true;
        }
        
        internal bool TryForget(INodeModelInternalVertex node)
        {
            var neighborsPast = node.Neighbors.ToList();

            ForgetLinks(node);

            var isForget = neighborsPast.All(neighbour => DFS(neighbour) != false);
            
            RestoreLinks(node, neighborsPast);
            return isForget;
        }
        
        internal bool DFS(INodeModelInternalVertex current)
        {
            _visited.Add(current);
            
            if (current == _root) return true;
            
            foreach (var neighbour in current.Neighbors)
            {
                if (_visited.Contains(neighbour) == false)
                    DFS(neighbour);
            }
            
            return false;
        }
        
        private void ForgetLinks(INodeModelInternalVertex node)
        {
            foreach (var neighbour in node.Neighbors)
                neighbour.RemoveNeighbour(node);
            
            node.RemoveAllNeighbors();
        }

        private void RestoreLinks(INodeModelInternalVertex node, IEnumerable<INodeModelInternalVertex> neighbors)
        {
            foreach (var neighbour in neighbors)
            {
                node.AddNeighbour(neighbour);
                neighbour.AddNeighbour(node);
            }
        }
    }
}