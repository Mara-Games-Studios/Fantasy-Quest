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
        private Manager manager;

        [SerializeField]
        private FloatRange mouseShowTime;

        [SerializeField]
        private float noMouseTime;

        [SerializeField]
        private int maxMousesToShow = 10;

        private Coroutine launchCoroutine;

        private void Awake()
        {
            holes = GetComponentsInChildren<Hole>().ToList();
        }

        public void ResetHay()
        {
            mousesShowed = 0;
            _ = this.KillCoroutine(launchCoroutine);
            holes.ForEach(x => x.HideMouse());
        }

        public void StartShowMouse()
        {
            _ = this.KillCoroutine(launchCoroutine);
            launchCoroutine = StartCoroutine(Launch());
        }

        public IEnumerator Launch()
        {
            float showMouseTime = mouseShowTime.GetRandomFloatInRange();
            Hole hole = holes[Random.Range(0, holes.Count)];
            yield return hole.ShowMouse(showMouseTime);
            mousesShowed++;
            yield return new WaitForSeconds(noMouseTime);
            if (mousesShowed >= maxMousesToShow)
            {
                manager.ExitGame(ExitGameState.Lose);
            }
            else
            {
                StartShowMouse();
            }
        }
    }
}
