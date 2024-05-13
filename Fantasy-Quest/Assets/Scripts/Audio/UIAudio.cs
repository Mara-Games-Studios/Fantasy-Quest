using System.Collections.Generic;
using Audio;
using Sirenix.OdinInspector;
using UI.Pages.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [AddComponentMenu("Scripts/Audio/Audio.UIAudio")]
    internal class UIAudio : MonoBehaviour
    {
        [SerializeField]
        private List<SoundPlayer> onButtonHover;

        [Required]
        [SerializeField]
        private SoundPlayer onButtonClicked;

        [Required]
        [SerializeField]
        private SoundPlayer onPageChanged;

        private void Awake()
        {
            Button[] allButtons = FindObjectsByType<Button>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

            foreach (Button button in allButtons)
            {
                button.onClick.AddListener(onButtonClicked.PlayClip);
            }
        }

        private void OnEnable()
        {
            IndicatorsBehaviour.OnButtonShowed += PlayButtonClickSound;
            Pages.View.OnPageShowing += PlayPageChangedSound;
            Pages.View.OnPageHiding += PlayPageChangedSound;
        }

        [Button]
        private void PlayButtonClickSound(Button button)
        {
            int index = Random.Range(0, onButtonHover.Count);
            onButtonHover[index].PlayClip();
        }

        [Button]
        private void PlayPageChangedSound(Pages.View pageView)
        {
            onPageChanged.PlayClip();
        }

        private void OnDisable()
        {
            IndicatorsBehaviour.OnButtonShowed -= PlayButtonClickSound;
            Pages.View.OnPageShowing -= PlayPageChangedSound;
            Pages.View.OnPageHiding -= PlayPageChangedSound;
        }
    }
}
