using Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Transition.End
{
    [AddComponentMenu("Scripts/Transition/End/Transition.End.Invoker")]
    internal class Invoker : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private Controller controller;

        [Scene]
        [SerializeField]
        private string nextScene;

        [Button]
        public void Invoke()
        {
            controller.LoadScene(nextScene);
        }
    }
}
