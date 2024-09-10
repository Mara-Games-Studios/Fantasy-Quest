using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace Common.DI
{
    [AddComponentMenu("Common/DI/Common.DI.TooledLifetimeScope")]
    public class TooledLifetimeScope : LifetimeScope
    {
        [Button]
        private void AddAllMarkedAutoInjectingGameObjects()
        {
            InjectingMonoBehaviour[] founded = FindObjectsOfType<InjectingMonoBehaviour>(true);
            IEnumerable<GameObject> gameObjects = founded.Select(x => x.gameObject).Distinct();
            gameObjects = gameObjects.Where(x => x.scene == gameObject.scene);
            autoInjectGameObjects = autoInjectGameObjects
                .Concat(gameObjects)
                .Where(x => x != null)
                .Distinct()
                .ToList();
        }
    }
}
