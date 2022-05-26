using System;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View;
using Kefir.View.Graph;
using Unity.VisualScripting;

namespace Kefir.ViewModel
{
    internal sealed class WindowsActionViewModel : ViewModelBase, IDisposable
    {
        private readonly WindowActionView _windowAction;
        private readonly AdvancedButton _buttonStudy;
        private readonly AdvancedButton _buttonForget;
        private readonly WindowErrorView _windowError;
        private readonly GraphSkillModel _graphSkillModel;
        private readonly GraphSkillViewModel _graphSkillViewModel;
        private readonly ScoreModel _scoreModel;

        internal  WindowsActionViewModel(WindowActionView windowAction,
                                         WindowErrorView windowError,
                                         AdvancedButton buttonStudy,
                                         AdvancedButton buttonForget,
                                         GraphSkillModel graphSkillModel,
                                         GraphSkillViewModel graphSkillViewModel,
                                         ScoreModel scoreModel)
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
        
        private void SubscribeClicked(SkillModel skill, NodeView view)
        {
            _graphSkillViewModel.CancelChooseAll();
            
            _buttonStudy.Unbind();
            _buttonForget.Unbind();

            _buttonStudy.Bind(() => StudyHandle(skill));
            _buttonForget.Bind(() => ForgetHandle(skill));

            _windowAction.Show(skill.IsOpened.Value);
            
            view.Choose();
        }
        
        private void StudyHandle(SkillModel skillModel)
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

        private void ForgetHandle(SkillModel skillModel)
        {
            if (_graphSkillModel.TryForget(skillModel) == false)
            {
                ResetChoose(_windowError.ShowErrorForget);
                return;
            }
            
            skillModel.SetOpened(false);
            ResetChoose();
        }

        private void ResetChoose(params Action[] actions)
        {
            foreach (var action in actions)
                action?.Invoke();
            
            _graphSkillViewModel.CancelChooseAll();
            _windowAction.Hide();
        }
        
    }
}