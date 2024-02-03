using UnityEngine;

namespace Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Scripts/Sprites/Sprites.NewSpriteSet")]
    internal class NewSpriteSet : MonoBehaviour
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
