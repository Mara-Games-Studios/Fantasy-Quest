using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
using Cysharp.Threading.Tasks;
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

        private CancellationTokenSource tokenSource = new();

        public UnityEvent OnFadeCompleted;

        public void SetFadeDuration(float fadeDuration)
        {
            this.fadeDuration = fadeDuration;
        }

        [Button]
        public void Appear()
        {
            _ = DoFadeForAll(1);
        }

        [Button]
        public void Disappear()
        {
            _ = DoFadeForAll(0);
        }

        public async UniTask DoFadeForAll(float endAlpha)
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            tokenSource = new();

            List<UniTask> tasks = new();
            foreach (Renderer renderer in renderers)
            {
                tasks.Add(ForceSetAlfa(renderer, endAlpha));
            }
            await UniTask.WhenAll(tasks);
        }

        private async UniTask ForceSetAlfa(Renderer renderer, float andAlfa)
        {
            Dictionary<Material, float> materials = new();

            if (renderer.TryGetComponent(out SpineSkeletonMaterialLinker linker))
            {
                materials.Add(linker.Material, linker.Material.color.a);
            }
            else
            {
                materials = renderer.materials.ToDictionary(x => x, x => x.color.a);
            }

            float timer = 0f;
            while (timer <= fadeDuration)
            {
                foreach (KeyValuePair<Material, float> pair in materials)
                {
                    float alfa = Mathf.Lerp(pair.Value, andAlfa, timer / fadeDuration);
                    SetAlfa(pair.Key, alfa);
                }
                await UniTask.Yield(PlayerLoopTiming.Update);
                timer += Time.deltaTime;
            }
        }

        private void SetAlfa(Material material, float alfa)
        {
            Color mc = material.color;
            material.color = new Color(mc.r, mc.g, mc.b, alfa);
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

        private void OnDestroy()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
        }
    }
}
