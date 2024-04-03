using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI.Pages
{
    public interface IHorizontalSlider
    {
        public void MoveLeft();
        public void MoveRight();
    }
    
    public class HorizontalSlider : MonoBehaviour, IHorizontalSlider
    {
        [SerializeField] 
        private PivotPoints localPoints;

        [SerializeField] 
        private List<RectTransform> slidedElements;

        [SerializeField]
        private int currentElementIndex;

        [SerializeField] 
        private float movementTime = 1.3f;

        private void OnEnable()
        {
            if (currentElementIndex < 0 || currentElementIndex >= slidedElements.Count)
            {
                currentElementIndex = 0;
            }

            foreach (var element in slidedElements)
            {
                element.localPosition = localPoints.StartPoint.localPosition;
            }
            slidedElements[currentElementIndex].localPosition = localPoints.MiddlePoint.localPosition;
        }

        public void MoveRight()
        {
            slidedElements[currentElementIndex].DOMove(localPoints.StartPoint.position, movementTime);
            IncreaseElementIndex(1);
            slidedElements[currentElementIndex].DOMove(localPoints.EndPoint.position, 0);
            slidedElements[currentElementIndex].DOMove(localPoints.MiddlePoint.position, movementTime);
        }
        
        public void MoveLeft()
        {
            slidedElements[currentElementIndex].DOMove(localPoints.EndPoint.position, movementTime);
            DecreaseElementIndex(1);
            slidedElements[currentElementIndex].DOMove(localPoints.StartPoint.position, 0);
            slidedElements[currentElementIndex].DOMove(localPoints.MiddlePoint.position, movementTime);
        }

        private void IncreaseElementIndex(int step)
        {
            currentElementIndex += step;
            if (currentElementIndex >= slidedElements.Count)
            {
                currentElementIndex = 0;
            }
        }

        private void DecreaseElementIndex(int step)
        {
            currentElementIndex -= step;
            if (currentElementIndex < 0)
            {
                currentElementIndex = slidedElements.Count - 1;
            }
        }
    }
}
