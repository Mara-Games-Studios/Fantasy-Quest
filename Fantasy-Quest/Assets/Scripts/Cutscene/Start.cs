using UnityEngine;
using UnityEngine.Playables;

namespace Cutscene
{
    [AddComponentMenu("Scripts/Cutscene/Cutscene.Start")]
    internal class Start : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector playableDirector;

        public void StartCutscene()
        {
            if (playableDirector.state == PlayState.Playing)
            {
                Debug.Log(
                    $"Try start cutscene while it playing - {playableDirector.gameObject.name}"
                );
                return;
            }

            playableDirector.Play();
        }
    }
}
