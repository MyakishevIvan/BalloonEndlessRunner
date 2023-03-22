using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace BalloonEndlessRunner.Systems
{
    public class ObstacleInstantiateSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<ObstacleTag, DespawnedComponent, SpriteRendererComponent, ModelComponent,
            BoxCollider2dComponent> _filter;

        private EcsWorld _ecsWorld = null;
        private GameObject _obstaclesContainer;
        private readonly ConfigStorage _configStorage = null;
        private readonly ScreenBorderData _screenBorderData = null;
        private readonly GameLevelData _gameLevelData;
        private readonly BackGroundLineData _backGroundLineData;
        private float _targetTime = 3f;
        private float _currentTime;

        public void PreInit()
        {
            CreatCashedObstacles();
        }
        
        private void CreatCashedObstacles()
        {
            _obstaclesContainer = new GameObject("ObstaclesContainer");

            for (var i = 0; i < _configStorage.CashedObstaclesCount; i++)
            {
                InstantiateObstacle();
            }
        }

        public void Run()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _targetTime)
            {
                _currentTime = 0;

                if (_filter.IsEmpty())
                    InstantiateObstacle();

                var unspawnedObstacle = _filter.GetEntity(0);
                ref var renderer = ref unspawnedObstacle.Get<SpriteRendererComponent>();
                ref var model = ref unspawnedObstacle.Get<ModelComponent>();
                ref var collider = ref unspawnedObstacle.Get<BoxCollider2dComponent>().collider;
                unspawnedObstacle.Del<DespawnedComponent>();
                unspawnedObstacle.Get<SpawnedComponent>();
                renderer.spriteRenderer.sprite = _configStorage.GetObstacleSprite(_gameLevelData.GameLevelType);
                model.modelTransform.position = SetObstaclePosition();
                model.modelTransform.gameObject.SetActive(true);
                var currentSize = renderer.spriteRenderer.sprite.bounds.size;
                collider.size = currentSize;
            }
        }

        private void InstantiateObstacle()
        {
            var obstacleEntity = _ecsWorld.NewEntity();
            ref var modelComponent = ref obstacleEntity.Get<ModelComponent>();
            ref var rendererComponent = ref obstacleEntity.Get<SpriteRendererComponent>();
            obstacleEntity.Get<DespawnedComponent>();
            obstacleEntity.Get<ObstacleTag>();
            ref var collider = ref obstacleEntity.Get<BoxCollider2dComponent>();
            var obstacle = GameObject.Instantiate(_configStorage.ObstaclePrefab, _obstaclesContainer.transform);
            obstacle.SetActive(false);
            modelComponent.modelTransform = obstacle.transform;
            rendererComponent.spriteRenderer = obstacle.GetComponent<SpriteRenderer>();
            collider.collider = obstacle.GetComponent<BoxCollider2D>();
        }

        private Vector3 SetObstaclePosition()
        {
            var rndIndex = Random.Range(0, 3);
            var xPos = _backGroundLineData.lines[rndIndex];
            var yPos = _screenBorderData.SpawnBorder();
            return new Vector3(xPos, yPos, 1);
        }
    }
}