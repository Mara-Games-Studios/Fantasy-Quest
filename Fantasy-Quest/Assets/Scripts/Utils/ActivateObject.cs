using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.ActivateObject")]
    internal class ActivateObject : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private List<GameObject> objs;

        public void Activation()
        {
            objs.ForEach(obj => obj.SetActive(true));
        }
    }
}
