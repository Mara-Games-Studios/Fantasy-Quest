using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.AuthorsPage
{
    [AddComponentMenu("Scripts/UI/AuthorsPage/UI.AuthorsPage.Mover")]
    public class Mover : MonoBehaviour
    {
        [Required]
        [SerializeField] 
        private RectTransform movedElement;
        
        [Required]
        [SerializeField] 
        private RectTransform startPoint;

        [Required]        
        [SerializeField] 
        private RectTransform endPoint;

        [SerializeField] 
        private float movementTime = 15f;

        [SerializeField] 
        private Ease movementEase = Ease.Linear;

        [SerializeField] 
        private Escaper escaper;

        [SerializeField] 
        private Pages.View pageParent;

        private Tween movementTween;
        
        private void OnEnable()
        {
            Pages.View.OnPageShowed += TryMove;
        }

        private void TryMove(Pages.View view)
        {
            if (view.Equals(pageParent))
            {
                Move();
            }
        }
        
        private void Move()
        {
            Reset();
            movementTween = movedElement.DOMove(endPoint.position, movementTime).SetEase(movementEase);
            movementTween.onComplete += () =>
            {
                Reset(); 
                escaper.Exit();
            };
        }

        private void Reset()
        {
            movementTween?.Kill();
            movedElement.DOMove(startPoint.position, 0);
        }
        
        
        private void OnDisable()
        {
            Pages.View.OnPageShowed -= TryMove;
            Reset();
        }
    }
}
