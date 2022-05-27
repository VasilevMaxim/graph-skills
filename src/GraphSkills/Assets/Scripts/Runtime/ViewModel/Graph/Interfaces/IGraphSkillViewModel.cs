using System.Collections.Generic;
using Kefir.Model.Graph;
using Kefir.View.Graph;

namespace Kefir.ViewModel
{
    internal interface IGraphSkillViewModel : IEnumerable<KeyValuePair<ISkillModel, ISkillView>>
    {
        void CancelChooseAll();
    }
}