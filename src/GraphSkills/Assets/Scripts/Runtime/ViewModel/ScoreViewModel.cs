using System;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View.Graph;
using Runtime.View;

namespace Kefir.ViewModel
{
    public class ScoreViewModel : IDisposable
    {
        private readonly ScoreView _scoreView;
        private readonly ScoreModel _scoreModel;

        internal ScoreViewModel(ScoreModel scoreModel, ScoreView scoreView)
        {
            _scoreModel = scoreModel;
            _scoreView = scoreView;
            
            _scoreModel.Value.Bind(_scoreView.UpdateText);
        }

        public void Dispose()
        {
            _scoreModel.Value.Unbind(_scoreView.UpdateText);
        }
    }
}