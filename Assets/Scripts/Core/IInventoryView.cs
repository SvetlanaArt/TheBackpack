using System;
using UnityEngine.EventSystems;

namespace BackpackUnit.Core
{
    public interface IInventoryView
    {
        public event Action<ItemType> OnRemoveItem;

        public bool AddItem(IItemInfo itemInfo);

        public void Open();

        public void TryRemoveItemAndClose(PointerEventData eventData);
    }

}
