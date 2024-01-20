using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Transitions.End
{
    [AddComponentMenu("Scripts/Transitions/End/Transitions.End.EndImpl")]
    internal class EndImpl : MonoBehaviour
    {
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
            gameObject.SetActive(true);
            animator.enabled = true;
            this.nextScene = nextScene;
            this.minDuration = minDuration;
        }

        // Called by animation clip event
        public void AppearanceEnd()
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
            _ = StartCoroutine(LoadSceneRoutine(minDuration, loading));
        }

        private IEnumerator LoadSceneRoutine(float minDuration, AsyncOperation loading)
        {
            float timer = 0f;
            while (timer < minDuration || !loading.isDone)
            {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
                float percentage = timer / minDuration;
                progressBar.SetProgress(
                    percentage > loading.progress ? loading.progress : percentage
                );
            }
            loading.allowSceneActivation = true;
        }
    }
}
