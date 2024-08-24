using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TimelineTrack.InteractionLockerTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/InteractionLockerTrack/TimelineTrack.InteractionLockerTrack"
    )]
    [TrackColor(0.74f, 0.33f, 0.85f)]
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
