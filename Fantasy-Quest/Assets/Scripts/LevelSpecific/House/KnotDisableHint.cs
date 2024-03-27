using Interaction.Item;
using UnityEngine;

namespace LevelSpecific.House
{
    [AddComponentMenu("Scripts/LevelSpecific/House/LevelSpecific.House.KnotDisableHint")]
    internal class KnotDisableHint : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GameObject hintToDisable;

        public void InteractByCat()
        {
            hintToDisable.SetActive(false);
            Destroy(this);
        }

        public void InteractByHuman()
        {
            return;
        }
    }
}
