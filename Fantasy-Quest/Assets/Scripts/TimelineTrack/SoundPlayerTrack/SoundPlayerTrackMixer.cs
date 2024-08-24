using Audio;
using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.SoundPlayerTrack
{
    [AddComponentMenu(
        "Scripts/TimelineTrack/SoundPlayerTrack/TimelineTrack.SoundPlayerTrack.SoundPlayerTrackMixer"
    )]
    internal class SoundPlayerTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            SoundPlayer soundPlayer = playerData as SoundPlayer;

            if (!soundPlayer)
            {
                return;
            }
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight > 0f && !soundPlayer.IsClipPLaying)
                {
                    soundPlayer.PlayClip();
                }
                if (inputWeight == 0f && soundPlayer.IsClipPLaying)
                {
                    soundPlayer.StopClip();
                }
            }
        }
    }
}
