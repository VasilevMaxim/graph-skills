using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kefir.Model.Graph;
using Kefir.View.Graph;
using Zenject;

namespace Kefir.ViewModel
{
    public sealed class GraphSkillViewModel : ViewModelBase, IGraphSkillViewModel
    {
        private Dictionary<ISkillModel, ISkillView> _matches;

        private readonly IGraphSkillModel _graphModel;
        private readonly List<ISkillView> _skillsView;

        [Inject]
        public GraphSkillViewModel(IGraphSkillModel graphModelModel,
                                   IEnumerable<ISkillView> skillsView)
        {
            _graphModel = graphModelModel;
            _skillsView = skillsView.ToList();
            InitMatches();
        }

        public IEnumerator<KeyValuePair<ISkillModel, ISkillView>> GetEnumerator() => _matches.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        private void InitMatches()
        {
            _matches = new Dictionary<ISkillModel, ISkillView>();
            for (int i = 0; i < _graphModel.Count; i++)
                _matches.Add(_graphModel[i], _skillsView[i]);
        }
        
        public void CancelChooseAll()
        {
            _skillsView.ForEach(view => view.CancelChoose());
        }
    }
}