using UnityEngine;
using VContainer;

namespace DI.Shared.Bootstrap
{
    [AddComponentMenu("Scripts/DI/Shared/Bootstrap/DI.Shared.Bootstrap.CursorLocker")]
    public class CursorLocker : MonoBehaviour
    {
        [Inject]
        private Project.Services.Cursor cursorService;

        [SerializeField]
        private bool lockOnStart;

        [SerializeField]
        private bool unLockOnStart;

        public void Start()
        {
            if (lockOnStart)
            {
                cursorService.LockCursor();
            }

            if (unLockOnStart)
            {
                cursorService.UnLockCursor();
            }
        }
    }
}
