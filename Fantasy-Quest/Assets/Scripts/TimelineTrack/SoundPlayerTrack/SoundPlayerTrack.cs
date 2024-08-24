using Audio;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TimelineTrack.SoundPlayerTrack
{
    [AddComponentMenu("Scripts/TimelineTrack/SoundPlayerTrack/TimelineTrack.SoundPlayerTrack")]
    [TrackColor(0.88f, 0.11f, 0.73f)]
    [TrackClipType(typeof(SoundPlayerClip))]
    [TrackBindingType(typeof(SoundPlayer))]
    internal class SoundPlayerTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(
            PlayableGraph graph,
            GameObject go,
            int inputCount
        )
        {
            return ScriptPlayable<SoundPlayerTrackMixer>.Create(graph, inputCount);
        }
    }
}
