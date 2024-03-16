using System;

namespace Effects
{
    internal interface IEffect
    {
        public void DoEffect();
        public void RefreshEffect();
        public event Action OnEffectEnded;
    }
}
