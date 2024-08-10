using DI.Project.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI.Shared.Bootstrap
{
    [AddComponentMenu("Scripts/DI/Shared/Bootstrap/DI.Shared.Bootstrap.CursorLocker")]
    internal class CursorLocker : MonoBehaviour, IStartable
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
