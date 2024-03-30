using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pages
{
    [AddComponentMenu("Scripts/UI/Pages/Pages.View")]
    [RequireComponent(typeof(EffectsShower))]
    public class View : MonoBehaviour
    {
        [Required]
        [SerializeField] 
        private EffectsShower pageEffectShower;

        private int effectsShowedAmount;

        public View LastPage;
        public List<Button> Buttons = new();
        
        public static event System.Action<View> OnPageShowing;
        public static event System.Action<View> OnPageShowed;
        public static event System.Action OnPageHiding;

        private void Awake()
        {
            pageEffectShower.Initialize();
        }

        private void OnEnable()
        {
            pageEffectShower.OnEffectShowed += () => OnPageShowed?.Invoke(this);
        }
        
        private void OnDisable()
        {
            pageEffectShower.OnEffectShowed -= () => OnPageShowed.Invoke(this);
        }

        public void ShowFromStart()
        {
            pageEffectShower.ShowFromStart();
            OnPageShowing?.Invoke(this);
        }

        public void ShowFromEnd()
        {
            pageEffectShower.ShowFromEnd();
            OnPageShowing?.Invoke(this);
        }
        
        public void HideToStart()
        {
            OnPageHiding?.Invoke();
            pageEffectShower.HideToStart();
        }
        
        public void HideToEnd()
        {
            OnPageHiding?.Invoke();
            pageEffectShower.HideToEnd();
        }
    }
}
