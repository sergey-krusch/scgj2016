using Configuration;
using UnityEngine;

namespace Animal
{
    public class DropState : State
    {
        private float velocity;
        private bool finished;

        public void Awake()
        {
            velocity = 0.0f;
        }

        public void FixedUpdate()
        {
            if (finished)
                return;
            var g = Root.Instance.Animal.Gravity;
            velocity += g * Time.fixedDeltaTime;
            var p = Subject.transform.localPosition;
            p.y -= velocity;
            if (p.y <= 0.0f)
            {
                if (velocity > Root.Instance.Audio.DropSoundVelocity)
                    AudioPlayer.Drop();
                finished = true;
                p.y = 0.0f;
                GoToNextState();
            }
            Subject.transform.localPosition = p;
        }

        protected virtual void GoToNextState()
        {
            Subject.SwitchToTowardsWaterState();
        }
    }
}