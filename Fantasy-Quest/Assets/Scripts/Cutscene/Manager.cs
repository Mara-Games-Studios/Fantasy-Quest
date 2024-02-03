using System.Collections.Generic;
using System.Linq;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Cutscene
{
    [AddComponentMenu("Scripts/Cutscene/Cutscene.Manager")]
    internal class Manager : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private List<PlayableDirector> playableDirectors;

        public void Pause()
        {
            foreach (PlayableDirector director in playableDirectors)
            {
                director.Pause();
            }
        }

        public void LockFromSettings()
        {
            if (playableDirectors.Any(x => x.state == PlayState.Paused))
            {
                LockerSettings.Instance.LockAll();
            }
        }

        public void Resume()
        {
            foreach (PlayableDirector director in playableDirectors)
            {
                director.Resume();
            }
        }

        [Button]
        private void CatchAllDirectors()
        {
            playableDirectors = FindObjectsOfType<PlayableDirector>().ToList();
        }
    }
}
