using Kefir.Model.Graph;
using Kefir.View;
using Zenject;

namespace Kefir.ViewModel
{
    public sealed class ForgetAllViewModel : ViewModelBase
    {
        private readonly IAdvancedButton _forgetAllButton;
        private readonly IGraphSkillModel _graphModel;

        [Inject]
        internal ForgetAllViewModel([Inject(Id = "forgetAllButton")] IAdvancedButton forgetAllButton,
                                  IGraphSkillModel graphModel)
        {
            _forgetAllButton = forgetAllButton;
            _graphModel = graphModel;
            
            _forgetAllButton.Bind(_graphModel.ForgetAll);
        }
    }
}