using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.InteractionLockerTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/InteractionLockerTrack/TimelineTrack.InteractionLockerTrack.LockerBehavior"
    )]
    [System.Serializable]
    internal class LockerBehaviour : PlayableBehaviour
    {
        public bool IsLocked = false;
        public PlayableDirector LockOwner = null;
    }
}
