using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.View;
using Kefir.View.Graph;
using Kefir.ViewModel;
using Runtime.View;
using UnityEngine;
using Zenject;
using Kefir.Extensions;

namespace Kefir.Bootstrap
{

    internal sealed class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
    {
        [SerializeField] private List<SkillView> _nodesView;
        [SerializeField] private WindowActionView _windowAction;
        [SerializeField] private WindowErrorView _windowError;

        [SerializeField] private AdvancedButton _scoreButton;
        
        [SerializeField] private AdvancedButton _forgetAllButton;
        
        [SerializeField] private AdvancedButton _studyButton;
        [SerializeField] private AdvancedButton _forgetButton;
        [SerializeField] private AdvancedButton _arrowButton;
        
        [SerializeField] private ScoreView _scoreView;

        private List<SkillModel> _nodeModels = new();
        
        private void InstallSkillModels()
        {
            _nodeModels = new List<SkillModel>();
            
            for (int i = 0; i < _nodesView.Count; i++)
                _nodeModels.Add(new SkillModel());

            CostsLoad();
            
            Container.Bind<IEnumerable<ISkillModel>>()
                     .To<List<SkillModel>>()
                     .FromInstance(_nodeModels);
            
            
            Container.Bind<IEnumerable<ISkillView>>()
                     .To<List<SkillView>>()
                     .FromInstance(_nodesView);
            
            Container.Bind<ISkillModel>()
                     .WithId("root")
                     .To<SkillModel>()
                     .FromInstance(_nodeModels[0]);
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

        private void CostsLoad()
        {
            _nodeModels.ForEach(LoadCosts(), (model, cost) => model.SetCost(cost));
        }

        private void GraphInit()
        {
            Container.BindInterfacesTo<GraphSkillModel>().AsSingle();
            Container.Bind<LoaderFile>().FromNew().AsSingle().NonLazy();
        }
        
        public override void InstallBindings()
        {
            InstallSkillModels();
            BindingButtons();
            
            Container.BindInterfacesTo<SkillsViewModel>().AsSingle().NonLazy();
            
            GraphInit();
            
            Container.BindInterfacesTo<GraphSkillViewModel>().AsSingle().NonLazy();

            Container.BindInterfacesTo<WindowActionView>().FromInstance(_windowAction).AsSingle();
            Container.BindInterfacesTo<WindowErrorView>().FromInstance(_windowError).AsSingle();
           
            Container.BindInterfacesTo<ScoreModel>().AsSingle();
            Container.BindInterfacesTo<ScoreView>().FromInstance(_scoreView).AsSingle();
            Container.BindInterfacesTo<ScoreViewModel>().AsSingle().NonLazy();

            Container.BindInterfacesTo<WindowActionViewModel>().AsSingle().NonLazy();

            Container.BindInterfacesTo<ForgetAllViewModel>().AsSingle().NonLazy();
        }

        private IEnumerable<int> LoadCosts()
        {
            var text = Resources.Load<TextAsset>("costs").text;
            return text.Split(' ').Select(int.Parse);
        }
    }
}