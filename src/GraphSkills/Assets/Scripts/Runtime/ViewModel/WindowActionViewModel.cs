using System;
using System.Collections.Generic;
using Kefir.Model.Graph;
using Kefir.View;
using Kefir.View.Graph;

namespace Kefir.ViewModel
{
    internal sealed class WindowActionViewModel : ViewModelBase, IDisposable
    {
        private readonly WindowErrorView _windowError;
        private readonly List<NodeView> _nodesView;
        private readonly List<NodeModel> _nodesModel;

        internal  WindowActionViewModel(WindowActionView windowAction,
                                       WindowErrorView windowError,
                                       List<NodeView> nodesView,
                                       List<NodeModel> nodesModel)
        {
            _windowError = windowError;
            _nodesView = nodesView;
            _nodesModel = nodesModel;
            
            for(int i = 0; i < nodesView.Count; i++)
            {
                var currentModel = nodesModel[i];
                _nodesView[i].Clicked += () =>
                {
                    if (currentModel.IsOpened.Value == true)
                        OnClick2(currentModel);
                    else
                        OnClick(currentModel);
                };
            }

            _nodesModel[0].SetOpened(true);
        }
        
        private void OnClick(NodeModel nodeModel)
        {
            if (nodeModel.TryOpen() == false)
            {
                _windowError.ShowErrorOpened();
                return;
            }
            
            nodeModel.SetOpened(true);
        }

        private void OnClick2(NodeModel nodeModel)
        {
            if (nodeModel.TryForget() == false)
            {
                _windowError.ShowErrorForget();
                return;
            }
            
            nodeModel.SetOpened(false);
        }
    }
}