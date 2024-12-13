using System;
using System.Collections.Generic;
using BackpackUnit.Core;
using UnityEngine;

namespace BackpackUnit.Backpack
{

    public class BackpackInventory : MonoBehaviour, ICollectItems
    {
        Dictionary<ItemType, Pocket> pockets;

        IBackpackView backpackView;

        public void Init(IBackpackView backpackView)
        {
            this.backpackView = backpackView;

            backpackView.OnRemoveItem += RemoveItem;

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

        public bool AddItem(Transform item, IItemInfo itemInfo)
        {
            ItemType itemType = itemInfo.GetItemType();
            if (pockets.TryGetValue(itemType, out var pocket))
            {
                if (pocket.IsEmpty())
                {
                    item.parent = pocket.transform;
                    pocket.PutItem(itemInfo);
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
        }
    }
}