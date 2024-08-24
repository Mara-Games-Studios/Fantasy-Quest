using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.InteractionLockerTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/InteractionLockerTrack/TimelineTrack.InteractionLockerTrack.LockerAsset"
    )]
    internal class LockerClip : PlayableAsset
    {
        public LockerBehaviour LockerParams;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<LockerBehaviour> playable = ScriptPlayable<LockerBehaviour>.Create(
                graph,
                LockerParams
            );
            // LockerBehaviour lightControlBehaviour = playable.GetBehaviour();

            //lightControlBehaviour.IsLocked = isLocked;
            return playable;
        }
    }
}
