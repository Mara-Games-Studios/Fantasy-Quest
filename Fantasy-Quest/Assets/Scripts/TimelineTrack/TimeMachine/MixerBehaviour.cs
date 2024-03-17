using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.TimeMachine
{
    public class MixerBehaviour : PlayableBehaviour
    {
        public Dictionary<string, double> MarkerClips;
        private PlayableDirector director;

        public override void OnPlayableCreate(Playable playable)
        {
            director = playable.GetGraph().GetResolver() as PlayableDirector;
        }

        private struct WeightedBehaviour
        {
            public Behaviour Behaviour;
            public float Weight;
        }

        private IEnumerable<WeightedBehaviour> GetWeightedBehaviors(Playable playable)
        {
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<Behaviour> inputPlayable =
                    (ScriptPlayable<Behaviour>)playable.GetInput(i);
                Behaviour behaviour = inputPlayable.GetBehaviour();
                yield return new() { Behaviour = behaviour, Weight = inputWeight };
            }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (!Application.isPlaying)
            {
                return;
            }

            IEnumerable<Behaviour> notExecutedBehaviors = GetWeightedBehaviors(playable)
                .Where(x => x.Weight > 0 && !x.Behaviour.IsClipExecuted)
                .Select(x => x.Behaviour);

            foreach (Behaviour behaviour in notExecutedBehaviors)
            {
                switch (behaviour.Action)
                {
                    case Action.Pause:
                        if (behaviour.ApplyCondition())
                        {
                            director.Pause();
                            behaviour.SetIsClipExecuted(true);
                        }
                        break;

                    case Action.JumpToTime:
                    case Action.JumpToMarker:
                        if (behaviour.ApplyCondition())
                        {
                            PlayableGraph graph = playable.GetGraph();
                            PlayableDirector director = graph.GetResolver() as PlayableDirector;

                            director.time =
                                behaviour.Action == Action.JumpToTime
                                    ? behaviour.TimeToJumpTo
                                    : MarkerClips[behaviour.MarkerToJumpTo];

                            behaviour.SetIsClipExecuted(false);
                        }
                        break;
                }
            }
        }
    }
}
