using Cutscene;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.EndEdge")]
    internal class EndEdge : MonoBehaviour
    {
        [SerializeField]
        private Start finalCutscene;

        [Button]
        public void EndGame()
        {
            finalCutscene.StartCutscene();
        }
    }
}
