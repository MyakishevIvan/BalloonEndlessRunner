using System;
using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Systems;
using Leopotam.Ecs;
using Voody.UniLeo;
using Zenject;

namespace BalloonEndlessRunner.Installers
{
    public class EcsGameInstaller : ITickable, IInitializable, IDisposable
    {
        [Inject] private ScreenBorderData _screenBorderData;
        [Inject] private ConfigStorage _configStorage;
        [Inject] private EcsWorld _ecsWorld;
        [Inject] private BackGroundMovementSystem _backGroundMovementSystem;
        [Inject] private ObstacleInstantiateSystem _obstacleInstantiateSystem;
        [Inject] private PlayerMovementSystem _playerMovementSystem;
        [Inject] private RestartGameSystem _restartGameSystem;
        [Inject] private ObstacleDespawnSystem _obstacleDespawnSystem;

        private EcsSystems _ecsSystems;

        private void AddSystems()
        {
            _ecsSystems.Add(new InputSystem());
            _ecsSystems.Add(new LineCalculationSystem());
            _ecsSystems.Add(new GameLevelDefineSystem());
            _ecsSystems.Add(_playerMovementSystem);
            _ecsSystems.Add(_backGroundMovementSystem);
            _ecsSystems.Add(_obstacleInstantiateSystem);
            _ecsSystems.Add(new ObstacleMovementSystem());
            _ecsSystems.Add(_obstacleDespawnSystem);
            _ecsSystems.Add(_restartGameSystem);
        }

        private void AddInjections()
        {
            _ecsSystems.Inject(_configStorage);
            _ecsSystems.Inject(_screenBorderData);
            _ecsSystems.Inject(new BackGroundLineData());
            _ecsSystems.Inject(new GameLevelData());
        }

        public void Tick()
        {
            _ecsSystems.Run();
        }

        public void Initialize()
        {
            _ecsSystems = new EcsSystems(_ecsWorld);

            _ecsSystems.ConvertScene();
            AddInjections();
            AddSystems();
            _ecsSystems.Init();
        }

        public void Dispose()
        {
            if (_ecsSystems == null)
                return;

            _ecsSystems.Destroy();
            _ecsSystems = null;
            _ecsWorld.Destroy();
            _ecsWorld = null;
        }
    }
}