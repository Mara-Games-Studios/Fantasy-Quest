using System.Collections.Generic;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.ChickensPlaceChanger"
    )]
    internal class ChickensPlaceChanger : MonoBehaviour
    {
        [SerializeField]
        private List<ChickenBehaviour> chickensForest;

        [SerializeField]
        private List<GameObject> chickensBarn;

        public void ShowChickensRunningFromBarn()
        {
            chickensForest.ForEach(chicken => chicken.gameObject.SetActive(true));
        }

        public void ReturnChickensToBarn()
        {
            chickensBarn.ForEach(chicken => chicken.SetActive(true));
            chickensForest.ForEach(chicken => chicken.gameObject.SetActive(false));
        }
    }
}
