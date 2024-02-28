using UnityEngine;

namespace CameraShake
{
    /// <summary>
    /// Contains methods for changing strength and direction of shakes depending on their position.
    /// </summary>
    public static class Attenuator
    {
        /// <summary>
        /// Returns multiplier for the strength of a shake, based on source and camera positions.
        /// </summary>
        public static float Strength(
            StrengthAttenuationParams pars,
            Vector3 sourcePosition,
            Vector3 cameraPosition
        )
        {
            Vector3 vec = cameraPosition - sourcePosition;
            float distance = Vector3.Scale(pars.AxesMultiplier, vec).magnitude;
            float strength = Mathf.Clamp01(
                1 - ((distance - pars.ClippingDistance) / pars.FalloffScale)
            );

            return Power.Evaluate(strength, pars.FalloffDegree);
        }

        /// <summary>
        /// Returns displacement, opposite to the direction to the source in camera's local space.
        /// </summary>
        public static Displacement Direction(
            Vector3 sourcePosition,
            Vector3 cameraPosition,
            Quaternion cameraRotation
        )
        {
            Displacement direction = Displacement.Zero;
            direction.Position = (cameraPosition - sourcePosition).normalized;
            direction.Position = Quaternion.Inverse(cameraRotation) * direction.Position;

            direction.EulerAngles.x = direction.Position.z;
            direction.EulerAngles.y = direction.Position.x;
            direction.EulerAngles.z = -direction.Position.x;

            return direction;
        }

        [System.Serializable]
        public class StrengthAttenuationParams
        {
            /// <summary>
            /// Radius in which shake doesn't lose strength.
            /// </summary>
            [Tooltip("Radius in which shake doesn't lose strength.")]
            public float ClippingDistance = 10;

            /// <summary>
            /// Defines how fast strength falls with distance.
            /// </summary>
            [Tooltip("How fast strength falls with distance.")]
            public float FalloffScale = 50;

            /// <summary>
            /// Power of the falloff function.
            /// </summary>
            [Tooltip("Power of the falloff function.")]
            public Degree FalloffDegree = Degree.Quadratic;

            /// <summary>
            /// Contribution of each axis to distance. E. g. (1, 1, 0) for a 2D game in XY plane.
            /// </summary>
            [Tooltip(
                "Contribution of each axis to distance. E. g. (1, 1, 0) for a 2D game in XY plane."
            )]
            public Vector3 AxesMultiplier = Vector3.one;
        }
    }
}
