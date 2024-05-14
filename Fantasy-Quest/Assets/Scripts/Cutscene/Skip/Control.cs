using System.Collections;
using System.Collections.Generic;
using Audio;
using Dialogue;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace Cutscene.Skip
{
    [RequireComponent(typeof(PlayableDirector))]
    [AddComponentMenu("Scripts/Cutscene/Skip/Cutscene.Skip.Control")]
    internal class Control : MonoBehaviour
    {
        [SerializeField]
        private Dialogue.Manager dialogueManager;

        [SerializeField]
        private List<ChainSpeaker> chainSpeakerList;

        [SerializeField]
        private List<SoundPlayer> soundPlayerList;

        [SerializeField]
        private PlayableDirector playableDirector;

        [SerializeField]
        private float endFrame = 0;

        [SerializeField]
        private Creator blackScreenCreator;

        private Panel blackScreen;

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
                blackScreen = blackScreenCreator.Create();
                blackScreen.SetSkipScript(this);
                playerInput.Disable();
            }
        }

        private const float FRAMES_PER_SECOND = 60;
        private const float FADE_DURATION = 1f;

        public void FadeInEndCallback()
        {
            playableDirector.time = endFrame / FRAMES_PER_SECOND;
            _ = StartCoroutine(WaitForSeconds(FADE_DURATION));
        }

        private IEnumerator WaitForSeconds(float duration)
        {
            yield return new WaitForSeconds(duration);
            blackScreen.FadeOut();
            chainSpeakerList.ForEach(x => x.StopTelling());
            soundPlayerList.ForEach(x => x.StopClip());
            dialogueManager.KillAllSpeakers();
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.Skip.performed -= SkipCutscene;
        }

        [Button]
        private void FindComponents()
        {
            dialogueManager = FindAnyObjectByType<Dialogue.Manager>();
            playableDirector = GetComponent<PlayableDirector>();
            blackScreenCreator = FindAnyObjectByType<Creator>();
        }
    }
}
