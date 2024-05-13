﻿using System;
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
            [SerializeReference]
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

        [SerializeField]
        private List<SoundPlayerPreview> musicPlayersPreviews;

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

            musicPlayersPreviews.Clear();
            foreach (SoundPlayer soundPlayer in soundPlayers)
            {
                musicPlayersPreviews.Add(
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
            foreach (SoundPlayerPreview soundPlayerPreview in musicPlayersPreviews)
            {
                soundPlayerPreview.Assign();
            }

            foreach (ChainSpeakerPreview chainSpeakerPreview in chainSpeakerPreviews)
            {
                chainSpeakerPreview.Assign();
            }
        }
    }
}
