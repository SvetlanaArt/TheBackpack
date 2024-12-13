using BackpackUnit.Core;
using UnityEngine;

namespace BackpackUnit.Core
{
    public interface ICollectItems
    {
        public bool AddItem(Transform item, IItemInfo itemInfo);
    }
}