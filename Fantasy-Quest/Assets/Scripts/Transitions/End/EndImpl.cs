using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Transitions.End
{
    [AddComponentMenu("Scripts/Transitions/End/Transitions.End")]
    internal class EndImpl : MonoBehaviour
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

        [ReadOnly]
        [SerializeField]
        private float minDuration = 0f;

        public void LoadScene(string nextScene, float minDuration)
        {
            view.SetActive(true);
            animator.enabled = true;
            this.nextScene = nextScene;
            this.minDuration = minDuration;
        }

        // Must me called by view callback
        public void StartLoading()
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
            loading.allowSceneActivation = false;
            _ = StartCoroutine(LoadSceneRoutine(minDuration, loading));
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
