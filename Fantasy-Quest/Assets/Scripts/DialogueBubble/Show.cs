using System.Collections.Generic;
using UnityEngine;

namespace DialogueBubble
{
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Scripts/DialogueBubble/DialogueBubble.Show")]
    internal class Show : MonoBehaviour
    {
        [Header("Show Settings")]
        [SerializeField]
        private bool canShow = false;

        [SerializeField]
        private float lerpRate = 0.01f;

        [Header("Sprites/Renderers")]
        [SerializeField]
        public List<Sprite> IconList = new List<Sprite>();

        [SerializeField]
        private SpriteRenderer bubbleSprite;

        [SerializeField]
        private SpriteRenderer iconSprite;

        private void Awake()
        {
            iconSprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            iconSprite.sprite = IconList[0];

            Color bubbleColor = bubbleSprite.GetComponent<SpriteRenderer>().color;
            Color iconColor = iconSprite.GetComponent<SpriteRenderer>().color;

            bubbleColor.a = 0f;
            iconColor.a = 0f;

            bubbleSprite.color = bubbleColor;
            iconSprite.color = iconColor;
        }

        private void OnEnable()
        {
            EventSystem.OnTriggerBubble += CanShowSwitch;
        }

        public void CanShowSwitch(bool newCanShow)
        {
            canShow = newCanShow;

            //-------FOR TEST PURPOSE---------
            if (newCanShow)
            {
                gameObject.SetActive(true);
                iconSprite.sprite = IconList[Random.Range(0, IconList.Count)];
            }

            //--------------------------------
        }

        private void EnableBubble()
        {
            Color maxAlpha = bubbleSprite.color;
            maxAlpha.a = 1f;
            Color lerpedColor = Color.Lerp(bubbleSprite.color, maxAlpha, lerpRate);

            if (lerpedColor.a > 0.95f)
            {
                return;
            }
            bubbleSprite.color = lerpedColor;
            iconSprite.color = lerpedColor;
        }

        private void DisableBubble()
        {
            Color minAlpha = bubbleSprite.color;
            minAlpha.a = 0f;
            Color lerpedColor = Color.Lerp(bubbleSprite.color, minAlpha, lerpRate);
            if (lerpedColor.a < 0.09f)
            {
                gameObject.SetActive(false);
                return;
            }
            bubbleSprite.color = lerpedColor;
            iconSprite.color = lerpedColor;
        }

        private void Update()
        {
            if (canShow)
            {
                EnableBubble();
            }
            else
            {
                DisableBubble();
            }
        }

        private void OnDisable()
        {
            EventSystem.OnTriggerBubble -= CanShowSwitch;
        }
    }
}
