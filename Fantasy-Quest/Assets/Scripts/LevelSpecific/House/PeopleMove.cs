using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LevelSpecific.House
{
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.PeopleMove")]
    internal class PeopleMove : MonoBehaviour
    {

        [SerializeField]
        private float distanceX = 3f;

        [SerializeField]
        private float duration;

        [SerializeField]
        [ReadOnly]
        private Vector3 startPos;

        [SerializeField]
        [ReadOnly]
        private Vector3 endPos;
        private void Start()
        {
            startPos = transform.localPosition;
            StartMove();
        }
        private void StartMove()
        {
            _ = StartCoroutine(Reseter());
        }

        private IEnumerator Reseter()
        {
            Debug.Log("Start movement");
            yield return Move();
            Debug.Log("Move ended");
            transform.position = new Vector3(transform.position.x - distanceX, transform.position.y, transform.position.z);
            StartMove();
        }

        private IEnumerator Move()
        {
            float timer = 0f;
            endPos = startPos + new Vector3(distanceX, 0, 0);
            Debug.Log("Timer and enePos reset");
            while (timer < duration)
            {
                timer += Time.deltaTime;

                transform.localPosition = Vector3.Lerp(startPos, endPos, timer / duration);
                yield return null;

            }
        }
    }
}
