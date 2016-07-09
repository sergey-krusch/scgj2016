using Configuration;
using UnityEngine;

namespace Animal
{
    public class DragState : State
    {
        private Vector2 dragStartSubjectPosition;
        private Vector2 dragStartMousePosition;

        public override void Initialize(Subject subject)
        {
            base.Initialize(subject);
            dragStartSubjectPosition = subject.transform.position;
            dragStartMousePosition = GetWorldMousePos();
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
        }

        public void Update()
        {
            var mousePosition = GetWorldMousePos();
            var pos = dragStartSubjectPosition - dragStartMousePosition + mousePosition;
            if (pos.x < Subject.EndX)
                pos.x = Subject.EndX;
            if (pos.x > Subject.WaterX)
                pos.x = Subject.WaterX;
            if (pos.y < 0.0f)
                pos.y = 0.0f;
            Subject.transform.position = pos;
            if (Input.GetMouseButtonUp(0))
                Subject.SwitchToDropState();
        }

        private Vector2 GetWorldMousePos()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}