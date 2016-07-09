using Configuration;
using UnityEngine;

namespace Animal
{
    public class WaitState: State
    {
        private float remainingTime;

        public override void Initialize(Subject subject)
        {
            base.Initialize(subject);
            var cfg = Root.Instance.Animal;
            remainingTime = cfg.WaitTime;
        }

        public void Update()
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0.0f)
                Subject.SwitchToTowardsWaterState();
        }
    }
}