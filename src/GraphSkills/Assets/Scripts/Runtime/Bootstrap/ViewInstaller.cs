using System.Collections.Generic;
using Kefir.View;
using Kefir.View.Graph;
using Runtime.View;
using UnityEngine;
using Zenject;

namespace Kefir.Bootstrap
{
    internal sealed class ViewInstaller : MonoInstaller<BootstrapInstaller>
    {
        public int SkillsCount => _skillsView.Count;
        
        [SerializeField] private List<SkillView> _skillsView;
        
        [SerializeField] private WindowActionView _windowAction;
        [SerializeField] private WindowErrorView _windowError;

        [SerializeField] private AdvancedButton _scoreButton;
        [SerializeField] private AdvancedButton _forgetAllButton;
        [SerializeField] private AdvancedButton _studyButton;
        [SerializeField] private AdvancedButton _forgetButton;
        
        [SerializeField] private ScoreView _scoreView;

        public override void InstallBindings()
        {
            InstallWindows();
            InstallScoreView();
            BindingButtons();
            InstallSkills();
        }

        private void InstallWindows()
        {
            Container.BindInterfacesTo<WindowActionView>().FromInstance(_windowAction).AsSingle();
            Container.BindInterfacesTo<WindowErrorView>().FromInstance(_windowError).AsSingle();
        }

        private void InstallSkills()
        {
            Container.Bind<IEnumerable<ISkillView>>()
                     .To<List<SkillView>>()
                     .FromInstance(_skillsView);
        }

        private void InstallScoreView()
        {
            Container.BindInterfacesTo<ScoreView>().FromInstance(_scoreView).AsSingle();
        }
        
        private void BindingButtons()
        {
            Container.Bind<IAdvancedButton>()
                     .WithId("buttonStudy")
                     .To<AdvancedButton>()
                     .FromInstance(_studyButton);
            
            Container.Bind<IAdvancedButton>()
                     .WithId("buttonForget")
                     .To<AdvancedButton>()
                     .FromInstance(_forgetButton);
            
            Container.Bind<IAdvancedButton>()
                     .WithId("scoreButton")
                     .To<AdvancedButton>()
                     .FromInstance(_scoreButton);
            
            Container.Bind<IAdvancedButton>()
                     .WithId("forgetAllButton")
                     .To<AdvancedButton>()
                     .FromInstance(_forgetAllButton);
        }
    }
}