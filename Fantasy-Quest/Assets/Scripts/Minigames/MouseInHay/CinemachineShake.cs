using System.Collections;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Minigames.MouseInHay
{
    [AddComponentMenu("Scripts/Minigames/MouseInHay/Minigames.MouseInHay.CinemachineShake")]
    internal class CinemachineShake : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private new CinemachineVirtualCamera camera;

        [SerializeField]
        private bool slowDrop = false;

        [SerializeField]
        private float amplitude = 1.0f;

        [SerializeField]
        private float frequency = 1.0f;

        [SerializeField]
        private float shakeDuration = 1.0f;

        private CinemachineBasicMultiChannelPerlin shakeComponent;

        private void Awake()
        {
            shakeComponent = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        [Button]
        public void ShakeCamera()
        {
            shakeComponent.m_FrequencyGain = frequency;
            if (slowDrop)
            {
                _ = StartCoroutine(SlowDropShake(shakeDuration));
            }
            else
            {
                _ = StartCoroutine(TimerShake(shakeDuration));
            }
        }

        private IEnumerator TimerShake(float time)
        {
            shakeComponent.m_AmplitudeGain = amplitude;
            yield return new WaitForSeconds(time);
            shakeComponent.m_AmplitudeGain = 0;
        }

        private IEnumerator SlowDropShake(float time)
        {
            float timer = 0;
            while (timer < time)
            {
                timer += Time.deltaTime;
                shakeComponent.m_AmplitudeGain = Mathf.Lerp(amplitude, 0f, timer / time);
                yield return null;
            }
        }
    }
}
