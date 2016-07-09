using Configuration;
using UnityEngine;

namespace Animal
{
    public class Visual: MonoBehaviour
    {
        public GameObject EmptyState;
        public GameObject NormalState;
        public GameObject FullState;
        public GameObject DeadState;

        public bool Dead;
        public float Value;

        public void Awake()
        {
            var cfg = Root.Instance.Animal;
            var i = Random.Range(0, cfg.Colors.Length);
            var c = cfg.Colors[i];
            SetColor(EmptyState, c);
            SetColor(NormalState, c);
            SetColor(FullState, c);
            SetColor(DeadState, c);
        }

        public void Update()
        {
            var cfg = Root.Instance.Animal;
            if (Dead)
                SwitchTo(DeadState);
            else if (Value < cfg.EmptyNormalBound)
                SwitchTo(EmptyState);
            else if (Value < cfg.NormalFullBound)
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

        private void SetColor(GameObject o, Color c)
        {
            o.GetComponent<SpriteRenderer>().color = c;
        }
    }
}