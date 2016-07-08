using UnityEngine;

namespace Animal
{
    public class State: MonoBehaviour
    {
        public Subject Subject;

        public virtual void Initialize(Subject subject)
        {
            Subject = subject;
        }

        public virtual void Deinitialize()
        {
            Destroy(this);
        }
    }
}