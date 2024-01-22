using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class Replica
    {
        public string Text;
        public AudioClip Audio;
        public float DelayAfterSaid = 1.2f;
    }
}
