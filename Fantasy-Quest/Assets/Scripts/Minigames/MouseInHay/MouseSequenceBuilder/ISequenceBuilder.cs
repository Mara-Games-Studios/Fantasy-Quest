using System;
using System.Collections.Generic;

namespace Minigames.MouseInHay.MouseSequenceBuilder
{
    [Serializable]
    public class ShowMouseConfig
    {
        public float ShowTime;
        public float Delay;
        public int HolesCount;
    }

    public interface ISequenceBuilder
    {
        public IEnumerable<ShowMouseConfig> BuildSequence();
    }
}
