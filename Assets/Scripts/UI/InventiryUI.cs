using System;
using System.Collections.Generic;
using BackpackUnit.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BackpackUnit.UI
{
    public class InventiryUI : MonoBehaviour, IInventoryView, IBackpackView
    {
        [SerializeField] GraphicRaycaster graphicRaycaster;
        
        public event Action<ItemType> OnRemoveItem;

        private Dictionary<ItemType, PocketUI> pockets;

        public void Init()
        {
            pockets = new Dictionary<ItemType, PocketUI>();
            PocketUI[] pocketsArr = GetComponentsInChildren<PocketUI>();
            foreach (var pocket in pocketsArr)
            {
                ItemType type = pocket.GetItemType();
                pockets.TryAdd(type, pocket);
            }
        }

        public bool AddItem(IItemInfo itemInfo)
        {
            ItemType type = itemInfo.GetItemType();
            if (pockets.TryGetValue(type, out PocketUI pocket))
            {
                pocket.SetImage(itemInfo.GetImage());
                return true;
            }
            return false;
        }

        public void TryRemoveItemAndClose(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, results);
            foreach (var result in results)
            {
                if (result.gameObject.TryGetComponent(out PocketUI pocket))
                {
                    RemoveItemFrom(pocket);
                    break;
                }
            }
            Close();
        }

        private void RemoveItemFrom(PocketUI pocket)
        {
            pocket.RemoveImage();
            ItemType type = pocket.GetItemType();
            Debug.Log("Item removed " + type);
            OnRemoveItem?.Invoke(type);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

