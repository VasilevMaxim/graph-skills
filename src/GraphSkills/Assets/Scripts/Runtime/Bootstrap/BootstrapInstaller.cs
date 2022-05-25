using System.Collections.Generic;
using System.IO;
using Kefir.Model.Graph;
using Kefir.View;
using Kefir.View.Graph;
using Kefir.ViewModel;
using UnityEngine;
using Zenject;

namespace Kefir.Bootstrap
{
    internal sealed class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
    {
        [SerializeField] private List<NodeView> _nodesView;
        [SerializeField] private WindowActionView _windowAction;
        [SerializeField] private WindowErrorView _windowError;
        private List<NodeModel> _nodeModels = new();
        
        public override void InstallBindings()
        {
            _nodeModels = new List<NodeModel>();
            
            var root = new NodeModel(5);
            _nodeModels.Add(root);
            new NodeViewModel(_nodesView[0], root);
            
            for (int i = 1; i < _nodesView.Count; i++)
            {
                var model = new NodeModel(5, root);
                _nodeModels.Add(model);
                new NodeViewModel(_nodesView[i], model);
            }

            LoadMatrix();
            new WindowActionViewModel(_windowAction, _windowError, _nodesView, _nodeModels);
        }

        private void LoadMatrix()
        {
            var text = File.ReadAllText("F:/matrix.txt");
            var lines = text.Split("\r\n");
            
            foreach (var line in lines)
            {
                var mainParts = line.Split(':');
                var neighbors = mainParts[1].Split(' ');
                foreach (var neighbour in neighbors)
                {
                    _nodeModels[int.Parse(mainParts[0])].AddNeighbour(_nodeModels[int.Parse(neighbour)]);
                }
                
            }
        }
    }
}