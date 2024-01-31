using UnityEngine;

namespace Cat
{
    [CreateAssetMenu(fileName = "CatConfig", menuName = "Configs/CatConfig")]
    public class CatConfig : ScriptableObject
    {
        [SerializeField]
        private RunStateConfig runStateConfig;

        public RunStateConfig RunStateConfig => runStateConfig;
    }
}
