using System;
using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Enums;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;

namespace BalloonEndlessRunner.Systems
{
    public class GameLevelDefineSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<LevelBordersComponent> _levelBorderFilter;
        private readonly GameLevelData _gameLevelData;
        private readonly EcsFilter<PlayerTag, ModelComponent> _playerFilter;
        private GameLevelBorder[] _gameBorders;

        public void PreInit()
        {
            var levelBorders = _levelBorderFilter.Get1(0);
            var borderCount = levelBorders.levelsBorders.Length - 1;
            var levelCount = Enum.GetNames(typeof(GameLevelType)).Length;

            if (borderCount != levelCount)
                throw new Exception("Levels border and game levels doesn't equal");

            _gameBorders = new GameLevelBorder[levelCount];
            for (int i = 0, j = 0; i < levelCount; i++, j++)
            {
                var startLevelBorder = levelBorders.levelsBorders[j];
                var endLevelBorder = levelBorders.levelsBorders[j + 1];
                _gameBorders[i] = new GameLevelBorder(startLevelBorder, endLevelBorder, (GameLevelType)i);
            }
        }

        public void Run()
        {
            var playerYPos = _playerFilter.Get2(0).modelTransform.position.y;

            for (int i = 0; i < _gameBorders.Length; i++)
            {
                if (_gameBorders[i].IsInclude(playerYPos, out var result))
                {
                    _gameLevelData.GameLevelType = result;
                    return;
                }
            }

            throw new Exception("Can't define level");
        }
    }
}