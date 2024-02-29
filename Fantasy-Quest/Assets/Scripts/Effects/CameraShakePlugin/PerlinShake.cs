using UnityEngine;

namespace CameraShake
{
    public class PerlinShake : ICameraShake
    {
        private readonly Params pars;
        private readonly Envelope envelope;

        public IAmplitudeController AmplitudeController;
        private Vector2[] seeds;
        private float time;
        private Vector3? sourcePosition;
        private float norm;

        /// <summary>
        /// Creates an instance of PerlinShake.
        /// </summary>
        /// <param name="parameters">Parameters of the shake.</param>
        /// <param name="maxAmplitude">Maximum amplitude of the shake.</param>
        /// <param name="sourcePosition">World position of the source of the shake.</param>
        /// <param name="manualStrengthControl">Pass true if you want to control amplitude manually.</param>
        public PerlinShake(
            Params parameters,
            float maxAmplitude = 1,
            Vector3? sourcePosition = null,
            bool manualStrengthControl = false
        )
        {
            pars = parameters;
            envelope = new Envelope(
                pars.Envelope,
                maxAmplitude,
                manualStrengthControl
                    ? Envelope.EnvelopeControlMode.Manual
                    : Envelope.EnvelopeControlMode.Auto
            );
            AmplitudeController = envelope;
            this.sourcePosition = sourcePosition;
        }

        public Displacement CurrentDisplacement { get; private set; }
        public bool IsFinished { get; private set; }

        public void Initialize(Vector3 cameraPosition, Quaternion cameraRotation)
        {
            seeds = new Vector2[pars.NoiseModes.Length];
            norm = 0;
            for (int i = 0; i < seeds.Length; i++)
            {
                seeds[i] = Random.insideUnitCircle * 20;
                norm += pars.NoiseModes[i].Amplitude;
            }
        }

        public void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation)
        {
            if (envelope.IsFinished)
            {
                IsFinished = true;
                return;
            }
            time += deltaTime;
            envelope.Update(deltaTime);

            Displacement disp = Displacement.Zero;
            for (int i = 0; i < pars.NoiseModes.Length; i++)
            {
                disp +=
                    pars.NoiseModes[i].Amplitude
                    / norm
                    * SampleNoise(seeds[i], pars.NoiseModes[i].Frequency);
            }

            CurrentDisplacement = envelope.Intensity * Displacement.Scale(disp, pars.Strength);
            if (sourcePosition != null)
            {
                CurrentDisplacement *= Attenuator.Strength(
                    pars.Attenuation,
                    sourcePosition.Value,
                    cameraPosition
                );
            }
        }

        private Displacement SampleNoise(Vector2 seed, float freq)
        {
            Vector3 position =
                new(
                    Mathf.PerlinNoise(seed.x + (time * freq), seed.y),
                    Mathf.PerlinNoise(seed.x, seed.y + (time * freq)),
                    Mathf.PerlinNoise(seed.x + (time * freq), seed.y + (time * freq))
                );
            position -= Vector3.one * 0.5f;

            Vector3 rotation =
                new(
                    Mathf.PerlinNoise(-seed.x - (time * freq), -seed.y),
                    Mathf.PerlinNoise(-seed.x, -seed.y - (time * freq)),
                    Mathf.PerlinNoise(-seed.x - (time * freq), -seed.y - (time * freq))
                );
            rotation -= Vector3.one * 0.5f;

            return new Displacement(position, rotation);
        }

        [System.Serializable]
        public class Params
        {
            /// <summary>
            /// Strength of the shake for each axis.
            /// </summary>
            [Tooltip("Strength of the shake for each axis.")]
            public Displacement Strength = new(Vector3.zero, new Vector3(2, 2, 0.8f));

            /// <summary>
            /// Layers of perlin noise with different frequencies.
            /// </summary>
            [Tooltip("Layers of perlin noise with different frequencies.")]
            public NoiseMode[] NoiseModes = { new(12, 1) };

            /// <summary>
            /// Strength over time.
            /// </summary>
            [Tooltip("Strength of the shake over time.")]
            public Envelope.EnvelopeParams Envelope;

            /// <summary>
            /// How strength falls with distance from the shake source.
            /// </summary>
            [Tooltip("How strength falls with distance from the shake source.")]
            public Attenuator.StrengthAttenuationParams Attenuation;
        }

        [System.Serializable]
        public struct NoiseMode
        {
            public NoiseMode(float frequency, float amplitude)
            {
                Frequency = frequency;
                Amplitude = amplitude;
            }

            /// <summary>
            /// Frequency multiplier for the noise.
            /// </summary>
            [Tooltip("Frequency multiplier for the noise.")]
            public float Frequency;

            /// <summary>
            /// Amplitude of the mode.
            /// </summary>
            [Tooltip("Amplitude of the mode.")]
            [Range(0, 1)]
            public float Amplitude;
        }
    }
}
