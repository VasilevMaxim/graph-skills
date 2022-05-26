using System;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View;
using Kefir.View.Graph;
using Runtime.View;
using Zenject;

namespace Kefir.ViewModel
{
    public class ScoreViewModel : ViewModelBase
    {
        private readonly ScoreView _scoreView;
        private readonly ScoreModel _scoreModel;
        private readonly AdvancedButton _scoreButton;
        
        [Inject]
        internal ScoreViewModel(ScoreView scoreView, 
                                AdvancedButton scoreButton, 
                                ScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
            _scoreView = scoreView;
            _scoreButton = scoreButton;
            
            _scoreButton.Bind(_scoreModel.Inc);
            
            Bind(_scoreModel.Value, _scoreView.UpdateText);
        }
    }
}