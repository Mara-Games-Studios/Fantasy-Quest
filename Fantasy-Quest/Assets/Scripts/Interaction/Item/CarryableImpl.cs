using Inventory;
using UnityEngine;

namespace Interaction.Item
{
    [RequireComponent(typeof(ItemImpl))]
    [AddComponentMenu("Scripts/Interaction/Item/Interaction.Item.Carryable")]
    internal class CarryableImpl : MonoBehaviour, ICarryable
    {
        //WORK IN PROGRESS

        //[Header("Cat and Boy objects")]
        //[Required]
        //[SerializeField]
        //private GameObject cat;

        //[Required]
        //[SerializeField]
        //private GameObject symon;

        //[Header("Settings")]
        //[ReadOnly]
        //[SerializeField]
        //private bool isCarryed = false;

        //[SerializeField]
        //private float lerpDuration = 1.0f;

        //private ItemImpl thisItem;

        //private void Awake()
        //{
        //    thisItem = GetComponent<ItemImpl>();
        //}

        //[Button]
        //public void FindAll()
        //{
        //    cat = GameObject.Find("Cat");
        //    symon = GameObject.Find("Symon");
        //}

        //public void CarryByCat()
        //{
        //    if (!isCarryed)
        //    {
        //        _ = cat.GetComponentInChildren<InventoryImpl>().AddItem(thisItem);
        //        transform.parent = cat.transform.Find("CatSkeleton").transform.Find("CatMouth");
        //        _ = StartCoroutine("GetItemToHead");
        //        //transform.localPosition = Vector3.zero;
        //    }
        //    else
        //    {
        //        _ = cat.GetComponentInChildren<InventoryImpl>().TakeItem(thisItem);
        //        transform.parent = null;
        //    }
        //}

        //private IEnumerator GetItemToHead()
        //{
        //    float timeElapsed = 0.0f;
        //    while (timeElapsed < lerpDuration)
        //    {
        //        transform.position = Vector3.Lerp(
        //            transform.position,
        //            transform.parent.position,
        //            timeElapsed / lerpDuration
        //        );
        //        yield return null;
        //        timeElapsed += Time.deltaTime;
        //    }
        //}

        //public void CarryByHuman()
        //{
        //    return;
        //}

        //public bool CanCatCarry()
        //{
        //    throw new System.NotImplementedException();
        //}
        public void CarryByCat()
        {
            throw new System.NotImplementedException();
        }

        public void CarryByHuman()
        {
            throw new System.NotImplementedException();
        }
    }
}
