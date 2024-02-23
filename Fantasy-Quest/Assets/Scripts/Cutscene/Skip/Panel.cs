using Sirenix.OdinInspector;
using UnityEngine;

namespace Cutscene.Skip
{
    [AddComponentMenu("Scripts/Cutscene/Skip/Cutscene.Skip.Panel")]
    internal class Panel : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        [ReadOnly]
        private Control skip;

        public void FadeInEndCallback()
        {
            skip.FadeInEndCallback();
        }

        public void FadeOut()
        {
            animator.Play("BlackOut");
        }

        public void SetSkipScript(Control parentSkip)
        {
            skip = parentSkip;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
