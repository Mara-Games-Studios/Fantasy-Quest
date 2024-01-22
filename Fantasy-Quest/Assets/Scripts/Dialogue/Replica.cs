using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class Replica
    {
        public string Text;
        public AudioClip Audio;
        public float delayAfterSaid = 1.2f;
    }
}