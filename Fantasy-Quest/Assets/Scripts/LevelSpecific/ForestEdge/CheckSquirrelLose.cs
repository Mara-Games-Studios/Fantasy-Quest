using System.Collections;
using Cutscene;
using UnityEngine;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu(
        "Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.CheckSquirrelLose"
    )]
    internal class CheckSquirrelLose : MonoBehaviour
    {
        [SerializeField]
        private bool canStart = false;

        [SerializeField]
        private Start fallDownCutscene;

        [SerializeField]
        private float offsetDuration;

        public void SetCanStart(bool canStart)
        {
            this.canStart = canStart;
        }

        public void StartCutscene()
        {
            _ = StartCoroutine(StartCutsceneRoutine());
        }

        private IEnumerator StartCutsceneRoutine()
        {
            yield return new WaitForSeconds(offsetDuration);
            if (canStart)
            {
                fallDownCutscene.StartCutscene();
                canStart = false;
            }
        }
    }
}
