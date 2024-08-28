using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.BlackLineTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/BlackLineTrack/TimelineTrack.BlackLineTrack.BlackLineClip"
    )]
    [System.Serializable]
    internal class BlackLineBehaviour : PlayableBehaviour
    {
        public bool IsLinesShowing = false;
    }

    internal class BlackLineClip : PlayableAsset
    {
        public BlackLineBehaviour LineProperties;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<BlackLineBehaviour> playable = ScriptPlayable<BlackLineBehaviour>.Create(
                graph,
                LineProperties
            );
            return playable;
        }
    }
}
