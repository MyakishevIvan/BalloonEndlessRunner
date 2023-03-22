using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class ObstacleSpawnSystem : IEcsRunSystem
    {
        [Inject] private ConfigStorage _configStorage;
        [Inject] private ScreenBorderData _screenBorderData;
        [Inject] private BackGroundLineData _backGroundLineData;
        [Inject] private GameLevelData _gameLevelData;

        private readonly EcsFilter<ObstacleTag, DespawnedComponent, SpriteRendererComponent, ModelComponent,
            BoxCollider2dComponent> _filter;

        private EcsWorld _ecsWorld = null;
        private GameObject _obstaclesContainer;
        private readonly float _targetTime = 3f;
        private float _currentTime;

        public void Run()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _targetTime)
            {
                _currentTime = 0;
                SpawnObstacle();
            }
        }

        private void SpawnObstacle()
        {
            var despawnedObstacle = _filter.GetEntity(0);
            ref var renderer = ref despawnedObstacle.Get<SpriteRendererComponent>();
            ref var model = ref despawnedObstacle.Get<ModelComponent>();
            ref var collider = ref despawnedObstacle.Get<BoxCollider2dComponent>().collider;
            despawnedObstacle.Del<DespawnedComponent>();
            despawnedObstacle.Get<SpawnedComponent>();
            renderer.spriteRenderer.sprite = _configStorage.GetObstacleSprite(_gameLevelData.GameLevelType);
            model.modelTransform.position = SetObstaclePosition();
            model.modelTransform.gameObject.SetActive(true);
            var currentSize = renderer.spriteRenderer.sprite.bounds.size;
            collider.size = currentSize * 0.7f;
        }

        private Vector3 SetObstaclePosition()
        {
            var rndIndex = Random.Range(0, 3);
            var xPos = _backGroundLineData.Lines[rndIndex];
            var yPos = _screenBorderData.SpawnBorder();
            return new Vector3(xPos, yPos, 1);
        }
    }
}