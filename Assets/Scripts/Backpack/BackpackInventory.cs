using System;
using System.Collections.Generic;
using BackpackUnit.Core;
using UnityEngine;

namespace BackpackUnit.Backpack
{

    public class BackpackInventory : MonoBehaviour, ICollectItems
    {
        [SerializeField] Transform throwPoint;
        Dictionary<ItemType, Pocket> pockets;
        Dictionary<ItemType, IThrowable> itemsInBackpack;

        IBackpackView backpackView;

        public void Init(IBackpackView backpackView)
        {
            this.backpackView = backpackView;
            backpackView.OnRemoveItem += RemoveItem;

            itemsInBackpack = new Dictionary<ItemType, IThrowable>();

            pockets = new Dictionary<ItemType, Pocket>();
            FindPockets();
        }

        private void FindPockets()
        {
            Pocket[] pocketsArr = GetComponentsInChildren<Pocket>();
            foreach (var pocket in pocketsArr)
            {
                ItemType type = pocket.GetItemType();
                pockets.TryAdd(type, pocket);
            }
        }

        public bool AddItem(Transform itemTransform, IThrowable item, IItemInfo itemInfo)
        {
            ItemType itemType = itemInfo.GetItemType();
            if (pockets.TryGetValue(itemType, out var pocket))
            {
                if (pocket.IsEmpty())
                {
                    itemsInBackpack.TryAdd(itemType, item);
                    itemTransform.parent = pocket.transform;
                    pocket.PutItem(itemInfo);
                    backpackView.AddItem(itemInfo);
                    return true;
                }
            }

            return false;
        }

        public void RemoveItem(ItemType itemType)
        {
            if (pockets.TryGetValue(itemType, out var pocket))
            {
                pocket.RemoveItem();
            }
            if (itemsInBackpack.TryGetValue(itemType, out var item))
            {
                item.ThrowAway(throwPoint.transform.position);
                itemsInBackpack.Remove(itemType);
            }
        }
    }
}