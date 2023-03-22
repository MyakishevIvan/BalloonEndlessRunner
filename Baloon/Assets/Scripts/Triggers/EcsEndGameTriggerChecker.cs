using BalloonEndlessRunner.Components;
using BalloonEndlessRunner.Signals;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace BalloonEndlessRunner.Triggers
{
    public class EcsEndGameTriggerChecker : MonoBehaviour
    {
        [SerializeField] private string triggerLayerName;
        [Inject] private SignalBus _signalBus;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer != LayerMask.NameToLayer(triggerLayerName))
                return;

            WorldHandler.GetWorld().NewEntity().Get<EndGameEvent>();
            _signalBus.Fire(new EndGameSignal());
        }
    }
}