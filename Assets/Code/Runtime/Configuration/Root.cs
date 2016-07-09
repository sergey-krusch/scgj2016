﻿using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu]
    public class Root : ScriptableObject
    {
        public const string ResourcePath = "DefaultSettings";

        public AnimalConfig Animal;
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