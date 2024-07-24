using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelQuests
{
    [AddComponentMenu("Scripts/LevelQuests/LevelQuests.Quest")]
    internal class Quest : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private bool isCompleted = false;
        public bool IsCompleted => isCompleted;

        [Required]
        [SerializeField]
        private GameObject minigameEnterPoint;

        [SerializeField]
        private GameObject cutsceneTriggerPoint;

        [SerializeField]
        private List<GameObject> questInteractivePoints;

        [Button]
        public void LockMinigame()
        {
            minigameEnterPoint.SetActive(false);
            SetActiveInteractivePoints(false);
        }

        [Button]
        public void UnlockMinigame()
        {
            minigameEnterPoint.SetActive(true);
            SetActiveInteractivePoints(true);
        }

        [Button]
        public void UnlockCutscene()
        {
            cutsceneTriggerPoint.SetActive(true);
        }

        [Button]
        public void LockCutscene()
        {
            cutsceneTriggerPoint.SetActive(false);
        }

        public void OnMinigameComplete()
        {
            LockMinigame();
            isCompleted = true;
            // ring complition to manager
        }

        private void SetActiveInteractivePoints(bool state)
        {
            if (questInteractivePoints.Any())
            {
                questInteractivePoints.ForEach(point => point.SetActive(state));
            }
        }
    }
}
