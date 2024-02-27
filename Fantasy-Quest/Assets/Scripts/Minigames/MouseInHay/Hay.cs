using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Scripts/Minigames/MouseInHay/Minigames.MouseInHay.Hay")]
    internal class Hay : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private List<Hole> holes;

        [SerializeField]
        private float showMouseTime;

        [SerializeField]
        private float noMouseTime;

        private float WaitTime => showMouseTime + noMouseTime;

        private void Awake()
        {
            holes = GetComponentsInChildren<Hole>().ToList();
        }

        private void Start()
        {
            _ = StartCoroutine(CallMouseAndWait(showMouseTime, WaitTime));
        }

        private IEnumerator CallMouseAndWait(float showMouseTime, float waitTime)
        {
            Hole hole = holes[Random.Range(0, holes.Count)];
            hole.ShowMouse(showMouseTime);
            yield return new WaitForSeconds(noMouseTime);
            _ = StartCoroutine(CallMouseAndWait(showMouseTime, waitTime));
        }
    }
}
