using UnityEngine;

namespace Animal
{
    public class Visual: MonoBehaviour
    {
        public GameObject EmptyState;
        public GameObject NormalState;
        public GameObject FullState;
        public GameObject DeadState;

        public float EmptyNormalBound;
        public float NormalFullBound;

        public bool Dead;
        public float Value;

        public void Update()
        {
            if (Dead)
                SwitchTo(DeadState);
            else if (Value < EmptyNormalBound)
                SwitchTo(EmptyState);
            else if (Value < NormalFullBound)
                SwitchTo(NormalState);
            else
                SwitchTo(FullState);
        }

        private void SwitchTo(GameObject go)
        {
            EmptyState.SetActive(false);
            NormalState.SetActive(false);
            FullState.SetActive(false);
            DeadState.SetActive(false);
            go.SetActive(true);
        }
    }
}