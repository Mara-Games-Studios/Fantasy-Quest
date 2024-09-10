using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DI.Project.Services
{
    [AddComponentMenu("Scripts/DI/Project/Services/DI.Project.Services.DoTweenManagerCleaner")]
    internal class DoTweenManagerCleaner : MonoBehaviour
    {
        [SerializeField]
        private DoTweenCleanerObject cleanObject;

        public void Awake()
        {
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            CreateCleanupObject();
        }

        private void SceneManager_activeSceneChanged(
            UnityEngine.SceneManagement.Scene from,
            UnityEngine.SceneManagement.Scene to
        )
        {
            CreateCleanupObject();
        }

        private void CreateCleanupObject()
        {
            _ = Object.Instantiate(cleanObject);
        }
    }
}
