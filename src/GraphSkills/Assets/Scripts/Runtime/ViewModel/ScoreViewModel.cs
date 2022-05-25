using System;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View.Graph;
using Runtime.View;

namespace Kefir.ViewModel
{
    public class ScoreViewModel : ViewModelBase, IDisposable
    {
        private readonly ScoreView _scoreView;
        private readonly ScoreModel _scoreModel;

        internal ScoreViewModel(ScoreModel scoreModel, ScoreView scoreView)
        {
            _scoreModel = scoreModel;
            _scoreView = scoreView;
            Bind(_scoreModel.Value, _scoreView.UpdateText);
        }
    }
}