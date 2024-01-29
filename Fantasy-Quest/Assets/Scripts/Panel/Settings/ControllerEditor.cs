#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

namespace Panel.Settings
{
    internal partial class Controller
    {
        [SerializeField]
        private AnimatorController controller;

        private IEnumerable<string> AnimatorBools =>
            animator == null ? null : controller.parameters.Select(x => x.name);
    }
}
#endif
