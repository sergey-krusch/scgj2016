using System;

namespace Configuration
{
    [Serializable]
    public class AnimalConfig
    {
        public float TowardsWaterSpeed;
        public float FromWaterSpeed;
        public float ShrinkingSpeed;
        public float GrowingSpeed;
        public float ConsumingSpeed;
        public float WaitTime;
    }
}