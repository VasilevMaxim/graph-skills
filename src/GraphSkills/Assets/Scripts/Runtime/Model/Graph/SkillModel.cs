using System.Collections.Generic;
using System.Linq;
using Kefir.Сommon.Model.Bindings;

namespace Kefir.Model.Graph
{
    public class SkillModel : ISkillModel
    {
        private readonly VertexModel<ISkillModel> _vertexModel;
        private readonly ModelItem<bool> _isOpened;
        public IReadOnlyModelItem<bool> IsOpened => _isOpened;
        
        private readonly ModelItem<int> _cost;
        public IReadOnlyModelItem<int> Cost => _cost;

        public SkillModel()
        {
            _isOpened = new ModelItem<bool>();
            _cost = new ModelItem<int>();
            _vertexModel = new VertexModel<ISkillModel>();
        }

        public void SetOpened(bool state) => _isOpened.Value = state;
        public void SetCost(int cost) => _cost.Value = cost;

        public IEnumerable<ISkillModel> Neighbors => _vertexModel.Neighbors;
        public void RemoveAllNeighbors() => _vertexModel.RemoveAllNeighbors();

        public void RemoveNeighbour(ISkillModel node) => _vertexModel.RemoveNeighbour(node);

        public void AddNeighbour(ISkillModel node) => _vertexModel.AddNeighbour(node);
    }
}
