using System.Collections.Generic;
using System.Linq;
using Common.DI;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using VContainer;

namespace Cutscene
{
    [AddComponentMenu("Scripts/Cutscene/Cutscene.Manager")]
    internal class Manager : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

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
            if (
                playableDirectors.Any(x =>
                    (x.time > 0 && x.GetComponent<UnlockZone>() == null)
                    || (x.time > 0 && !x.GetComponent<UnlockZone>().IsInRange(x.time))
                )
            ) { }
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
