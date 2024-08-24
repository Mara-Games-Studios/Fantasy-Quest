using Configs;
using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.InteractionLockerTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/InteractionLockerTrack/TimelineTrack.InteractionLockerTrack.LockerTrackMixer"
    )]
    internal class LockerTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                PlayableGraph graph = playable.GetGraph();
                PlayableDirector director = graph.GetResolver() as PlayableDirector;

                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight > 0f)
                {
                    LockerSettings.Instance.LockAll(director);
                }
                else
                {
                    LockerSettings.Instance.UnlockAll(director);
                    //LockerSettings.Instance.SetOwner(null);
                }
            }
        }
    }
}
