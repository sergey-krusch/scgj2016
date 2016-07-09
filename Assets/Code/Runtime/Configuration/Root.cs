using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu]
    public class Root : ScriptableObject
    {
        public const string ResourcePath = "DefaultSettings";

        public float LevelTime;
        public float InitialWaterLevel;
        public float AnimalSpawnInterval;
        public AnimalConfig Animal;
        public DiggerConfig Digger;
        public ScoreConfig Score;
        public AudioConfig Audio;
        public bool DeveloperMode;

        private static Root instance;
        public static Root Instance
        {
            get
            {
                if (instance == null)
                    instance = Resources.Load<Root>(ResourcePath);
                return instance;
            }
        }
    }
}