using System.Collections.Generic;
using UnityEngine;

namespace UI.Pages
{
    [AddComponentMenu("Scripts/UI/Pages/UI.Pages.View")]
    [RequireComponent(typeof(EffectsShower))]
    public class View : MonoBehaviour
    {
        private EffectsShower effectShower;
        private int effectsShowedAmount;

        public View PreviousPage;
        public List<IndicatedButton> VerticalButtons = new();

        public static event System.Action<View> OnPageShowing;
        public static event System.Action<View> OnPageShowed;
        public static event System.Action<View> OnPageHiding;

        private void Awake()
        {
            effectShower = GetComponent<EffectsShower>();
            effectShower.Initialize();
        }

        private void OnEnable()
        {
            effectShower.OnEffectShowed += () => OnPageShowed?.Invoke(this);
        }

        private void OnDisable()
        {
            effectShower.OnEffectShowed -= () => OnPageShowed.Invoke(this);
        }

        public void ShowFromStart()
        {
            effectShower.ShowFromStart();
            OnPageShowing?.Invoke(this);
        }

        public void ShowFromEnd()
        {
            effectShower.ShowFromEnd();
            OnPageShowing?.Invoke(this);
        }

        public void HideToStart()
        {
            OnPageHiding?.Invoke(this);
            effectShower.HideToStart();
        }

        public void HideToEnd()
        {
            OnPageHiding?.Invoke(this);
            effectShower.HideToEnd();
        }
    }
}
