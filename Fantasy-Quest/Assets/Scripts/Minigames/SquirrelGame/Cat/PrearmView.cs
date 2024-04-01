using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.SquirrelGame
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Scripts/Minigames/SquirrelGame/Cat/Minigames.SquirrelGame.Cat.PrearmView")]
    public class PrearmView : MonoBehaviour, ISquirrelTouchable
    {
        [Required]
        [SerializeField] 
        private SpriteRenderer view;

        [Range(0, 1)]
        [SerializeField] 
        private float minFadeValue;
        
        [Range(0, 1)]
        [SerializeField] 
        private float maxFadeValue;
        
        [SerializeField] 
        private float fadeTime;

        private Tween fadeTween;
        private int triggersInteractingAmount;

        private void Start()
        {
            view.DOFade(maxFadeValue, 0);
        }

        public void Touch()
        {
            if (triggersInteractingAmount == 0)
            {
                fadeTween?.Kill();
                fadeTween = view.DOFade(minFadeValue, fadeTime);
            }

            triggersInteractingAmount++;
        }

        public void UnTouch()
        {
            if (triggersInteractingAmount == 1)
            {
                fadeTween?.Kill();
                fadeTween = view.DOFade(maxFadeValue, fadeTime);
            }

            triggersInteractingAmount--;
        }
    }
}
