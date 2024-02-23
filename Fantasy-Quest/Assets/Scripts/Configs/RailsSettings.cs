using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Rails Settings", menuName = "Settings/Create Rails Settings")]
    internal class RailsSettings : SingletonScriptableObject<RailsSettings>
    {
        [SerializeField]
        private float magnetSpeed = 1f;

        public float MagnetSpeed => magnetSpeed;
    }
}
