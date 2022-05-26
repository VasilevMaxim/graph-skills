using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kefir.Model.Graph;
using Kefir.View.Graph;

namespace Kefir.ViewModel
{
    internal sealed class GraphSkillViewModel : ViewModelBase, IEnumerable<KeyValuePair<SkillModel, NodeView>>
    {
        public Dictionary<SkillModel, NodeView> Matches { get; private set; }

        private readonly GraphSkillModel _graph;
        private readonly List<NodeView> _skillsView;

        public GraphSkillViewModel(GraphSkillModel model,
                                   IEnumerable<NodeView> skillsView)
        {
            _graph = model;
            _skillsView = skillsView.ToList();
            InitMatches();
        }

        public IEnumerator<KeyValuePair<SkillModel, NodeView>> GetEnumerator() => Matches.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        private void InitMatches()
        {
            Matches = new Dictionary<SkillModel, NodeView>();
            for (int i = 0; i < _graph.Count; i++)
                Matches.Add(_graph[i], _skillsView[i]);
        }
        
        public void CancelChooseAll()
        {
            _skillsView.ForEach(view => view.CancelChoose());
        }
    }
}