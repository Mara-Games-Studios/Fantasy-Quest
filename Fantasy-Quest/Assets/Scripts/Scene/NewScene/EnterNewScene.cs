using System.Collections;
using System.Collections.Generic;
using Interaction.Item;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene.NewScene
{
    [AddComponentMenu("Scripts/Scene/NewScene/Scene.NewScene.EnterNewScene")]
    internal class EnterNewScene : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private bool isJumpUp = true;

        [SerializeField]
        private List<ParticleSystem> effects = new();

        [SerializeField]
        private string newScene;

        [SerializeField]
        private bool isFiringInParallel = false;

        //[SerializeField]
        //private bool afterAllEffects = false;

        [ShowIf("isFiringInParallel")]
        [SerializeField]
        private bool afterFirstEffect = false;

        private float duration = 0f;

        public void InteractByCat()
        {
            if (isFiringInParallel)
            {
                _ = StartCoroutine("GoInParallel");
            }
            else
            {
                _ = StartCoroutine("GoOneByOne");
            }
        }

        //If effects must activate in parallel, then it all will be played together and load will start after the longest
        private IEnumerator GoInParallel()
        {
            if (afterFirstEffect)
            {
                duration = effects[0].main.duration;
                effects.ForEach(effect => effect.Play());
            }
            else
            {
                effects.ForEach(effect =>
                {
                    if (effect.main.duration > duration)
                    {
                        effect.Play();
                        duration = effect.main.duration;
                    }
                });
            }

            yield return new WaitForSeconds(duration);
            LoadAnotherScene();
        }

        private IEnumerator GoOneByOne()
        {
            foreach (ParticleSystem effect in effects)
            {
                effect.Play();
                yield return new WaitForSeconds(effect.main.duration);
            }
            LoadAnotherScene();
        }

        private void LoadAnotherScene()
        {
            UnityEngine.SceneManagement.Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(newScene, LoadSceneMode.Additive);
            _ = SceneManager.UnloadSceneAsync(activeScene);
            _ = SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));
        }

        public void InteractByHuman()
        {
            return;
        }
    }
}
