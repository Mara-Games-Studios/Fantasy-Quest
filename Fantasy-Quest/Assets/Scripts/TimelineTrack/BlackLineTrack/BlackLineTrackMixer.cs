using Cutscene;
using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.BlackLineTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/BlackLineTrack/TimelineTrack.BlackLineTrack.BlackLineTrackMixer"
    )]
    internal class BlackLineTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            BlackLinesShower blackLines = playerData as BlackLinesShower;

            if (!blackLines)
            {
                return;
            }
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<BlackLineBehaviour> inputPlayable =
                    (ScriptPlayable<BlackLineBehaviour>)playable.GetInput(i);
                BlackLineBehaviour input = inputPlayable.GetBehaviour();
                if (inputWeight > 0f && !input.IsLinesShowing)
                {
                    blackLines.Show();
                    input.IsLinesShowing = true;
                }
                else if (inputWeight == 0f)
                {
                    blackLines.Hide();
                    input.IsLinesShowing = false;
                }
            }
        }
    }
}
