using DG.Tweening;
using UnityEngine;

namespace Minigames.SquirrelGame
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Minigames.SquirrelGame.PrearmView")]
    public class PrearmView : MonoBehaviour, ISquirrelTouchable
    {
        [SerializeField] 
        private SpriteRenderer view;

        [SerializeField] 
        private float minFadeValue;
        
        [SerializeField] 
        private float maxFadeValue;
        
        [SerializeField] 
        private float fadeTime;

        private Tween fadeTween;

        private void Start()
        {
            view.DOFade(maxFadeValue, 0);
        }

        public void Touch()
        {
            fadeTween?.Kill();
            fadeTween = view.DOFade(minFadeValue, fadeTime);
        }

        public void UnTouch()
        {
            fadeTween?.Kill();
            fadeTween = view.DOFade(maxFadeValue, fadeTime);
        }
    }
}
