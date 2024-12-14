using System;
using BackpackUnit.Core;
using UnityEngine;

namespace BackpackUnit.Items
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "SO/ItemData", order = 0)]
    public class ItemData: ScriptableObject, IItemInfo
    {
        [SerializeField] float weight;
        [SerializeField] ItemType type;
        [SerializeField] string localName;
        [SerializeField] Sprite image;

        public ItemType GetItemType()
        {
            return type;
        }

        public Sprite GetImage()
        {
            return image;
        }
    }

}

