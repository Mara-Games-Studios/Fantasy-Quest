using System;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
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

        [Scene]
        [SerializeField]
        private string sceneToLoad;
        public string SceneToLoad => sceneToLoad;

        public void SetSceneToLoad(string sceneToLoad)
        {
            this.sceneToLoad = sceneToLoad;
        }

        [Button]
        public void ResetToDefault()
        {
            sceneToLoad = defaultConfig.SceneToLoad;
        }
    }
}
