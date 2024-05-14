using System.Collections;
using Configs;
using Configs.Progression;
using Cutscene;
using Dialogue;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace LevelSpecific.ForestEdge
{
    [AddComponentMenu("Scripts/LevelSpecific/ForestEdge/LevelSpecific.ForestEdge.SymonMoveCaller")]
    internal class SymonMoveCaller : MonoBehaviour
    {
        [SerializeField]
        private Collider2D correctZone;

        [SerializeField]
        private Transform callPoint;

        [ReadOnly]
        [SerializeField]
        private bool isMoving = false;

        [SerializeField]
        private Symon.Movement symonMovement;

        [SerializeField]
        private float waitTime = 2.0f;

        [Required]
        [SerializeField]
        private ChainSpeaker explanationSpeak;

        [Required]
        [SerializeField]
        private Start cutsceneStarter;

        public UnityEvent ComingToBack;

        [Button]
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

            if (correctZone.OverlapPoint(callPoint.position))
            {
                // Start cutscene
                _ = StartCoroutine(GoToCutscene());
            }
            else
            {
                // Just walk
                _ = StartCoroutine(TravelToPoint());
            }
        }

        public void SendSymonToStartAfterCutscene()
        {
            _ = StartCoroutine(GoToStartPointAfterCutscene());
        }

        private IEnumerator GoToCutscene()
        {
            LockerSettings.Instance.LockAll();
            ComingToBack?.Invoke();
            yield return explanationSpeak.Tell();
            yield return symonMovement.MoveToPoint(callPoint.position);
            cutsceneStarter.StartCutscene();
            ProgressionConfig.Instance.ForestEdgeLevel.BagTaken = true;
        }

        private IEnumerator GoToStartPointAfterCutscene()
        {
            yield return symonMovement.MoveToStartPoint();
            LockerSettings.Instance.UnlockAll();
        }

        public UnityEvent TravelledToPoint;

        private IEnumerator TravelToPoint()
        {
            isMoving = true;
            yield return symonMovement.MoveToPoint(callPoint.position);
            TravelledToPoint?.Invoke();
            yield return new WaitForSeconds(waitTime);
            yield return symonMovement.MoveToStartPoint();
            isMoving = false;
        }
    }
}
