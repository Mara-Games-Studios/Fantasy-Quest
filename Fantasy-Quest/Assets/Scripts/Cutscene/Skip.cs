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
        private float endTime = 0;

        private PlayerInput playerInput;

        private void Awake()
        {
            playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            playerInput.Enable();
            playerInput.Player.Skip.performed += SkipCutscene;
        }

        public void SkipCutscene(InputAction.CallbackContext context)
        {
            playableDirector.time = endTime;
            playerInput.Disable();
            playerInput.Player.Skip.performed -= SkipCutscene;
        }

        private void OnDisable()
        {
            playerInput.Disable();
            playerInput.Player.Skip.performed -= SkipCutscene;
        }
    }
}
