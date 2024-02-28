using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Effects
{
    [AddComponentMenu("Scripts/Effects/Effects.Fade")]
    internal class Fade : MonoBehaviour
    {
        [SerializeField]
        private List<Renderer> renderers = new();

        [SerializeField]
        private float fadeDuration = 1f;

        private Coroutine finishGates;
        private List<Tween> tweens = new();

        public UnityEvent OnFadeCompleted;

        [Button]
        public void Appear()
        {
            DoFadeForAll(1);
        }

        [Button]
        public void Disappear()
        {
            DoFadeForAll(0);
        }

        [Button]
        public void DoFadeForAll(float endAlpha)
        {
            KillAllActions();
            List<Func<bool>> conditions = new();
            foreach (Renderer renderer in renderers)
            {
                Func<bool> endAction = DoFadeForRenderer(renderer, endAlpha);
                conditions.Add(endAction);
            }
            finishGates = this.CreateGate(
                conditions,
                new() { () => KillAllActions(), () => OnFadeCompleted?.Invoke(), }
            );
        }

        [Button]
        private void KillAllActions()
        {
            tweens.ForEach(x => x?.Kill(true));
            tweens.Clear();
            if (this.KillCoroutine(finishGates))
            {
                finishGates = null;
            }
        }

        [Button]
        private Func<bool> DoFadeForRenderer(Renderer renderer, float endAlpha)
        {
            List<Func<bool>> conditions = new();
            foreach (Material material in renderer.materials)
            {
                Tweener tween = DOTween.ToAlpha(
                    () => material.color,
                    (x) => material.color = x,
                    endAlpha,
                    fadeDuration
                );
                tweens.Add(tween);
                conditions.Add(GetOnTweenComplete(tween));
            }

            bool coroutineCompleted = false;
            finishGates = this.CreateGate(conditions, new() { () => coroutineCompleted = true });
            return () => coroutineCompleted;
        }

        private Func<bool> GetOnTweenComplete(Tween tween)
        {
            bool completed = false;
            tween.onComplete += () => completed = true;
            return () => completed;
        }

        [Button]
        private void CatchRenderersOnThisObject()
        {
            renderers = GetComponents<Renderer>().ToList();
        }

        [Button]
        private void CatchRenderersOnThisObjectAndChilds()
        {
            renderers = GetComponentsInChildren<Renderer>(true).ToList();
        }
    }
}
