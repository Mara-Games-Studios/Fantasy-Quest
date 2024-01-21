using Common;
using Settings;
using Transitions.End;
using UnityEngine;

namespace StartScene
{
    [AddComponentMenu("Scripts/StartScene/StartScene.Controller")]
    internal class Controller : MonoBehaviour
    {
        [Scene]
        [SerializeField]
        private string nextScene;

        [SerializeField]
        private EndImpl endTransition;

        // Must be called by view (button) callback
        public void PlayGame()
        {
            Debug.Log(TransitionSettings.Instance.MinLoadingDuration);
            endTransition.LoadScene(nextScene, TransitionSettings.Instance.MinLoadingDuration);
        }

        // Must be called by view (button) callback
        public void OpenSettings() { }

        // Must be called by view (button) callback
        public void QuitGame() { }
    }
}
