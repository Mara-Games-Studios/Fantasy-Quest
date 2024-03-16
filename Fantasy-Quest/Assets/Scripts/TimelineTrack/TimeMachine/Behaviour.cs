using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Playables;

namespace TimelineTrack.TimeMachine
{
    public enum Action
    {
        Marker,
        JumpToTime,
        JumpToMarker,
        Pause,
    }

    public enum ConditionType
    {
        Always,
        Never,
        Custom,
    }

    public interface IConditionSource
    {
        public bool CheckCondition();
    }

    [Serializable]
    public class Behaviour : PlayableBehaviour
    {
        private ConditionType conditionType;
        private List<IConditionSource> conditions;

        private Action action;
        public Action Action => action;

        private string markerToJumpTo;
        public string MarkerToJumpTo => markerToJumpTo;

        private float timeToJumpTo;
        public float TimeToJumpTo => timeToJumpTo;

        private bool isClipExecuted = false;
        public bool IsClipExecuted => isClipExecuted;

        public void SetIsClipExecuted(bool isClipExecuted)
        {
            this.isClipExecuted = isClipExecuted;
        }

        public bool ApplyCondition()
        {
            return conditionType switch
            {
                ConditionType.Always => true,
                ConditionType.Custom => conditions.All(x => x.CheckCondition()),
                _ => false,
            };
        }

        public void AssignValuesFromClip(Clip clip)
        {
            action = clip.Action;
            conditionType = clip.ConditionType;
            conditions = clip.Conditions.ToList();
            markerToJumpTo = clip.MarkerToJumpTo;
        }
    }
}
