using Common;
using Common.DI;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace DI.Project.Services
{
    [AddComponentMenu("DI/Project/Services/DI.Project.Services.LockerSettingsCleaner")]
    internal class LockerSettingsCleaner : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [Required]
        [SerializeField]
        private LockerSettingsCleanupObject cleanupObject;

        public void Awake()
        {
            lockerSettings.Api.SetToDefault();
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
            LockerSettingsCleanupObject cleanupObjectInstance = Object.Instantiate(cleanupObject);
            cleanupObjectInstance.SetLockerApi(lockerSettings);
        }
    }
}
