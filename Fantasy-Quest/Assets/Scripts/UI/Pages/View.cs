using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Pages
{
    [AddComponentMenu("Scripts/UI/Pages/UI.Pages.View")]
    [RequireComponent(typeof(EffectsShower))]
    public class View : MonoBehaviour
    {
        private EffectsShower effectShower;

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
            effectShower.OnEffectShowed.AddListener(InvokeOnPageShowed);
        }

        private void OnDisable()
        {
            effectShower.OnEffectShowed.RemoveListener(InvokeOnPageShowed);
        }

        private void InvokeOnPageShowed()
        {
            OnPageShowed?.Invoke(this);
        }

        private void InvokeOnPageHiding()
        {
            OnPageHiding?.Invoke(this);
        }

        [Button]
        public void ShowFromStart()
        {
            effectShower.ShowFromStart();
            OnPageShowing?.Invoke(this);
        }

        [Button]
        public void ShowFromEnd()
        {
            effectShower.ShowFromEnd();
            OnPageShowing?.Invoke(this);
        }

        [Button]
        public void HideToStart()
        {
            InvokeOnPageHiding();
            effectShower.HideToStart();
        }

        [Button]
        public void HideToEnd()
        {
            InvokeOnPageHiding();
            effectShower.HideToEnd();
        }
    }
}
