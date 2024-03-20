using UnityEngine;
using UnityEngine.Events;

namespace Interaction.Item
{
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.CarryTrigger")]
    internal class CarryTrigger : MonoBehaviour, ICarryable
    {
        public UnityEvent OnCarryByCat;
        public UnityEvent OnCarryByHuman;

        public void CarryByCat()
        {
            OnCarryByCat?.Invoke();
        }

        public void CarryByHuman()
        {
            OnCarryByHuman?.Invoke();
        }
    }
}
