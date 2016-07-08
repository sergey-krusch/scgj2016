using UnityEngine;

namespace Animal
{
    public class Visual: MonoBehaviour
    {
        public GameObject EmptyState;
        public GameObject NormalState;
        public GameObject FullState;

        public float EmptyNormalBound;
        public float NormalFullBound;

        public float Value;

        public void Update()
        {
            if (Value < EmptyNormalBound)
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
            go.SetActive(true);
        }
    }
}