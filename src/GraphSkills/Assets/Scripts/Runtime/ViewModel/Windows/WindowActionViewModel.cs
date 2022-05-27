using System;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View;
using Kefir.View.Graph;
using Zenject;

namespace Kefir.ViewModel
{
    public sealed class WindowActionViewModel : ViewModelBase, IWindowActionViewModel
    {
        private readonly IWindowActionView _windowAction;
        private readonly IAdvancedButton _buttonStudy;
        private readonly IAdvancedButton _buttonForget;
        private readonly IWindowErrorView _windowError;
        private readonly IGraphSkillModel _graphSkillModel;
        private readonly IGraphSkillViewModel _graphSkillViewModel;
        private readonly IScoreModel _scoreModel;

        [Inject]
        internal  WindowActionViewModel(IWindowActionView windowAction,
                                         IWindowErrorView windowError,
                                         [Inject(Id = "buttonStudy")] IAdvancedButton buttonStudy,
                                         [Inject(Id = "buttonForget")] IAdvancedButton buttonForget,
                                         IGraphSkillModel graphSkillModel,
                                         IGraphSkillViewModel graphSkillViewModel,
                                         IScoreModel scoreModel)
        {
            _windowAction = windowAction;
            _buttonStudy = buttonStudy;
            _buttonForget = buttonForget;
            _windowError = windowError;
            _graphSkillModel = graphSkillModel;
            _graphSkillViewModel = graphSkillViewModel;
            _scoreModel = scoreModel;

            foreach (var (model, view) in _graphSkillViewModel)
                view.Clicked += () => SubscribeClicked(model, view);

            graphSkillModel.Root.SetOpened(true);
        }
        
        public void ResetChoose(params Action[] actions)
        {
            foreach (var action in actions)
                action?.Invoke();
            
            _graphSkillViewModel.CancelChooseAll();
            _windowAction.Hide();
        }
        
        private void SubscribeClicked(ISkillModel skill, ISkillView view)
        {
            _graphSkillViewModel.CancelChooseAll();
            
            _buttonStudy.Unbind();
            _buttonForget.Unbind();

            _buttonStudy.Bind(() => StudyHandle(skill));
            _buttonForget.Bind(() => ForgetHandle(skill));

            _windowAction.Show(skill.IsOpened.Value);
            
            view.Choose();
        }
        
        private void StudyHandle(ISkillModel skillModel)
        {
            if ((_scoreModel.Value.Value - skillModel.Cost.Value) < 0)
            {
                ResetChoose(_windowError.ShowErrorScore);
                return;
            }
            
            if (_graphSkillModel.TryOpen(skillModel) == false)
            {
                ResetChoose(_windowError.ShowErrorOpened);
                return;
            }
            
            skillModel.SetOpened(true);
            ResetChoose();
        }

        private void ForgetHandle(ISkillModel skillModel)
        {
            if (_graphSkillModel.TryForget(skillModel) == false)
            {
                ResetChoose(_windowError.ShowErrorForget);
                return;
            }
            
            skillModel.SetOpened(false);
            ResetChoose();
        }
    }
}