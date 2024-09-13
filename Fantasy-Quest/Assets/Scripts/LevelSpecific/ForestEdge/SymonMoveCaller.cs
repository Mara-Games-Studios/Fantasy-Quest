using Cat;
using Common.DI;
using Configs;
using Configs.Progression;
using Cutscene;
using Cysharp.Threading.Tasks;
using Dialogue;
using Hints;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SymonMoveCaller")]
    internal class SymonMoveCaller : InjectingMonoBehaviour
    {
        [Inject]
        private LockerApi lockerSettings;

        [SerializeField]
        private Collider2D zoneToCheck;

        [SerializeField]
        private Transform callPoint;

        [SerializeField]
        private Symon.Movement symonMovement;

        [Required]
        [SerializeField]
        private ChainSpeaker explanationSpeak;

        [Required]
        [SerializeField]
        private Start cutsceneStarter;

        [Required]
        [SerializeField]
        private Meowing meowing;

        [Required]
        [SerializeField]
        private ShowHints hintsShower;

        private bool isMoving = false;
        public UnityEvent ComingToBack;

        // Called by interaction
        public void CallSymon()
        {
            if (isMoving)
            {
                return;
            }

            if (!ProgressionConfig.Instance.ForestEdgeLevel.FirstDialoguePassed)
            {
                return;
            }

            if (ProgressionConfig.Instance.ForestEdgeLevel.BagTaken)
            {
                return;
            }

            if (zoneToCheck.OverlapPoint(callPoint.position))
            {
                _ = GoToCutscene();
            }
        }

        private async UniTaskVoid GoToCutscene()
        {
            hintsShower.TurnOffHints();
            await meowing.CatMeowingTask();
            ComingToBack?.Invoke();
            lockerSettings.Api.LockAll(this);
            await UniTask.WhenAll(
                explanationSpeak.Tell().ToUniTask(this),
                symonMovement.MoveToPoint(callPoint.position)
            );
            lockerSettings.Api.UnlockAll(this);
            cutsceneStarter.StartCutscene();
            ProgressionConfig.Instance.ForestEdgeLevel.BagTaken = true;
        }
    }
}
