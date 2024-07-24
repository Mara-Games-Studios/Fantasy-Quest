using System.Collections.Generic;
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

        // Methods are used on scene, in object LevelQuestManager as a signal callback

        [Button]
        public void UnlockSecondaryQuests()
        {
            secondaryQuests.ForEach(quest => quest.UnlockCutscene());
        }

        [Button]
        public void LockSecondaryQuests()
        {
            secondaryQuests.ForEach(quest => quest.LockCutscene());
        }

        public void UnlockMainMinigame()
        {
            mainQuest.UnlockMinigame();
        }
    }
}
