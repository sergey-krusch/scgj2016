using System;
using UnityEngine;

namespace Digger
{
    public class Subject: MonoBehaviour
    {
        public float MinX;
        public float MaxX;
        public float Speed;
        public float DigTime;
        public float ProducingRate;

        public Visual Visual;
        public WaterTank WaterTank;

        public float Direction;

        public event Action TappedEvent;

        public void Awake()
        {
            Direction = 1.0f;
        }

        public void Start()
        {
            SwitchToWalkState();
        }

        public void OnMouseDown()
        {
            ActionInvoker.Invoke(TappedEvent);
        }

        public void SwitchToWalkState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<WalkState>().Initialize(this);
        }

        public void SwitchToDigState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<DigState>().Initialize(this);
        }

        private void DeinitializeCurrentState()
        {
            var cs = GetComponent<State>();
            if (cs == null)
                return;
            cs.Deinitialize();
        }
    }
}