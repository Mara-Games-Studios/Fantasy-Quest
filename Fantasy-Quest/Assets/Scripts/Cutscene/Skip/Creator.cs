using UnityEngine;

namespace Cutscene.Skip
{
    [AddComponentMenu("Scripts/Cutscene/Skip/Cutscene.Skip.Creator")]
    internal class Creator : MonoBehaviour
    {
        [SerializeField]
        private Panel panel;

        public Panel Create()
        {
            return Instantiate(panel, transform);
        }
    }
}
