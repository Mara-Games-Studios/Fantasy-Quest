using System.Collections;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Transition.End
{
    [AddComponentMenu("Scripts/Transition/End/Transition.End.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private GameObject view;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private ProgressBar progressBar;

        [ReadOnly]
        [SerializeField]
        private string nextScene = "NULL";

        public void LoadScene(string nextScene)
        {
            view.SetActive(true);
            animator.enabled = true;
            this.nextScene = nextScene;
        }

        // Must me called by view callback
        public void StartLoading()
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
            loading.allowSceneActivation = false;
            _ = StartCoroutine(
                LoadSceneRoutine(TransitionSettings.Instance.MinLoadingDuration, loading)
            );
        }

        private const float max_loading_progress = 0.9f;

        private IEnumerator LoadSceneRoutine(float minDuration, AsyncOperation loading)
        {
            float timer = 0f;
            while (timer <= minDuration || loading.progress != max_loading_progress)
            {
                yield return null;
                timer += Time.deltaTime;
                float percentage = timer / minDuration;
                float loadingPercentage = loading.progress / max_loading_progress;
                progressBar.SetProgress(
                    percentage > loadingPercentage ? loadingPercentage : percentage
                );
            }
            loading.allowSceneActivation = true;
        }
    }
}
