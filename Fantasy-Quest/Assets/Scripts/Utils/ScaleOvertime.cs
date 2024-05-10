using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.ScaleOvertime")]
    internal class ScaleOvertime : MonoBehaviour
    {
        [SerializeField]
        private bool fromToMode = false;

        [SerializeField]
        private Transform target;

        [ShowIf(nameof(fromToMode))]
        [SerializeField]
        private Vector3 fromScale;

        [SerializeField]
        private Vector3 toScale;

        [SerializeField]
        private float duration;

        private void ScaleSnapshot()
        {
            if (!fromToMode)
            {
                fromScale = target.localScale;
            }
        }

        public void ChangeScale()
        {
            ScaleSnapshot();
            _ = StartCoroutine(ScaleChangerRoutine());
        }

        private IEnumerator ScaleChangerRoutine()
        {
            float timer = 0;
            while (timer < duration)
            {
                timer += Time.deltaTime;

                target.localScale = Vector3.Lerp(fromScale, toScale, timer / duration);

                yield return null;
            }
        }
    }
}
