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
        private BoxCollider2D bounds;

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
        private float minMoveTime;

        [SerializeField]
        private float maxMoveTime;

        //If move time is above that value the WalkState will be chosen, otherwise RunState
        [SerializeField]
        private float runTimeBorder = 3f;

        private float moveTime = 0f;

        [HideInInspector]
        public Vector2 RandomPoint;
        public UnityAction<ChickenState> StateChanged;

        private void Start()
        {
            RandomPoint = RandomPointInBounds(bounds);
            ChooseState(ChickenState.idle);
        }

        private void OnEnable()
        {
            StateChanged += ChangeChickenState;
        }

        private void ChangeChickenState(ChickenState state)
        {
            _ = StartCoroutine(SwitchChickenState(state));
        }

        private IEnumerator SwitchChickenState(ChickenState state)
        {
            switch (state)
            {
                case ChickenState.idle:
                    float idleTime = UnityEngine.Random.Range(minIdleTime, maxIdleTime);
                    yield return StayOnPosition(idleTime);
                    ChooseRandomState();
                    break;
                case ChickenState.walk:
                case ChickenState.run:
                    RandomPoint = RandomPointInBounds(bounds);
                    yield return MoveToPosition(moveTime);
                    ChooseState(ChickenState.idle);
                    break;
            }
        }

        private IEnumerator MoveToPosition(float duration)
        {
            float time = 0;
            Vector2 startPos = transform.position;
            while (time < duration)
            {
                time += Time.deltaTime;

                transform.position = Vector2.Lerp(startPos, RandomPoint, time / duration);
                yield return null;
            }
        }

        private IEnumerator StayOnPosition(float duration)
        {
            yield return new WaitForSeconds(duration);
        }

        public Vector2 RandomPointInBounds(BoxCollider2D boxCollider)
        {
            Vector3 extents = boxCollider.size / 2;
            Vector3 point =
                new(
                    UnityEngine.Random.Range(-extents.x, extents.x),
                    UnityEngine.Random.Range(-extents.y, extents.y)
                );

            return boxCollider.transform.TransformPoint(point);
        }

        private void ChooseRandomState()
        {
            chickenState = (ChickenState)
                UnityEngine.Random.Range(0, Enum.GetValues(typeof(ChickenState)).Length);

            if (chickenState != ChickenState.idle)
            {
                moveTime = UnityEngine.Random.Range(minMoveTime, maxMoveTime);
                if (moveTime < runTimeBorder)
                {
                    chickenState = ChickenState.run;
                }
                else
                {
                    chickenState = ChickenState.walk;
                }
            }

            StateChanged?.Invoke(chickenState);
        }

        private void ChooseState(ChickenState state)
        {
            chickenState = state;
            StateChanged?.Invoke(chickenState);
        }

        private void OnDisable()
        {
            StateChanged -= ChangeChickenState;
        }
    }
}
