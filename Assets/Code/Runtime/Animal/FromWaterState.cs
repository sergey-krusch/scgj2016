using Configuration;
using UnityEngine;

namespace Animal
{
    public class FromWaterState : State
    {
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
    }
}