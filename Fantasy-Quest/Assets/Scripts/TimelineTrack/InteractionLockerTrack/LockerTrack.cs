using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TimelineTrack.InteractionLockerTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/InteractionLockerTrack/TimelineTrack.InteractionLockerTrack"
    )]
    [TrackColor(0.88f, 0.45f, 0.12f)]
    [TrackClipType(typeof(LockerClip))]
    internal class LockerTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(
            PlayableGraph graph,
            GameObject go,
            int inputCount
        )
        {
            return ScriptPlayable<LockerTrackMixer>.Create(graph, inputCount);
        }
    }
}
