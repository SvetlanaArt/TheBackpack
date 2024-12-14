using System;
using BackpackUnit.Core;
using UnityEngine;

namespace BackpackUnit.Items
{
    [CreateAssetMenu(fileName = "NewItemConfig", menuName = "SO/ItemConfig", order = 0)]
    public class ItemConfig : ScriptableObject, IItemInfo
    {
        [SerializeField] string id;
        [SerializeField] float weight;
        [SerializeField] ItemType type;
        [SerializeField] string localName;
        [SerializeField] Sprite image;

        public ItemConfig()
        {
            id = Guid.NewGuid().ToString();
        }

        public ItemType GetItemType()
        {
            return type;
        }

        public Sprite GetImage()
        {
            return image;
        }

        public string GetId()
        {
            return id;
        }
    }

}

