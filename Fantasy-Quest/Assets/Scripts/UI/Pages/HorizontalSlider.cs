using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI.Pages
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
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
                element.localPosition = localPoints.LeftPoint.localPosition;
            }
            slidedElements[currentElementIndex].localPosition = localPoints.MiddlePoint.localPosition;
        }

        public void MoveRight()
        {
            slidedElements[currentElementIndex].DOMove(localPoints.LeftPoint.position, movementTime);
            IncreaseElementIndex(1);
            slidedElements[currentElementIndex].DOMove(localPoints.RightPoint.position, 0);
            slidedElements[currentElementIndex].DOMove(localPoints.MiddlePoint.position, movementTime);
        }
        
        public void MoveLeft()
        {
            slidedElements[currentElementIndex].DOMove(localPoints.RightPoint.position, movementTime);
            DecreaseElementIndex(1);
            slidedElements[currentElementIndex].DOMove(localPoints.LeftPoint.position, 0);
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
    
    [Serializable]
    public struct PivotPoints
    {
        public RectTransform LeftPoint;
        public RectTransform MiddlePoint;
        public RectTransform RightPoint;
    }

    public interface IHorizontalSlider
    {
        public void MoveLeft();
        public void MoveRight();
    }
}
