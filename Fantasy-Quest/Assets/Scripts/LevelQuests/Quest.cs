using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelQuests
{
    [AddComponentMenu("Scripts/LevelQuests/LevelQuests.Quest")]
    internal class Quest : MonoBehaviour
    {
        [RequiredIn(PrefabKind.InstanceInScene)]
        [SerializeField]
        private GameObject minigameEnterPoint;

        [RequiredIn(PrefabKind.InstanceInScene)]
        [SerializeField]
        private GameObject cutsceneTriggerPoint;

        [SerializeField]
        private List<GameObject> questInteractablePoints;

        // Used on minigame objects as OnGameFinishedWin callback
        [Button]
        public void LockMinigame()
        {
            minigameEnterPoint.SetActive(false);
            SetActiveInteractivePoints(false);
        }

        // Used in cutscenes which grant player a mission, as a signal callback
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

        private void SetActiveInteractivePoints(bool state)
        {
            questInteractablePoints.ForEach(point => point.SetActive(state));
        }
    }
}
