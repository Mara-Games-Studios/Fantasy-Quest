using UnityEngine;

namespace CameraShake
{
    /// <summary>
    /// Representation of translation and rotation.
    /// </summary>
    [System.Serializable]
    public struct Displacement
    {
        public Vector3 Position;
        public Vector3 EulerAngles;

        public Displacement(Vector3 position, Vector3 eulerAngles)
        {
            Position = position;
            EulerAngles = eulerAngles;
        }

        public Displacement(Vector3 position)
        {
            Position = position;
            EulerAngles = Vector3.zero;
        }

        public static Displacement Zero => new(Vector3.zero, Vector3.zero);

        public static Displacement operator +(Displacement a, Displacement b)
        {
            return new Displacement(a.Position + b.Position, b.EulerAngles + a.EulerAngles);
        }

        public static Displacement operator -(Displacement a, Displacement b)
        {
            return new Displacement(a.Position - b.Position, b.EulerAngles - a.EulerAngles);
        }

        public static Displacement operator -(Displacement disp)
        {
            return new Displacement(-disp.Position, -disp.EulerAngles);
        }

        public static Displacement operator *(Displacement coords, float number)
        {
            return new Displacement(coords.Position * number, coords.EulerAngles * number);
        }

        public static Displacement operator *(float number, Displacement coords)
        {
            return coords * number;
        }

        public static Displacement operator /(Displacement coords, float number)
        {
            return new Displacement(coords.Position / number, coords.EulerAngles / number);
        }

        public static Displacement Scale(Displacement a, Displacement b)
        {
            return new Displacement(
                Vector3.Scale(a.Position, b.Position),
                Vector3.Scale(b.EulerAngles, a.EulerAngles)
            );
        }

        public static Displacement Lerp(Displacement a, Displacement b, float t)
        {
            return new Displacement(
                Vector3.Lerp(a.Position, b.Position, t),
                Vector3.Lerp(a.EulerAngles, b.EulerAngles, t)
            );
        }

        public Displacement ScaledBy(float posScale, float rotScale)
        {
            return new Displacement(Position * posScale, EulerAngles * rotScale);
        }

        public Displacement Normalized => new(Position.normalized, EulerAngles.normalized);

        public static Displacement InsideUnitSpheres()
        {
            return new Displacement(Random.insideUnitSphere, Random.insideUnitSphere);
        }
    }
}
