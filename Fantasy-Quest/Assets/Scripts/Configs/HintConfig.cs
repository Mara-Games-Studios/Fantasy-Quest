using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Hint Config", menuName = "Settings/Create Hint Config")]
    internal class HintConfig : SingletonScriptableObject<HintConfig>
    {
        [Min(0)]
        public float SecondUntilShow;
    }
}
