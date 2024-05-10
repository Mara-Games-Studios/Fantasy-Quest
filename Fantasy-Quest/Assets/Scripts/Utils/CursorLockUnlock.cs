using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.CursorLockUnlock")]
    internal class CursorLockUnlock : MonoBehaviour
    {
        [SerializeField]
        private bool lockOnStart;

        [SerializeField]
        private bool unLockOnStart;

        private void Start()
        {
            if (lockOnStart)
            {
                LockCursor();
            }

            if (unLockOnStart)
            {
                UnLockCursor();
            }
        }

        [Button]
        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        [Button]
        public static void UnLockCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
