using Configuration;
using UnityEngine;

namespace Digger
{
    public class WalkState : State
    {
        public override void Initialize(Subject subject)
        {
            base.Initialize(subject);
            Subject.Visual.Walk();
            subject.TappedEvent += Tapped;
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
            Subject.TappedEvent -= Tapped;
        }

        public void Update()
        {
            var cfg = Root.Instance.Digger;
            var p = Subject.transform.position;
            p.x += Subject.Direction * cfg.Speed * Time.deltaTime;
            if (p.x >= Subject.MaxX)
            {
                p.x = Subject.MaxX;
                Subject.Direction *= -1.0f;
            }
            else if (p.x <= Subject.MinX)
            {
                p.x = Subject.MinX;
                Subject.Direction *= -1.0f;
            }
            Subject.transform.position = p;
        }

        private void Tapped()
        {
            Subject.SwitchToDigState();
        }
    }
}