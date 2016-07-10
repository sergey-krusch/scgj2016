using Configuration;
using UnityEngine;

namespace Animal
{
    public class DrinkState: State
    {
        private float drinkSoundRemainingTime;
        private bool drinkingGood;

        public override void Initialize(Subject subject)
        {
            base.Initialize(subject);
            subject.TappedEvent += Tapped;
            drinkingGood = true;
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
            Subject.TappedEvent -= Tapped;
        }

        public void Update()
        {
            HandleSound();
            var cfg = Root.Instance.Animal;
            var twa = cfg.ConsumingSpeed * Time.deltaTime;
            var awa = Subject.WaterTank.Consume(twa);
            drinkingGood = awa >= twa - Mathf.Epsilon;
            var tga = cfg.GrowingSpeed * Time.deltaTime;
            var aga = tga * awa / twa;
            Subject.Grow(aga);
        }

        private void HandleSound()
        {
            drinkSoundRemainingTime -= Time.deltaTime;
            if (drinkSoundRemainingTime < 0.0f)
            {
                AudioPlayer.Drink(drinkingGood);
                drinkSoundRemainingTime = Root.Instance.Audio.DrinkLoopLength;
            }
        }

        private void Tapped()
        {
            Subject.SwitchToDragState();
        }
    }
}