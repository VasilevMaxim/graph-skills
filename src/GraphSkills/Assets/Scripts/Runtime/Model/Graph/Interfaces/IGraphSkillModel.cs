using System.Collections.Generic;

namespace Kefir.Model.Graph
{
    public interface IGraphSkillModel
    {
        int Count { get; }
        ISkillModel this[int index] { get; }
        ISkillModel Root { get; }
        
        
        void BuildLinks(Dictionary<int, IEnumerable<int>> links);
        bool TryOpen(ISkillModel skill);
        bool TryForget(ISkillModel skill);
        void ForgetAll();
    }
}