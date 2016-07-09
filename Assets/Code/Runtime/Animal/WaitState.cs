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
            subject.TappedEvent += Tapped;
            var cfg = Root.Instance.Animal;
            remainingTime = cfg.WaitTime;
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
            Subject.TappedEvent -= Tapped;
        }

        public void Update()
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0.0f)
                Subject.SwitchToTowardsWaterState();
        }

        private void Tapped()
        {
            Subject.SwitchToTowardsWaterState();
        }
    }
}