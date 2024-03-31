using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.MouseInHay.MouseSequenceBuilder
{
    internal class StandardSequenceBuilder : ISequenceBuilder
    {
        [InfoBox("$" + nameof(Message))]
        [SerializeField]
        private int allMousesToShow;

        [SerializeField]
        private float showMouseTime;

        [SerializeField]
        private float noMouseTime;

        [SerializeField]
        private int doubleMousesCount;

        private string Message =>
            $"All sequence time: {allMousesToShow * (showMouseTime + noMouseTime)} seconds.\n"
            + $"Time to show one mouse: {showMouseTime + noMouseTime} seconds.";

        public IEnumerable<ShowMouseConfig> BuildSequence()
        {
            IEnumerable<int> doublesIndexes = Enumerable
                .Range(0, allMousesToShow)
                .OrderBy(t => Random.value)
                .Take(doubleMousesCount);

            foreach (int index in Enumerable.Range(0, allMousesToShow))
            {
                ShowMouseConfig config =
                    new()
                    {
                        ShowTime = showMouseTime,
                        Delay = noMouseTime,
                        HolesCount = doublesIndexes.Contains(index) ? 2 : 1
                    };
                yield return config;
            }
        }
    }
}
