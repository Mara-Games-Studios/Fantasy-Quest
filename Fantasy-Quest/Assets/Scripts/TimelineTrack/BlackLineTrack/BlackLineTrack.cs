using Cutscene;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TimelineTrack.BlackLineTrack
{
    [AddComponentMenu("Scripts/TimelineTrack/BlackLineTrack/TimelineTrack.BlackLineTrack")]
    [TrackColor(0.11f, 0.11f, 0.11f)]
    [TrackClipType(typeof(BlackLineClip))]
    [TrackBindingType(typeof(BlackLinesShower))]
    internal class BlackLineTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(
            PlayableGraph graph,
            GameObject go,
            int inputCount
        )
        {
            return ScriptPlayable<BlackLineTrackMixer>.Create(graph, inputCount);
        }
    }
}
