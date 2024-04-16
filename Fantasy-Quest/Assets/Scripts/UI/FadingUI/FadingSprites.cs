using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FadingUI
{
    [AddComponentMenu("Scripts/UI/FadingUI/UI.FadingUI.FadingSprites")]
    internal class FadingSprites : MonoBehaviour, IFadingUI
    {
        [SerializeField]
        private List<TMP_Text> labels;

        [SerializeField]
        private List<Image> images;

        public void FadeIn(float time)
        {
            foreach (Image image in images)
            {
                image.color = new(image.color.r, image.color.g, image.color.b, 0);
                _ = image.DOFade(1, time);
            }
            foreach (TMP_Text label in labels)
            {
                label.color = new(label.color.r, label.color.g, label.color.b, 0);
                _ = label.DOFade(1, time);
            }
        }

        public void FadeOut(float time)
        {
            foreach (Image image in images)
            {
                image.color = new(image.color.r, image.color.g, image.color.b, 1);
                _ = image.DOFade(0, time);
            }
            foreach (TMP_Text label in labels)
            {
                label.color = new(label.color.r, label.color.g, label.color.b, 1);
                _ = label.DOFade(0, time);
            }
        }

        [Button]
        private void CatchAllLabelsAndImages()
        {
            labels = GetComponentsInChildren<TMP_Text>(true).ToList();
            images = GetComponentsInChildren<Image>(true).ToList();
        }
    }
}
