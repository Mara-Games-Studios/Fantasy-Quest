using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.Shutdown")]
    internal class Shutdown : MonoBehaviour
    {
        public void Close()
        {
            Application.Quit();
        }
    }
}
