using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Cutscene
{
    [AddComponentMenu("Scripts/Cutscene/Cutscene.Start")]
    internal class Start : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector playableDirector;

        [Button]
        public void StartCutscene()
        {
            playableDirector.Play();
        }
    }
}
