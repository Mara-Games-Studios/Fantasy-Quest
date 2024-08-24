using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.InteractionLockerTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/InteractionLockerTrack/TimelineTrack.InteractionLockerTrack.LockerAsset"
    )]
    [System.Serializable]
    internal class LockerBehaviour : PlayableBehaviour { }

    internal class LockerClip : PlayableAsset
    {
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<LockerBehaviour> playable = ScriptPlayable<LockerBehaviour>.Create(
                graph
            );
            return playable;
        }
    }
}
