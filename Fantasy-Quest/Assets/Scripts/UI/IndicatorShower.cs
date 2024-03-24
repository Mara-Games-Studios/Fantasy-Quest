using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UI.Pages;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/UI/UI.IndicatorShower")]
public class IndicatorShower : MonoBehaviour
{
    [Required]
    [SerializeField]
    private RectTransform rectTransform;

    [Required]
    [SerializeField]
    private HorizontalLayoutGroup indicates;

    [Required]
    [SerializeField]
    private CanvasGroup indicatesAlpha;

    [SerializeField]
    private float fadeDuration;

    [SerializeField] 
    private float minAlpha = 0.1f;
    
    [SerializeField] 
    private float maxAlpha = 1f;

    [SerializeField]
    private int defaultLeftOffset = 18;

    [SerializeField]
    private int additiveSpacing = 30;

    private int currentImageIndex;
    private Tween vanishingTween;
    private List<Image> images = new();
    private MainMenuInput mainMenuInput;

    private void Awake()
    {
        mainMenuInput = new();
        mainMenuInput.Enable();
        indicatesAlpha.alpha = minAlpha;
    }

    private void OnEnable()
    {
        View.OnPageShowed += ShowIndicates;
        View.OnPageHiding += HideIndicates;
        mainMenuInput.UI.IndicateDown.performed += ctx => GoDown();
        mainMenuInput.UI.IndicateUp.performed += ctx => GoUp();
        mainMenuInput.UI.MenuClick.performed += ctx => Click();
    }

    private void OnDisable()
    {
        View.OnPageShowed -= ShowIndicates;
        View.OnPageHiding -= HideIndicates;
        mainMenuInput.UI.IndicateDown.performed -= ctx => GoDown();
        mainMenuInput.UI.IndicateUp.performed -= ctx => GoUp();
        mainMenuInput.UI.MenuClick.performed -= ctx => Click();
    }

    private void ShowIndicates(View view)
    {
        images = view.ImageButtons;
        if (images == null || images.Count == 0)
        {
            return;
        }
        indicates.gameObject.SetActive(true);
        currentImageIndex = images.Count - 1;
        ShowOn(images[^1]);
        vanishingTween?.Kill();
        _ = indicatesAlpha.DOFade(maxAlpha, fadeDuration);
    }

    private void HideIndicates()
    {
        vanishingTween?.Kill();
        vanishingTween = indicatesAlpha.DOFade(minAlpha, 0);
        indicatesAlpha.gameObject.SetActive(false);
    }

    private void Click()
    {
        if(images == null)
            return;
        if (images[currentImageIndex].TryGetComponent(out Button button))
            button.onClick.Invoke();
    }

    private void GoDown()
    {
        if (images == null || images.Count == 0)
        {
            // indicates.gameObject.SetActive(false);
            return;
        }
        // indicates.gameObject.SetActive(true);

        currentImageIndex--;
        if (currentImageIndex < 0)
        {
            currentImageIndex = images.Count - 1;
        }

        ShowOn(images[currentImageIndex]);
    }

    private void GoUp()
    {
        if (images == null || images.Count == 0)
        {
            // indicates.gameObject.SetActive(false);
            return;
        }
        // indicates.gameObject.SetActive(true);

        currentImageIndex++;
        if (currentImageIndex >= images.Count)
        {
            currentImageIndex = 0;
        }

        ShowOn(images[currentImageIndex]);
    }

    [Button]
    public void ShowOn(Image image)
    {
        int spacing = Convert.ToInt32(Mathf.Round(image.sprite.rect.width)) + additiveSpacing;
        indicates.spacing = spacing;
        indicates.padding.left = (-spacing / 2) - defaultLeftOffset;
        rectTransform.position = image.rectTransform.position;
    }
}
