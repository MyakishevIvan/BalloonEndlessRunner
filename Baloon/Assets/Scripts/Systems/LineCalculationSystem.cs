using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Data;
using BalloonEndlessRunner.Tags;
using Leopotam.Ecs;
using Zenject;

namespace BalloonEndlessRunner.Systems
{
    public class LineCalculationSystem : IEcsPreInitSystem
    {
       [Inject] private BackGroundLineData _backGroundLineData = null;
        private readonly EcsFilter<BackGroundTag, SpriteRendererComponent> _filter = null;
        
        public void PreInit()
        {
            CalculateLines();
        }
        
        private void CalculateLines()
        {
            var spriteRenderer = _filter.GetEntity(0).Get<SpriteRendererComponent>().spriteRenderer;
            var leftBorder = spriteRenderer.transform.TransformPoint(spriteRenderer.sprite.bounds.min).x;
            var lineOffset = spriteRenderer.size.x / 4;
            ref var lines = ref _backGroundLineData.Lines;
            for (var i = 0; i < 3; i++)
                lines[i] = leftBorder + lineOffset + lineOffset * i;
        }
    }
}