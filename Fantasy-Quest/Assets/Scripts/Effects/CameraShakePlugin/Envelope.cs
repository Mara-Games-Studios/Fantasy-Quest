using UnityEngine;

namespace CameraShake
{
    /// <summary>
    /// Controls strength of the shake over time.
    /// </summary>
    public class Envelope : IAmplitudeController
    {
        private readonly EnvelopeParams pars;
        private readonly EnvelopeControlMode controlMode;
        private float amplitude;
        private float targetAmplitude;
        private float sustainEndTime;
        private bool finishWhenAmplitudeZero;
        private bool finishImmediately;
        private EnvelopeState state;

        /// <summary>
        /// Creates an Envelope instance.
        /// </summary>
        /// <param name="pars">Envelope parameters.</param>
        /// <param name="controlMode">Pass Auto for a single shake, or Manual for controlling strength manually.</param>
        public Envelope(
            EnvelopeParams pars,
            float initialTargetAmplitude,
            EnvelopeControlMode controlMode
        )
        {
            this.pars = pars;
            this.controlMode = controlMode;
            SetTarget(initialTargetAmplitude);
        }

        /// <summary>
        /// The value by which you want to multiply shake displacement.
        /// </summary>
        public float Intensity { get; private set; }

        public bool IsFinished
        {
            get
            {
                if (finishImmediately)
                {
                    return true;
                }

                return (finishWhenAmplitudeZero || controlMode == EnvelopeControlMode.Auto)
                    && amplitude <= 0
                    && targetAmplitude <= 0;
            }
        }

        public void Finish()
        {
            finishWhenAmplitudeZero = true;
            SetTarget(0);
        }

        public void FinishImmediately()
        {
            finishImmediately = true;
        }

        /// <summary>
        /// Update is called every frame by the shake.
        /// </summary>
        public void Update(float deltaTime)
        {
            if (IsFinished)
            {
                return;
            }

            if (state == EnvelopeState.Increase)
            {
                if (pars.Attack > 0)
                {
                    amplitude += deltaTime * pars.Attack;
                }

                if (amplitude > targetAmplitude || pars.Attack <= 0)
                {
                    amplitude = targetAmplitude;
                    state = EnvelopeState.Sustain;
                    if (controlMode == EnvelopeControlMode.Auto)
                    {
                        sustainEndTime = Time.time + pars.Sustain;
                    }
                }
            }
            else
            {
                if (state == EnvelopeState.Decrease)
                {
                    if (pars.Decay > 0)
                    {
                        amplitude -= deltaTime * pars.Decay;
                    }

                    if (amplitude < targetAmplitude || pars.Decay <= 0)
                    {
                        amplitude = targetAmplitude;
                        state = EnvelopeState.Sustain;
                    }
                }
                else
                {
                    if (controlMode == EnvelopeControlMode.Auto && Time.time > sustainEndTime)
                    {
                        SetTarget(0);
                    }
                }
            }

            amplitude = Mathf.Clamp01(amplitude);
            Intensity = Power.Evaluate(amplitude, pars.Degree);
        }

        public void SetTargetAmplitude(float value)
        {
            if (controlMode == EnvelopeControlMode.Manual && !finishWhenAmplitudeZero)
            {
                SetTarget(value);
            }
        }

        private void SetTarget(float value)
        {
            targetAmplitude = Mathf.Clamp01(value);
            state = targetAmplitude > amplitude ? EnvelopeState.Increase : EnvelopeState.Decrease;
        }

        [System.Serializable]
        public class EnvelopeParams
        {
            /// <summary>
            /// How fast the amplitude rises.
            /// </summary>
            [Tooltip("How fast the amplitude increases.")]
            public float Attack = 10;

            /// <summary>
            /// How long in seconds the amplitude holds a maximum value.
            /// </summary>
            [Tooltip("How long in seconds the amplitude holds maximum value.")]
            public float Sustain = 0;

            /// <summary>
            /// How fast the amplitude falls.
            /// </summary>
            [Tooltip("How fast the amplitude decreases.")]
            public float Decay = 1f;

            /// <summary>
            /// Power in which the amplitude is raised to get intensity.
            /// </summary>
            [Tooltip("Power in which the amplitude is raised to get intensity.")]
            public Degree Degree = Degree.Cubic;
        }

        public enum EnvelopeControlMode
        {
            Auto,
            Manual
        }

        public enum EnvelopeState
        {
            Sustain,
            Increase,
            Decrease
        }
    }

    public interface IAmplitudeController
    {
        /// <summary>
        /// Sets value to which amplitude will move over time.
        /// </summary>
        void SetTargetAmplitude(float value);

        /// <summary>
        /// Sets amplitude to zero and finishes the shake when zero is reached.
        /// </summary>
        void Finish();

        /// <summary>
        /// Immediately finishes the shake.
        /// </summary>
        void FinishImmediately();
    }
}
