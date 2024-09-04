using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace DI.Project.Bootstrap
{
    internal class SpineSkeletonMaterialRefresher : IInitializable
    {
        [Preserve]
        public SpineSkeletonMaterialRefresher() { }

        public void Initialize()
        {
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            ResetMaterialsAlfa();
        }

        private void SceneManager_activeSceneChanged(
            UnityEngine.SceneManagement.Scene from,
            UnityEngine.SceneManagement.Scene to
        )
        {
            ResetMaterialsAlfa();
        }

        private void ResetMaterialsAlfa()
        {
            SpineSkeletonMaterialLinker[] linkers =
                Object.FindObjectsOfType<SpineSkeletonMaterialLinker>();
            foreach (SpineSkeletonMaterialLinker linker in linkers)
            {
                linker.Material.color = Color.white;
            }
        }
    }
}
