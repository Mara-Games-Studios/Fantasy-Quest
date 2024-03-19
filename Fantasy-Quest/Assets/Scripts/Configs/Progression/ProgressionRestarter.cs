using UnityEngine;

namespace Configs.Progression
{
    [AddComponentMenu("Scripts/Configs/Progression/Configs.Progression.ProgressionRestarter")]
    internal class ProgressionRestarter : MonoBehaviour
    {
        private void Start()
        {
            ProgressionConfig.Instance.ResetToDefault();
        }
    }
}
