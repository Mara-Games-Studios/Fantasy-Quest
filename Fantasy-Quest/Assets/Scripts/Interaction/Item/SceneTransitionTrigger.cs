﻿using UnityEngine;
using UnityEngine.Events;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.SceneTransitionTrigger")]
    internal class SceneTransitionTrigger : MonoBehaviour, ISceneTransition
    {
        public UnityEvent Triggered;

        public void ToNewScene()
        {
            Triggered?.Invoke();
        }
    }
}
