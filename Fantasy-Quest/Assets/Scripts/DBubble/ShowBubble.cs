using System.Collections.Generic;
using UnityEngine;

namespace DBubble
{
    [AddComponentMenu("Scripts/DBubble/DBubble.ShowBubble")]
    internal class ShowBubble : MonoBehaviour
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
            bubbleSprite = GetComponent<SpriteRenderer>();
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
            BubbleEventSystem.OnTriggerBubble += CanShowSwitch;
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
            Color MaxAlpha = bubbleSprite.color;
            MaxAlpha.a = 1f;
            Color LerpedColor = Color.Lerp(bubbleSprite.color, MaxAlpha, lerpRate);

            if (LerpedColor.a > 0.95f)
            {
                return;
            }
            bubbleSprite.color = LerpedColor;
            iconSprite.color = LerpedColor;
        }

        private void DisableBubble()
        {
            Color MinAlpha = bubbleSprite.color;
            MinAlpha.a = 0f;
            Color LerpedColor = Color.Lerp(bubbleSprite.color, MinAlpha, lerpRate);
            if (LerpedColor.a < 0.09f)
            {
                gameObject.SetActive(false);
                return;
            }
            bubbleSprite.color = LerpedColor;
            iconSprite.color = LerpedColor;
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
            BubbleEventSystem.OnTriggerBubble -= CanShowSwitch;
        }
    }
}
