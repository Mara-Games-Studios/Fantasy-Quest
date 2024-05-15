using System;
using System.Collections;
using Sirenix.OdinInspector;
using UI.Pause;
using UnityEngine;

namespace Minigames.SquirrelGame.Screamer
{
    [AddComponentMenu("Scripts/Minigames/Squirrel.ScreamerActivator")]
    public class ScreamerActivator : MonoBehaviour
    {
        [SerializeField]
        private DarkBackground background;

        [SerializeField]
        private SquirrelShower squirrelShower;

        [SerializeField]
        private Audio.SoundPlayer soundPlayer;

        [SerializeField]
        private float duration = 0.3f;

        private Coroutine waitCoroutine;
        public event Action OnScreamerShowed;

        [Button]
        public void Show()
        {
            StopTheCoroutine(waitCoroutine);

            background.Show();
            squirrelShower.Show();
            soundPlayer.PlayClip();

            waitCoroutine = StartCoroutine(WaitInvokeOnScreamerShowed(duration));
        }

        [Button]
        public void Hide()
        {
            background.Hide(() => { });
            squirrelShower.Hide();
        }

        private void StopTheCoroutine(Coroutine coroutine)
        {
            if (coroutine == null)
            {
                return;
            }

            StopCoroutine(coroutine);
        }

        private IEnumerator WaitInvokeOnScreamerShowed(float timeBeforeInvoke)
        {
            yield return new WaitForSeconds(duration);
            InvokeOnScreamerShowed();
            Hide();
        }

        private void InvokeOnScreamerShowed()
        {
            OnScreamerShowed?.Invoke();
        }
    }
}
