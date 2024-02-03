using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace Cutscene
{
    [RequireComponent(typeof(PlayableDirector))]
    [AddComponentMenu("Scripts/Cutscene/Cutscene.Skip")]
    internal class Skip : MonoBehaviour
    {
        [SerializeField]
        private Dialogue.Manager dialogueManager;

        [SerializeField]
        private PlayableDirector playableDirector;

        [SerializeField]
        private float endFrame = 0;

        [SerializeField]
        private GameObject canvas;

        [SerializeField]
        private GameObject blackScreenPrefab;

        private CutsceneFade blackScreen;

        private GameplayInput playerInput;

        private void Awake()
        {
            playerInput = new GameplayInput();
        }

        private void OnEnable()
        {
            playerInput.Enable();
            playerInput.Player.Skip.performed += SkipCutscene;
        }

        public void SkipCutscene(InputAction.CallbackContext context)
        {
            if (playableDirector.state == PlayState.Playing)
            {
                blackScreen = Instantiate(blackScreenPrefab, canvas.transform)
                    .GetComponent<CutsceneFade>();
                blackScreen.SetSkipScript(this);
                playerInput.Disable();
                playerInput.Player.Skip.performed -= SkipCutscene;
            }
        }

        public void FadeInEndCallback()
        {
            playableDirector.time = endFrame / 60;
            dialogueManager.KillCurrentSpeakers();
            _ = StartCoroutine(WaitForSeconds(1f));
        }

        private IEnumerator WaitForSeconds(float duration)
        {
            yield return new WaitForSeconds(duration);
            blackScreen.FadeOut();
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.Skip.performed -= SkipCutscene;
        }
    }
}
