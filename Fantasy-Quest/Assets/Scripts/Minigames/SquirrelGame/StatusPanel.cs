using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.StatusPanel")]
    internal class StatusPanel : MonoBehaviour
    {
        public enum State
        {
            Win,
            Lose,
            NotFinished
        }

        [Serializable]
        private struct LabelByState
        {
            public State State;
            public string Name;
        }

        [SerializeField]
        private Manager manager;

        [SerializeField]
        private List<LabelByState> labels;
        private Dictionary<State, string> Labels => labels.ToDictionary(x => x.State, x => x.Name);

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private TMP_Text label;

        public void ShowPanel(State state)
        {
            label.text = Labels[state];
            animator.enabled = true;
        }

        public void ShowEndCallback()
        {
            manager.ExitMinigame();
        }
    }
}
