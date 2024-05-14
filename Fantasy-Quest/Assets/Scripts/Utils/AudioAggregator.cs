using System;
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
        [Serializable]
        private struct DialogueSpeakerCover
        {
            [InfoBox("@" + nameof(DialogueSpeakerInfo))]
            public DialogueSpeaker DialogueSpeaker;
            private string DialogueSpeakerInfo =>
                DialogueSpeaker.FFirstTrySpeech.Select(x => x.Audio.name).Any()
                    ? DialogueSpeaker
                        .FFirstTrySpeech.Select(x => x.Audio.name)
                        .Aggregate((x, y) => x + "\n" + y)
                    : "";
        }

        [Serializable]
        private struct ChainSpeakerCover
        {
            [InfoBox("@" + nameof(DialogueSpeakerInfo))]
            public ChainSpeaker ChainSpeaker;
            private string DialogueSpeakerInfo =>
                ChainSpeaker.Replicas.Select(x => x.Audio.name).Aggregate((x, y) => x + "\n" + y);
        }

        [ReadOnly]
        [SerializeField]
        private List<SoundPlayer> soundPlayers;

        [ReadOnly]
        [SerializeField]
        private List<ChainSpeakerCover> chainSpeakers;

        [ReadOnly]
        [SerializeField]
        private List<DialogueSpeakerCover> dialogueSpeakers;

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
                .Select(x => new ChainSpeakerCover() { ChainSpeaker = x })
                .ToList();

            dialogueSpeakers = FindObjectsByType<DialogueSpeaker>(
                    FindObjectsInactive.Include,
                    FindObjectsSortMode.InstanceID
                )
                .Select(x => new DialogueSpeakerCover() { DialogueSpeaker = x })
                .ToList();
        }
    }
}
