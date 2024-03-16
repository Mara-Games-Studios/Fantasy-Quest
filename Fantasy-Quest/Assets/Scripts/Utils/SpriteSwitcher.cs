using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Scripts/Utils/Utils.SpriteSwitcher")]
    internal class SpriteSwitcher : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Sprite sprite;

        public void ChangeSprite()
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
