using Common;
using Item;

namespace Inventory
{
    internal interface IInventory
    {
        public bool IsHasItem(string uid);
        public Result AddItem(ItemImpl newItem);
        public ValueResult<ItemImpl> TakeItem(ItemImpl item);
    }
}
