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
    private List<Button> verticalButtons = new();
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
        mainMenuInput.UI.IndicateLeft.performed += ctx => GoLeft();
        mainMenuInput.UI.IndicateRight.performed += ctx => GoRight();
        mainMenuInput.UI.MenuClick.performed += ctx => Click();
    }

    private void OnDisable()
    {
        View.OnPageShowed -= ShowIndicates;
        View.OnPageHiding -= HideIndicates;
        mainMenuInput.UI.IndicateDown.performed -= ctx => GoDown();
        mainMenuInput.UI.IndicateUp.performed -= ctx => GoUp();
        mainMenuInput.UI.IndicateLeft.performed -= ctx => GoLeft();
        mainMenuInput.UI.IndicateRight.performed -= ctx => GoRight();
        mainMenuInput.UI.MenuClick.performed -= ctx => Click();
    }

    private void ShowIndicates(View view)
    {
        verticalButtons = view.Buttons;
        if (verticalButtons == null || verticalButtons.Count == 0)
        {
            return;
        }
        indicates.gameObject.SetActive(true);
        currentImageIndex = verticalButtons.Count - 1;
        ShowOn(verticalButtons[^1]);
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
        verticalButtons?[currentImageIndex].onClick.Invoke();
    }

    private void GoDown()
    {
        if (verticalButtons == null || verticalButtons.Count == 0)
        {
            return;
        }
        currentImageIndex--;
        if (currentImageIndex < 0)
        {
            currentImageIndex = verticalButtons.Count - 1;
        }

        ShowOn(verticalButtons[currentImageIndex]);
    }

    private void GoUp()
    {
        if (verticalButtons == null || verticalButtons.Count == 0)
        {
            return;
        }
        currentImageIndex++;
        if (currentImageIndex >= verticalButtons.Count)
        {
            currentImageIndex = 0;
        }

        ShowOn(verticalButtons[currentImageIndex]);
    }

    private void GoLeft()
    {
        if(verticalButtons == null)
            return;
        if (verticalButtons[currentImageIndex].TryGetComponent(out IHorizontalSlider horizontalSlider))
        {
            horizontalSlider.MoveLeft();
        }
    }

    private void GoRight()
    {
        if(verticalButtons == null)
            return;
        if (verticalButtons[currentImageIndex].TryGetComponent(out IHorizontalSlider horizontalSlider))
        {
            horizontalSlider.MoveRight();
        }
    }
    
    [Button]
    public void ShowOn(Button button)
    {
        var rectTransform = button.GetComponent<RectTransform>();
        int spacing = Convert.ToInt32(Mathf.Round(rectTransform.rect.width)) + additiveSpacing;
        indicates.spacing = spacing;
        indicates.padding.left = (-spacing / 2) - defaultLeftOffset;
        this.rectTransform.position = rectTransform.position;
    }
}
