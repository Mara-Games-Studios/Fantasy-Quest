using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelQuests
{
    [AddComponentMenu("Scripts/LevelQuests/LevelQuests.Manager")]
    internal class Manager : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Quest mainQuest;

        [Required]
        [SerializeField]
        private List<Quest> secondaryQuests;

        [Button]
        public void UnlockSecondaryQuests()
        {
            secondaryQuests.ForEach(quest => quest.UnlockCutscene());
        }

        public void UnlockMainMinigamePreviev()
        {
            mainQuest.UnlockMinigame();
            //after the first cutscene
        }
        public void UnlockMainMinigamePlay()
        {
            //after all the secondary quests are done
            if (!secondaryQuests.Any(quest => quest.IsCompleted == true))
            {
                Debug.Log("unlocked minigame");
            }
        }

    }
}
