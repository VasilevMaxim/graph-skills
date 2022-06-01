using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Kefir.Model.Graph
{
    public sealed class DepthFirstSearch : IPathfinding
    {
        private readonly ISkillModel _root;
        private List<ISkillModel> _visitedVertexes = new();
        private bool _isRootBeenFound;

        public DepthFirstSearch([Inject(Id = "root")] ISkillModel root)
        {
            _root = root;
        }
        
        public bool IsCanFindWayToRoot(ISkillModel currentVertex)
        {
            _visitedVertexes = new();
            _isRootBeenFound = false;
            Run(currentVertex);
            return _isRootBeenFound;
        }
        
        private void Run(ISkillModel current)
        {
            _visitedVertexes.Add(current);

            if (current == _root)
            {
                _isRootBeenFound = true;
                return;
            }

            foreach (var neighbour in current.Neighbors.Where(neighbour => neighbour.IsOpened.Value == true))
            {
                if (_visitedVertexes.Contains(neighbour) == false)
                    Run(neighbour);
            }
        }
    }
}