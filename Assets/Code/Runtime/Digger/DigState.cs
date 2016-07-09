using Configuration;
using UnityEngine;

namespace Digger
{
    public class DigState : State
    {
        private float remainingTime;

        public override void Initialize(Subject subject)
        {
            base.Initialize(subject);
            remainingTime = Root.Instance.Digger.DigTime;
            Subject.Visual.Dig();
            AudioPlayer.Dig();
        }

        public void Update()
        {
            Subject.WaterTank.Produce(Root.Instance.Digger.ProducingRate * Time.deltaTime);
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0.0f)
                Subject.SwitchToWalkState();
        }
    }
}