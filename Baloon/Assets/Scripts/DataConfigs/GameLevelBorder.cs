using BalloonEndlessRunner.Enums;
using UnityEngine;

namespace BalloonEndlessRunner.Data
{
    public class GameLevelBorder
    {
        private readonly GameLevelType _gameLevelType;
        private readonly Transform _startLevelBorder;
        private readonly Transform _endLevelBorder;
        public GameLevelType GameLevelType => _gameLevelType;

        public GameLevelBorder(Transform startLevelBorder, Transform endLevelBorder, GameLevelType gameLevelType)
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