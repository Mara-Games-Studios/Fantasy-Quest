using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Common;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    [AddComponentMenu("Scripts/Inventory/Inventory")]
    internal class InventoryImpl : MonoBehaviour, IInventory
    {
        [SerializeField]
        private uint capacity;

        private ObservableCollection<ItemImpl> items = new();
        public UnityEvent<object, NotifyCollectionChangedEventArgs> CollectionChanged = new();

        private void Awake()
        {
            items.CollectionChanged += CollectionChanged.Invoke;
        }

        public bool IsHasItem(string uid)
        {
            return items.Any(x => x.Uid == uid);
        }

        public Result AddItem(ItemImpl newItem)
        {
            if (items.Count() >= capacity)
            {
                return new FailResult("Reached maximum capacity");
            }
            else
            {
                items.Add(newItem);
                Debug.Log("Added item '" + newItem.ItemName + "' to inventory");
                return new SuccessResult();
            }
        }

        public ValueResult<ItemImpl> TakeItem(ItemImpl item)
        {
            if (items.Remove(item))
            {
                return new SuccessValueResult<ItemImpl>(item);
            }
            else
            {
                return new FailValueResult<ItemImpl>("No such item in collection");
            }
        }
    }
}
