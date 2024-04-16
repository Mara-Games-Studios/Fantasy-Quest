using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.AlwaysRotator")]
    internal class AlwaysRotator : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            float z = Time.realtimeSinceStartup * speed % 1 * 360;
            rectTransform.rotation = Quaternion.Euler(0, 0, z);
        }
    }
}
