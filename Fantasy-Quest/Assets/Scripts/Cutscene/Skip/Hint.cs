using Configs;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cutscene.Skip
{
    [AddComponentMenu("Scripts/Cutscene/Skip/Cutscene.Skip.Hint")]
    internal class Hint : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TextMeshProUGUI hintLabel;

        private GameplayInput playerInput;
        private Sequence showHintSequence;

        private void Awake()
        {
            playerInput = new GameplayInput();
            _ = hintLabel.DOFade(0f, 0f);
        }

        private void OnEnable()
        {
            playerInput.Enable();
            playerInput.Player.CutsceneAnyKey.performed += ShowSkipHint;
        }

        private void ShowSkipHint(InputAction.CallbackContext context)
        {
            showHintSequence?.Kill();
            showHintSequence = DOTween.Sequence();
            _ = showHintSequence.Append(
                hintLabel.DOFade(1f, SubtitlesSettings.Instance.FadeHintDuration)
            );
            _ = showHintSequence.AppendInterval(
                SubtitlesSettings.Instance.SubtitlesHintShowDuration
            );
            _ = showHintSequence.Append(
                hintLabel.DOFade(0f, SubtitlesSettings.Instance.FadeHintDuration)
            );
            _ = showHintSequence.Play();
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.CutsceneAnyKey.performed -= ShowSkipHint;
        }
    }
}
