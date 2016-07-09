using System;
using UnityEngine;

namespace Configuration
{
    [Serializable]
    public class AnimalConfig
    {
        public Color[] Colors;
        public float TowardsWaterSpeed;
        public float FromWaterSpeed;
        public float ShrinkingSpeed;
        public float GrowingSpeed;
        public float ConsumingSpeed;
        public float WaitTime;
        public float EmptyNormalBound;
        public float NormalFullBound;
    }
}