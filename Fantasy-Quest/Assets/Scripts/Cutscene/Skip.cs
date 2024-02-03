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
        private PlayableDirector playableDirector;

        [SerializeField]
        private float endFrame = 0;

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
                playableDirector.time = endFrame / 60;
                playerInput.Disable();
                playerInput.Player.Skip.performed -= SkipCutscene;
            }
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.Skip.performed -= SkipCutscene;
        }
    }
}
