using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.ScreenRatioSetter")]
    internal class ScreenRatioSetter : MonoBehaviour
    {
        private void Start()
        {
            SetRatio(16, 9);
        }

        private void SetRatio(float w, float h)
        {
            if ((Screen.width / ((float)Screen.height)) > w / h)
            {
                Screen.SetResolution((int)(Screen.height * (w / h)), Screen.height, true);
            }
            else
            {
                Screen.SetResolution(Screen.width, (int)(Screen.width * (h / w)), true);
            }
        }
    }
}
