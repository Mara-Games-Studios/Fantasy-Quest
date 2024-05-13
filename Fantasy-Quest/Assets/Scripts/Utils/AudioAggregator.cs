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
        [ReadOnly]
        [SerializeField]
        private List<SoundPlayer> soundPlayers;

        [ReadOnly]
        [SerializeField]
        private List<ChainSpeaker> chainSpeakers;

        [ReadOnly]
        [SerializeField]
        private List<DialogueSpeaker> dialogueSpeakers;

        [Serializable]
        public class SoundPlayerPreview
        {
            private string Name => AudioClip.name;

            [FoldoutGroup("@" + nameof(Name))]
            public SoundPlayer SoundPlayer;

            [FoldoutGroup("@" + nameof(Name))]
            public AudioClip AudioClip;

            [FoldoutGroup("@" + nameof(Name))]
            public bool Loop = false;

            [FoldoutGroup("@" + nameof(Name))]
            public bool PlayOnStart = false;

            [FoldoutGroup("@" + nameof(Name))]
            public bool IgnorePause = false;

            [FoldoutGroup("@" + nameof(Name))]
            public bool CreateOnThisPosition = false;

            [FoldoutGroup("@" + nameof(Name))]
            public AudioSourceConfig AudioSourceConfig;

            public void Assign()
            {
                SoundPlayer.Loop = Loop;
                SoundPlayer.AudioClip = AudioClip;
                SoundPlayer.PlayOnStart = PlayOnStart;
                SoundPlayer.IgnorePause = IgnorePause;
                SoundPlayer.CreateOnThisPosition = CreateOnThisPosition;
                SoundPlayer.AudioSourceConfig = AudioSourceConfig;
            }
        }

        [Serializable]
        public class ReplicaPreview
        {
            public Replica Replica;
            public AudioClip AudioClip;
            public AudioSourceConfig AudioSourceConfig;
        }

        [Serializable]
        public class ChainSpeakerPreview
        {
            [FoldoutGroup("@" + nameof(Name))]
            public ChainSpeaker ChainSpeaker;

            [HideInInspector]
            public string Name;

            [FoldoutGroup("@" + nameof(Name))]
            public List<ReplicaPreview> ReplicaPreviews;

            public void Assign()
            {
                foreach (ReplicaPreview rep in ReplicaPreviews)
                {
                    rep.Replica.AudioSourceConfig = rep.AudioSourceConfig;
                }
            }
        }

        [HideInInspector]
        [SerializeField]
        private List<SoundPlayerPreview> soundPlayersPreviews;

        [HideInInspector]
        [SerializeField]
        private List<ChainSpeakerPreview> chainSpeakerPreviews;

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

            soundPlayersPreviews.Clear();
            foreach (SoundPlayer soundPlayer in soundPlayers)
            {
                soundPlayersPreviews.Add(
                    new()
                    {
                        SoundPlayer = soundPlayer,
                        AudioClip = soundPlayer.AudioClip,
                        Loop = soundPlayer.Loop,
                        PlayOnStart = soundPlayer.PlayOnStart,
                        IgnorePause = soundPlayer.IgnorePause,
                        CreateOnThisPosition = soundPlayer.CreateOnThisPosition,
                        AudioSourceConfig = soundPlayer.AudioSourceConfig
                    }
                );
            }

            chainSpeakerPreviews.Clear();
            foreach (ChainSpeaker chainSpeaker in chainSpeakers)
            {
                chainSpeakerPreviews.Add(
                    new()
                    {
                        ChainSpeaker = chainSpeaker,
                        Name =
                            chainSpeaker.gameObject.name
                            + " "
                            + chainSpeaker
                                .Replicas.Select(X => X.Audio.name)
                                .Aggregate((x, y) => x + " " + y),
                        ReplicaPreviews = new(
                            chainSpeaker
                                .Replicas.Select(x => new ReplicaPreview()
                                {
                                    Replica = x,
                                    AudioClip = x.Audio,
                                    AudioSourceConfig = x.AudioSourceConfig
                                })
                                .ToList()
                        )
                    }
                );
            }
        }

        [Button]
        private void UpdateAudioParameters()
        {
            foreach (SoundPlayerPreview soundPlayerPreview in soundPlayersPreviews)
            {
                soundPlayerPreview.Assign();
            }

            foreach (ChainSpeakerPreview chainSpeakerPreview in chainSpeakerPreviews)
            {
                chainSpeakerPreview.Assign();
            }
        }

        [Button]
        private void SetSpatialToZero()
        {
            dialogueSpeakers.ForEach(
                (x) =>
                {
                    x.FFirstTrySpeech.ForEach(x => x.AudioSourceConfig.SpatialBlend = 0);
                    x.AAlternativeSpeech.ForEach(x => x.AudioSourceConfig.SpatialBlend = 0);
                }
            );
        }
    }
}
