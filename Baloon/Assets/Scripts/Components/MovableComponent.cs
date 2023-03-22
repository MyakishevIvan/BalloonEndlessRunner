using System;
using UnityEngine;

namespace BalloonEndlessRunner.Components
{
    [Serializable]
    public struct MovableComponent
    {
        [Range(0,2)] public int currentLine;
    }
}