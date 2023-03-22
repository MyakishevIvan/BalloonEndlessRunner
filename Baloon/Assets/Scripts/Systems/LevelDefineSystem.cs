using System;
using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class LevelDefineSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, ModelComponent> _playerFilter;
        [Inject] private  GameLevelData _gameLevelData;

        public void Run()
        {
            var playerYPos = _playerFilter.Get2(0).modelTransform.position.y;

            for (int i = 0; i < _gameLevelData.LevelsBorders.Length; i++)
            {
                if (_gameLevelData[i].IsInclude(playerYPos, out var result))
                {
                    _gameLevelData.GameLevelType = result;
                    return;
                }
            }

            throw new Exception("Can't define level");
        }
    }
}