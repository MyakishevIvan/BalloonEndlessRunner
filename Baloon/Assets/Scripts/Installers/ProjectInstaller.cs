using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Signals;
using BalloonEndlessRunner.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ConfigStorage _configStorage;
        public override void InstallBindings()
        {
            DeclareSignals();
            BindSystems();
        }
    
        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<EndGameSignal>();
        }
    
        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<ScreenBorderData>().AsSingle().NonLazy();
            Container.Bind<ConfigStorage>().FromInstance(_configStorage).AsSingle().NonLazy();
            Container.BindInstance(new EcsWorld());
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LineCalculationSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelsBordersDefineSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelDefineSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObstacleSpawnSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObstacleMovementSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RestartGameSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BackGroundMovementSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObstacleInstantiationSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObstacleDespawnSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BackGroundLineData>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameLevelData>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EcsGameInstaller>().AsSingle().NonLazy();
        }
    }    
}
