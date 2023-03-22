using System.Collections.Generic;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Enums;
using UnityEngine;

namespace BalloonEndlessRunner.Configs
{
    [CreateAssetMenu(fileName = nameof(ConfigStorage), menuName = "Config/" + nameof(ConfigStorage))]
    public class ConfigStorage : ScriptableObject
    {
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private List<ObstaclesSpritesConfig> obstaclesConfig;
        [SerializeField] private int obstacleSpeed = 3;
        [SerializeField] private int changeLineDuration = 8;
        [SerializeField] private int moveBackgroundDuration = 8;
        [SerializeField] private int reloadGameTime = 3;
        private int _cashedObstaclesCount = 10;
        private Dictionary<GameLevelType, ObstaclesSpritesConfig> _obstacleConfigDictionary;
        
        public GameObject ObstaclePrefab => obstaclePrefab;
        public int CashedObstaclesCount => _cashedObstaclesCount;
        public int ObstacleSpeed => obstacleSpeed;
        public int ChangeLineDuration => changeLineDuration;
        public int MoveBackgroundDuration => moveBackgroundDuration;
        public int ReloadGameTime => reloadGameTime;
        

        public Sprite GetObstacleSprite(GameLevelType gameLevelType)
        {
            var obstacleConfigDictionary = _obstacleConfigDictionary ?? CreatObstacleDict();
            return obstacleConfigDictionary[gameLevelType].GetSprite();
        }
        
        private Dictionary<GameLevelType, ObstaclesSpritesConfig> CreatObstacleDict()
        {
            _obstacleConfigDictionary = new Dictionary<GameLevelType, ObstaclesSpritesConfig>();

            foreach (var config in obstaclesConfig)
                _obstacleConfigDictionary.Add(config.GameLevelType, config);

            return _obstacleConfigDictionary;
        }
    }
}