using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.Slideparent")]
    internal class SlideshowParent : MonoBehaviour
    {
        [Serializable]
        private struct SlideStruct
        {
            public Sprite Slide;
            public float WaitBefore;
            public float HoldTime;
            public float WaitAfter;
            public float FadeTime;
        }

        [SerializeField]
        private bool childrenComponentsMode = true;

        [ShowIf(nameof(childrenComponentsMode))]
        [SerializeField]
        private List<Slide> slides = new();

        [ShowIf("@!" + nameof(childrenComponentsMode))]
        [SerializeField]
        private List<SlideStruct> slideStructs = new();

        public UnityEvent SlideshowEnded;

        private Image image;
        private Tween fadeTween;

        private IEnumerator ShowSlides()
        {
            if (childrenComponentsMode)
            {
                FillChildren();
                foreach (Slide slide in slides)
                {
                    yield return ShowSlide(slide);
                }
                SlideshowEnded?.Invoke();
            }
            else
            {
                if (image == null)
                {
                    image = gameObject.AddComponent<Image>();
                }

                image.color = new UnityEngine.Color(image.color.r, image.color.g, image.color.b, 0);
                foreach (SlideStruct slide in slideStructs)
                {
                    yield return ShowSlide(slide);
                }
                SlideshowEnded?.Invoke();
            }
        }

        private IEnumerator ShowSlide(Slide slide)
        {
            slide.gameObject.SetActive(true);
            yield return new WaitForSeconds(slide.WaitBefore);
            slide.FadeIn();
            yield return new WaitForSeconds(slide.HoldTime);
            slide.FadeOut();
            yield return new WaitForSeconds(slide.FadeTime);
            yield return new WaitForSeconds(slide.WaitAfter);
        }

        private IEnumerator ShowSlide(SlideStruct slide)
        {
            image.sprite = slide.Slide;

            yield return new WaitForSeconds(slide.WaitBefore);
            FadeIn(slide.FadeTime);
            yield return new WaitForSeconds(slide.HoldTime);
            FadeOut(slide.FadeTime);
            yield return new WaitForSeconds(slide.FadeTime);
            yield return new WaitForSeconds(slide.WaitAfter);
        }

        [Button]
        public void StartSlideshow()
        {
            _ = StartCoroutine(ShowSlides());
        }

        [Button]
        public void FillChildren()
        {
            if (slides.Count == 0)
            {
                foreach (Transform child in transform)
                {
                    slides.Add(child.GetComponent<Slide>());
                }
            }
        }

        public void FadeIn(float duration)
        {
            Fade(1f, duration, () => { });
        }

        public void FadeOut(float duration)
        {
            Fade(0f, duration, () => { });
        }

        private void Fade(float endValue, float duration, TweenCallback onEnd)
        {
            fadeTween.Kill(false);

            fadeTween = image.DOFade(endValue, duration);
            fadeTween.onComplete += onEnd;
        }
    }
}
