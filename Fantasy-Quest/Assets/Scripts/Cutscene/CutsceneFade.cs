using Sirenix.OdinInspector;
using UnityEngine;

namespace Cutscene
{
    [AddComponentMenu("Scripts/Cutscene/Cutscene.CutsceneFade")]
    internal class CutsceneFade : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        [ReadOnly]
        private Skip skip;

        public void FadeInEndCallback()
        {
            skip.FadeInEndCallback();
        }

        public void FadeOut()
        {
            animator.Play("BlackOut");
        }

        public void SetSkipScript(Skip parentSkip)
        {
            skip = parentSkip;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
