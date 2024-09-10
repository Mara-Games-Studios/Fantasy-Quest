using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Minigames.MouseInHay.SeparateGame
{
    [AddComponentMenu(
        "Scripts/Minigames/MouseInHay/SeparateGame/Minigames.MouseInHay.SeparateGame.MissVolumeEffect"
    )]
    internal class MissVolumeEffect : MonoBehaviour
    {
        [SerializeField]
        private VolumeProfile volumeProfile;

        [SerializeField]
        private float startIntensity;

        [SerializeField]
        private float endIntensity;

        [SerializeField]
        private float interval;

        private Vignette vignette;
        private Sequence seq;

        private void Awake()
        {
            _ = volumeProfile.TryGet(out vignette);
        }

        private void Start()
        {
            SetDefault();
        }

        private void OnDestroy()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            vignette.intensity.value = startIntensity;
        }

        public void ApplyEffect()
        {
            seq?.Kill(true);
            seq = DOTween.Sequence();
            _ = seq.Append(
                DOVirtual.Float(
                    startIntensity,
                    endIntensity,
                    interval,
                    (x) => vignette.intensity.value = x
                )
            );
            _ = seq.Append(
                DOVirtual.Float(
                    endIntensity,
                    startIntensity,
                    interval,
                    (x) => vignette.intensity.value = x
                )
            );
            _ = seq.Play();
        }
    }
}
