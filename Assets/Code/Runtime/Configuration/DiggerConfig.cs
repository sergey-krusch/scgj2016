using System;

namespace Configuration
{
    [Serializable]
    public class DiggerConfig
    {
        public float Speed;
        public float DigTime;
        public float DigTimeMin;
        public float DigTimeMax;
        public float ProducingRate;
        public float ProducingRateMin;
        public float ProducingRateMax;
    }
}