using System;
using System.Collections.Generic;
using Kefir.Model.Graph;
using Kefir.View;
using Kefir.Сommon.Model.Bindings;

namespace Kefir.ViewModel
{
    internal sealed class WindowActionViewModel : ViewModelBase
    {
        private readonly AdvancedButton _buttonOpened;
        private readonly AdvancedButton _buttonForget;
        private readonly WindowErrorView _windowError;

        private readonly NodeModel _nodeModel;

        internal WindowActionViewModel(AdvancedButton buttonOpened, 
                                        AdvancedButton buttonForget,
                                        WindowErrorView windowError,
                                        NodeModel nodeModel)
        {
            _buttonOpened = buttonOpened;
            _buttonForget = buttonForget;
            _windowError = windowError;

            _nodeModel = nodeModel;
            
            _buttonOpened.Bind(OnClick);
            _buttonForget.Bind(OnClick2);
        }

        private void OnClick()
        {
            if (_nodeModel.TryOpen() == false)
            {
                _windowError.ShowErrorOpened();
                return;
            }
            
            _nodeModel.SetOpened(true);
        }

        private void OnClick2()
        {
            if (_nodeModel.TryForget() == false)
            {
                _windowError.ShowErrorForget();
                return;
            }
            
            _nodeModel.SetOpened(false);
        }
    }
}