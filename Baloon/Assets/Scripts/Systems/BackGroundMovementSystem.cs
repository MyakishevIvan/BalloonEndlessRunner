using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Signals;
using BalloonEndlessRunner.Tags;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class BackGroundMovementSystem : IEcsPreInitSystem, IEcsInitSystem
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private readonly ScreenBorderData _screenBorderData;
        [Inject] private readonly ConfigStorage _configStorage;
        private readonly EcsFilter<BackGroundTag, ModelComponent, SpriteRendererComponent> _filter;
        private readonly EcsFilter<EndGameEvent> _endGameFilter;
        private Sequence _sequence;
        private float _endMoveValue;
        private EcsEntity _entity;
        private Vector2 _startPosition;
        
        
        public void PreInit()
        {
            _signalBus.Subscribe<EndGameSignal>(StopMoving);
            _signalBus.Subscribe<StartGameSignal>((_) => StartMoving());
            _entity = _filter.GetEntity(0);
            var bgHeight = _entity.Get<SpriteRendererComponent>().spriteRenderer.size.y;
            _endMoveValue = -bgHeight + _screenBorderData.Height()/2;
            _startPosition = _entity.Get<ModelComponent>().modelTransform.position;
        }

        private void StopMoving(EndGameSignal signal) => _sequence.Kill();
        
        public void Init()
        {
            StartMoving();
        }

        private void StartMoving()
        {
            var transform = _entity.Get<ModelComponent>().modelTransform;
            transform.position = _startPosition;
            _sequence =  DOTween.Sequence();
            _sequence.Append(transform.DOMoveY(_endMoveValue, _configStorage.MoveBackgroundDuration).SetEase(Ease.InCubic));
            _sequence.Play();
        }
    }
}