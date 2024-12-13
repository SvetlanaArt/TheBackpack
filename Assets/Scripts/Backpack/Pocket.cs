using BackpackUnit.Core;
using UnityEngine;

namespace BackpackUnit.Backpack
{
    public class Pocket: MonoBehaviour
    {
        [SerializeField] ItemType type;

        IItemInfo itemInfo = null;

        public ItemType GetItemType()
        {
            return type;
        }

        public void PutItem(IItemInfo itemInfo)
        {
            this.itemInfo = itemInfo;
        }

        public void RemoveItem()
        {
            itemInfo = null;
        }

        public bool IsEmpty()
        {
            return itemInfo == null;
        }
    }
}