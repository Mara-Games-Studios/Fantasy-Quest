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

        [Required]
        [SerializeField]
        private UI.Pages.View menuView;

        [Required]
        [SerializeField]
        private UI.Pages.View authorsView;

        [Button]
        public void StartGame()
        {
            ProgressionConfig.Instance.ResetToDefault();
            endController.LoadScene(ProgressionConfig.Instance.SceneToLoad);
        }

        [Button]
        public void InitMainMenu()
        {
            if (ProgressionConfig.Instance.IsGamePassed)
            {
                authorsView.ShowFromStart();
            }
            else
            {
                menuView.ShowFromStart();
            }
        }
    }
}
