using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.SoundPlayerTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/SoundPlayerTrack/TimelineTrack.SoundPlayerTrack.SoundPlayerClip"
    )]
    [System.Serializable]
    internal class SoundPlayerBehaviour : PlayableBehaviour
    {
        public bool IsSoundPlaying = false;
    }

    internal class SoundPlayerClip : PlayableAsset
    {
        public SoundPlayerBehaviour SoundProperties;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<SoundPlayerBehaviour> playable =
                ScriptPlayable<SoundPlayerBehaviour>.Create(graph, SoundProperties);
            return playable;
        }
    }
}
