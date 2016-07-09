using Configuration;
using UnityEngine;

namespace Animal
{
    public class TowardsWaterState: State
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
            p.x += cfg.TowardsWaterSpeed * Time.deltaTime;
            if (p.x >= Subject.WaterX)
            {
                p.x = Subject.WaterX;
                Subject.SwitchToDrinkState();
            }
            Subject.transform.position = p;
        }

        private void Tapped()
        {
            Subject.SwitchToDragState();
        }
    }
}