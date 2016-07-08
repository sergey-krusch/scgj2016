using UnityEngine;

namespace Digger
{
    public class Visual: MonoBehaviour
    {
        public GameObject NormalState;
        public GameObject DiggingState;

        public void Dig()
        {
            SwitchTo(DiggingState);
        }

        public void Walk()
        {
            SwitchTo(NormalState);
        }

        private void SwitchTo(GameObject go)
        {
            NormalState.SetActive(false);
            DiggingState.SetActive(false);
            go.SetActive(true);
        }
    }
}