using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TimelineTrack.TimeMachine
{
    [TrackColor(0.74f, 0.33f, 0.85f)]
    [TrackClipType(typeof(Clip))]
    public class Track : TrackAsset
    {
        public override Playable CreateTrackMixer(
            PlayableGraph graph,
            GameObject go,
            int inputCount
        )
        {
            ScriptPlayable<MixerBehaviour> scriptPlayable = ScriptPlayable<MixerBehaviour>.Create(
                graph,
                inputCount
            );
            MixerBehaviour mixerBehaviour = scriptPlayable.GetBehaviour();
            mixerBehaviour.MarkerClips = new Dictionary<string, double>();

            foreach (TimelineClip timeLineClip in GetClips())
            {
                Clip timeMachineClip = timeLineClip.asset as Clip;

                timeLineClip.displayName = timeMachineClip.Action switch
                {
                    Action.Marker => "● " + timeMachineClip.MarkerLabel.ToString(),
                    Action.JumpToTime => "↩ " + timeMachineClip.TimeToJumpTo.ToString(),
                    Action.JumpToMarker => "↩︎  " + timeMachineClip.MarkerToJumpTo.ToString(),
                    Action.Pause => "||",
                    _ => throw new System.ArgumentException(),
                };

                if (timeMachineClip.Action == Action.Marker)
                {
                    if (!mixerBehaviour.MarkerClips.ContainsKey(timeMachineClip.MarkerLabel))
                    {
                        mixerBehaviour.MarkerClips.Add(
                            timeMachineClip.MarkerLabel,
                            (double)timeLineClip.start
                        );
                    }
                }
            }

            return scriptPlayable;
        }
    }
}
