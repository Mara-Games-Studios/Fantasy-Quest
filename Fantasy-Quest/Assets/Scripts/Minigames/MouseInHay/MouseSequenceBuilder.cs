using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [Serializable]
    public class ShowMouseConfig
    {
        public int ShowTime;
        public int Delay;
        public int HolesCount;
    }

    [Serializable]
    internal class MouseSequenceBuilder
    {
        [Serializable]
        private class MouseDhowDelayByChance
        {
            public int Bits;
            public float Chance;
        }

        [SerializeField]
        private List<MouseDhowDelayByChance> mouseShowDelays;
        private int MaxSkipLength => mouseShowDelays.Select(x => x.Bits).Max();

        [SerializeField]
        private float doubleMousesChance;

        [SerializeField]
        private int soundLengthInBits;

        [SerializeField]
        private int doubleShowMouseCount;

        [SerializeField]
        private float mousesInTwoHolesChance;

        [SerializeField]
        private int defaultMouseShowTime;

        [SerializeField]
        private int luckyMouseShowTime;

        private int GetRandomDelay()
        {
            float sumChance = 0;
            float chance = UnityEngine.Random.Range(0, mouseShowDelays.Sum(x => x.Chance));
            foreach (MouseDhowDelayByChance bitsSkipChance in mouseShowDelays)
            {
                sumChance += bitsSkipChance.Chance;
                if (chance <= sumChance)
                {
                    return bitsSkipChance.Bits;
                }
            }
            throw new Exception();
        }

        public List<ShowMouseConfig> BuildSequence()
        {
            List<ShowMouseConfig> sequence = new();

            while (sequence.Sum(x => x.Delay) <= soundLengthInBits - MaxSkipLength)
            {
                ShowMouseConfig config =
                    new()
                    {
                        ShowTime = defaultMouseShowTime,
                        Delay = GetRandomDelay(),
                        HolesCount = UnityEngine.Random.value <= mousesInTwoHolesChance ? 2 : 1,
                    };
                sequence.Add(config);
            }

            List<ShowMouseConfig> doubled = new();
            List<ShowMouseConfig> toSelect = sequence
                .Where(x => x.Delay >= luckyMouseShowTime)
                .ToList();

            if (toSelect.Count < doubleShowMouseCount)
            {
                Debug.LogError("Not enough places to set lucky show times");
                toSelect.ForEach(x => x.ShowTime = luckyMouseShowTime);
                return sequence;
            }

            for (int i = 0; i < doubleShowMouseCount; i++)
            {
                ShowMouseConfig chosen;
                do
                {
                    chosen = toSelect.ElementAt(UnityEngine.Random.Range(0, toSelect.Count));
                } while (doubled.Contains(chosen));
                chosen.ShowTime = luckyMouseShowTime;
                doubled.Add(chosen);
            }
            return sequence;
        }
    }
}
