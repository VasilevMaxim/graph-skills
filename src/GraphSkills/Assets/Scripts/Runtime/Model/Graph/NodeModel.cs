using System;
using System.Collections.Generic;
using System.Linq;
using Kefir.Сommon.Model.Bindings;

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
        
        private List<INodeModelInternalVertex> _visited = new(); 
        private readonly INodeModelInternalVertex _root;
        
        private NodeModel()
        {
            _isOpened = new ModelItem<bool>();
            _cost = new ModelItem<int>();
            _neighbors = new List<INodeModelInternalVertex>();
        }
        
        internal NodeModel(int cost, INodeModelInternalVertex root = null) : this()
        {
            _root = root;
            _cost.Value = cost;

            _root ??= this;
        }

        public void SetOpened(bool state) => _isOpened.Value = state;
        public void SetCost(int cost) => _cost.Value = cost;
        
        public void RemoveAllNeighbors() => _neighbors = new List<INodeModelInternalVertex>();

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
        
       
        public bool TryOpen()
        {
            if (Neighbors.All(neighbour => neighbour.IsOpened.Value == false)) return false;
                SetOpened(true);
            return true;
        }
        
        public bool TryForget()
        {
           
            var neighborsPast = Neighbors.ToList();

            ForgetLinks(this);

            var isForgetAll = neighborsPast.Where(neighbour => neighbour.IsOpened.Value == true)
                                           .All(neighbour => neighbour.IsCanBeForget() == true);
            
            RestoreLinks(this, neighborsPast);
            return isForgetAll;
        }

        public bool IsCanBeForget()
        {
            _visited = new();
            _canRoot = false;
            DFS(this);
            return _canRoot;
        }

        private bool _canRoot;
        private void DFS(INodeModelInternalVertex current)
        {
            _visited.Add(current);

            if (current == _root)
            {
                _canRoot = true;
                return;
            }
            
            foreach (var neighbour in current.Neighbors.Where(neighbour => neighbour.IsOpened.Value == true))
            {
                if (_visited.Contains(neighbour) == false)
                    DFS(neighbour);
            }
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
