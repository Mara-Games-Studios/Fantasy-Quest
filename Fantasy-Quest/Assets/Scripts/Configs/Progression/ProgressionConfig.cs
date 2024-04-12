using System;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs.Progression
{
    [CreateAssetMenu(
        fileName = "Progression Config",
        menuName = "Configs/Create Progression Config"
    )]
    internal class ProgressionConfig : SingletonScriptableObject<ProgressionConfig>
    {
        [Serializable]
        private struct DefaultConfig
        {
            [Scene]
            [SerializeField]
            public string SceneToLoad;
        }

        [SerializeField]
        private DefaultConfig defaultConfig;

        [SerializeField]
        private bool isGamePassed = false;
        public bool IsGamePassed => isGamePassed;

        public void SetGamePassed()
        {
            isGamePassed = true;
        }

        [Scene]
        [SerializeField]
        private string sceneToLoad;
        public string SceneToLoad => sceneToLoad;

        [SerializeField]
        private ForestEdgeLevel forestEdgeLevel;

        public ForestEdgeLevel ForestEdgeLevel
        {
            get => forestEdgeLevel;
            set => forestEdgeLevel = value;
        }

        public void SetSceneToLoad(string sceneToLoad)
        {
            this.sceneToLoad = sceneToLoad;
        }

        [Button]
        public void ResetToDefault()
        {
            isGamePassed = false;
            sceneToLoad = defaultConfig.SceneToLoad;
            forestEdgeLevel = new();
        }
    }
}
