using System;
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

        [Header("Sound properties")]
        [Range(0f, 1f)]
        public float Volume = 1f;

        [Range(-3f, 3f)]
        public float Pitch = 1f;

        [Range(-1f, 1f)]
        public float PanStereo = 0f;

        [Range(0f, 1.1f)]
        public float ReverbZoneMix = 1f;

        [Header("Spatialisation")]
        [Range(0f, 1f)]
        public float SpatialBlend = 1f;
        public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;

        [Range(0.01f, 5f)]
        public float MinDistance = 0.1f;

        [Range(5f, 100f)]
        public float MaxDistance = 50f;

        [Range(0, 360)]
        public int Spread = 0;

        [Range(0f, 5f)]
        public float DopplerLevel = 1f;

        [Header("Ignores")]
        public bool BypassEffects = false;
        public bool BypassListenerEffects = false;
        public bool BypassReverbZones = false;
        public bool IgnoreListenerVolume = false;
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

        public AudioSourceConfig LoadFrom(AudioSource audioSource)
        {
            return new AudioSourceConfig()
            {
                priorityLevel = (PriorityLevel)audioSource.priority,
                Volume = audioSource.volume,
                Pitch = audioSource.pitch,
                PanStereo = audioSource.panStereo,
                ReverbZoneMix = audioSource.reverbZoneMix,
                SpatialBlend = audioSource.spatialBlend,
                RolloffMode = audioSource.rolloffMode,
                MinDistance = audioSource.minDistance,
                MaxDistance = audioSource.maxDistance,
                Spread = (int)audioSource.spread,
                DopplerLevel = audioSource.dopplerLevel,
                BypassEffects = audioSource.bypassEffects,
                BypassListenerEffects = audioSource.bypassListenerEffects,
                BypassReverbZones = audioSource.bypassReverbZones,
                IgnoreListenerVolume = audioSource.ignoreListenerVolume,
                IgnoreListenerPause = audioSource.ignoreListenerPause,
            };
        }
    }
}
