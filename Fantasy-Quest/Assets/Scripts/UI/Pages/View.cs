using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pages
{
    [AddComponentMenu("Scripts/UI/Pages/Pages.View")]
    public class View : MonoBehaviour
    {
        [SerializeField] 
        private EffectsShower effectShower;

        private int effectsShowedAmount;

        public View LastPage;
        public List<Image> ImageButtons = new();
        public static event System.Action<View> OnPageShowing;
        public static event System.Action<View> OnPageShowed;
        public static event System.Action OnPageHiding;

        private void Awake()
        {
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

        public void Show()
        {
            OnPageShowing?.Invoke(this);
            effectShower.Show();
        }
        
        public void Hide()
        {
            OnPageHiding?.Invoke();
            effectShower.Hide();
        }
    }
}
