using Common;
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

        public void PlayGame()
        {
            endTransition.LoadScene(nextScene, 3);
        }
    }
}
