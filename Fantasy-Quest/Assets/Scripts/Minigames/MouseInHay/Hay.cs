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
        [SerializeField]
        private Manager manager;

        [HideLabel]
        [SerializeField]
        [FoldoutGroup("Mouse sequence parameters")]
        private MouseSequenceBuilder mouseSequenceBuilder;

        [SerializeField]
        private float secondsPerBit;

        [SerializeField]
        private int maxMousesInRow;

        [ReadOnly]
        [SerializeField]
        private List<Hole> lastSameVisited;

        [ReadOnly]
        [SerializeField]
        private List<Hole> holes;

        [ReadOnly]
        [SerializeField]
        private List<ShowMouseConfig> showMouseConfigs;
        public int AllMousesCount => showMouseConfigs.Count;

        private Coroutine launchCoroutine;

        private void Awake()
        {
            holes = GetComponentsInChildren<Hole>().ToList();
        }

        public void StopShowMouses()
        {
            _ = this.KillCoroutine(launchCoroutine);
            holes.ForEach(x => x.HideMouse());
        }

        public void StartShowMouses()
        {
            showMouseConfigs = mouseSequenceBuilder.BuildSequence();
            _ = this.KillCoroutine(launchCoroutine);
            launchCoroutine = StartCoroutine(ShowMouseRoutine());
        }

        public IEnumerator ShowMouseRoutine()
        {
            foreach (ShowMouseConfig showMouseConfig in showMouseConfigs)
            {
                float showTime = showMouseConfig.ShowTime * secondsPerBit;
                ShowMousesInHoles(showMouseConfig.HolesCount, showTime);
                yield return new WaitForSeconds(showMouseConfig.Delay * secondsPerBit);
            }
            manager.ExitGame(ExitGameState.Lose);
        }

        private void ShowMousesInHoles(int mousesCount, float showTime)
        {
            List<Hole> list = new(holes);
            for (int i = 0; i < mousesCount; i++)
            {
                Hole hole;
                do
                {
                    hole = list.ElementAt(Random.Range(0, list.Count));
                } while (lastSameVisited.Contains(hole) && lastSameVisited.Count >= maxMousesInRow);
                hole.ShowMouse(showTime);
                _ = list.Remove(hole);
                if (!lastSameVisited.Contains(hole))
                {
                    lastSameVisited.Clear();
                }
                lastSameVisited.Add(hole);
            }
        }
    }
}
