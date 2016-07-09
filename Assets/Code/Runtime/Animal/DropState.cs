using UnityEngine;

namespace Animal
{
    public class DropState : State
    {
        private const float gravity = 6.0f;
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
            velocity += gravity * Time.fixedDeltaTime;
            var p = Subject.transform.localPosition;
            p.y -= velocity;
            if (p.y <= 0.0f)
            {
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