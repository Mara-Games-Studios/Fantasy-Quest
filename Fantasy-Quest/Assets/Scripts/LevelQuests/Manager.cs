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

        [Button]
        public void UnlockSecondaryQuests()
        {
            secondaryQuests.ForEach(quest => quest.UnlockCutscene());
        }

        public void UnlockMainMinigamePreviev()
        {
            mainQuest.UnlockMinigame();
        }
    }
}
