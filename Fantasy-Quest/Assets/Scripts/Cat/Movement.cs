using System;
using Rails;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat
{
    public enum State
    {
        Moving,
        Staying
    }

    public enum Vector
    {
        Right,
        Left
    }

    [SelectionBase]
    [AddComponentMenu("Scripts/Cat/Cat.Movement")]
    internal class Movement : MonoBehaviour
    {
        [SerializeField]
        private bool bindOnStart = false;

        [ShowIf(nameof(bindOnStart))]
        [SerializeField]
        private Point point;

        private RailsImpl rails;

        [SerializeField]
        private float speed;

        [SerializeField]
        private State state = State.Staying;
        public State State
        {
            get => state;
            private set
            {
                if (state != value)
                {
                    OnStateChanged?.Invoke(value);
                }
                state = value;
            }
        }

        [SerializeField]
        private Vector vector = Vector.Right;
        public Vector Vector
        {
            get => vector;
            private set
            {
                if (vector != value)
                {
                    OnVectorChanged?.Invoke(value);
                }
                vector = value;
            }
        }

        [SerializeField]
        private float movingThreshold = 0.1f;
        private float thresholdTimer = 0f;

        public event Action<State> OnStateChanged;
        public event Action<Vector> OnVectorChanged;

        private void Start()
        {
            if (bindOnStart)
            {
                SetOnRails(point);
                rails = point.Rails;
            }
        }

        private void Update()
        {
            thresholdTimer -= Time.deltaTime;
        }

        public void Move(float amount)
        {
            if (amount != 0)
            {
                thresholdTimer = movingThreshold;
                State = State.Moving;
                rails.MoveBody(speed * amount);
            }
            else if (thresholdTimer <= 0)
            {
                State = State.Staying;
            }

            if (amount > 0)
            {
                Vector = Vector.Right;
            }
            else if (amount < 0)
            {
                Vector = Vector.Left;
            }
        }

        public void ReTriggerVector()
        {
            OnStateChanged?.Invoke(State);
        }

        public void ChangeVector(Vector newVector)
        {
            Vector = newVector;
        }

        [Title("Debug buttons for testing")]
        [Button(Style = ButtonStyle.Box)]
        public void SetOnRails(RailsImpl rails, float point)
        {
            this.rails = rails;
            rails.BindBody(transform, point);
        }

        [Button(Style = ButtonStyle.Box)]
        public void SetOnRails(Point point)
        {
            rails = point.Rails;
            rails.BindBody(transform, point);
        }

        [Button(Style = ButtonStyle.Box)]
        public void RemoveFromRails()
        {
            rails.UnBindBody();
        }
    }
}
