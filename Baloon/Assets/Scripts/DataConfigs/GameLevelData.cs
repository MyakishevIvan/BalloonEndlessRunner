using BalloonEndlessRunner.Enums;
using UnityEngine;

namespace BalloonEndlessRunner.Data
{
    public class GameLevelData
    {
        public GameLevelType GameLevelType;
        public Border[] LevelsBorders;

        public Border this[int index]
        {
            get => LevelsBorders[index];
            set => LevelsBorders[index] = value;
        }
        public class Border
        {
            private  GameLevelType _gameLevelType;
            private  Transform _startLevelBorder;
            private  Transform _endLevelBorder;

            public Border(Transform startLevelBorder, Transform endLevelBorder, GameLevelType gameLevelType)
            {
                _startLevelBorder = startLevelBorder;
                _endLevelBorder = endLevelBorder;
                _gameLevelType = gameLevelType;
            }

            public bool IsInclude(float yPos, out GameLevelType result)
            {
                result = default;
                var isInclude = yPos >= _startLevelBorder.position.y && yPos < _endLevelBorder.transform.position.y;

                if (isInclude)
                    result = _gameLevelType;

                return isInclude;
            }
        }
    }
}