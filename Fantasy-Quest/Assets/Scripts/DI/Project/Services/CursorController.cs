using UnityEngine;
using VContainer;

namespace DI.Project.Services
{
    public class CursorController
    {
        [Preserve]
        public CursorController() { }

        public void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void UnLockCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
