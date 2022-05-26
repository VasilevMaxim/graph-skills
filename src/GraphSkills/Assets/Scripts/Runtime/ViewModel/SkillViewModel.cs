using System;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View.Graph;

namespace Kefir.ViewModel
{
    internal sealed class SkillViewModel : ViewModelBase, IDisposable
    {
        private readonly NodeView _nodeView;
        private readonly SkillModel _skillModel;
        private readonly ScoreModel _scoreModel;

        internal SkillViewModel(NodeView nodeView,
                                SkillModel skillModel,
                                ScoreModel scoreModel)
        {
            _nodeView = nodeView;
            _skillModel = skillModel;
            _scoreModel = scoreModel;

            Bind(_skillModel.IsOpened, state =>
            {
                if (state == true)
                {
                    nodeView.Open();
                    _scoreModel.Remove(_skillModel.Cost.Value);
                }
                else
                {
                    nodeView.Forget();
                    _scoreModel.Add(_skillModel.Cost.Value);
                }
            });
        }
    }
}