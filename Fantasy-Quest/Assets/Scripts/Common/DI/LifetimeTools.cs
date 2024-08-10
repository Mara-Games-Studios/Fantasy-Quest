using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace Common.DI
{
    public class TooledLifetimeScope : LifetimeScope
    {
        [Button]
        private void AddAllMarkedAutoInjectingGameObjects()
        {
            InjectingMonoBehaviour[] founded = FindObjectsOfType<InjectingMonoBehaviour>();
            IEnumerable<GameObject> gameObjects = founded.Select(x => x.gameObject).Distinct();
            gameObjects = gameObjects.Where(x => x.scene == gameObject.scene);
            autoInjectGameObjects = autoInjectGameObjects.Concat(gameObjects).Distinct().ToList();
        }
    }
}
