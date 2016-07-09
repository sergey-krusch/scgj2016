using Configuration;
using UnityEngine;

namespace Animal
{
    public class FromWaterState : State
    {
        public override void Initialize(Subject subject)
        {
            base.Initialize(subject);
            subject.TappedEvent += Tapped;
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
            Subject.TappedEvent -= Tapped;
        }

        public void Update()
        {
            var cfg = Root.Instance.Animal;
            var p = Subject.transform.position;
            p.x -= cfg.FromWaterSpeed * Time.deltaTime;
            if (p.x <= Subject.EndX)
            {
                p.x = Subject.EndX;
                Subject.SwitchToWaitState();
            }
            Subject.transform.position = p;
        }

        private void Tapped()
        {
            Subject.SwitchToTowardsWaterState();
        }
    }
}