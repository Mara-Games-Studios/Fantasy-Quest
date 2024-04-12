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

        [SerializeField]
        private int currentElementIndex;

        [SerializeField]
        private float duration = 1.3f;

        [SerializeField]
        private float minFade = 0.1f;

        [SerializeField]
        private float maxFade = 1f;

        private List<RectTransform> elementsTransform;

        private void OnEnable()
        {
            elementsTransform = new List<RectTransform>(
                from element in elementsCanvasGroup
                select element.GetComponent<RectTransform>()
            );
            if (currentElementIndex < 0 || currentElementIndex >= elementsCanvasGroup.Count)
            {
                currentElementIndex = 0;
            }

            foreach (RectTransform element in elementsTransform)
            {
                element.localPosition = localPoints.StartPoint.localPosition;
            }
            elementsTransform[currentElementIndex].localPosition = localPoints
                .MiddlePoint
                .localPosition;
            elementsCanvasGroup[currentElementIndex].alpha = maxFade;
        }

        public void MoveRight()
        {
            //Hide element
            _ = elementsTransform[currentElementIndex]
                .DOMove(localPoints.StartPoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[currentElementIndex].DOFade(minFade, duration).SetUpdate(true);

            IncreaseElementIndex(1);
            //Show element
            _ = elementsTransform[currentElementIndex]
                .DOMove(localPoints.EndPoint.position, 0)
                .SetUpdate(true);
            _ = elementsTransform[currentElementIndex]
                .DOMove(localPoints.MiddlePoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[currentElementIndex].DOFade(maxFade, duration).SetUpdate(true);
        }

        public void MoveLeft()
        {
            //Hide element
            _ = elementsTransform[currentElementIndex]
                .DOMove(localPoints.EndPoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[currentElementIndex].DOFade(minFade, duration).SetUpdate(true);

            DecreaseElementIndex(1);

            //Show element
            _ = elementsTransform[currentElementIndex]
                .DOMove(localPoints.StartPoint.position, 0)
                .SetUpdate(true);
            _ = elementsTransform[currentElementIndex]
                .DOMove(localPoints.MiddlePoint.position, duration)
                .SetUpdate(true);
            _ = elementsCanvasGroup[currentElementIndex].DOFade(maxFade, duration).SetUpdate(true);
        }

        private void IncreaseElementIndex(int step)
        {
            currentElementIndex += step;
            if (currentElementIndex >= elementsCanvasGroup.Count)
            {
                currentElementIndex = 0;
            }
        }

        private void DecreaseElementIndex(int step)
        {
            currentElementIndex -= step;
            if (currentElementIndex < 0)
            {
                currentElementIndex = elementsCanvasGroup.Count - 1;
            }
        }
    }
}
