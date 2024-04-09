using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [AddComponentMenu("Scripts/UI/UI.Slideparent")]
    internal class SlideshowParent : MonoBehaviour
    {
        [SerializeField]
        private bool childrenComponentsMode = true;

        [ShowIf("childrenComponentsMode")]
        [SerializeField]
        private List<Slide> slides = new();

        [ShowIf("@!childrenComponentsMode")]
        [SerializeField]
        private List<SlideStruct> slideStructs = new();

        private Image image;
        private Tween fadeTween;

        private void Awake()
        {
            if (childrenComponentsMode)
            {
                FillChildren();
            }
            else
            {
                image = gameObject.AddComponent<Image>();
                image.color = new Color(255, 255, 255, 0);
            }
        }

        private IEnumerator ShowSlides()
        {
            if (childrenComponentsMode)
            {
                foreach (Slide slide in slides)
                {
                    yield return ShowSlide(slide);
                }
            }
            else
            {
                foreach (SlideStruct slide in slideStructs)
                {
                    yield return ShowSlide(slide);
                }
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
            foreach (Transform child in transform)
            {
                slides.Add(child.GetComponent<Slide>());
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
