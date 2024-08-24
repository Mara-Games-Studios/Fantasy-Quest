using System.Collections;
using Cat;
using Cinemachine;
using Common.DI;
using Configs;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using VContainer;

namespace Dialogue
{
    [AddComponentMenu("Scripts/Dialogue/Dialogue.CatEnterDialogue")]
    internal class CatEnterDialogue : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [Header("Controllers")]
        private CinemachineVirtualCamera dialogueCamera;

        [Header("Transforms")]
        [SerializeField]
        private Transform cat;

        [SerializeField]
        private Transform companion;

        [Header("Speaker")]
        [SerializeField]
        private ChainSpeaker companionSpeaker;

        [Header("Controllers")]
        [SerializeField]
        private DialogueAnimationSwitch catSwitch;

        [SerializeField]
        private SpineTalkAnimationSwitch companionSwitch;

        [SerializeField]
        private ChangeWatchDir watchDirChanger;

        public UnityEvent DialogueEnded;

        public void StartDialogue()
        {
            _ = StartCoroutine(ListenToCompanion());
        }

        public IEnumerator ListenToCompanion()
        {
            lockerSettings.Api.LockAll(this);

            float direction = companion.position.x - cat.position.x;
            if (direction < 0)
            {
                watchDirChanger.ChangeWatchDirection(Cat.Vector.Left);
            }
            else
            {
                watchDirChanger.ChangeWatchDirection(Cat.Vector.Left);
            }
            dialogueCamera.Priority = 100;

            catSwitch.SetSeatAnimation();
            companionSwitch.SetTalkAnimation();

            yield return companionSpeaker.JustTellRoutine();
            DialogueEnded?.Invoke();

            dialogueCamera.Priority = 0;

            catSwitch.SetIdleAnimation();
            companionSwitch.SetIdleAnimation();

            lockerSettings.Api.UnlockAll(this);
        }
    }
}
