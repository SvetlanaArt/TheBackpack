using System;
using UnityEngine.EventSystems;

namespace BackpackUnit.Core
{
    public interface IInventoryView
    {
        public void Open();

        public void TryRemoveItemAndClose(PointerEventData eventData);
    }

    public interface IBackpackView
    {
        public event Action<ItemType> OnRemoveItem;

        public bool AddItem(IItemInfo itemInfo);

    }

}
