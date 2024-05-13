using System.Collections.Generic;
using System.Linq;
using Audio;
using Dialogue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Scripts/Utils/Utils.AudioAggregator")]
    internal class AudioAggregator : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private List<SoundPlayer> soundPlayers;

        [ReadOnly]
        [SerializeField]
        private List<ChainSpeaker> chainSpeakers;

        [ReadOnly]
        [SerializeField]
        private List<DialogueSpeaker> dialogueSpeakers;

        [Button]
        private void CatchAllAudioReferences()
        {
            soundPlayers = FindObjectsByType<SoundPlayer>(
                    FindObjectsInactive.Include,
                    FindObjectsSortMode.InstanceID
                )
                .ToList();

            chainSpeakers = FindObjectsByType<ChainSpeaker>(
                    FindObjectsInactive.Include,
                    FindObjectsSortMode.InstanceID
                )
                .ToList();

            dialogueSpeakers = FindObjectsByType<DialogueSpeaker>(
                    FindObjectsInactive.Include,
                    FindObjectsSortMode.InstanceID
                )
                .ToList();
        }
    }
}
