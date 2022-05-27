using System;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View;
using Kefir.View.Graph;
using Runtime.View;
using Zenject;

namespace Kefir.ViewModel
{
    public class ScoreViewModel : ViewModelBase, IScoreViewModel
    {
        private readonly IScoreView _scoreView;
        private readonly IScoreModel _scoreModel;
        private readonly IAdvancedButton _scoreButton;
        
        [Inject]
        internal ScoreViewModel(IScoreView scoreView, 
                                [Inject(Id = "scoreButton")] IAdvancedButton scoreButton, 
                                IScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
            _scoreView = scoreView;
            _scoreButton = scoreButton;
            
            _scoreButton.Bind(_scoreModel.Inc);
            
            Bind(_scoreModel.Value, _scoreView.UpdateText);
        }
    }
}