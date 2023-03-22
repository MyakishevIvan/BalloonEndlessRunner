using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Signals;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class RestartGameSystem : IEcsRunSystem
    {
        [Inject] private SignalBus _signalBus;
        private readonly EcsWorld _ecsWorld;
        private readonly EcsFilter<EndGameEvent> _endGameFilter;
        private readonly ConfigStorage _configStorage;
        private float _currentTime;

        public void Run()
        {
            foreach (var i in _endGameFilter)
            {
                ref var entity = ref _endGameFilter.GetEntity(i);
                _currentTime += Time.deltaTime;
                    Debug.Log("EndGame");

                if (_currentTime >= _configStorage.ReloadGameTime)
                    RestartGame(ref entity);
            }
        }

        private void RestartGame(ref EcsEntity entity)
        {
            _signalBus.Fire(new StartGameSignal());
            _currentTime = 0;
            entity.Del<EndGameEvent>();
        }
    }
}