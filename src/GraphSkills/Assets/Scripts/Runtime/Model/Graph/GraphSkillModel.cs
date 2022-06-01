using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Kefir.Model.Graph
{
    public sealed class GraphSkillModel : IGraphSkillModel
    {
        private readonly IPathfinding _pathfinding;
        
        public int Count => _skillsModel.Count;

        public ISkillModel Root { get; }
        
        public ISkillModel this[int index] => _skillsModel[index];
        private readonly List<ISkillModel> _skillsModel;

        [Inject]
        public GraphSkillModel(IEnumerable<ISkillModel> skillsModel, 
                               IPathfinding pathfinding,
                               [Inject(Id = "root")] ISkillModel root)
        {
            _pathfinding = pathfinding;
            _skillsModel = skillsModel.ToList();
            Root = root;
        }

        public void BuildLinks(IDictionary<int, IEnumerable<int>> links)
        {
            foreach (var (key, values) in links)
                foreach (var value in values)
                    _skillsModel[key].AddNeighbour(_skillsModel[value]);
        }

        public bool TryOpen(ISkillModel skill)
        {
            if (skill.Neighbors.All(neighbour => neighbour.IsOpened.Value == false)) return false;
            skill.SetOpened(true);
            return true;
        }

        public bool TryForget(ISkillModel skill)
        {
            var neighborsPast = skill.Neighbors.ToList();

            ForgetLinks(skill);
            
            var isForgetAll = neighborsPast.Where(neighbour => neighbour.IsOpened.Value == true)
                                           .All(neighbour => _pathfinding.IsCanFindWayToRoot(neighbour));

            RestoreLinks(skill, neighborsPast);
            return isForgetAll;
        }

        public void ForgetAll()
        {
            for (int i = 1; i < _skillsModel.Count; i++)
                _skillsModel[i].SetOpened(false);
        }

        private void ForgetLinks(ISkillModel node)
        {
            foreach (var neighbour in node.Neighbors)
                neighbour.RemoveNeighbour(node);

            node.RemoveAllNeighbors();
        }

        private void RestoreLinks(ISkillModel node, IEnumerable<ISkillModel> neighbors)
        {
            foreach (var neighbour in neighbors)
            {
                node.AddNeighbour(neighbour);
                neighbour.AddNeighbour(node);
            }
        }
    }
}