using UnityEngine;

namespace Digger
{
    public class DigState : State
    {
        private float remainingTime;

        public override void Initialize(Subject subject)
        {
            base.Initialize(subject);
            remainingTime = subject.DigTime;
            Subject.Visual.Dig();
        }

        public void Update()
        {
            Subject.WaterTank.Produce(Subject.ProducingRate * Time.deltaTime);
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0.0f)
                Subject.SwitchToWalkState();
        }
    }
}