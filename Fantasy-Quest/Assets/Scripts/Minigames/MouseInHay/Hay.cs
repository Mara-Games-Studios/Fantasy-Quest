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
        public int MousesShowed => mousesShowed;

        [SerializeField]
        private Manager manager;

        [SerializeField]
        private FloatRange mouseShowTime;

        [SerializeField]
        private float noMouseTime;

        [SerializeField]
        private int maxMousesToShow = 10;

        private Coroutine launchCoroutine;

        public float NoMouseTime
        {
            get => noMouseTime;
            set => noMouseTime = value;
        }
        public int MaxMousesToShow
        {
            get => maxMousesToShow;
            set => maxMousesToShow = value;
        }
        public FloatRange MouseShowTime
        {
            get => mouseShowTime;
            set => mouseShowTime = value;
        }

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
            float showMouseTime = MouseShowTime.GetRandomFloatInRange();
            Hole hole = holes[Random.Range(0, holes.Count)];
            yield return hole.ShowMouse(showMouseTime);
            mousesShowed++;
            yield return new WaitForSeconds(NoMouseTime);
            if (mousesShowed >= MaxMousesToShow)
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
