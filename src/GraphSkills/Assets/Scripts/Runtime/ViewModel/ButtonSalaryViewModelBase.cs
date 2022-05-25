using System;
using Kefir.Model.Score;
using Kefir.View;
using Runtime.View;

namespace Kefir.ViewModel
{
    public class ButtonSalaryViewModelBase : ViewModelBase, IDisposable
    {
        private readonly ButtonSalaryView _view;
        private readonly ScoreModel _scoreModel;

        internal ButtonSalaryViewModelBase(ButtonSalaryView view, ScoreModel scoreModel)
        {
            _view = view;
            _scoreModel = scoreModel;
            
            _view.Button.onClick.AddListener(_scoreModel.Add);
        }
        
        public new void Dispose()
        {
            _view.Button.onClick.RemoveListener(_scoreModel.Add);
        }
    }
}