using Configuration;
using UnityEngine;

namespace Digger
{
    public class DigState : State
    {
        private float remainingTime;
        private float producingRate;

        public override void Initialize(Subject subject)
        {
            base.Initialize(subject);
            var cfg = Root.Instance.Digger;
            float m = Random.Range(0.0f, 1.0f);
            remainingTime = cfg.DigTimeMin + m * (cfg.DigTimeMax - cfg.DigTimeMin);
            producingRate = cfg.ProducingRateMin + m * (cfg.ProducingRateMax - cfg.ProducingRateMin);
            Subject.Visual.Dig();
            AudioPlayer.Dig();
        }

        public void Update()
        {
            Subject.WaterTank.Produce(producingRate * Time.deltaTime);
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0.0f)
                Subject.SwitchToWalkState();
        }
    }
}