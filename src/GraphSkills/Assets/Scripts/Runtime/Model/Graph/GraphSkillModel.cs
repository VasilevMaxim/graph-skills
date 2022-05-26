using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kefir.Сommon.Model.Bindings;

namespace Kefir.Model.Graph
{
    internal sealed class GraphSkillModel
    {
        public int Count => _skillsModel.Count;
        public SkillModel Root { get; }

        public SkillModel this[int index] => _skillsModel[index];

        private readonly List<SkillModel> _skillsModel;
        private List<SkillModel> _visited = new();

        private bool _canRoot;

        public GraphSkillModel(IEnumerable<SkillModel> skillsModel, SkillModel root)
        {
            Root = root;
            _skillsModel = skillsModel.ToList();
        }

        public void BuildLinks(Dictionary<int, IEnumerable<int>> links)
        {
            foreach (var (key, values) in links)
                foreach (var value in values)
                    _skillsModel[key].AddNeighbour(_skillsModel[value]);
        }

        public bool TryOpen(SkillModel skill)
        {
            if (skill.Neighbors.All(neighbour => neighbour.IsOpened.Value == false)) return false;
            skill.SetOpened(true);
            return true;
        }

        public bool TryForget(SkillModel skill)
        {
            var neighborsPast = skill.Neighbors.ToList();

            ForgetLinks(skill);

            var isForgetAll = neighborsPast.Where(neighbour => neighbour.IsOpened.Value == true)
                                           .All(neighbour => IsCanBeForget(neighbour) == true);

            RestoreLinks(skill, neighborsPast);
            return isForgetAll;
        }
        
        private bool IsCanBeForget(SkillModel skill)
        {
            _visited = new();
            _canRoot = false;
            DFS(skill);

            return _canRoot;
        }

        private void DFS(SkillModel current)
        {
            _visited.Add(current);

            if (current == Root)
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

        private void ForgetLinks(SkillModel node)
        {
            foreach (var neighbour in node.Neighbors)
                neighbour.RemoveNeighbour(node);

            node.RemoveAllNeighbors();
        }

        private void RestoreLinks(SkillModel node, IEnumerable<SkillModel> neighbors)
        {
            foreach (var neighbour in neighbors)
            {
                node.AddNeighbour(neighbour);
                neighbour.AddNeighbour(node);
            }
        }
    }
}