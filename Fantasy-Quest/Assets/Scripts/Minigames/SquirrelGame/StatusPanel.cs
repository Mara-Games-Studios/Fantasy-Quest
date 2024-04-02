using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Minigames.SquirrelGame
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.StatusPanel")]
    internal class StatusPanel : MonoBehaviour
    {
        [Serializable]
        private struct LabelByState
        {
            public ExitGameState State;
            public string Name;
        }

        [Required]
        [SerializeField]
        private Manager manager;

        [SerializeField]
        private float statusPanelShowDuration = 1f;

        [SerializeField]
        private List<LabelByState> labels;
        private Dictionary<ExitGameState, string> Labels =>
            labels.ToDictionary(x => x.State, x => x.Name);

        [Required]
        [SerializeField]
        private GameObject panel;

        [Required]
        [SerializeField]
        private TMP_Text label;

        private Action panelShowed;

        public void HidePanel()
        {
            panel.SetActive(false);
        }

        public void ShowPanel(ExitGameState state, Action panelShowed)
        {
            label.text = Labels[state];
            panel.SetActive(true);
            this.panelShowed = panelShowed;

            // TODO: call from animator
            Invoke(nameof(ShowEndCallback), statusPanelShowDuration);
        }

        public void ShowEndCallback()
        {
            panelShowed?.Invoke();
        }
    }
}
