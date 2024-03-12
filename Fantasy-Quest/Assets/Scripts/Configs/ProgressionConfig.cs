using Common;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "Progression Config",
        menuName = "Configs/Create Progression Config"
    )]
    internal class ProgressionConfig : SingletonScriptableObject<ProgressionConfig>
    {
        [Scene]
        [SerializeField]
        private string sceneToLoad;
        public string SceneToLoad => sceneToLoad;

        public void SetSceneToLoad(string sceneToLoad)
        {
            this.sceneToLoad = sceneToLoad;
        }
    }
}
