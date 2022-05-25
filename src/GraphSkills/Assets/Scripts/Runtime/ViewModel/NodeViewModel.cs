using System;
using Kefir.Model.Graph;
using Kefir.View.Graph;

namespace Kefir.ViewModel
{
    public class NodeViewModel : IDisposable
    {
        private readonly NodeView _view;
        private readonly NodeModel _nodeModel;

        internal NodeViewModel(NodeView view, NodeModel nodeModel)
        {
            _view = view;
            _nodeModel = nodeModel;

            _view.Clicked += OnClick;
        }
        
        public void Dispose()
        {
            _view.Clicked -= OnClick;
        }

        private void OnClick()
        {
            bool state = _nodeModel.IsOpened.Value ? _nodeModel.TryForget() : _nodeModel.TryOpen();
            if (state == false)
            {
                
            }
        }
    }
}