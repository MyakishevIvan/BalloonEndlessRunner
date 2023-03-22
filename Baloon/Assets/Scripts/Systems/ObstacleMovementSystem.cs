using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class ObstacleMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ObstacleTag, SpawnedComponent, ModelComponent> _filter = null;
        private readonly EcsFilter<EndGameEvent> _endGameFilter;
        [Inject] private ConfigStorage _configStorage;
        
        public void Run()
        {
            if(!_endGameFilter.IsEmpty())
                return;
            
            foreach (var i in _filter)
            {
                ref var transform = ref _filter.Get3(i).modelTransform;
                transform.Translate(Vector3.down * _configStorage.ObstacleSpeed * Time.deltaTime);
            }    
        }
    }
}