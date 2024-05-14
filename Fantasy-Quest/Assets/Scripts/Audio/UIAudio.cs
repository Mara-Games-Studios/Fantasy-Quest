using Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [AddComponentMenu("Scripts/Audio/Audio.UIAudio")]
    internal class UIAudio : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SoundPlayer onButtonClicked;

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
    }
}
