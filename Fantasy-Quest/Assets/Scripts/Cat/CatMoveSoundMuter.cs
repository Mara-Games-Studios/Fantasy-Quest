using Audio;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cat
{
    [AddComponentMenu("Scripts/Cat/Cat.CatMoveSoundMuter")]
    internal class CatMoveSoundMuter : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SoundPlayer soundPlayer;

        private void Update()
        {
            soundPlayer.AudioSource.mute = LockerSettings.Instance.IsCatMovementLocked;
        }
    }
}
