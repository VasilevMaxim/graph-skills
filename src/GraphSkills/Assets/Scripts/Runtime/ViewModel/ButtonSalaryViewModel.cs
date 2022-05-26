using System;
using Kefir.Model.Score;
using Kefir.View;

namespace Kefir.ViewModel
{
    public class ButtonSalaryViewModel : ViewModelBase, IDisposable
    {
        private readonly ButtonSalaryView _view;
        private readonly ScoreModel _scoreModel;

        internal ButtonSalaryViewModel(ButtonSalaryView view, ScoreModel scoreModel)
        {
            _view = view;
            _scoreModel = scoreModel;
            
            _view.Button.onClick.AddListener(_scoreModel.Inc);
        }
        
        public new void Dispose()
        {
            _view.Button.onClick.RemoveListener(_scoreModel.Inc);
        }
    }
}