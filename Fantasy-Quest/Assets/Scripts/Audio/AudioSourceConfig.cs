using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [Serializable]
    [AddComponentMenu("Scripts/Audio/Audio.AudioSourceConfig")]
    public class AudioSourceConfig
    {
        [SerializeField]
        private PriorityLevel priorityLevel = PriorityLevel.Standard;

        [HideInInspector]
        public int Priority
        {
            get => (int)priorityLevel;
            set => priorityLevel = (PriorityLevel)value;
        }

        public bool Mute = false;

        [Range(0f, 1f)]
        public float Volume = 1f;

        [FoldoutGroup("Sound properties")]
        [Range(-3f, 3f)]
        public float Pitch = 1f;

        [FoldoutGroup("Sound properties")]
        [Range(-1f, 1f)]
        public float PanStereo = 0f;

        [FoldoutGroup("Sound properties")]
        [Range(0f, 1.1f)]
        public float ReverbZoneMix = 1f;

        [Range(0f, 1f)]
        public float SpatialBlend = 0f;

        [FoldoutGroup("Spatialisation")]
        public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;

        [FoldoutGroup("Spatialisation")]
        [Range(0.01f, 5f)]
        public float MinDistance = 0.1f;

        [FoldoutGroup("Spatialisation")]
        [Range(5f, 100f)]
        public float MaxDistance = 50f;

        [FoldoutGroup("Spatialisation")]
        [Range(0, 360)]
        public int Spread = 0;

        [FoldoutGroup("Spatialisation")]
        [Range(0f, 5f)]
        public float DopplerLevel = 1f;

        [FoldoutGroup("Ignores")]
        public bool BypassEffects = false;

        [FoldoutGroup("Ignores")]
        public bool BypassListenerEffects = false;

        [FoldoutGroup("Ignores")]
        public bool BypassReverbZones = false;

        [FoldoutGroup("Ignores")]
        public bool IgnoreListenerVolume = false;

        [FoldoutGroup("Ignores")]
        public bool IgnoreListenerPause = false;

        private enum PriorityLevel
        {
            Highest = 0,
            High = 64,
            Standard = 128,
            Low = 194,
            VeryLow = 256,
        }

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.mute = Mute;
            audioSource.bypassEffects = BypassEffects;
            audioSource.bypassListenerEffects = BypassListenerEffects;
            audioSource.bypassReverbZones = BypassReverbZones;
            audioSource.priority = Priority;
            audioSource.volume = Volume;
            audioSource.pitch = Pitch;
            audioSource.panStereo = PanStereo;
            audioSource.spatialBlend = SpatialBlend;
            audioSource.reverbZoneMix = ReverbZoneMix;
            audioSource.dopplerLevel = DopplerLevel;
            audioSource.spread = Spread;
            audioSource.rolloffMode = RolloffMode;
            audioSource.minDistance = MinDistance;
            audioSource.maxDistance = MaxDistance;
            audioSource.ignoreListenerVolume = IgnoreListenerVolume;
            audioSource.ignoreListenerPause = IgnoreListenerPause;
        }
    }
}
