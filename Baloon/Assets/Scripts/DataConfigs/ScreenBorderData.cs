using System;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Data
{
    public class ScreenBorderData : IInitializable
    {
        private Camera _camera;
        private float _offset;
        private Transform _transform;

        public void Initialize()
        {
            _camera = Camera.main;
            _transform = _camera.transform;
            _offset = _camera.orthographicSize * 1.25f;
        }
        
        public float DespawnBorder()
        {
            return _transform.position.y - _offset;
        }

        public float SpawnBorder()
        {
            return _transform.position.y + _offset;
        }
        
        public float Height()
        {
            return _camera.orthographicSize * 2;
        }

        public float Width()
        {
            return Height() * _camera.aspect;
        }
    }
}