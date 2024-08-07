using UnityEngine;
using VContainer.Unity;

namespace Lifetimes.Project.Bootstrap
{
    internal class ScreenRatioSetter : IInitializable
    {
        public void Initialize()
        {
            QualitySettings.vSyncCount = 1;
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
