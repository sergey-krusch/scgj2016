using Configuration;
using UnityEngine;

namespace Animal
{
    public class Visual: MonoBehaviour
    {
		public GameObject AliveState;
        public GameObject DeadState;
        public SkinnedMeshRenderer MeshRenderer;

        public bool Dead;
        public float Value;

        public void Awake()
        {
            var cfg = Root.Instance.Animal;
            var i = Random.Range(0, cfg.Colors.Length);
            var c = cfg.Colors[i];
            SetColor(AliveState, c);
            SetColor(DeadState, c);
        }

        public void Update()
        {
			if (Dead)
				SwitchTo(DeadState);
			else
				SwitchTo(AliveState);
            if (!Dead)
            {
                MeshRenderer.SetBlendShapeWeight(0, Value * 100f);
                MeshRenderer.SetBlendShapeWeight(1, (1f - Value) * 100f);
            }
        }

        private void SwitchTo(GameObject go)
        {
            AliveState.SetActive(false);
            DeadState.SetActive(false);
            go.SetActive(true);
        }

        private void SetColor(GameObject o, Color c)
        {
        }
    }
}