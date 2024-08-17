using UnityEngine;
using VContainer;

namespace DI.Project.Services
{
    public class Cursor
    {
        [Preserve]
        public Cursor() { }

        public void LockCursor()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }

        public void UnLockCursor()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = true;
        }
    }
}
