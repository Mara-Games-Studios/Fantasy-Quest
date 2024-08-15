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

        private IFadingUI screenToShow;
        private GameObject ScreenGameObject => (screenToShow as MonoBehaviour).gameObject;
        private int currentFilling;

        public UnityEvent OnQuitGame;

        private void Awake()
        {
            currentFilling = TransitionSettings.Instance.CurrentFilling;
            List<SerializableInterface<IFadingUI>> uisToShow = TransitionSettings.Instance.UiToShow;
            SerializableInterface<IFadingUI> uiToShow = uisToShow[currentFilling];
            screenToShow = uiToShow.Instantiate(uiParent);
            ScreenGameObject.SetActive(false);
        }

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
                return;
            }
            AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
            loading.allowSceneActivation = false;
            _ = StartCoroutine(
                LoadSceneRoutine(TransitionSettings.Instance.LoadingDuration, loading)
            );
        }

        private IEnumerator LoadSceneRoutine(float duration, AsyncOperation loading)
        {
            ScreenGameObject.SetActive(true);
            float fadeDuration = TransitionSettings.Instance.FadingDuration;
            TransitionSettings.Instance.CurrentFilling =
                (currentFilling + 1) % TransitionSettings.Instance.UiToShow.Count;

            screenToShow.FadeIn(fadeDuration);
            loadingUI.Value.FadeIn(fadeDuration);
            yield return new WaitForSecondsRealtime(duration - fadeDuration);

            screenToShow.FadeOut(fadeDuration);
            loadingUI.Value.FadeOut(fadeDuration);
            yield return new WaitForSecondsRealtime(fadeDuration);

            Time.timeScale = 1;
            loading.allowSceneActivation = true;
        }
    }
}
