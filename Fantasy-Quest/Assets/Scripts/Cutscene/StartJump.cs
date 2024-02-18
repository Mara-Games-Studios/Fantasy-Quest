using Interaction;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cutscene
{
    [RequireComponent(typeof(BoxCollider2D))]
    [AddComponentMenu("Scripts/Cutscene/Cutscene.StartJump")]
    internal class StartJump : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Start cutsceneStarter;

        [SerializeField]
        private bool isJumpUp = true;

        private GameplayInput playerInput;

        private void Awake()
        {
            playerInput = new GameplayInput();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<InteractionImpl>(out _))
            {
                Debug.Log("Ready for jump");
                if (isJumpUp)
                {
                    Debug.Log("Assigned for jump up");
                    playerInput.Player.UpJump.performed += StartCutscene;
                }
                else
                {
                    Debug.Log("Assigned for jump down");
                    playerInput.Player.DownJump.performed += StartCutscene;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<InteractionImpl>(out _))
            {
                Debug.Log("Exit from jump");
                if (isJumpUp)
                {
                    playerInput.Player.UpJump.performed -= StartCutscene;
                }
                else
                {
                    playerInput.Player.DownJump.performed -= StartCutscene;
                }
            }
        }

        private void StartCutscene(InputAction.CallbackContext context)
        {
            cutsceneStarter.StartCutscene();
        }

        private void OnDisable() { }
    }
}
