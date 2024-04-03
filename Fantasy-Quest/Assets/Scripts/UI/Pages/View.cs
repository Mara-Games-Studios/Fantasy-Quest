using System.Collections.Generic;
using UnityEngine;

namespace UI.Pages
{
    [AddComponentMenu("Scripts/UI/Pages/Pages.View")]
    [RequireComponent(typeof(PageEffectsShower))]
    public class View : MonoBehaviour
    { 
        private PageEffectsShower pagePageEffectShower;
        private int effectsShowedAmount;

        public View LastPage;
        public List<IndicatedButton> Buttons = new();
        
        public static event System.Action<View> OnPageShowing;
        public static event System.Action<View> OnPageShowed;
        public static event System.Action OnPageHiding;

        private void Awake()
        {
            pagePageEffectShower = GetComponent<PageEffectsShower>();
            pagePageEffectShower.Initialize();
        }

        private void OnEnable()
        {
            pagePageEffectShower.OnEffectShowed += () => OnPageShowed?.Invoke(this);
        }
        
        private void OnDisable()
        {
            pagePageEffectShower.OnEffectShowed -= () => OnPageShowed.Invoke(this);
        }

        public void ShowFromStart()
        {
            pagePageEffectShower.ShowFromStart();
            OnPageShowing?.Invoke(this);
        }

        public void ShowFromEnd()
        {
            pagePageEffectShower.ShowFromEnd();
            OnPageShowing?.Invoke(this);
        }
        
        public void HideToStart()
        {
            OnPageHiding?.Invoke();
            pagePageEffectShower.HideToStart();
        }
        
        public void HideToEnd()
        {
            OnPageHiding?.Invoke();
            pagePageEffectShower.HideToEnd();
        }
    }
}
