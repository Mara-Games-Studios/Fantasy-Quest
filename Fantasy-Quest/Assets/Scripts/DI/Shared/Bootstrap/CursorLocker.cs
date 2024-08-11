using DI.Project.Services;
using UnityEngine;
using VContainer;

namespace DI.Shared.Bootstrap
{
    [AddComponentMenu("Scripts/DI/Shared/Bootstrap/DI.Shared.Bootstrap.CursorLocker")]
    public class CursorLocker : MonoBehaviour
    {
        [Inject]
        private CursorController cursorController;

        [SerializeField]
        private bool lockOnStart;

        public void Start()
        {
            if (lockOnStart)
            {
                cursorController.LockCursor();
            }
        }
    }
}
