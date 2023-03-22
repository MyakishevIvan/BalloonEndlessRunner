using System;
using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Enums;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class LevelsBordersDefineSystem : IEcsPreInitSystem
    {
        [Inject] private  GameLevelData _gameLevelData;
        private readonly EcsFilter<LevelBordersComponent> _levelBorderFilter;

        public void PreInit()
        {
            var levelBorders = _levelBorderFilter.Get1(0);
            var borderCount = levelBorders.levelsBorders.Length - 1;
            var levelCount = Enum.GetNames(typeof(GameLevelType)).Length;

            if (borderCount != levelCount)
                throw new Exception("Levels border and game levels doesn't equal");

            _gameLevelData.LevelsBorders = new GameLevelData.Border[levelCount];
            for (int i = 0, j = 0; i < levelCount; i++, j++)
            {
                var startLevelBorder = levelBorders.levelsBorders[j];
                var endLevelBorder = levelBorders.levelsBorders[j + 1];
                _gameLevelData[i] = new GameLevelData.Border(startLevelBorder, endLevelBorder, (GameLevelType)i);
            }
        }
    }
}