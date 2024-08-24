using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public static class CoroutineTools
    {
        public static Coroutine CreateGate(
            this MonoBehaviour initiator,
            List<Func<bool>> conditions,
            List<Action> actions
        )
        {
            return initiator.StartCoroutine(CheckConditions(conditions, actions));
        }

        private static IEnumerator CheckConditions(
            List<Func<bool>> conditions,
            List<Action> actions
        )
        {
            while (!conditions.All(x => x.Invoke()))
            {
                yield return new WaitForEndOfFrame();
            }
            foreach (Action action in actions)
            {
                action.Invoke();
            }
        }

        public static bool KillCoroutine(this MonoBehaviour handler, Coroutine coroutine)
        {
            if (coroutine != null)
            {
                handler.StopCoroutine(coroutine);
                return true;
            }
            return false;
        }
    }
}
