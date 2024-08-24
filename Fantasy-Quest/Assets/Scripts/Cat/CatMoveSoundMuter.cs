using Audio;
using Common.DI;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.CatMoveSoundMuter")]
    internal class CatMoveSoundMuter : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [Required]
        [SerializeField]
        private SoundPlayer soundPlayer;

        private void Update()
        {
            soundPlayer.AudioSource.mute = lockerSettings.Api.IsCatMovementLocked;
        }
    }
}
