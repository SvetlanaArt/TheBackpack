using System;
using BackpackUnit.Core;
using UnityEngine;

namespace BackpackUnit.Items
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "SO/ItemData", order = 0)]
    public class ItemData: ScriptableObject
    {
        [SerializeField] float weight;
        [SerializeField] ItemType type;
        [SerializeField] string localName;

        private string id;

        public ItemData()
        {
            id = Guid.NewGuid().ToString();
        }

        public string GetId()
        {
            return id;
        }

    }

}

