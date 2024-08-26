using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs.Progression
{
    [AddComponentMenu("Scripts/Configs/Progression/Configs.Progression.ProgressionRestarter")]
    internal class ProgressionRestarter : MonoBehaviour
    {
        private void Start()
        {
            ResetProgress();
        }

        [Button]
        public void ResetProgress()
        {
            ProgressionConfig.Instance.ResetToDefault();
        }
    }
}
