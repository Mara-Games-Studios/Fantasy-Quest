using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.ChickenBehaviour")]
    internal class ChickenBehaviour : MonoBehaviour
    {
        public enum ChickenState
        {
            idle,
            walk,
            run
        }

        [Header("Bounds")]
        [Required]
        [SerializeField]
        private Collider2D boundsCollider;

        [ReadOnly]
        [SerializeField]
        private ChickenState chickenState = ChickenState.idle;

        [Header("Randomization")]
        [Header("Idle Time")]
        [SerializeField]
        private float minIdleTime;

        [SerializeField]
        private float maxIdleTime;

        [Header("Walk Time")]
        [SerializeField]
        private float minWalkTime;

        [SerializeField]
        private float maxWalkTime;

        [Header("Run Time")]
        [SerializeField]
        private float minRunTime;

        [SerializeField]
        private float maxRunTime;

        private Bounds bounds;
        private Vector2 randomPoint;

        public UnityAction StateChanged;

        private void Start()
        {
            bounds = boundsCollider.bounds;
            randomPoint = RandomPointInBounds(bounds);
            ChooseState(ChickenState.idle);
        }

        private void OnEnable()
        {
            StateChanged += ChangeChickenState;
        }

        private void ChangeChickenState()
        {
            _ = StartCoroutine(SwitchChickenState());
        }

        private IEnumerator SwitchChickenState()
        {
            switch (chickenState)
            {
                case ChickenState.idle:
                    float idleTime = UnityEngine.Random.Range(minIdleTime, maxIdleTime);
                    yield return StayOnPosition(idleTime);
                    ChooseRandomState();
                    break;
                case ChickenState.walk:
                    float walkTime = UnityEngine.Random.Range(maxWalkTime, minWalkTime);
                    yield return WalkToPosition(walkTime);
                    ChooseRandomState();
                    break;
                case ChickenState.run:
                    float runTime = UnityEngine.Random.Range(minRunTime, maxRunTime);
                    yield return RunToPosition(runTime);
                    ChooseRandomState();
                    break;
            }
        }

        private IEnumerator WalkToPosition(float duration)
        {
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                transform.position = Vector2.Lerp(transform.position, randomPoint, time / duration);
                yield return null;
            }
        }

        private IEnumerator RunToPosition(float duration)
        {
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                transform.position = Vector2.Lerp(transform.position, randomPoint, time / duration);
                yield return null;
            }
        }

        private IEnumerator StayOnPosition(float duration)
        {
            yield return new WaitForSeconds(duration);
        }

        public Vector2 RandomPointInBounds(Bounds bounds)
        {
            return new Vector2(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                UnityEngine.Random.Range(bounds.min.y, bounds.max.y)
            );
        }

        private void ChooseRandomState()
        {
            chickenState = (ChickenState)
                UnityEngine.Random.Range(0, Enum.GetValues(typeof(ChickenState)).Length);
            StateChanged?.Invoke();
        }

        private void ChooseState(ChickenState state)
        {
            chickenState = state;
            StateChanged?.Invoke();
        }

        private void OnDisable()
        {
            StateChanged -= ChangeChickenState;
        }
    }
}
