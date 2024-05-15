    using System.Collections;
using System.Collections.Generic;
using Configs;
using Sirenix.OdinInspector;
using TNRD;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Transition.End
{
    [AddComponentMenu("Scripts/Transition/End/Transition.End.Controller")]
    internal class Controller : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private GameObject view;

        [Required]
        [SerializeField]
        private Animator animator;

        [Required]
        [SerializeField]
        private RectTransform uiParent;

        [Required]
        [SerializeField]
        private SerializableInterface<IFadingUI> loadingUI;

        [ReadOnly]
        [SerializeField]
        private string nextScene = "NULL";
        private bool exitingGame = false;

        public UnityEvent OnQuitGame;

        [Button]
        public void LoadScene(string nextScene)
        {
            view.SetActive(true);
            animator.enabled = true;
            this.nextScene = nextScene;
        }

        [Button]
        public void QuitGame()
        {
            view.SetActive(true);
            animator.enabled = true;
            exitingGame = true;
            OnQuitGame?.Invoke();
        }

        // Must me called by view callback
        public void StartLoading()
        {
            if (exitingGame)
            {
                Application.Quit();
            }
            AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
            loading.allowSceneActivation = false;
            _ = StartCoroutine(
                LoadSceneRoutine(TransitionSettings.Instance.LoadingDuration, loading)
            );
        }

        private IEnumerator LoadSceneRoutine(float duration, AsyncOperation loading)
        {
            int currentFilling = TransitionSettings.Instance.CurrentFilling;
            float fadeDuration = TransitionSettings.Instance.FadingDuration;
            List<SerializableInterface<IFadingUI>> uisToShow = TransitionSettings.Instance.UiToShow;

            SerializableInterface<IFadingUI> uiToShow = uisToShow[currentFilling];
            TransitionSettings.Instance.CurrentFilling = (currentFilling + 1) % uisToShow.Count;
            IFadingUI ui = uiToShow.Instantiate(uiParent);
            ui.FadeIn(fadeDuration);
            loadingUI.Value.FadeIn(fadeDuration);
            yield return new WaitForSecondsRealtime(duration - fadeDuration);

            ui.FadeOut(fadeDuration);
            loadingUI.Value.FadeOut(fadeDuration);
            yield return new WaitForSecondsRealtime(fadeDuration);

            Time.timeScale = 1;
            loading.allowSceneActivation = true;
        }
    }
}
