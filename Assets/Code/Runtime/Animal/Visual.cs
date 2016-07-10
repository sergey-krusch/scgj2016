using Configuration;
using UnityEngine;

namespace Animal
{
    public class Visual: MonoBehaviour
    {
		public GameObject AliveState;
        public GameObject DeadState;

        public bool Dead;
        public float Value;
		public SkinnedMeshRenderer MeshRenderer;

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
            var cfg = Root.Instance.Animal;
			if (Dead)
				SwitchTo (DeadState);
			else
			{
				SwitchTo (AliveState);
				MeshRenderer.SetBlendShapeWeight (1, (1f - Value) * 100f);
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