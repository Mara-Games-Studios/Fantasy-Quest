using Common;
using UnityEngine;

namespace StartScene
{
    [AddComponentMenu("Scripts/Scene/Start/Scene.Start.Controller")]
    internal class Controller : MonoBehaviour
    {
        [Scene]
        [SerializeField]
        private string nextScene;

        [SerializeField]
        private Transition.End.Controller endTransition;

        [SerializeField]
        private Panel.Settings.Controller panelSettings;

        // Must be called by view (button) callback
        public void PlayGame()
        {
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
