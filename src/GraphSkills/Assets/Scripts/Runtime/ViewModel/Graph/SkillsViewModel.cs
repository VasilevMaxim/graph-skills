using System;
using System.Collections.Generic;
using System.Linq;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View.Graph;
using Zenject;

namespace Kefir.ViewModel
{
    public sealed class SkillsViewModel : ViewModelBase, ISkillsViewModel
    {
        private readonly IScoreModel _scoresModel;
        private readonly List<ISkillModel> _skillsModel;
        private readonly List<ISkillView> _skillsView;
        
        [Inject]
        internal SkillsViewModel(IScoreModel scoresModel,
                                 IEnumerable<ISkillModel> skillsModel,
                                 IEnumerable<ISkillView> skillsView)
        
        {
            _skillsView = skillsView.ToList();
            _skillsModel = skillsModel.ToList();
            _scoresModel = scoresModel;
            
            BindActions();
        }

        private void BindActions()
        {
            for (int i = 0; i < _skillsView.Count; i++)
            {
                var index = i;
                Bind(_skillsModel[index].IsOpened, state =>
                {
                    if (state == true)
                    {
                        _skillsView[index].Open();
                        _scoresModel.Subtract(_skillsModel[index].Cost.Value);
                    }
                    else
                    {
                        _skillsView[index].Forget();
                        _scoresModel.Add(_skillsModel[index].Cost.Value);
                    }
                });

                _skillsView[index].SetCost(_skillsModel[index].Cost.Value);
                Bind(_skillsModel[index].Cost, _skillsView[index].SetCost);
            }
        }
    }
}