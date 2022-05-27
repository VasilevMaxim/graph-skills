using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kefir.Сommon.Model.Bindings;
using UnityEngine;
using Zenject;

namespace Kefir.Model.Graph
{
    public sealed class LoaderFile
    {
        private IGraphSkillModel _graphSkillModel;

        [Inject]
        private void Init(IGraphSkillModel graphSkillModel)
        {
            _graphSkillModel = graphSkillModel;
            _graphSkillModel.BuildLinks(LoadMatrix());
        }
        
        private Dictionary<int, IEnumerable<int>> LoadMatrix()
        {
            var dictionary = new Dictionary<int, IEnumerable<int>>();
            var text = Resources.Load<TextAsset>("matrix").text;
            var lines = text.Split("\r\n");
            
            foreach (var line in lines)
            {
                var mainParts = line.Split(':');
                var neighbors = mainParts[1].Split(' ');
                dictionary.Add(int.Parse(mainParts[0]), neighbors.Select(int.Parse));
            }

            return dictionary;
        }
    }
    
    
    public sealed class GraphSkillModel : IGraphSkillModel
    {
        public int Count => _skillsModel.Count;
        public ISkillModel Root { get; }

        public ISkillModel this[int index] => _skillsModel[index];

        private readonly List<ISkillModel> _skillsModel;
        private List<ISkillModel> _visited = new();

        private bool _canRoot;

        [Inject]
        public GraphSkillModel(IEnumerable<ISkillModel> skillsModel, [Inject(Id = "root")] ISkillModel root)
        {
            Root = root;
            _skillsModel = skillsModel.ToList();
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
                                           .All(neighbour => IsCanBeForget(neighbour) == true);

            RestoreLinks(skill, neighborsPast);
            return isForgetAll;
        }

        public void ForgetAll()
        {
            for (int i = 1; i < _skillsModel.Count; i++)
                _skillsModel[i].SetOpened(false);
        }
        
        private bool IsCanBeForget(ISkillModel skill)
        {
            _visited = new();
            _canRoot = false;
            DFS(skill);

            return _canRoot;
        }

        private void DFS(ISkillModel current)
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