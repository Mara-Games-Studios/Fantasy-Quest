using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public struct SlideStruct
    {
        [SerializeField]
        public Sprite Slide;

        [SerializeField]
        public float WaitBefore;

        [SerializeField]
        public float HoldTime;

        [SerializeField]
        public float WaitAfter;

        [SerializeField]
        public float FadeTime;
    }
}
