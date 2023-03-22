using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Configs;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class ObstacleInstantiationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        [Inject] private ConfigStorage _configStorage;
        private readonly EcsFilter<ObstacleTag, DespawnedComponent, SpriteRendererComponent, ModelComponent,
            BoxCollider2dComponent> _filter;
        private EcsWorld _ecsWorld = null;
        private GameObject _obstaclesContainer;

        public void PreInit()
        {
            CreatCashedObstacles();
        }

        private void CreatCashedObstacles()
        {
            _obstaclesContainer = new GameObject("ObstaclesContainer");

            for (var i = 0; i < _configStorage.CashedObstaclesCount; i++)
                InstantiateObstacle();
        }

        public void Run()
        {
            if (_filter.IsEmpty())
                InstantiateObstacle();
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
    }
}