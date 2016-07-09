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
        public event Action<string> DiedEvent;

        public void Start()
        {
            SwitchToTowardsWaterState();
        }

        public void Update()
        {
            var cfg = Root.Instance.Animal;
            Value -= cfg.ShrinkingSpeed * Time.deltaTime;
            if (Value < 0.0f)
                ActionInvoker.Invoke(DiedEvent, "Exhausted =(");
            Visual.Value = Value;
        }

        public void OnMouseDown()
        {
            ActionInvoker.Invoke(TappedEvent);
        }

        public void Grow(float amount)
        {
            Value += amount;
            if (Value > 1.0f)
                ActionInvoker.Invoke(DiedEvent, "Exploded :D");
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

        private void DeinitializeCurrentState()
        {
            var cs = GetComponent<State>();
            if (cs == null)
                return;
            cs.Deinitialize();
        }
    }
}