using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Enums;
using BalloonEndlessRunner.Signals;
using BalloonEndlessRunner.Tags;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private ConfigStorage _configStorage;
        [Inject] private BackGroundLineData _backGroundLineData;
        private readonly EcsFilter<PlayerTag, ModelComponent, MovableComponent, DirectionComponent> _filter;
        private readonly EcsFilter<EndGameEvent> _endGameFilter;
        private bool _canMove = true;

        public void Init()
        {
            _signalBus.Subscribe<StartGameSignal>((v) =>
            {
                _canMove = true;
                var balloonTransform = _filter.Get2(0).modelTransform;
                ref var movableComponent = ref _filter.Get3(0);
                movableComponent.currentLine = 1;

                balloonTransform.position = new Vector3
                (
                    GetTargetPosition(movableComponent.currentLine),
                    balloonTransform.transform.position.y,
                    balloonTransform.position.z
                );
            });
        }

        public void Run()
        {
            if (!_endGameFilter.IsEmpty())
                return;

            foreach (var i in _filter)
            {
                ref var modelComponent = ref _filter.Get2(i);
                ref var movableComponent = ref _filter.Get3(i);
                ref var directionComponent = ref _filter.Get4(i);

                if (directionComponent.SwipeType == SwipeType.Left && movableComponent.currentLine == 0)
                    return;
                if (directionComponent.SwipeType == SwipeType.Right && movableComponent.currentLine == 2)
                    return;
                if (directionComponent.SwipeType == SwipeType.None)
                    return;

                if (!_canMove)
                    return;

                movableComponent.currentLine += (int)directionComponent.SwipeType;
                var target = GetTargetPosition(movableComponent.currentLine);
                modelComponent.modelTransform.DOMoveX(target, _configStorage.ChangeLineDuration)
                    .OnComplete(() => _canMove = true);
                _canMove = false;
            }
        }

        private float GetTargetPosition(int lineIndex)
        {
            return _backGroundLineData.Lines[lineIndex];
        }
    }
}