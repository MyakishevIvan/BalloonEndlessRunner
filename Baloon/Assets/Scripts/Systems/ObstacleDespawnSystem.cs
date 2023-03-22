using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Signals;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class ObstacleDespawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private readonly ScreenBorderData _screenBorderData;
        private readonly EcsFilter<ObstacleTag, SpawnedComponent, ModelComponent> _filter = null;
        
        public void Init()
        {
            _signalBus.Subscribe<StartGameSignal>(DespawnAllObstacles);
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var transformComponent = ref _filter.Get3(i).modelTransform;
                var yPos = transformComponent.position.y;
                
                if (yPos < _screenBorderData.DespawnBorder())
                    Despawn(i, transformComponent);
            }
        }

        private void DespawnAllObstacles()
        {
            foreach (var i in _filter)
            {
                ref var transformComponent = ref _filter.Get3(i).modelTransform;
                Despawn(i, transformComponent);
            }
        }
        
        private void Despawn(int i, Transform transformComponent)
        {
            var entity = _filter.GetEntity(i);
            entity.Del<SpawnedComponent>();
            transformComponent.gameObject.SetActive(false);
            entity.Get<DespawnedComponent>();
        }
        
    }
}