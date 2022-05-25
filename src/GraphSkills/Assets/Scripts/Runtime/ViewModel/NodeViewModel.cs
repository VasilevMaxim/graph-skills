using System;
using Kefir.Model.Graph;
using Kefir.View.Graph;

namespace Kefir.ViewModel
{
    internal sealed class NodeViewModel : ViewModelBase, IDisposable
    {
        private readonly NodeView _nodeView;
        private readonly NodeModel _nodeModel;

        internal NodeViewModel(NodeView nodeView,
                               NodeModel nodeModel)
        {
            _nodeView = nodeView;
            _nodeModel = nodeModel;
            
            Bind(_nodeModel.IsOpened, state =>
            {
                if (state == true) 
                    nodeView.Open();
                else 
                    nodeView.Forget();
            });
        }
    }
}