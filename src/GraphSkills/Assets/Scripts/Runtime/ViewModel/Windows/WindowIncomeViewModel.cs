using Kefir.View;

namespace Kefir.ViewModel
{
    internal sealed class WindowIncomeViewModel : ViewModelBase, IWindowIncomeViewModel
    {
        private readonly AdvancedButton _arrowButton;
        private readonly IWindowIncomeView _scoreView;
        private readonly IWindowActionViewModel _windowActionViewModel;

        public WindowIncomeViewModel(AdvancedButton arrowButton,
                                     IWindowActionViewModel windowActionViewModel)
        {
            _arrowButton = arrowButton;
            _windowActionViewModel = windowActionViewModel;
            
            _arrowButton.Bind(() => _windowActionViewModel.ResetChoose());
        }
    }
}