using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.SquirrelGame.Screamer
{
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Screamer.SquirrelShower")]
    public class SquirrelShower : MonoBehaviour
    {
        [SerializeField]
        private RectTransform squirrelTransform;

        [SerializeField]
        private float minScale = 0.4f;

        [SerializeField]
        private float maxScale = 1f;

        [SerializeField]
        private Ease ease = Ease.OutSine;

        [SerializeField]
        private float scaleDuration = 1f;

        private Tween scaleTween;

        private void Awake()
        {
            squirrelTransform.localScale = new Vector3(minScale, minScale, 1f);
            squirrelTransform.gameObject.SetActive(false);
        }

        [Button]
        public void Show()
        {
            if (squirrelTransform.localScale.x == maxScale)
            {
                return;
            }

            scaleTween?.Kill();

            squirrelTransform.gameObject.SetActive(true);

            scaleTween = squirrelTransform
                .DOScale(new Vector3(maxScale, maxScale, 1f), scaleDuration)
                .SetEase(ease);
        }

        [Button]
        public void Hide()
        {
            if (squirrelTransform.localScale.x == minScale)
            {
                return;
            }

            scaleTween?.Kill();

            _ = squirrelTransform
                .DOScale(new Vector3(minScale, minScale, 1f), scaleDuration)
                .SetEase(ease)
                .OnComplete(() => squirrelTransform.gameObject.SetActive(false));
        }
    }
}
