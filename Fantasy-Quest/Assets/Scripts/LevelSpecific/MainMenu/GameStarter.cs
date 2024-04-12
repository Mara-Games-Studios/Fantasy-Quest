using Configs.Progression;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelSpecific.MainMenu
{
    [AddComponentMenu("Scripts/LevelSpecific/MainMenu/LevelSpecific.MainMenu.GameStarter")]
    internal class GameStarter : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Transition.End.Controller endController;

        public void StartGame()
        {
            if (ProgressionConfig.Instance.IsGamePassed)
            {
                ProgressionConfig.Instance.ResetToDefault();
            }
            endController.LoadScene(ProgressionConfig.Instance.SceneToLoad);
        }
    }
}
