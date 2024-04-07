using System.Collections.Generic;
using DG.Tweening;
using UI.Indicators;
using UI.Pages;
using UI.Pages.Behaviours;
using UnityEngine;

[AddComponentMenu("Scripts/UI/UI.IndicatorsView")]
public class IndicatorsView : MonoBehaviour
{
    [SerializeField] 
    private EffectModel effectModel;
    
    private LayoutModel layoutModel ;
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
        
        foreach (var behaviour in indicatorsBehaviours)
        {
            behaviour.Enable();
        }
    }

    private void OnDisable()
    {
        View.OnPageShowed -= UpdateLayoutModel;
        View.OnPageShowed -= ShowIndicates;
        View.OnPageHiding -= HideIndicates;

        foreach (var behaviour in indicatorsBehaviours)
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
            return;
        
        effectModel.Indicators.gameObject.SetActive(true);
        layoutModel.CurrentButtonIndex = layoutModel.VerticalButtons.Count - 1;
        IndicatorsBehaviour.ShowOn(layoutModel.VerticalButtons[^1], effectModel);
        effectModel.VanishingTween?.Kill();
        _ = effectModel.IndicatorsAlpha.DOFade(effectModel.MaxAlpha, effectModel.FadeDuration);
    }
    
    private void HideIndicates()
    {
        effectModel.VanishingTween?.Kill();
        effectModel.VanishingTween = effectModel.IndicatorsAlpha.DOFade(effectModel.MinAlpha, 0);
        effectModel.IndicatorsAlpha.gameObject.SetActive(false);
    }
}
