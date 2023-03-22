using System;
using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Enums;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace BalloonEndlessRunner.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, DirectionComponent> directionFilter = null;
        private bool _isDragging;
        private bool _isMobilePlatform;
        private float _minSwipeDelta = 5f;
        private Vector2 _swipeDelta;
        private Vector2 _tapPoint;
        private SwipeType _currentSwipe;

        public void Run()
        {
            DetectSwipe();
            CalculateSwipe();

            foreach (var i in directionFilter)
            {
                ref var directionComponent = ref directionFilter.Get2(i);
                directionComponent.SwipeType = _currentSwipe;
            }

            if (_currentSwipe != SwipeType.None)
                ResetSwipe();
        }

        private void DetectSwipe()
        {
            if (!_isMobilePlatform)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _isDragging = true;
                    _tapPoint = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    ResetSwipe();
                }
            }
            else
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _isDragging = true;
                    _tapPoint = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Canceled
                         || Input.touches[0].phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }

        private void CalculateSwipe()
        {
            _swipeDelta = Vector2.zero;

            if (_isDragging)
            {
                if (!_isMobilePlatform && Input.GetMouseButton(0))
                    _swipeDelta = (Vector2)Input.mousePosition - _tapPoint;
                else if (Input.touchCount > 0)
                    _swipeDelta = Input.touches[0].position - _tapPoint;

                if (_swipeDelta.magnitude > _minSwipeDelta)
                    _currentSwipe = _swipeDelta.x > 0 ? SwipeType.Right : SwipeType.Left;
            }
        }

        private void ResetSwipe()
        {
            _isDragging = false;
            _tapPoint = Vector2.zero;
            _swipeDelta = Vector2.zero;
            _currentSwipe = SwipeType.None;
        }
    }
}