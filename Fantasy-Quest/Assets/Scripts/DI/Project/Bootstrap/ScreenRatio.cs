using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI.Project.Bootstrap
{
    public class ScreenRatio : IInitializable
    {
        [Preserve]
        public ScreenRatio() { }

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
