using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace UI.Pages
{
    public interface IHorizontalSlider
    {
        public void MoveLeft();
        public void MoveRight();
    }

    [AddComponentMenu("Scripts/UI/Pages/Buttons/UI.Pages.Buttons.HorizontalSlider")]
    public class HorizontalSlider : MonoBehaviour, IHorizontalSlider
    {
        [SerializeField]
        private PivotPoints localPoints;

        [SerializeField]
        private List<CanvasGroup> elementsCanvasGroup;
        public List<CanvasGroup> ElementsCanvasGroup => elementsCanvasGroup;

        [SerializeField]
        private int currentElementIndex;

        [SerializeField]
        private float duration = 1.3f;

        [SerializeField]
        private float minFade = 0.1f;

        [SerializeField]
        private float maxFade = 1f;

        private List<RectTransform> elementsTransform;

        public event Action<int> OnElementIndexChanged;
        public CanvasGroup Current => elementsCanvasGroup[currentElementIndex];

        public int CurrentElementIndex
        {
            get => currentElementIndex;
            set
            {
                if (currentElementIndex != value)
                {
                    OnElementIndexChanged?.Invoke(value);
                }
                currentElementIndex = value;
            }
        }

        private void OnEnable()
        {
            elementsTransform = new List<RectTransform>(
                from element in elementsCanvasGroup
                select element.GetComponent<RectTransform>()
            );
            if (CurrentElementIndex < 0 || CurrentElementIndex >= elementsCanvasGroup.Count)
            {
                CurrentElementIndex = 0;
            }

            foreach (RectTransform element in elementsTransform)
            {
                element.localPosition = localPoints.StartPoint.localPosition;
            }
            elementsTransform[CurrentElementIndex].localPosition = localPoints
                .MiddlePoint
                .localPosition;
            elementsCanvasGroup[CurrentElementIndex].alpha = maxFade;
        }

        public void MoveRight()
        {
            //Hide element
            _ = elementsTransform[CurrentElementIndex]
                .DOMove(localPoints.StartPoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[CurrentElementIndex].DOFade(minFade, duration).SetUpdate(true);

            IncreaseElementIndex(1);
            //Show element
            _ = elementsTransform[CurrentElementIndex]
                .DOMove(localPoints.EndPoint.position, 0)
                .SetUpdate(true);
            _ = elementsTransform[CurrentElementIndex]
                .DOMove(localPoints.MiddlePoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[CurrentElementIndex].DOFade(maxFade, duration).SetUpdate(true);
        }

        public void MoveLeft()
        {
            //Hide element
            _ = elementsTransform[CurrentElementIndex]
                .DOMove(localPoints.EndPoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[CurrentElementIndex].DOFade(minFade, duration).SetUpdate(true);

            DecreaseElementIndex(1);

            //Show element
            _ = elementsTransform[CurrentElementIndex]
                .DOMove(localPoints.StartPoint.position, 0)
                .SetUpdate(true);
            _ = elementsTransform[CurrentElementIndex]
                .DOMove(localPoints.MiddlePoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[CurrentElementIndex].DOFade(maxFade, duration).SetUpdate(true);
        }

        private void IncreaseElementIndex(int step)
        {
            if (CurrentElementIndex + step >= elementsCanvasGroup.Count)
            {
                CurrentElementIndex = 0;
            }
            else
            {
                CurrentElementIndex += step;
            }
        }

        private void DecreaseElementIndex(int step)
        {
            if (CurrentElementIndex - step < 0)
            {
                CurrentElementIndex = elementsCanvasGroup.Count - 1;
            }
            else
            {
                CurrentElementIndex -= step;
            }
        }
    }
}
