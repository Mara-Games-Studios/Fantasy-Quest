using System.Collections.Generic;
using DG.Tweening;
using UI.Indicators;
using UI.Pages;
using UI.Pages.Behaviours;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils;
using EventSystem = UnityEngine.EventSystems.EventSystem;

[AddComponentMenu("Scripts/UI/UI.IndicatorsView")]
public class IndicatorsView : MonoBehaviour
{
    [SerializeField]
    private EffectModel effectModel;

    private LayoutModel layoutModel;
    private List<IndicatorsBehaviour> indicatorsBehaviours;

    private void Awake()
    {
        layoutModel = new LayoutModel();
        effectModel.Initialize();

        indicatorsBehaviours = new List<IndicatorsBehaviour>()
        {
            new IndicatorsKeyboardBehaviour(layoutModel, effectModel),
            new IndicatorsMouseBehaviour(layoutModel, effectModel)
        };
    }

    private void OnEnable()
    {
        View.OnPageShowed += UpdateLayoutModel;
        View.OnPageShowed += ShowIndicates;
        View.OnPageHiding += HideIndicates;

        foreach (IndicatorsBehaviour behaviour in indicatorsBehaviours)
        {
            behaviour.Enable();
        }
    }

    private void OnDisable()
    {
        View.OnPageShowed -= UpdateLayoutModel;
        View.OnPageShowed -= ShowIndicates;
        View.OnPageHiding -= HideIndicates;

        foreach (IndicatorsBehaviour behaviour in indicatorsBehaviours)
        {
            behaviour.Disable();
        }
    }

    private void UpdateLayoutModel(View view)
    {
        layoutModel.VerticalButtons = view.VerticalButtons;
        layoutModel.CurrentButtonIndex = 0;
    }

    private void ShowIndicates(View view)
    {
        if (view.VerticalButtons == null || view.VerticalButtons.Count == 0)
        {
            return;
        }

        bool isPointingOnButton = false;
        var results = PointerOverUI.GetEventSystemRaycastResults();
        if (PointerOverUI.IsPointerOverUIElement(results))
        {
            foreach (var raycastResult in results)
            {
                if (raycastResult.gameObject.TryGetComponent(out IndicatedButton button))
                {
                    ShowOn(button);
                    isPointingOnButton = true;
                }
            }
        }

        if (!isPointingOnButton)
        {
            ShowOn(layoutModel.VerticalButtons[^1]);
        }
    }

    private void ShowOn(IndicatedButton button)
    {
        effectModel.Indicators.gameObject.SetActive(true);
        layoutModel.CurrentButtonIndex = layoutModel.VerticalButtons.IndexOf(button);
        IndicatorsBehaviour.ShowOn(button, effectModel);
        effectModel.VanishingTween?.Kill();
        _ = effectModel
            .IndicatorsAlpha.DOFade(effectModel.MaxAlpha, effectModel.FadeDuration)
            .SetUpdate(true);
    }

    private void HideIndicates(View view)
    {
        effectModel.VanishingTween?.Kill();
        effectModel.VanishingTween = effectModel
            .IndicatorsAlpha.DOFade(effectModel.MinAlpha, 0)
            .SetUpdate(true);
        effectModel.IndicatorsAlpha.gameObject.SetActive(false);
    } 
}
