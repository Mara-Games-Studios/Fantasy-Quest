using Common.DI;
using Configs;
using Configs.Progression;
using Cutscene;
using Cysharp.Threading.Tasks;
using Dialogue;
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

        [ReadOnly]
        [SerializeField]
        private bool isMoving = false;

        [SerializeField]
        private Symon.Movement symonMovement;

        [Required]
        [SerializeField]
        private ChainSpeaker explanationSpeak;

        [Required]
        [SerializeField]
        private Start cutsceneStarter;

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
            ComingToBack?.Invoke();
            lockerSettings.Api.LockAll(this);
            await UniTask.WhenAll(
                explanationSpeak.Tell().ToUniTask(this),
                symonMovement.MoveToPoint(callPoint.position).ToUniTask(this)
            );
            lockerSettings.Api.UnlockAll(this);
            cutsceneStarter.StartCutscene();
            ProgressionConfig.Instance.ForestEdgeLevel.BagTaken = true;
        }
    }
}
