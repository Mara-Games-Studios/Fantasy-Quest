using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs.Progression
{
    [AddComponentMenu("Scripts/Configs/Progression/Configs.Progression.ProgressionRestarter")]
    internal class ProgressionRestarter : MonoBehaviour
    {
        private void Start()
        {
            Starter();
        }

        [Button]
        public void Starter()
        {
            ProgressionConfig.Instance.ResetToDefault();
        }
    }
}
