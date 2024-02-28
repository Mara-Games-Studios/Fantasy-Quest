using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.Hay")]
    internal class Hay : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private List<Hole> holes;

        [ReadOnly]
        [SerializeField]
        private int mousesShowed = 0;

        [SerializeField]
        private ScoreCounter scoreCounter;

        [SerializeField]
        private FloatRange mouseShowTime;

        [SerializeField]
        private float noMouseTime;

        [SerializeField]
        private int maxMousesToShow = 10;

        private void Awake()
        {
            holes = GetComponentsInChildren<Hole>().ToList();
        }

        private void Start()
        {
            Launch();
        }

        private void Launch()
        {
            float showTime = mouseShowTime.GetRandomFloatInRange();
            _ = StartCoroutine(CallMouseAndWait(showTime, showTime + noMouseTime));
        }

        private IEnumerator CallMouseAndWait(float showMouseTime, float waitTime)
        {
            Hole hole = holes[Random.Range(0, holes.Count)];
            hole.ShowMouse(showMouseTime);
            mousesShowed++;
            yield return new WaitForSeconds(waitTime);
            if (mousesShowed >= maxMousesToShow)
            {
                Debug.Log("You lose game");
                scoreCounter.ExitGame();
            }
            else
            {
                Launch();
            }
        }
    }
}
