using UnityEngine;

namespace Animal
{
    public class DrinkState: State
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
            var twa = Subject.ConsumingSpeed * Time.deltaTime;
            var awa = Subject.WaterTank.Consume(twa);
            var tga = Subject.GrowingSpeed * Time.deltaTime;
            var aga = tga * awa / twa;
            Subject.Grow(aga);
        }

        private void Tapped()
        {
            Subject.SwitchToFromWaterState();
        }
    }
}