using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    [AddComponentMenu("Scripts/Scripts/Global/Global.InitSceneSwitcher")]
    internal class InitSceneSwitcher : MonoBehaviour
    {
        [Scene]
        [SerializeField]
        private string nextScene;

        private void Start()
        {
            Configs.AudioSettings.Instance.RefreshAudio();
            ISingleton<MusicManager>.Instance.PlayMusic();
            SceneManager.LoadScene(nextScene);
        }
    }
}
