using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kefir.Model.Graph;
using Kefir.Model.Score;
using Kefir.ViewModel;
using Zenject;
using Kefir.Extensions;
using Kefir.Loading;
using UnityEngine;

namespace Kefir.Bootstrap
{
    internal sealed class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
    {
        private const string NameFileGraph = "matrix";
        private const string NameFileCosts = "costs";

        [SerializeField] private ViewInstaller _viewInstaller;
        
        private List<SkillModel> _nodeModels = new();

        public override void InstallBindings()
        {
            Loading();
            InstallSkillModels();
            InstallScore();
            GraphInit();
            InstallViewModels();
        }

        private void InstallViewModels()
        {
            Container.BindInterfacesTo<SkillsViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GraphSkillViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WindowActionViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ForgetAllViewModel>().AsSingle().NonLazy();
        }
        
        private void InstallScore()
        {
            Container.BindInterfacesTo<ScoreModel>().AsSingle();
            Container.BindInterfacesTo<ScoreViewModel>().AsSingle().NonLazy();
        }
        
        private void InstallSkillModels()
        {
            _nodeModels = new List<SkillModel>();
            
            for (int i = 0; i < _viewInstaller.SkillsCount; i++)
                _nodeModels.Add(new SkillModel());

            CostsLoad();
            
            Container.Bind<IEnumerable<ISkillModel>>()
                     .To<List<SkillModel>>()
                     .FromInstance(_nodeModels);
            
            Container.Bind<ISkillModel>()
                     .WithId("root")
                     .To<SkillModel>()
                     .FromInstance(_nodeModels[0]);
        }



        private void CostsLoad()
        {
            _nodeModels.ForEach(new LoaderCosts(NameFileCosts).LoadCosts(), (model, cost) => model.SetCost(cost));
        }

        private void GraphInit()
        {
            Container.BindInterfacesTo<GraphSkillModel>().AsSingle();
            Container.BindInterfacesTo<DepthFirstSearch>().AsSingle();
        }

        private void Loading()
        {
            Container.BindInterfacesTo<LoaderGraphAdjacencyList>()
                     .FromInstance(new LoaderGraphAdjacencyList(NameFileGraph))
                     .AsSingle();

            Container.Bind<IRouterData>()
                     .To<RouterData>()
                     .FromNew()
                     .AsSingle()
                     .NonLazy();
        }
        
    }
}