using Configs.Progression;
using UnityEngine;

namespace StartScene
{
    [AddComponentMenu("Scripts/Scene/Start/Scene.Start.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private Transition.End.Controller endTransition;

        [SerializeField]
        private Panel.Settings.Controller panelSettings;

        // Must be called by view (button) callback
        public void PlayGame()
        {
            string nextScene = ProgressionConfig.Instance.SceneToLoad;
            endTransition.LoadScene(nextScene);
        }

        // Must be called by view (button) callback
        public void OpenSettings()
        {
            panelSettings.ShowSettings();
        }

        // Must be called by view (button) callback
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
