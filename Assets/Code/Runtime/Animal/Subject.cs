using System;
using Configuration;
using UnityEngine;

namespace Animal
{
    public class Subject: MonoBehaviour
    {
        public float EndX;
        public float WaterX;

        public float Value;
        public Visual Visual;
        public WaterTank WaterTank;

        public event Action TappedEvent;

        public void Start()
        {
            SwitchToTowardsWaterState();
        }

        public void Update()
        {
            var cfg = Root.Instance.Animal;
            Value -= cfg.ShrinkingSpeed * Time.deltaTime;
            if (Value < 0.0f)
                SwitchToDeadState();
            Visual.Value = Value;
        }

        public void SignalTapDown()
        {
            ActionInvoker.Invoke(TappedEvent);
        }

        public void Grow(float amount)
        {
            Value += amount;
            if (Value > 1.0f)
                SwitchToDeadState();
        }

        public void SwitchToTowardsWaterState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<TowardsWaterState>().Initialize(this);
        }

        public void SwitchToDrinkState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<DrinkState>().Initialize(this);
        }

        public void SwitchToFromWaterState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<FromWaterState>().Initialize(this);
        }

        public void SwitchToWaitState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<WaitState>().Initialize(this);
        }

        public void SwitchToDragState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<DragState>().Initialize(this);
        }

        public void SwitchToDropState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<DropState>().Initialize(this);
        }

        public void SwitchToDeadState()
        {
            DeinitializeCurrentState();
            gameObject.AddComponent<DeadState>().Initialize(this);
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