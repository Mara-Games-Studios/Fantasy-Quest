using Cutscene;
using Inventory;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.EggOrNotEgg")]
    internal class EggOrNotEgg : MonoBehaviour
    {
        [SerializeField]
        private ItemTaker item;

        [SerializeField]
        private Item egg;

        [SerializeField]
        private Start eggCutscene;

        [SerializeField]
        private Start nonEggCutscene;

        public void StartScene()
        {
            if (item.TakenItem == egg)
            {
                eggCutscene.StartCutscene();
            }
            else
            {
                nonEggCutscene.StartCutscene();
            }
        }
    }
}
