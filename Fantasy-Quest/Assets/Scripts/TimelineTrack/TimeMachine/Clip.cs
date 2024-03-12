using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace TimelineTrack.TimeMachine
{
    [Serializable]
    public class Clip : PlayableAsset
    {
        private Behaviour template = new();

        [Title("Marker Label")]
        [SerializeField]
        private string markerLabel = "MarkerName";
        public string MarkerLabel => markerLabel;

        [Title("Action Type")]
        [SerializeField]
        private Action action;
        public Action Action => action;

        [SerializeField]
        [ShowIf(nameof(action), Action.JumpToMarker)]
        private string markerToJumpTo = "";
        public string MarkerToJumpTo => markerToJumpTo;

        [SerializeField]
        [ShowIf(nameof(action), Action.JumpToTime)]
        private float timeToJumpTo = 0f;
        public float TimeToJumpTo => timeToJumpTo;

        [Title("Condition")]
        [SerializeField]
        private ConditionType condition;
        public ConditionType ConditionType => condition;

        [SerializeField]
        [ValidateInput("$" + nameof(ValidateList))]
        [ShowIf(nameof(condition), ConditionType.Custom)]
        private List<GameObject> referenceCondition = new();
        public IEnumerable<IConditionSource> Conditions =>
            referenceCondition.Select(x => x.GetComponent<IConditionSource>());

        private bool ValidateList(List<GameObject> gameObjects)
        {
            return gameObjects.All(x => x.TryGetComponent(out IConditionSource _));
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<Behaviour> playable = ScriptPlayable<Behaviour>.Create(graph, template);
            Behaviour behaviour = playable.GetBehaviour();
            behaviour.AssignValuesFromClip(this);
            return playable;
        }
    }
}
