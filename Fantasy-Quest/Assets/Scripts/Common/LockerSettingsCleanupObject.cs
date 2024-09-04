using Configs;
using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.LockerSettingsCleanupObject")]
    internal class LockerSettingsCleanupObject : MonoBehaviour
    {
        private LockerApi lockerSettings;

        public void SetLockerApi(LockerApi lockerSettings)
        {
            this.lockerSettings = lockerSettings;
        }

        private void OnDestroy()
        {
            lockerSettings.Api.SetToDefault();
        }
    }
}
