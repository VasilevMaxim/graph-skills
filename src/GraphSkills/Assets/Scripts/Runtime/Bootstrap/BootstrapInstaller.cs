using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View;
using Kefir.View.Graph;
using Kefir.ViewModel;
using Runtime.View;
using UnityEngine;
using Zenject;

namespace Kefir.Bootstrap
{
    internal sealed class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
    {
        [SerializeField] private List<NodeView> _nodesView;
        [SerializeField] private WindowActionView _windowAction;
        [SerializeField] private WindowErrorView _windowError;
        [SerializeField] private WindowScoreView _windowScoreView;
        
        [SerializeField] private AdvancedButton _scoreButton;
        
        [SerializeField] private AdvancedButton _studyButton;
        [SerializeField] private AdvancedButton _forgetButton;
        
        [SerializeField] private AdvancedButton _arrowButton;
        
        [SerializeField] private ScoreView _scoreView;
        
        private List<SkillModel> _nodeModels = new();
        
        public override void InstallBindings()
        {
            var scoreModel = new ScoreModel();
            
            _nodeModels = new List<SkillModel>();
            
            var root = new SkillModel(0);
            _nodeModels.Add(root);
            new SkillViewModel(_nodesView[0], root, scoreModel);
            
            for (int i = 1; i < _nodesView.Count; i++)
            {
                var model = new SkillModel(i);
                _nodeModels.Add(model);
                new SkillViewModel(_nodesView[i], model, scoreModel);
            }

            var graph = new GraphSkillModel(_nodeModels, root);
            graph.BuildLinks(LoadMatrix());
            var graphViewModel = new GraphSkillViewModel(graph, _nodesView);
            
            new WindowsActionViewModel(_windowAction, 
                                _windowError, 
                                _studyButton, 
                                _forgetButton,
                                graph,
                                graphViewModel,
                                scoreModel);

            new ScoreViewModel(_scoreView, _scoreButton, scoreModel);
        }

        private Dictionary<int, IEnumerable<int>> LoadMatrix()
        {
            var dictionary = new Dictionary<int, IEnumerable<int>>();
            var text = File.ReadAllText("F:/matrix.txt");
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
}