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
        [Inject] private EcsWorld _ecsWorld;
        [Inject] private BackGroundMovementSystem _backGroundMovementSystem;
        [Inject] private ObstacleInstantiationSystem _obstacleInstantiationSystem;
        [Inject] private PlayerMovementSystem _playerMovementSystem;
        [Inject] private RestartGameSystem _restartGameSystem;
        [Inject] private ObstacleDespawnSystem _obstacleDespawnSystem;
        [Inject] private InputSystem _inputSystem;
        [Inject] private LineCalculationSystem _lineCalculationSystem;
        [Inject] private LevelsBordersDefineSystem _levelsBordersDefineSystem;
        [Inject] private ObstacleSpawnSystem _obstacleSpawnSystem;
        [Inject] private ObstacleMovementSystem _obstacleMovementSystem;
        [Inject] private LevelDefineSystem _levelDefineSystem;
        
        private EcsSystems _ecsSystems;

        public void Initialize()
        {
            _ecsSystems = new EcsSystems(_ecsWorld);
            _ecsSystems.ConvertScene();
            AddSystems();
            _ecsSystems.Init();
        }
        
        private void AddSystems()
        {
            _ecsSystems.Add(_inputSystem);
            _ecsSystems.Add(_lineCalculationSystem);
            _ecsSystems.Add(_levelsBordersDefineSystem);
            _ecsSystems.Add(_levelDefineSystem);
            _ecsSystems.Add(_playerMovementSystem);
            _ecsSystems.Add(_backGroundMovementSystem);
            _ecsSystems.Add(_obstacleInstantiationSystem);
            _ecsSystems.Add(_obstacleSpawnSystem);
            _ecsSystems.Add(_obstacleMovementSystem);
            _ecsSystems.Add(_obstacleDespawnSystem);
            _ecsSystems.Add(_restartGameSystem);
        }
        
        public void Tick()
        {
            _ecsSystems.Run();
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