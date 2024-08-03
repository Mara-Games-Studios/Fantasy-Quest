using System.Collections;
using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cutscene.Skip
{
    [AddComponentMenu("Scripts/Cutscene/Skip/Cutscene.Skip.Hint")]
    internal class Hint : MonoBehaviour
    {
        private GameplayInput playerInput;

        private TMP_Text textMeshPro;

        [SerializeField]
        private float fadeHintDuration;

        private void Awake()
        {
            playerInput = new GameplayInput();
            textMeshPro = GetComponent<TMP_Text>();
            _ = textMeshPro.DOFade(0f, 0f);
        }

        private void OnEnable()
        {
            playerInput.Enable();
            playerInput.Player.CutsceneAnyKey.performed += ShowSkipHint;
        }

        private void ShowSkipHint(InputAction.CallbackContext context)
        {
            _ = textMeshPro.DOFade(1f, fadeHintDuration);
            _ = StartCoroutine(HideSkipHintRoutine());
        }

        private IEnumerator HideSkipHintRoutine()
        {
            yield return new WaitForSeconds(SubtitlesSettings.Instance.SubtitlesHintShowDuration);
            _ = textMeshPro.DOFade(0f, fadeHintDuration);
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.CutsceneAnyKey.performed -= ShowSkipHint;
        }
    }
}
