using System;
using BalloonEndlessRunner.Enums;
using UnityEngine;

namespace BalloonEndlessRunner.Data
{
    [Serializable]
    public class ObstaclesSpritesConfig
    {
        [SerializeField] private GameLevelType gameLevelType;
        [SerializeField] private Sprite[] sprites;
        public GameLevelType GameLevelType => gameLevelType;
        private int _index;
        
        public Sprite GetSprite()
        {
            var result = sprites[_index];
            if (++_index > sprites.Length - 1)
                _index = 0;

            return result;
        }
    }
}