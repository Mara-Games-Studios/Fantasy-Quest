using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace UI.Pages
{
    public interface IHorizontalSlider
    {
        public void MoveLeftWithAnimation();
        public void MoveRightWithAnimation();
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

        public void MoveRightImmediately()
        {
            Debug.Log("MoveRightImmediately");
            HideElement(localPoints.StartPoint.position, CurrentElementIndex, 0);
            IncreaseElementIndex(1);
            ShowElement(localPoints.EndPoint.position, CurrentElementIndex, 0);
        }

        public void MoveLeftImmediately()
        {
            Debug.Log("MoveLeftImmediately");
            HideElement(localPoints.EndPoint.position, CurrentElementIndex, 0);
            DecreaseElementIndex(1);
            ShowElement(localPoints.StartPoint.position, CurrentElementIndex, 0);
        }
        
        public void MoveRightWithAnimation()
        {
            HideElement(localPoints.StartPoint.position, CurrentElementIndex, duration);
            IncreaseElementIndex(1);
            ShowElement(localPoints.EndPoint.position, CurrentElementIndex, duration);
        }

        public void MoveLeftWithAnimation()
        {
            HideElement(localPoints.EndPoint.position, CurrentElementIndex, duration);
            DecreaseElementIndex(1);
            ShowElement(localPoints.StartPoint.position, CurrentElementIndex, duration);
        }

        private void HideElement(Vector3 endPosition, int index, float duration)
        {
            _ = elementsTransform[index]
                .DOMove(endPosition, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[index].DOFade(minFade, duration).SetUpdate(true);
        }

        private void ShowElement(Vector3 startPosition, int index, float duration)
        {
            _ = elementsTransform[index]
                .DOMove(startPosition, 0)
                .SetUpdate(true);
            _ = elementsTransform[index]
                .DOMove(localPoints.MiddlePoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[index].DOFade(maxFade, duration).SetUpdate(true);
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
