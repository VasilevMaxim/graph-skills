using Kefir.Model.Graph;
using Zenject;

namespace Kefir.Loading
{
    public sealed class RouterData : IRouterData
    {
        private ILoaderGraphAdjacencyList _loader;

        [Inject]
        private void Init(ILoaderGraphAdjacencyList loader,
                          IGraphSkillModel graphSkillModel)
        {
            _loader = loader;
            graphSkillModel.BuildLinks( _loader.Load());
        }
    }
}