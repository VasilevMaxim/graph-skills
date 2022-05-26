using System.Collections.Generic;
using System.Linq;
using Kefir.Сommon.Model.Bindings;

namespace Kefir.Model.Graph
{
    internal class SkillModel: VertexModel<SkillModel>, ISkillInternal
    {
        private readonly ModelItem<bool> _isOpened;
        public IReadOnlyModelItem<bool> IsOpened => _isOpened;
        
        private readonly ModelItem<int> _cost;
        public IReadOnlyModelItem<int> Cost => _cost;

        public SkillModel()
        {
            _isOpened = new ModelItem<bool>();
            _cost = new ModelItem<int>();
        }
        
        internal SkillModel(int cost) : this()
        {
            _cost.Value = cost;
        }

        public void SetOpened(bool state) => _isOpened.Value = state;
        public void SetCost(int cost) => _cost.Value = cost;
    }
}
